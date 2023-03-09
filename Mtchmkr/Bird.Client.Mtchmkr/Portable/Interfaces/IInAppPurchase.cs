using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bird.Client.Mtchmkr.Portable.Comon;

namespace Bird.Client.Mtchmkr.Portable.Interfaces
{
	public interface IInAppPurchase
	{
		public Task<List<Purchase>> GetPurchases(params string[] ids);
		public Task<List<Reciept>> Purchase(Purchase purchase); 
	}
}

