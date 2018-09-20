using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
