using System;
using NUnit.Framework;
using Shouldly;

namespace vtb.InvoicesService.Domain.Tests.Invoices
{
    public class Invoice_SetTemplateVersion : InvoiceTests
    {
        [Test]
        public void Will_Set_TemplateVersion()
        {
            var invoice = GenerateEmptyTestInvoice();

            var templateVersionId = Guid.NewGuid();
            invoice.SetTemplateVersion(templateVersionId);

            invoice.TemplateVersionId.ShouldBe(templateVersionId);
        }

        [Test]
        public void Will_Require_Not_Empty_Guid()
        {
            var invoice = GenerateEmptyTestInvoice();

            var templateVersionId = Guid.Empty;
            Should.Throw<ArgumentException>(() => invoice.SetTemplateVersion(templateVersionId));
        }
    }
}