using System;
namespace Bird.Client.Mtchmkr.Business.ServiceCenter.Response
{
    
    public class PaymentInfo
    {
        public string usersPaymentInfoId { get; set; }
        public string transactionId { get; set; }
        public Guid userId { get; set; }
        public double payment { get; set; }
        public int matchCount { get; set; }
        public DateTime createdDate { get; set; }
    }

    public class PaymentRequest
    {
        public string transactionId { get; set; }
        public Guid userId { get; set; }
        public double payment { get; set; }
        public int matchCount { get; set; }
        public DateTime createdDate { get; set; }
    }
}
