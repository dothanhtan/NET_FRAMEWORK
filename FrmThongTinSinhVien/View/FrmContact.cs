using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmThongTinSinhVien
{
    
    public partial class FrmContact : Form
    {
        #region Variables to process contact
        String IDContact;
        String pathContact;
        #endregion
        public FrmContact()
        {
            InitializeComponent();
            pathContact = Application.StartupPath + @"\Data\Contact.txt";

        }

    }
}
