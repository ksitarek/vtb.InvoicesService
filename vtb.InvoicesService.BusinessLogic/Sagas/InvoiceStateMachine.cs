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

        public Event<IInvoiceDraftCreated> DraftCreated { get; private set; }
        public Event<IInvoiceIssued> InvoiceIssued { get; private set; }
        public Event<IPaidInvoiceIssued> PaidInvoiceIssued { get; private set; }
        public Event<IInvoicePrinted> InvoicePrinted { get; private set; }
        public Event<IInvoicePaid> InvoicePaid { get; private set; }
        public Event<IInvoicePositionsSet> InvoicePositionsSet { get; private set; }
        public Event<IInvoiceTemplateSet> InvoiceTemplateSet { get; private set; }

        public Schedule<Invoice, IInvoicePaymentDeadlineExpired> InvoicePaymentDeadline { get; private set; }

        public InvoiceStateMachine()
        {
            InstanceState(x => x.State);
            Configure();

            Initially(When(DraftCreated).TransitionTo(Draft));

            During(Draft,
                When(InvoiceIssued).Then(IssueInvoice)/*.Schedule(InvoicePaymentDeadline, InitExpireEvent, CalculateDelay)*/.TransitionTo(Issued),
                When(PaidInvoiceIssued).Then(IssueInvoice).Then(PrintInvoice).Then(SetPaymentDate).TransitionTo(Paid),
                When(InvoicePositionsSet).Then(SetInvoicePositions),
                When(InvoiceTemplateSet).Then(SetInvoiceTemplate));

            During(Issued,
                When(InvoicePrinted).Then(PrintInvoice).TransitionTo(Printed),
                When(InvoicePaymentDeadline.Received).TransitionTo(Overdue));

            During(Printed, Overdue,
                When(InvoicePaid).Then(SetPaymentDate).Unschedule(InvoicePaymentDeadline).TransitionTo(Paid));
        }

        private Task<IInvoicePaymentDeadlineExpired> InitExpireEvent(ConsumeEventContext<Invoice, IInvoiceIssued> c)
            => c.Init<IInvoicePaymentDeadlineExpired>(new { c.Data.InvoiceId });

        private DateTime CalculateDelay(ConsumeEventContext<Invoice, IInvoiceIssued> context)
            => context.Instance.PaymentDeadline.Value;

        private void SetInvoiceTemplate(BehaviorContext<Invoice, IInvoiceTemplateSet> context)
            => context.Instance.SetTemplateVersion(context.Data.TemplateVersionId);

        private void SetInvoicePositions(BehaviorContext<Invoice, IInvoicePositionsSet> context)
            => context.Instance.SetPositions(context.Data.InvoicePositions.ToList());

        private void SetPaymentDate(BehaviorContext<Invoice, IInvoicePaid> context)
            => context.Instance.SetPaymentDate(context.Data.PaymentDate);

        private void SetPaymentDate(BehaviorContext<Invoice, IPaidInvoiceIssued> context)
            => context.Instance.SetPaymentDate(context.Data.IssueDate);

        private void PrintInvoice(BehaviorContext<Invoice, IInvoicePrinted> context)
            => context.Instance.SetPrintoutDate(context.Data.PrintoutDate);

        private void PrintInvoice(BehaviorContext<Invoice, IPaidInvoiceIssued> context)
            => context.Instance.SetPrintoutDate(context.Data.IssueDate);

        private void IssueInvoice(BehaviorContext<Invoice, IInvoiceIssued> context)
            => context.Instance.Issue(
                context.Data.InvoiceNumber,
                context.Data.IssueDate,
                context.Data.IssuerId,
                context.Data.PaymentDeadline
            );

        private void IssueInvoice(BehaviorContext<Invoice, IPaidInvoiceIssued> context)
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
                e.SelectId(x => Guid.NewGuid());
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