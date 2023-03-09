using System;
namespace Bird.Client.Mtchmkr.Portable.Comon
{
	public class Purchase
	{
        public string Id { get; set; }
        public double Price { get; set; }
        public object NativeObject { get; set; }
        public string Description { get; set; }
        public string LocalizeTitle { get; set; }
    }
}

