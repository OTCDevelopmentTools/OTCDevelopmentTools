using System;
namespace Bird.Client.Mtchmkr.Portable.Interfaces
{
    public interface IProgressDialog
    {
        bool IsShowing { get; set; }

        void ShowProgressAlt(string message);

        void ShowProgress(string message);

        void HideProgress();
    }
}
