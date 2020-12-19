using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Models.Datas.FleaData
{
    public class FleaObjectModel : BaseOrderModel
    {
        public string details { get; set; }
        public List<Picture> pictures { get; set; }
    }
}
