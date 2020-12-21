using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Models.Datas.CourseData
{
    public class CourseModel : BaseOrderModel
    {
        public string Location { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool isAccepted { get; set; }
    }
}
