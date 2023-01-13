using System;
using System.Drawing;

namespace Bird.Client.Mtchmkr.Portable.Models
{
    public class GameTypeModel : BaseModel
    {
        public Guid Key { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public Color Colour { get; set; }
        public string Image { get; set; }

        private bool m_Selected;
        public bool Selected 
        {
            get { return m_Selected; }
            set
            {
                if (m_Selected != value)
                {
                    m_Selected = value;
                    OnPropertyChanged(nameof(Selected));
                    OnPropertyChanged(nameof(Self));

                }
            }
                
        }
    }
}