using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.OrganizerWPF.Model
{
    class OrganizerUI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Email { get; set; }
        //public string Phone { get; set; }
        //public string Address { get; set; }
        public ObservableCollection<ActivityUI> ActivityUIs { get; set; }

    }
}
