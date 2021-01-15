using Automatonymous;
using MassTransit;
using System;
using System.Linq;
using System.Threading.Tasks;
using vtb.InvoicesService.BusinessLogic.Events;
using vtb.InvoicesService.Domain;

namespace vtb.InvoicesService.BusinessLogic.Sagas
{
    public class InvoiceStateMachine : MassTransitStateMachine<Invoice>
    {
        public State Draft { get; private set; }
        public State Issued { get; private set; }
        public State Printed { get; private set; }
        public State Paid { get; private set; }
        public State Overdue { get; private set; }

        public Event<ICreateInvoiceDraft> DraftCreated { get; private set; }
        public Event<IIssueInvoice> InvoiceIssued { get; private set; }
        public Event<IIssuePaidInvoice> PaidInvoiceIssued { get; private set; }
        public Event<IPrintInvoice> InvoicePrinted { get; private set; }
        public Event<IPayInvoice> InvoicePaid { get; private set; }
        public Event<ISetInvoicePositions> InvoicePositionsSet { get; private set; }
        public Event<ISetInvoiceTemplate> InvoiceTemplateSet { get; private set; }

        public Schedule<Invoice, IInvoicePaymentDeadlineExpired> InvoicePaymentDeadline { get; private set; }

        public InvoiceStateMachine()
        {
            InstanceState(x => x.State);
            Configure();

            Initially(When(DraftCreated).TransitionTo(Draft));

            During(Draft,
                When(InvoiceIssued).Then(IssueInvoice).Schedule(InvoicePaymentDeadline, InitExpireEvent, CalculateDelay).TransitionTo(Issued),
                When(PaidInvoiceIssued).Then(IssueInvoice).Then(PrintInvoice).Then(SetPaymentDate).TransitionTo(Paid),
                When(InvoicePositionsSet).Then(SetInvoicePositions),
                When(InvoiceTemplateSet).Then(SetInvoiceTemplate));

            During(Issued,
                When(InvoicePrinted).Then(PrintInvoice).TransitionTo(Printed),
                When(InvoicePaymentDeadline.Received).TransitionTo(Overdue));

            During(Printed, Overdue,
                When(InvoicePaid).Then(SetPaymentDate).TransitionTo(Paid));
        }

        private Task<IInvoicePaymentDeadlineExpired> InitExpireEvent(ConsumeEventContext<Invoice, IIssueInvoice> c)
            => c.Init<IInvoicePaymentDeadlineExpired>(new { c.Data.InvoiceId });

        private DateTime CalculateDelay(ConsumeEventContext<Invoice, IIssueInvoice> context)
            => context.Instance.PaymentDeadline.Value;

        private void SetInvoiceTemplate(BehaviorContext<Invoice, ISetInvoiceTemplate> context)
            => context.Instance.SetTemplateVersion(context.Data.TemplateVersionId);

        private void SetInvoicePositions(BehaviorContext<Invoice, ISetInvoicePositions> context)
            => context.Instance.SetPositions(context.Data.InvoicePositions.ToList());

        private void SetPaymentDate(BehaviorContext<Invoice, IPayInvoice> context)
            => context.Instance.SetPaymentDate(context.Data.PaymentDate);

        private void SetPaymentDate(BehaviorContext<Invoice, IIssuePaidInvoice> context)
            => context.Instance.SetPaymentDate(context.Data.IssueDate);

        private void PrintInvoice(BehaviorContext<Invoice, IPrintInvoice> context)
            => context.Instance.SetPrintoutDate(context.Data.PrintoutDate);

        private void PrintInvoice(BehaviorContext<Invoice, IIssuePaidInvoice> context)
            => context.Instance.SetPrintoutDate(context.Data.IssueDate);

        private void IssueInvoice(BehaviorContext<Invoice, IIssueInvoice> context)
            => context.Instance.Issue(
                context.Data.InvoiceNumber,
                context.Data.IssueDate,
                context.Data.IssuerId,
                context.Data.PaymentDeadline
            );

        private void IssueInvoice(BehaviorContext<Invoice, IIssuePaidInvoice> context)
            => context.Instance.Issue(
                    context.Data.InvoiceNumber,
                    context.Data.IssueDate,
                    context.Data.IssuerId,
                    context.Data.IssueDate
                );

        private void Configure()
        {
            Event(() => DraftCreated, e =>
            {
                e.CorrelateById(s => s.InvoiceId, c => c.Message.InvoiceId);
                e.InsertOnInitial = true;
                e.SetSagaFactory(c => new Invoice(
                    c.Message.DraftCreatedAtUtc,
                    c.Message.TemplateVersionId,
                    c.Message.BuyerId,
                    c.Message.SellerId,
                    c.Message.Currency,
                    c.Message.CalculationDirection
                ));
            });

            Event(() => InvoiceIssued, cc => cc.CorrelateById(s => s.InvoiceId, c => c.Message.InvoiceId));
            Event(() => PaidInvoiceIssued, cc => cc.CorrelateById(s => s.InvoiceId, c => c.Message.InvoiceId));
            Event(() => InvoicePrinted, cc => cc.CorrelateById(s => s.InvoiceId, c => c.Message.InvoiceId));
            Event(() => InvoicePaid, cc => cc.CorrelateById(s => s.InvoiceId, c => c.Message.InvoiceId));
            Event(() => InvoicePositionsSet, cc => cc.CorrelateById(s => s.InvoiceId, c => c.Message.InvoiceId));
            Event(() => InvoiceTemplateSet, cc => cc.CorrelateById(s => s.InvoiceId, c => c.Message.InvoiceId));

            Schedule(() => InvoicePaymentDeadline, i => i.InvoicePaymentDeadlineToken, s =>
            {
                s.Received = r => r.CorrelateById(i => i.Message.InvoiceId);
            });
        }
    }
}