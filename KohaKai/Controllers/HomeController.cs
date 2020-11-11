using System;
using KohaKai.Models;
using System.Data;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Web.Mvc;
using System.Data.Entity;

namespace KohaKai.Controllers
{
    public class HomeController : Controller
    {
        //private KohaKai.Models.MyModel MM = new KohaKai.Models.MyModel();
        public async Task<ActionResult> Index()
        {
            
            //获取blog数据
            //return View(await MM.db.blogs.ToListAsync());
            return View();
        }

        public string SendMail()
        {
            SmtpClient MailClient = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential("2017010657@student.sit.ac.nz", "Cqzz2014")
            };
            string Temp;
            Temp = "Message:" + Request.Form["msg_message"];
            Temp = Temp + "\n" + "Name:" + Request.Form["msg_name"];
            Temp = Temp + "\n" + "Email:" + Request.Form["msg_email"];
            Temp = Temp + "\n" + "Phone:" + Request.Form["msg_phone"];
            MailMessage Mail = new MailMessage(new MailAddress("2017010657@student.sit.ac.nz"), new MailAddress("183122228@qq.com"))
            {
                Body = Temp,
                Subject = "A message from Koha Kai Website"
            };
            MailClient.Send(Mail);
            return "Thank you!";
        }
    }
}
