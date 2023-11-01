using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.CustomerWPF.Model
{
    public class MemberUI : INotifyPropertyChanged
    {
        private string _name;
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
        private DateOnly _birthday;
        public DateOnly Birthday { get { return _birthday; } set { _birthday = value; OnPropertyChanged(); } }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
