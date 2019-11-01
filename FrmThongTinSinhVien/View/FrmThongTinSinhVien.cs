using FrmThongTinSinhVien.Controller;
using FrmThongTinSinhVien.Model;
using FrmThongTinSinhVien.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmThongTinSinhVien
{
    public partial class FrmThongTinSinhVien : Form
    {
        #region Variables to process Image Avatar 
        Image image;
        string maSinhVien;
        string pathDirectoryImg;
        string pathAvatarImg;
        #endregion

        #region Path data file
        string pathStudentDataFile;
        string pathLearningHistoryDataFile;
        #endregion

        public FrmThongTinSinhVien(string maSinhVien)
        {
            InitializeComponent();
            this.maSinhVien = maSinhVien;
            pathDirectoryImg = Application.StartupPath + "\\Img";
            pathAvatarImg = pathDirectoryImg + "\\avatar.png";
            picAnhDaiDien.AllowDrop = true;

            pathStudentDataFile = Application.StartupPath + @"\Data\student.txt";
            pathLearningHistoryDataFile = Application.StartupPath + @"\Data\learninghistory.txt";

            if (File.Exists(pathAvatarImg))
            {
                FileStream fileStream = new FileStream(pathAvatarImg, FileMode.Open, FileAccess.Read);
                picAnhDaiDien.Image = Image.FromStream(fileStream);
                fileStream.Close();
                reloadDataHistory(maSinhVien);
            }
        }
        private void reloadDataHistory(string maSinhVien)
        {
            bdsQuaTrinhHocTap.DataSource = null;
            dtgQuaTrinhHocTap.AutoGenerateColumns = false;

            //var student = StudentService.GetStudent(maSinhVien);
            var student = StudentService.GetStudent(pathStudentDataFile, maSinhVien);
            if (student == null)
            {
                throw new Exception("Không tồn tại sinh viên này");
            }
            else
            {
                student.ListLearningHistory = StudentService.GetHistoryLearning(pathLearningHistoryDataFile, maSinhVien);
                txtMaSinhVien.Text = student.IDStudent;
                txtHo.Text = student.LastName;
                txtTen.Text = student.FirstName;
                chkGioiTinh.Checked = student.Gender == GENDER.Male;
                dtpNgaySinh.Value = student.DateOfBirth;
                txtQueQuan.Text = student.PlaceOfBirth;
                if (student.ListLearningHistory != null)
                {
                    bdsQuaTrinhHocTap.DataSource = student.ListLearningHistory;
                }    
            }
            dtgQuaTrinhHocTap.DataSource = bdsQuaTrinhHocTap;
        }
            
        private void LnkChonAnhDaiDien_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn ảnh đại diện";
            openFileDialog.Filter = "File ảnh(*.png, *.jpg)|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog.FileName);
                picAnhDaiDien.Image = image;
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            #region Cập nhật hình đại diện
            bool imageSave = false;
            if (image != null)
            {
                if (!Directory.Exists(pathDirectoryImg))
                {
                    Directory.CreateDirectory(pathDirectoryImg);
                }
                image.Save(pathAvatarImg);
                imageSave = true;
            }
            #endregion
            if (imageSave)
            {
                MessageBox.Show(
                    "Đã cập nhật thông tin sinh viên thành công",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }
        }

        private void picAnhDaiDien_DragDrop(object sender, DragEventArgs e)
        {
            var rs = (string[])e.Data.GetData(DataFormats.FileDrop);
            var filePath = rs.FirstOrDefault();
            image = Image.FromFile(filePath);
            picAnhDaiDien.Image = image;
        }

        private void picAnhDaiDien_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var rs = MessageBox.Show(
                 "Bạn có chắc là muốn xóa dữ liệu này không?",
                 "Thông báo",
                 MessageBoxButtons.OKCancel,
                 MessageBoxIcon.Warning);
            if (rs == DialogResult.OK)
            {
                //Viết code xóa dữ liệu tại đây
                var historyID = dtgQuaTrinhHocTap.CurrentRow.Cells[0].Value.ToString();
                StudentService.DeleteHistoryLearning(pathLearningHistoryDataFile, historyID);
                dtgQuaTrinhHocTap.Rows.RemoveAt(dtgQuaTrinhHocTap.CurrentRow.Index);
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Bạn đã không xóa");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var f = new FrmQuaTrinhHocTapChiTiet(null, maSinhVien, pathLearningHistoryDataFile);
            if(f.ShowDialog() == DialogResult.OK)
            {
                //Tien hanh nap lai du lieu len luoi
                reloadDataHistory(maSinhVien);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var history = bdsQuaTrinhHocTap.Current as LearningHistory;
            if(history != null)
            {
                var f = new FrmQuaTrinhHocTapChiTiet(history, maSinhVien, pathLearningHistoryDataFile);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    //Tien hanh nap lai du lieu len luoi
                    reloadDataHistory(maSinhVien);
                }
            }
            
        }
    }
}
