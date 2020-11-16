using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Models
{
    public class Values
    {
        public enum RegisterCode
        {
            Success,//成功
            UserExist//用户已存在
        }

        public enum ChangePasswordCode
        {
            //TODO:完善更改密码，用于传递更改结果
        }
    }
}
