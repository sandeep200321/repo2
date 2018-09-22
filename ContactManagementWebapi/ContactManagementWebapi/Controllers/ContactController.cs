using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ContactManagementWebapi.Models;

namespace ContactManagementWebapi.Controllers
{
    public class ContactController : ApiController
    {
        #region Private Members
        private readonly IContact _repository;
        #endregion

        #region Constructor

        public ContactController(IContact contactRepository) => _repository = contactRepository;

        public ContactController()
        {
            _repository = new ContactRepository();
        }
        #endregion

        #region Public Methods
        // GET api/values
        [HttpGet]
        [Route("api/contact/GetAllContacts")]
        public IEnumerable<Contact> Get()
        {
            return _repository.GetContacts();
        }

        // GET api/values/5
        [HttpGet]
        [Route("api/contact/GetContactById")]
        public Contact GetContactById(int id)
        {
            Contact contact = _repository.GetContact(id);

            if (contact == null)
            {
                //return contact;
                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return contact;
            }
        }

        // POST api/values

        public HttpResponseMessage Post(Contact contact)
        {
            if (ModelState.IsValid)
            {
                // this will set the ID for instance...
                contact = _repository.AddContact(contact);

                var response = this.Request.CreateResponse<Contact>(HttpStatusCode.Created, contact);

                // string uri = Url.Link("DefaultApi", new { id = contact.Id });
                // response.Headers.Location = new System.Uri(uri);
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT api/product/5
        public HttpResponseMessage Put(Contact contact)
        {
            if (ModelState.IsValid)
            {
                var result = _repository.Update(contact);
                if (result == false)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            Contact product = _repository.GetContact(id);

            if (product == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            try
            {
                _repository.Delete(id);
            }
            catch (System.Exception)
            {
                return Request.CreateResponse("error while deleting");
            }

            return Request.CreateResponse(HttpStatusCode.OK, product);
        }
    }
    #endregion
}