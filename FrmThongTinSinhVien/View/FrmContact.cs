using FrmThongTinSinhVien.Controller;
using FrmThongTinSinhVien.Model;
using FrmThongTinSinhVien.View;
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
        String pathDataFile;
        #endregion
        public FrmContact()
        {
            InitializeComponent();
            pathDataFile = Application.StartupPath + @"\Data\Contact.txt";
            loadContact();
        }
        private void loadContact()
        {

            bdsConTact.DataSource = null;
            dtgConTact.AutoGenerateColumns = false;
            List<Contact> lstContacts = ContactService.GetContacts(pathDataFile);
            if (lstContacts == null)
                throw new Exception("Chưa có thông tin về danh bạ");
            else
            {
                bdsConTact.DataSource = lstContacts;
            }
            dtgConTact.DataSource = bdsConTact;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var f = new FrmContactDetail(null, pathDataFile);
            if (f.ShowDialog() == DialogResult.OK)
            {
                // Tiến hành nạp lại dữ liệu lên lưới
                loadContact();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var contact = bdsConTact.Current as Contact;
            if (contact != null)
            {
                var f = new FrmContactDetail(contact, pathDataFile);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    // Tiến hành nạp lại dữ liệu lên lưới
                    loadContact();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var contactID = dtgConTact.CurrentRow.Cells[4].Value.ToString();
            ContactService.deleteContact(pathDataFile, contactID);
            dtgConTact.Rows.RemoveAt(dtgConTact.CurrentRow.Index);
            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string value = txtSearch.Text;
            bdsConTact.DataSource = null;
            dtgConTact.AutoGenerateColumns = false;
            List<Contact> lstSearchContacts = ContactService.SearchContacts(pathDataFile, value);
            if (lstSearchContacts == null)
                throw new Exception("Không tìm thấy thông tin cần tìm");
            else
            {
                bdsConTact.DataSource = lstSearchContacts;
            }
            dtgConTact.DataSource = bdsConTact;
        }
    }
}
