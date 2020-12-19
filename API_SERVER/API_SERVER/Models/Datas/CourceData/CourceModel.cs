﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Models.Datas.CourceData
{
    public class CourceModel : BaseOrderModel
    {
        public string Location { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool isAccepted { get; set; }
    }
}