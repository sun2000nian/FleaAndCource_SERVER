using API_SERVER.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Models.Datas
{
    public class BaseOrderModel
    {
        public string orderID { get; set; }
        public DateTime creaTime { get; set; }
        public PersonalData sponsorID { get; set; }
        public PersonalData receiverID { get; set; }
        public ICollection<PersonalData> likedUserID { get; set; }
    }
}
