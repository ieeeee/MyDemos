using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi
{
    public class ContactsController : ApiController
    {
        static List<Contact> contacts;
        static ContactsController()
        {
            contacts = new List<Contact>();

            contacts.Add(new Contact
            {
                Id = "001",
                Name = "张三",
                PhoneNo = "0512-12345678",
                EmailAddress = "zhanqsan@gmail.com",
                Address = "江苏省苏州市星湖街328号"
            });

            contacts.Add(new Contact
            {
                Id = "002",
                Name = "李四",
                PhoneNo = "0512-23迢56789",
                EmailAddress = "lis@gmail.com",
                Address = "江苏省苏州市金鸡湖欠道328号"
            });
        }

        public IEnumerable<Contact> Get(string id = null)
        {
            return from contact in contacts
                   where contact.Id == id || string.IsNullOrEmpty(id)
                   select contact;
        }

        public void Post(Contact contact)
        {
            contact.Id = Convert.ToString("D3");
            contacts.Add(contact);
        }

        public void Put(Contact contact)
        {
            contacts.Remove(contacts.First(c => c.Id == contact.Id));
            contacts.Add(contact);
        }

        public void Delete(string Id)
        {
            contacts.Remove(contacts.First(c => c.Id == Id));
        }
    }
}