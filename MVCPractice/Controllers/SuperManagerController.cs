using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using System.Net.Http;
using MVCPractice.Models;

namespace MVCPractice.Controllers
{
    public class SuperManagerController : Controller
    {
        public ActionResult Index()
        {
            List<userDetailViewModel> allManagers = new List<userDetailViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:80");
                var responseTask = client.GetAsync("BankApiNew/api/SuperManager/getAllManagers");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<userDetailViewModel>>();
                    readTask.Wait();
                    allManagers = readTask.Result;
                }
                if (Session["mgrId"] != null)
                {
                    Session["mgrId"] = null;
                    
                }
                return View(allManagers);
            }
        }

        public ActionResult ManagersList()
        {
            List<userDetailViewModel> allManagers = new List<userDetailViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:80");
                var responseTask = client.GetAsync("BankApiNew/api/SuperManager/getAllManagers");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<userDetailViewModel>>();
                    readTask.Wait();
                    allManagers = readTask.Result;
                }
                if (Session["mgrId"] != null)
                {
                    Session["mgrId"] = null;

                }
                return View(allManagers);
            }
        }

        public ActionResult ManageManager()
        {
            return View();
        }

        [HttpPost]

        public ActionResult ManageManager(int managerId)
        {
            
            userDetailViewModel singleManager = new userDetailViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:80");
                var responseTask = client.GetAsync("BankApiNew/api/SuperManager/getSingleManager?managerId="+managerId);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<userDetailViewModel>();
                    readTask.Wait();
                    singleManager = readTask.Result;
                    ViewBag.mgrId = managerId; 
                }
                else
                {
                    ViewBag.NotFoundManager = "Manager Not Found";
                    return View("ManageManager");
                   
                }
                if (Session["mgrId"] != null)
                {
                    Session["mgrId"] = null;

                }
                return View(singleManager);
            }
        }
        public string getBranchId(int managerId)
        {

            string singleManager="";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:80");
                var responseTask = client.GetAsync("BankApiNew/api/SuperManager/getBranchId?managerId=" + managerId);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<string>();
                    readTask.Wait();
                    singleManager = readTask.Result;
                }
                if (Session["mgrId"] != null)
                {
                    Session["mgrId"] = null;

                }
                ViewBag.mgrId = managerId;
               
            }
            return singleManager;
        }

        public ActionResult AddManager()
        {
            List<branchDetailViewModel> branchList=new List<branchDetailViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:80");
                var responseTask = client.GetAsync("BankApiNew/api/SuperManager/getUnassignedBranch");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<branchDetailViewModel>>();
                    readTask.Wait();
                    branchList = readTask.Result;
                }
               
                
            }
            return View(branchList);
        }

         [HttpPost]
        public ActionResult AddManager(string name, string gender,DateTime dateOfBirth,string address,string state,string city,int pincode,string mail,string branch, string phoneNumber)
        {
        
             
            userDetailViewModel addManagerDetails = new userDetailViewModel
            {
                userName = name,
                gender=gender,
                dateOfBirth=dateOfBirth,
                address = address,
                state=state,
                city=city,
                pincode=pincode,
                branchId = branch, 
                phoneNumber = phoneNumber,
                emailId = mail,
                createdDate=DateTime.Now,
                editedDate=DateTime.Now,
                managerId=999,
                createdBy=999,
                editedBy=999
            };
          
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:80");
                var responseTask = client.PostAsJsonAsync("BankApiNew/api/SuperManager/addManager", addManagerDetails);
                responseTask.Wait();
                var result = responseTask.Result;
                ViewBag.result = responseTask.Result;
            }
              
            
            return RedirectToAction("Index");
        }

        
        public ActionResult EditManager(int id)
         {
             ViewBag.mgrId = id;
             Session["mgrId"] = id;
         
             userDetailViewModel singleManager = new userDetailViewModel();
             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri("http://localhost:80");
                 var responseTask = client.GetAsync("BankApiNew/api/SuperManager/getSingleManager?managerId=" + id);
                 responseTask.Wait();
                 var result = responseTask.Result;
                 if (result.IsSuccessStatusCode)
                 {
                     var readTask = result.Content.ReadAsAsync<userDetailViewModel>();
                     readTask.Wait();
                     singleManager = readTask.Result;
                 }
             }
            Session["oldEmailId"]=singleManager.emailId;
            Session["oldBranchId"]=singleManager.branchId;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:80");
                var responseTask = client.GetAsync("BankApiNew/api/SuperManager/getUnassignedBranch");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<branchDetailViewModel>>();
                    readTask.Wait();
                    ViewBag.branchList = readTask.Result;
                }


            }

            return View(singleManager);
         }

        [HttpPost]
        public ActionResult EditManager(string name, string gender, DateTime dateOfBirth, string address, string state, string city, int pinCode,  string branch, string phoneNumber)
        {
            int userId = (int)Session["mgrId"];
            string oldBranchId = Session["oldBranchId"].ToString();
            string oldEmailId = Session["oldEmailId"].ToString();
            userDetailViewModel editManagerDetails = new userDetailViewModel();
            {
                editManagerDetails.userId = userId;
                editManagerDetails.userName = name;
                editManagerDetails.gender = gender;
                editManagerDetails.dateOfBirth = dateOfBirth;
                editManagerDetails.address = address;
                editManagerDetails.state = state;
                editManagerDetails.city = city;
                editManagerDetails.pincode = pinCode;
                editManagerDetails.branchId = branch;
                editManagerDetails.phoneNumber = phoneNumber;
                editManagerDetails.emailId = oldEmailId;
                editManagerDetails.createdDate = DateTime.Now;
                editManagerDetails.editedDate = DateTime.Now;
                editManagerDetails.managerId = 999;
                editManagerDetails.createdBy = 999;
                editManagerDetails.editedBy = 999;
              
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:80");
                var responseTask = client.PostAsJsonAsync("BankApiNew/api/SuperManager/changeBranch?oldBranchId=" + oldBranchId + "&newBranchId="+branch,1);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<string>();
                    readTask.Wait();
                }
                ViewBag.result = responseTask.Result;
            }
           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:80");
                var responseTask = client.PostAsJsonAsync("BankApiNew/api/SuperManager/editManager", editManagerDetails);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<string>();
                    readTask.Wait();
                }
                ViewBag.result = responseTask.Result;
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteManager(int id)
        {
            ViewBag.mgrId = id;
            Session["mgrId"] = id;
            return View();
        }

        [HttpPost]

        public ActionResult DeleteManager()
        {
            int managerId = (int)Session["mgrId"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:80");
                var responseTask = client.PostAsJsonAsync("BankApiNew/api/SuperManager/deleteManager?managerId=" + managerId,1);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<string>();
                    readTask.Wait();
                }
            }
            return RedirectToAction("Index");
        }


    }
}
