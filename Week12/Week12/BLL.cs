using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week12
{
    interface DataAccessLayer
    {
        ContactDTO GetContactById(string id);
        string CreateContact(ContactDTO contact);
        bool DeleteContactById(string id);
        bool UpdateContact(ContactDTO contact);
        List<ContactDTO> GetContactsByName(string name);
        List<ContactDTO> GetAllContacts();
        List<ContactDTO> GetAllContactsInPage(int offset, string name);
    }
    public abstract class BaseContact
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
    public class CreateContactCommand : BaseContact
    {

    }
    public class ContactDTO : BaseContact
    {
        public string Id { get; set; }
    }
    class BLL
    {
        DataAccessLayer dal = default(DataAccessLayer);
        public BLL(DataAccessLayer dal)
        {
            this.dal = dal;
        }
        public ContactDTO GetContactById(string id)
        {
            return dal.GetContactById(id);
        }
        public List<ContactDTO> GetContactsByName(string name)
        {
            return dal.GetContactsByName(name);
        }
        public bool UpdateContact(ContactDTO contact)
        {
            return dal.UpdateContact(contact);
        }
        public string CreateContact(CreateContactCommand contact)
        {
            ContactDTO contact1 = new ContactDTO();
            contact1.Id = Guid.NewGuid().ToString();
            contact1.Name = contact.Name;
            contact1.Address = contact.Address;
            contact1.Phone = contact.Phone;
            return dal.CreateContact(contact1);
        }
        public bool RemoveContact(string id)
        {
            return dal.DeleteContactById(id);
        }
        public List<ContactDTO> GetContacts()
        {
            return dal.GetAllContacts();
        }
        public List<ContactDTO> GetContactsInPage(int offset, string name)
        {
            return dal.GetAllContactsInPage(offset, name);
        }
    }
}
