using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmThongTinSinhVien.Model
{
    public class Contact
    {
        public String contactID { get; set; }
        public String contactName { get; set; }
        public String contactPhone { get; set; }
        public String contactEmail { get; set; }
        public string Character
        {
            get
            {
                return contactName[0].ToString().ToUpper();
            }
        }
    }
}
