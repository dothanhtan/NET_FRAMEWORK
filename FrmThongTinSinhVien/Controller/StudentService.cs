using FrmThongTinSinhVien.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmThongTinSinhVien.Controller
{
    public class StudentService
    {
        /// <summary>
        /// Lấy sinh viên theo mã sinh viên từ MockData
        /// </summary>
        /// <param name="idStudent">Mã sinh viên</param>
        /// <returns>Thông tin của sinh viên có mã tương ứng.
        /// Nếu sinh viên không tồn tại thì trả về NULL
        /// </returns>
        public static Student GetStudent(string idStudent)
        {
            var student = new Student
            {
                IDStudent = idStudent,
                FirstName = "Tân",
                LastName = "Đỗ Thanh",
                DateOfBirth = new DateTime(1998, 01, 28),
                Gender = GENDER.Male,
                PlaceOfBirth = "Thừa Thiên Huế"
            };
            student.ListLearningHistory = new List<LearningHistory>();
            for (int i = 1; i <= 12; i++)
            {
                LearningHistory learning = new LearningHistory
                {
                    IDLearningHistory = i.ToString(),
                    YearFrom = 2004 + i,
                    YearEnd = 2005 + i,
                    IDStudent = idStudent
                };
                if (i <= 5)
                    learning.Address = "Tiểu học Thanh Toàn";
                else if (i <= 9)
                    learning.Address = "Trung học Cơ sở Thủy Vân";
                else
                    learning.Address = "Trung học Phổ Thông Phan Đăng Lưu";
                student.ListLearningHistory.Add(learning);
            }
            return student;
        }

        /// <summary>
        /// Lấy sinh viên theo mã sinh viên từ File
        /// </summary>
        /// <param name="pathDataFile">Đường dẫn tới file chứa dữ liệu</param>
        /// <param name="idStudent">Mã sinh viên</param>
        /// <returns>Sinh viên theo mã sinh viên hoặc NULL nếu không thấy</returns>
        public static Student GetStudent(string pathDataFile, string idStudent)
        {
            if (File.Exists(pathDataFile))
            {
                CultureInfo culture = CultureInfo.InvariantCulture;
                var listLines = File.ReadAllLines(pathDataFile);
                foreach (var line in listLines)
                {
                    var rs = line.Split(new char[] { '#' });
                    Student student = new Student
                    {
                        IDStudent = rs[0],
                        LastName = rs[1],
                        FirstName = rs[2],
                        Gender = rs[3] == "Male" ? GENDER.Male : (rs[3] == "Female" ? GENDER.Female : GENDER.Other),
                        DateOfBirth = DateTime.ParseExact(rs[4], "yyyy-MM-dd", culture),
                        PlaceOfBirth = rs[5]
                    };
                    if (student.IDStudent == idStudent)
                        return student;
                }
                return null;
            }
            else
                return null;
        }

        /// <summary>
        /// Lấy danh sách quá trình học tập của 1 sinh viên
        /// </summary>
        /// <param name="pathDataFile">Đường dẫn file chứa dữ liệu</param>
        /// <param name="idStudent">Mã sinh viên cần lấy</param>
        /// <returns>Danh sách quá trình học tập</returns>
        public static List<LearningHistory> GetHistoryLearning(string pathDataFile, string idStudent)
        {
            if (File.Exists(pathDataFile))
            {
                var listLines = File.ReadAllLines(pathDataFile);
                List<LearningHistory> listHistory = new List<LearningHistory>();
                foreach (var line in listLines)
                {
                    var rs = line.Split(new char[] { '#' });
                    LearningHistory history = new LearningHistory
                    {
                        IDLearningHistory = rs[0],
                        YearFrom = int.Parse(rs[1]),
                        YearEnd = int.Parse(rs[2]),
                        Address = rs[3],
                        IDStudent = rs[4]
                    };
                    if (history.IDStudent == idStudent)
                        listHistory.Add(history);
                }
                return listHistory;
            }
            else
                return null;
        }
        /// <summary>
        /// Tạo quá trình học tập cho mỗi sinh viên
        /// </summary>
        /// <param name="yearFrom"></param>
        /// <param name="yearEnd"></param>
        /// <param name="address"></param>
        /// <param name="maSinhVien"></param>
        /// <param name="pathHistoryFile"></param>
        public static void CreateHistoryLearning(int yearFrom, int yearEnd, string address, string maSinhVien, string pathHistoryFile)
        {
            var IDHistoryLearning = Guid.NewGuid().ToString();
            string lineHistory = IDHistoryLearning + "#" + yearFrom + "#" + yearEnd + "#" + address + "#" + maSinhVien;
            if (File.Exists(pathHistoryFile))
            {
                File.AppendAllText(pathHistoryFile, "\r\n" + lineHistory + "\r\n");
            }
        }
        /// <summary>
        /// Chỉnh sửa quá trình học tập của sinh viên
        /// </summary>
        /// <param name="historyID"></param>
        /// <param name="yearFrom"></param>
        /// <param name="yearEnd"></param>
        /// <param name="address"></param>
        /// <param name="pathHistoryFile"></param>
        public static void EditHistoryLearning(string historyID, int yearFrom, int yearEnd, string address, string pathHistoryFile)
        {
            if (File.Exists(pathHistoryFile))
            {
                var listHistoryLines = File.ReadAllLines(pathHistoryFile);
                File.WriteAllText(pathHistoryFile, "");
                bool isNextLine = false;
                foreach (var lineHistory in listHistoryLines)
                {
                    var rsHistory = lineHistory.Split(new char[] { '#' });
                    if (rsHistory[0] != historyID)
                    {
                        if (!isNextLine)
                        {
                            File.AppendAllText(pathHistoryFile, lineHistory + "\r\n");
                        }
                        else
                        {
                            File.AppendAllText("\r\n" + pathHistoryFile, lineHistory + "\r\n");
                            isNextLine = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show(address);
                        string content = historyID + "#" + yearFrom + "#" + yearEnd + "#" + address + "#" + rsHistory[4];
                        File.AppendAllText(pathHistoryFile, content);
                        isNextLine = true;
                    }
                }
            }
        }
        /// <summary>
        /// Xóa quá trình học tập của sinh viên
        /// </summary>
        /// <param name="pathHistoryFile"></param>
        /// <param name="historyID"></param>
        public static void DeleteHistoryLearning(string pathHistoryFile, string historyID)
        {
            if (File.Exists(pathHistoryFile))
            {
                var listHistoryLines = File.ReadAllLines(pathHistoryFile);
                File.WriteAllText(pathHistoryFile, "");
                foreach (var lineHistory in listHistoryLines)
                {
                    var rsHistory = lineHistory.Split(new char[] { '#' });
                    if (rsHistory[0] != historyID)
                        File.AppendAllText(pathHistoryFile, lineHistory + "\r\n");
                }
            }
        }
    }
}
