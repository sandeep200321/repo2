using System.Collections.Generic;
using System.Linq;

namespace ContactManagementWebapi.Models
{
    public class ContactRepository : IContact
    {
        #region Private Members
        private static readonly List<Contact> Contacts = new List<Contact>();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        static ContactRepository()
        {
            Contacts.Add(new Contact() { Id = 1, FirstName = "Amit", LastName = "Rastogi", Email = "a@a.com", Status = "Active", PhoneNumber = "5665678909" });
            Contacts.Add(new Contact() { Id = 2, FirstName = "Bhavan", LastName = "Shah", Email = "b@b.com", Status = "InActive", PhoneNumber = "5665678980" });
            Contacts.Add(new Contact() { Id = 3, FirstName = "Charan", LastName = "Singh", Email = "c@c.com", Status = "Active", PhoneNumber = "5665678988" });
        }
        #endregion

        #region Public Methods
        public Contact GetContact(int id)
        {
            var product = Contacts.Find(p => p.Id == id);
            return product;
        }

        public IEnumerable<Contact> GetContacts() => Contacts;

        public Contact AddContact(Contact c)
        {
            Contacts.Add(c);
            return c;
        }

        public void Delete(int id)
        {
            var Contact = Contacts.FirstOrDefault(p => p.Id == id);
            if (Contact != null)
            {
                Contacts.Remove(Contact);
            }
        }

        public bool Update(Contact contact)
        {

            Contact contactToUpdate = Contacts.Where(ContactIid => ContactIid.Id == contact.Id).SingleOrDefault();
            if (contact != null)
            {
                contactToUpdate.FirstName = contact.FirstName;
                contactToUpdate.LastName = contact.LastName;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Status = contact.Status;
                contactToUpdate.PhoneNumber = contact.PhoneNumber;
                return true;
            }

            return false;
        }
    }
    #endregion
}