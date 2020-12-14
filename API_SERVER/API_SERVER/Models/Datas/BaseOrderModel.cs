using API_SERVER.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Models.Datas
{
    public class BaseOrderModel
    {
        [Key]
        public int orderID { get; set; }
        public DateTime creaTime { get; set; }

        public string displayName { get; set; }

        public string sponsorID_FK { get; set; }
        [ForeignKey("sponsorID_FK")]
        public PersonalData sponsor { get; set; }
        public string receiverID_FK { get; set; }
        [ForeignKey("receiverID_FK")]
        public PersonalData receiver { get; set; }
        public ICollection<PersonalData> likedUserID { get; set; }

        //TODO 添加关闭属性
    }
}
