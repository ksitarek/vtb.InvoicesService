using System;

namespace vtb.InvoicesService.BusinessLogic
{
    public static class Endpoints
    {
        public static readonly Uri CreateInvoiceDraft = new Uri("queue:create-invoice-draft");
    }
}