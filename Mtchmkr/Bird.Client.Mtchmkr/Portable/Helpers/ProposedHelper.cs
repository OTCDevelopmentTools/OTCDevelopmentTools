using Bird.Client.Mtchmkr.Portable.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
//using System.Drawing;

namespace Bird.Client.Mtchmkr.Helpers
{
    public class ProposedHelper
    {
        public static ObservableCollection<ProposedMatchModel> Create(int count)
        {
            List<ProposedMatchModel> _Collection = new List<ProposedMatchModel>();
            for (int i = 0; i < count; i++)
            {
                _Collection.Add(Create());
            }
            _Collection.Sort((x,y) => (x.Date>y.Date ? 1 : x.Date.Equals(y.Date) ? 0:-1));
            return _Collection.ToObservableCollection<ProposedMatchModel>();
        }
        public static ProposedMatchModel Create()
        {
            ProposedMatchModel model = new ProposedMatchModel();
            PlayerHelper.Build(model);
            MatchHelper.Build(model);
            GameHelper.Build(model);
            return model;
        }
    }
}
