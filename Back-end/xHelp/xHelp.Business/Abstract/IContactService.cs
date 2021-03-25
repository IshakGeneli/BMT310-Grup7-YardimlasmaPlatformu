using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Entity.Concrete;

namespace xHelp.Business.Abstract
{
    public interface IContactService
    {
        Task<ICollection<Contact>> GetAllAsync();
        Task<Contact> GetContactByIdAsync(int id);
        Task<Contact> AddContactAsync(Contact contact);
        Task UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(int id);
    }
}
