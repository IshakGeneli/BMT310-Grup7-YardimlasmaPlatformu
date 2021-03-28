using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using xHelp.Business.Abstract;
using xHelp.DataAccess.Abstract;
using xHelp.Entity.Concrete;

namespace xHelp.Business.Concrete
{
    public class ContactManager : IContactService
    {
        private readonly IContactDal _contactDal;

        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }

        public async Task<Contact> AddContactAsync(Contact contact)
        {
            return await _contactDal.AddAsync(contact);
        }

        public async Task DeleteContactAsync(int id)
        {
            await _contactDal.DeleteAsync(new Contact { Id = id });
        }

        public async Task<ICollection<Contact>> GetAllAsync()
        {
            return await _contactDal.GetListAsync();
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            return await _contactDal.GetAsync(m => m.Id == id);
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            await _contactDal.UpdateAsync(contact);
        }
    }
}
