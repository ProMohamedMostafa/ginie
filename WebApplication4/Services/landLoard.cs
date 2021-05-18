using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

    }
}
