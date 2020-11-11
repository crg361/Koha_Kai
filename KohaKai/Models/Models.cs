using System;
using System.Data;




namespace KohaKai.Models
{
    public class MyModel
    {
        public static string AdminName = "";//当前登陆的用户名
        public kohakaiEntities db= new kohakaiEntities();
    }

}