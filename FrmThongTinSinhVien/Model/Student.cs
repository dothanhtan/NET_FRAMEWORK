using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmThongTinSinhVien.Model
{
    public class Student
    {
        public string IDStudent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GENDER Gender { get; set; }
        public string PlaceOfBirth { get; set; }
        public List<LearningHistory> ListLearningHistory { get; set; }
    }
    public enum GENDER
    {
        Female, Male, Other
    }
}
