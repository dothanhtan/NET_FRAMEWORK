using FrmThongTinSinhVien.Controller;
using FrmThongTinSinhVien.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmThongTinSinhVien.View
{
    public partial class FrmContactDetail : Form
    {
        Contact contact;
        string pathDataFile;

        public FrmContactDetail(Contact contact = null, string pathDataFile = null)
        {
            InitializeComponent();
            this.pathDataFile = pathDataFile;
            this.contact = contact;
            if (contact != null)
            {
                txtName.Text = contact.contactName;
                txtPhone.Text = contact.contactPhone;
                txtEmail.Text = contact.contactEmail;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (contact != null)
            {
                // Cập nhật
                var contactID = contact.contactID;
                var contactName = txtName.Text;
                var contactPhone = txtPhone.Text;
                var email = txtEmail.Text;
                ContactService.editContact(contactID, contactName, contactPhone, email, pathDataFile);
            }
            else
            {
                // Thêm mới
                var contactName = txtName.Text;
                var contactPhone = txtPhone.Text;
                var email = txtEmail.Text;
                ContactService.addContact(contactName, contactPhone, email, pathDataFile);
            }
            MessageBox.Show("Đã cập nhật dữ liệu thành công");
            DialogResult = DialogResult.OK; // Đóng Form
        }
    }
}
