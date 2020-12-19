using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Models.Datas.CourceData
{
    public class ClassTimeNum
    {
        public int WeekNum { get; set; }
        public DateTime date { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public int StartClassNum { get; set; }
        public int EndClassNum { get; set; }
    }
}
