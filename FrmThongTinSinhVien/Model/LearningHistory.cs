using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmThongTinSinhVien.Model
{
    public class LearningHistory
    {
        public string IDLearningHistory { get; set; }
        public int YearFrom { get; set; }
        public int YearEnd { get; set; }
        public string Address { get; set; }
        public string IDStudent { get; set; }
        public Student Student { get; set; }

        public string Period
        {
            get
            {
                return string.Format("{0} -> {1}", YearFrom, YearEnd);
            }
        }
    }
    
}
