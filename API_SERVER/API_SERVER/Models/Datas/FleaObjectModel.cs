using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Models.Datas
{
    public class FleaObjectModel : BaseOrderModel
    {
        public string picturePath { get; set; }
        public double price { get; set; }
    }
}
