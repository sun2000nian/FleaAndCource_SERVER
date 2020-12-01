using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Models
{
    public class RecvImg_SubmitData
    {
        public string userID { get; set; }
        public IFormFile file { get; set; }
    }
}
