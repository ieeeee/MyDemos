using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WF_Entity;

namespace APP
{
    public partial class Default : System.Web.UI.Page
    {
        protected readonly string services_host = "http://10.16.77.54/selfhost/";

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage();
        }

        private async void InitPage()
        {
            //获取当前联系人列表
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(services_host + "api/Contacts");
            IEnumerable<Contact> Contacts = await response.Content.ReadAsAsync<IEnumerable<Contact>>();
            ListContacts(Contacts, GridView1);

            //获取当前联系人列表
            HttpClient httpClient_WebHost = new HttpClient();
            HttpResponseMessage response_WebHost = await httpClient_WebHost.GetAsync("http://localhost/webhost/api/Contacts");
            IEnumerable<Contact> Contacts_WebHost = await response_WebHost.Content.ReadAsAsync<IEnumerable<Contact>>();
            ListContacts(Contacts_WebHost, GridView2);

            //添加新的联系人
            //Contact Contact = new Contact { Name = "王五", PhoneNo = "0512-34567890", EmailAddress = "wangwu@gmail.com" };
            //await httpClient.PostAsJsonAsync<Contact>(services_host + "api/Contacts", Contact);
            //Console.WriteLine("添加新联系人“王五”：");
            //response = await httpClient.GetAsync(services_host + "api/Contacts");
            //Contacts = await response.Content.ReadAsAsync<IEnumerable<Contact>>();
            //ListContacts(Contacts);

            //修改现有的某个联系人
            //response = await httpClient.GetAsync(services_host + "api/Contacts/001");
            //Contact = (await response.Content.ReadAsAsync<IEnumerable<Contact>>()).First();
            //Contact.Name = "赵六";
            //Contact.EmailAddress = "zhaoliu@gmail.com";
            //await httpClient.PutAsJsonAsync<Contact>(services_host + "api/Contacts/001", Contact);
            //Console.WriteLine("修改联系人“001”信息：");
            //response = await httpClient.GetAsync(services_host + "api/Contacts");
            //Contacts = await response.Content.ReadAsAsync<IEnumerable<Contact>>();
            //ListContacts(Contacts);

            //删除现有的某个联系人
            //await httpClient.DeleteAsync(services_host + "api/Contacts/002");
            //Console.WriteLine("删除联系人“002”：");
            //response = await httpClient.GetAsync(services_host + "api/Contacts");
            //Contacts = await response.Content.ReadAsAsync<IEnumerable<Contact>>();
            //ListContacts(Contacts);
        }
        private void ListContacts(IEnumerable<Contact> Contacts, GridView gridView)
        {
            gridView.DataSource = Contacts;
            gridView.DataBind();
            //foreach (Contact Contact in Contacts)
            //{
            //    Console.WriteLine("{0,-6}{1,-6}{2,-20}{3,-10}", Contact.Id, Contact.UAge, Contact.UName, Contact.UAddress);
            //}
            //Console.WriteLine();
        }
    }
}