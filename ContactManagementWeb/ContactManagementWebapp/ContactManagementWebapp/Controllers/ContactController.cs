using ContactManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace ContactManagementWebapp.Controllers
{
    public class ContactController : Controller
    {
        #region Public Methods

        // GET: Contact
        /// <summary>
        /// Get All Contacts
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            IEnumerable<ContactViewModel> contacts = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:51232/api/Contact/GetAllContacts/");
                    //HTTP GET
                    var responseTask = client.GetAsync(client.BaseAddress);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<ContactViewModel>>();
                        readTask.Wait();

                        contacts = readTask.Result;
                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        contacts = Enumerable.Empty<ContactViewModel>();

                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                }
            }

            catch (Exception)
            {
                contacts = Enumerable.Empty<ContactViewModel>();

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                return View(contacts);
            }

            

            return View(contacts);
        }

        /// <summary>
        /// Create Contact
        /// </summary>
        /// <returns></returns>
        public ActionResult create()
        {

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Active", Value = "0" });

            items.Add(new SelectListItem { Text = "Inactive", Value = "1" });

            var contactModel = new ContactViewModel() { ListItems = items, SelectedItem = 1 };

            return View(contactModel);
        }

        /// <summary>
        /// Create Contacts
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult create(ContactViewModel contact)
        {
            SetStatus(contact);

            try
            {
                if (ModelState.IsValid)
                {
                    Random random = new Random();
                    contact.Id = random.Next();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:51232/api/contact/");

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<ContactViewModel>("post", contact);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            TempData["SuccessMessage"] = " Contact Saved Successfully";
                            return RedirectToAction("Index");
                        }
                    }

                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                }
            }
            catch (Exception)
            {
                var contacts = new ContactViewModel();

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                return View(contacts);

            }

            return View(contact);

        }


        /// <summary>
        /// Edit contacts by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            ContactViewModel contact = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:51232/api/Contact/GetContactById/");

                    //HTTP GET
                    var responseTask = client.GetAsync("?id=" + id.ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        var readTask = result.Content.ReadAsAsync<ContactViewModel>();
                        readTask.Wait();

                        contact = readTask.Result;
                    }
                }

            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

                var contacts = new ContactViewModel();
                return View(contacts);
            }
            BindModel(contact);

            return View(contact);
        }

        /// <summary>
        /// Edit Contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(ContactViewModel contact)
        {
            SetStatus(contact);

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:51232/api/contact/");

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<ContactViewModel>("put", contact);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = " Contact Updated Successfully";

                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                var contacts = new ContactViewModel();
                return View(contacts);
            }
            return View(contact);
        }

        /// <summary>
        /// Delete contacts by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:51232/api/contact/");

                    //HTTP DELETE
                    var deleteTask = client.DeleteAsync("delete?id=" + id.ToString());
                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = " Contact Deleted Successfully";
                        return RedirectToAction("Index");
                    }
                }
            }

            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                var contacts = new ContactViewModel();
                return View(contacts);
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Private Methods
    

        private static void SetStatus(ContactViewModel contact)
        {
            if (contact.SelectedItem == 0)
            {
                contact.Status = "Active";
            }
            else
            {
                contact.Status = "InActive";
            }
        }

       

        private static void BindModel(ContactViewModel contact)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Active", Value = "0" });

            items.Add(new SelectListItem { Text = "Inactive", Value = "1" });

            contact.ListItems = items;

            if (contact.Status.Equals("Active"))
            {
                contact.SelectedItem = 0;
            }
            else
            {
                contact.SelectedItem = 1;
            }
        }


       
        #endregion
    }
}