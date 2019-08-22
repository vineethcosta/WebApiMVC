using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace MVCPractice.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {

            string role=Convert.ToString(Session["role"]);           
            if (Session["accountNumber"] != null)
            {
                Session["accountNumber"] = null;
            }
            if (role.Equals("Manager"))
            {
               
                return RedirectToAction("Index", "Manager");
            }
            else if (role.Equals("Customer"))
                return RedirectToAction("Index", "Customer");
            else if (role.Equals("BankManager"))
                return RedirectToAction("Index", "SuperManager");
            else
            {
            
                return View();
            }
        }
        [HttpPost]
        public ActionResult loginCheck(string userId,string password)
        {

            string role="",error="";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:80");
                var responseTask = client.GetAsync("BankApiNew/api/Login/checkCredentials?loginId=" + userId + "&password=" + password);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<string>();
                    readTask.Wait();
                    role = readTask.Result;
                }
                else
                {
                    var readTask = result.Content.ReadAsAsync<string>();
                    readTask.Wait();
                    error = readTask.Result;
                    //ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
                

            if (role == "Manager")
            {
                Session["role"] = "Manager";
                Session["userId"] = userId;
                return RedirectToAction("Index", "Manager");
            }
            else if (role == "Customer")
            {
                Session["role"] = "Customer";
                Session["userId"] = userId;
                return RedirectToAction("Index", "Customer");

            }
            else if (role == "BankManager")
            {
                Session["role"] = "BankManager";
                Session["userId"] = userId;
                return RedirectToAction("Index", "SuperManager");

            }
            else
            {
                Session["loginErrorMessage"] = error;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }

    }
}
