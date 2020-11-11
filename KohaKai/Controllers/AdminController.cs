using KohaKai.Models;
using System.Data;
using System.Web.Mvc;


namespace KohaKai.Controllers
{

    public class AdminController : Controller
    {
        public DataSet MyDS;
        public ActionResult Index()
        {
            if (MyModel.AdminName == "") { Response.Redirect("/Login"); }
            //Layout = "~/Views/Admin/_AdminLayout.cshtml";
            return View();
        }
        public ActionResult Logout()
        { 
            MyModel.AdminName = "";
            Response.Redirect("../Login");
            return View();
            }
    }
}