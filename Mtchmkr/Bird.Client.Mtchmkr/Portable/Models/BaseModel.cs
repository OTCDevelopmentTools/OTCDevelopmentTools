using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Bird.Client.Mtchmkr.Portable.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
        public BaseModel Self
        {
            get { return this; }
        }

        private bool m_HasChanged;
        public bool HasChanged
        {
            get { return m_HasChanged; }
            set
            {
                if (m_HasChanged == value) return;
                m_HasChanged = value;
                OnPropertyChanged(nameof(HasChanged));
            }
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            HasChanged = true;
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}