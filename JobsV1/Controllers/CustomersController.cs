﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobsV1.Models;

namespace JobsV1.Controllers
{
    public class CustomersController : Controller
    {
        private JobDBContainer db = new JobDBContainer();
        private List<SelectListItem> StatusList = new List<SelectListItem> {
                new SelectListItem { Value = "ACT", Text = "Active" },
                new SelectListItem { Value = "INC", Text = "Inactive" },
                new SelectListItem { Value = "BAD", Text = "Bad Account" }
                };

        // GET: Customers
        public ActionResult Index()
        {

            var customerList = db.Customers.ToList();
            string category;
            string companyName;


            List<CustomerDetails> customerDetailList = new List<CustomerDetails>();
            foreach (var customer in customerList)
            {
                CustCategory custcategory = new CustCategory();
                CustCat custcat = new CustCat();
                CustEntity companyEntity = new CustEntity();
                CustEntMain company = new CustEntMain();

                try
                {
                    custcat = db.CustCats.Where(c => c.CustomerId == customer.Id).FirstOrDefault();
                    custcategory = db.CustCategories.Where(cat => cat.Id == custcat.CustCategoryId).FirstOrDefault();

                }
                catch (Exception ex)
                {
                    custcategory = new CustCategory
                    {
                        Id = 0,
                        Name = "Not Assigned",
                        iconPath = "http://localhost:50382/Images/Customers/Category/unavailable-40.png"
                    };
                }

                try
                {
                    companyEntity = db.CustEntities.Where(ce => ce.CustomerId == customer.Id).FirstOrDefault();
                    company = db.CustEntMains.Where(co => co.Id == companyEntity.CustEntMainId).FirstOrDefault();

                }
                catch (Exception ex)
                {
                    company = new CustEntMain
                    {
                        Id = 0,
                        Name = "Not Assigned",
                        Address = "none",
                        Contact1 = "0",
                        Contact2 = "0",
                        iconPath = "http://localhost:50382/Images/Customers/Category/unavailable-40.png"
                    };
                }


                customerDetailList.Add(new CustomerDetails
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    Contact1 = customer.Contact1,
                    Contact2 = customer.Contact2,
                    Remarks = customer.Remarks,
                    Status = customer.Status,
                    JobID = customer.JobMains.Count(),
                    CustCategoryID = custcategory.Id,
                    CustCategoryIcon = custcategory.iconPath,
                    CustEntID = company.Id,
                    CustEntName = company.Name,
                    CustEntIconPath = company.iconPath,
                    categories = getCategoriesList(customer.Id),
                    companies = getCompanyList(customer.Id)

                    // JobID = db.JobMains.Where(jm => jm.CustomerId.Equals(customer.Id)).FirstOrDefault() == null ? 0 : db.JobMains.Where(jm => jm.CustomerId.Equals(customer.Id)).FirstOrDefault().Id,

                    //end
                });

            }


            //return View(db.Customers.ToList());
            return View(customerDetailList);


        }

        private List<CustCategory> getCategoriesList(int id) {

            //PartialView for Details of the Customer
            List<CustCategory> categoryDetails = new List<CustCategory>();

            //error
            var categoryList = db.CustCats.Where(c => c.CustomerId == id).ToList();

            if (categoryList == null)
            {

                categoryDetails.Add(new CustCategory
                {
                    Id = 0,
                    iconPath = "http://localhost:50382/Images/Customers/Category/unavailable-40.png",
                    Name = "not assigned"
                });

            }
            else
            {

                foreach (var cat in categoryList)
                {
                    categoryDetails.Add(new CustCategory
                    {
                        Id = cat.CustCategory.Id,
                        iconPath = cat.CustCategory.iconPath,
                        Name = cat.CustCategory.Name

                    });
                }
            }

            return categoryDetails;
        }


        private List<CustEntMain> getCompanyList(int id)
        {

            //PartialView for Details of the Customer
            List<CustEntMain> CompanyList = new List<CustEntMain>();
            //error
            var CompanyRecord = db.CustEntities.Where(c => c.CustomerId == id).ToList();

            if (CompanyRecord == null)
            {

                CompanyList.Add(new CustEntMain
                {
                    Id = 0,
                    Name = "None",
                    Address = "None",
                    Contact1 = "None",
                    Contact2 = "None",
                    iconPath = "None"
                });

            }
            else
            {

                foreach (var record in CompanyRecord)
                {
                    CompanyList.Add(new CustEntMain
                    {
                        Id = record.CustEntMain.Id,
                        Name = record.CustEntMain.Name,
                        Address = record.CustEntMain.Address,
                        Contact1 = record.CustEntMain.Contact1,
                        Contact2 = record.CustEntMain.Contact2,
                        iconPath = record.CustEntMain.iconPath
                    });

                }

            }

            ViewBag.companyList = CompanyList;
            ViewBag.CustomerID = id;


            return CompanyList;
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            //generate partial view list for companies
            PartialView_Companies(id);
            PartialView_Jobs(id);
            PartialView_Categories(id);
            PartialView_CustomerFiles(id);

            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.Status = new SelectList(StatusList, "value", "text");

            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Contact1,Contact2,Remarks,Status")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.Status == null || customer.Status.Trim() == "") customer.Status = "ACT";

                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            ViewBag.Status = new SelectList(StatusList, "value", "text", customer.Status);

            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Contact1,Contact2,Remarks,Status")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Status = new SelectList(StatusList, "value", "text", customer.Status);

            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PartialView_Companies(int? id)
        {

            //PartialView for Details of the Customer
            List<CustEntMain> CompanyList = new List<CustEntMain>();
            //error
            var CompanyRecord = db.CustEntities.Where(c => c.CustomerId == id).ToList();

            if (CompanyRecord == null)
            {

                CompanyList.Add(new CustEntMain
                {
                    Id = 0,
                    Name = "None",
                    Address = "None",
                    Contact1 = "None",
                    Contact2 = "None",
                    iconPath = "None"
                });

            }
            else
            {

                foreach (var record in CompanyRecord)
                {
                    CompanyList.Add(new CustEntMain
                    {
                        Id = record.CustEntMain.Id,
                        Name = record.CustEntMain.Name,
                        Address = record.CustEntMain.Address,
                        Contact1 = record.CustEntMain.Contact1,
                        Contact2 = record.CustEntMain.Contact2,
                        iconPath = record.CustEntMain.iconPath
                    });

                }

            }
            List<CustEntMain> List = new List<CustEntMain>();
            List = db.CustEntMains.ToList();
            ViewBag.companies = List;
            
            ViewBag.companyList = CompanyList;
            ViewBag.CustomerID = id;

        }


        private void PartialView_Jobs(int? id)
        {

            //PartialView for Details of the Customer
            List<CustomerJobDetails> jobList = new List<CustomerJobDetails>();
            //error
            var jobRecord = db.JobMains.Where(j => j.CustomerId == id).ToList();

            if (jobRecord == null)
            {

                jobList.Add(new CustomerJobDetails
                {
                    Id = 0,
                    JobDate = "7/24/2018",
                    Description = "none",
                    AgreedAmt = "0",
                    NoOfDays = "0",
                    NoOfPax = "0",
                    StatusRemarks = "none"
                });

            }
            else
            {

                foreach (var record in jobRecord)
                {
                    jobList.Add(new CustomerJobDetails
                    {

                        Id = record.Id,
                        JobDate = record.JobDate.ToString(),
                        Description = record.Description,
                        AgreedAmt = record.AgreedAmt.ToString(),
                        NoOfDays = record.NoOfDays.ToString(),
                        NoOfPax = record.NoOfPax.ToString(),
                        StatusRemarks = record.JobStatus.Status

                    });

                }

            }

            ViewBag.jobList = jobList;

        }


        private void PartialView_Categories(int? id)
        {

            //PartialView for Details of the Customer
            List<CustCategory> categoryDetails = new List<CustCategory>();

            //error
            var categoryList = db.CustCats.Where(c => c.CustomerId == id).ToList();

            if (categoryList == null)
            {

                categoryDetails.Add(new CustCategory
                {
                    Id = 0,
                    iconPath = "/Images/Customers/Category/unavailable-40.png",
                    Name = "not assigned"
                });

            }
            else
            {

                foreach (var category in categoryList)
                {
                    categoryDetails.Add(new CustCategory
                    {
                        Id = category.CustCategory.Id,
                        iconPath = category.CustCategory.iconPath,
                        Name = category.CustCategory.Name

                    });
                }
            }

            ViewBag.categoryDetails = categoryDetails;

        }

        private void PartialView_CustomerFiles(int? id)
        {
            
            //PartialView for Details of the Customer
           List<CustFiles> FilesList = new List<CustFiles>();

            //error
            var customerFiles = db.CustFiles.Where(c => c.CustomerId == id).ToList();

            if (customerFiles == null)
            {

                FilesList.Add(new CustFiles
                {
                    Id = 0,
                    CustomerId = 0,
                    Path = "none",
                    Desc = "none",
                    Folder = "none",
                    Remarks = "",

                });

            }
            else
            {
                foreach (var file in customerFiles)
                {
                    FilesList.Add(new CustFiles
                    {
                        Id = file.Id,
                        CustomerId = file.CustomerId,
                        Path = file.Path,
                        Desc = file.Desc,
                        Folder = file.Folder,
                        Remarks = file.Remarks,

                    });
                }
            }
            ViewBag.fileList = FilesList;
            
        }

    }
}
