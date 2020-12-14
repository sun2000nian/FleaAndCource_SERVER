using API_SERVER.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API_SERVER.Models.Datas
{
    public class BaseOrderModel
    {
        [Key]
        public int orderID { get; set; }
        public DateTime createTime { get; set; }

        public string displayName { get; set; }

        [JsonPropertyName("sponsor")]
        public string sponsor { get; set; }
        [JsonIgnore]
        [ForeignKey("sponsor")]
        public PersonalData sponsorData { get; set; }

        [JsonPropertyName("receiver")]
        public string receiver { get; set; }
        [JsonIgnore]
        [ForeignKey("receiver")]
        public PersonalData receiverData { get; set; }

        [JsonIgnore]
        public ICollection<PersonalData> likedUserID { get; set; }
        public int likedUserCount
        {
            get
            {
                return likedUserID.Count;
            }
        }

        //TODO 添加关闭属性
        public bool isClosed { get; set; }

        public DateTime closeTime { get; set; }
    }
}
