using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Models.Datas.FleaData
{
    public class Picture
    {
        [Key]
        public int id { get; set; }
        public string Path { get; set; }
        public string title { get; set; }
        public string content { get; set; }
    }
}
