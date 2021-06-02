using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace GenieMistro.Services
{
    public class landLoard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string ConnectionString { get; set; }


        public String GetDatabase(String Email)
        {
            landLoard landLoardDB = null;
            MailAddress address = new MailAddress(Email);
            string host = address.Host;
            IEnumerable<landLoard> Dbs = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64189/api/");
                //HTTP GET
                var responseTask = client.GetAsync("GetTbLandLoards");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<landLoard>>();
                    readTask.Wait();

                    Dbs = readTask.Result;
                }
               
                foreach (landLoard item in Dbs)
                {
                    if (item.Domain == host)
                    {
                        landLoardDB = item;
                        break;
                    }
                }

            }
            
            return landLoardDB.Name;
        }
        public async Task<string> GetConnectionStringIDAsync(String Email)
        {
            MailAddress address = new System.Net.Mail.MailAddress(Email);

            string host = address.Host;

            string Baseurl = "https://localhost:44336/";
            landLoard Db = new landLoard();
            var client = new HttpClient();

            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage Res = await client.GetAsync("api/TbLandLoards/GetTbLandLoard?domain=" + host.ToString());

            if (Res.IsSuccessStatusCode)
            {
                var Response = Res.Content.ReadAsStringAsync().Result;
                Db = JsonConvert.DeserializeObject<landLoard>(Response);
                String response = Db.Id.ToString();
                return response;
            }



           
           
            return null;
        }
        public async Task<landLoard> CreateDbConnectionString(landLoard landLoard )
        {
            landLoard ls = new landLoard();
            landLoard landLoard1 = new landLoard();
            //  landLoard.Domain = "mohamed.com";
            //  landLoard.Name = "mohamdDb";


            string Baseurl = "https://localhost:44336/";

            var client = new HttpClient();

            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //  HttpResponseMessage Res = await client.GetAsync("/api/TbLandLoards/GetTbLandLoards");
            var Res = client.PostAsJsonAsync<landLoard>("api/TbLandLoards/PostTbLandLoard", landLoard);
            Res.Wait();
            var results = Res.Result;




            if (results.IsSuccessStatusCode)
            {
                var EmpResponse = results.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<landLoard>(EmpResponse);
                return ls;
            }
            return null;
        }



    }
}
