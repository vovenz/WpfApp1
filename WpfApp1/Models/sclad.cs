using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    internal class sclad: INotifyPropertyChanged
    {
        private bool _IsDone;
        private string _Text;

        public DateTime CreationTime { get; set; } = DateTime.Now;
        public bool IsDone
        {
            get { return _IsDone; }
            set 
            {
                if (_IsDone == value)
                    return;
                _IsDone = value; 
                OnPropertyChanged("IsDone");
            }
        }
        public string Text
        {
            get { return _Text; }
            set 
            {
                if (_Text == value)
                    return;
                _Text = value; 
                OnPropertyChanged("Text");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName ="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
        }
    }
}
