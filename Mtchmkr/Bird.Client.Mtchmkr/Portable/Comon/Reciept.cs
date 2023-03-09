using System;
namespace Bird.Client.Mtchmkr.Portable.Comon
{
    public class Reciept
    {
        public PurchaseState PurchasedState { get; set; }
        public string Description { get; set; }
        public string Error { get; set; }
        public string Id { get; set; }
        public override string ToString()
        {
            return $"{PurchasedState}||{Description}";
        }
    }
}

