//using System.Drawing;

using Bird.Client.Mtchmkr.Portable.Models;
using System.Collections.ObjectModel;

namespace Bird.Client.Mtchmkr.Helpers
{
    public class PendingHelper
    {
        static PendingModel CreateModel()
        {

            PendingModel _model = new PendingModel();
            MatchHelper.Build(_model);
            PlayerHelper.Build(_model);
            GameHelper.Build(_model);
            return _model;
        }
    public static ObservableCollection<PendingModel> Create()
    {
        ObservableCollection<PendingModel> _Collection = new ObservableCollection<PendingModel>();
        _Collection.Add(CreatePending());
        _Collection.Add(CreatePending());
        _Collection.Add(CreatePending());
        _Collection.Add(CreatePending());
        return _Collection;
    }
   static PendingModel CreatePending()
    {
        PendingModel model = new PendingModel();
        PlayerHelper.Build(model);

        return model;
    }
}
}