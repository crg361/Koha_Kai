using System.Web.Mvc;
using KohaKai.Models;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System;

namespace KohaKai.Controllers
{
    public class LoginController : Controller
    {
        public kohakaiEntities db= new kohakaiEntities();
        public ActionResult Index()
        {
            return View();
        }
        public bool CheckLog(string name, string pw)
        {
            //MyModel.AdminName = "Admin";
            //return true;
            var user = db.users.SqlQuery("SELECT * FROM [user] where name = '"+ name +"'").ToListAsync();
            if (user.Result.Count > 0)
            {
                if (user.Result[0].pw == pw) {
                    MyModel.AdminName = name;
                    return true; }//
                else { return false; }//密码错误
            }
            else { return false; }//找不到用户
        }
    }
}