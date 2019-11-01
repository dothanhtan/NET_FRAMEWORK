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
    public partial class FrmQuaTrinhHocTapChiTiet : Form
    {
        string maSinhVien;
        string pathHistoryFile;
        LearningHistory history;
        public FrmQuaTrinhHocTapChiTiet(LearningHistory history = null, string maSinhVien = null, string pathHistoryFile = null)
        {
            InitializeComponent();
            this.history = history;
            this.maSinhVien = maSinhVien;
            this.pathHistoryFile = pathHistoryFile;
            if(history != null)
            {
                //Chinh sua
                this.Text = "Chỉnh sửa quá trình học tập";
                numTuNam.Value = history.YearFrom;
                numDenNam.Value = history.YearEnd;
                txtNoiHoc.Text = history.Address;
            }
            else
            {
                //Them moi
                this.Text = "Thêm mới quá trình học tập";
            }
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if(history != null)
            {
                //Update
                var yearFrom = numTuNam.Value;
                var yearEnd = numDenNam.Value;
                var address = txtNoiHoc.Text;
                StudentService.EditHistoryLearning(history.IDLearningHistory, (int)yearFrom, (int)yearEnd, address, pathHistoryFile);
            }
            else
            {
                //Them moi
                var yearFrom = numTuNam.Value;
                var yearEnd = numDenNam.Value;
                var address = txtNoiHoc.Text;
                StudentService.CreateHistoryLearning((int)yearFrom, (int)yearEnd, address, maSinhVien, pathHistoryFile);
            }
            MessageBox.Show("Đã cập nhật dữ liệu thành công");
            DialogResult = DialogResult.OK;
        }
    }
}
