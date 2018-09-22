using System.Collections.Generic;

namespace ContactManagementWebapi.Models
{
    public interface IContact
    {
        Contact GetContact(int id);
        IEnumerable<Contact> GetContacts();
        Contact AddContact(Contact c);
        void Delete(int id);
        bool Update(Contact contact);

    }
}
