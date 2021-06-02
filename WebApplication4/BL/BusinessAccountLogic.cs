using GenieMistro.Models;
using GenieMistro.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GenieMistro.BL
{
    public class BusinessAccountLogic
    {
        private readonly genieDBContext _context;

        public BusinessAccountLogic(genieDBContext context)
        {
            _context = context;
        }

        // check if account Exist
        public bool BusinessAccountExists(int id)
        {
                var AccountExist = _context.BusinessAccount.Any(e => e.Id == id);
                if (AccountExist == false)
                {
                    return false;
                }
                return true; 
        }

        // Delete Account
        public async Task<bool> DeleteBusinessAccount(int id)
        {
                var businessAccount = await _context.BusinessAccount.FindAsync(id);
                if (businessAccount == null)
                {
                    return false;
                }
                _context.BusinessAccount.Remove(businessAccount);
                await _context.SaveChangesAsync();
                return true;

        }

        // get all Accounts 
        public async Task<List<BusinessAccount>> Get()
        {
                var Accounts = await _context.BusinessAccount.ToListAsync();
                return Accounts;
        }

        // Get BusinessAccount match email and password
        public async Task<BusinessAccount> GetBusinessAccount(string email, string password)
        {
            MailAddress address = new System.Net.Mail.MailAddress(email);

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
            }

            List<BusinessAccount> bs = _context.BusinessAccount.ToList();
            BusinessAccount businessAccount = new BusinessAccount();
            foreach (BusinessAccount b in bs)
            {
                if (b.Email.Trim() == email.Trim() && b.BaPassword.Trim() == password.Trim())
                {
                    businessAccount = b;
                    break;
                }
            }
            String response = "Accepted Username and password you will connected on database:" + Db.Name;

            return businessAccount;

        }

        // Post Business Account 
        public async Task<BusinessAccount> PostBusinessAccount(BusinessAccount businessAccount)
        {
            landLoard ld = new landLoard();
            MailAddress address = new System.Net.Mail.MailAddress(businessAccount.Email);
            string host = address.Host;
            ld.Domain = host;
            ld.Name = businessAccount.CompanyName + "DB";

            await _context.BusinessAccount.AddAsync(businessAccount);
            await _context.SaveChangesAsync();
            postLandLord(ld);
            return businessAccount;
        }
        // post LandLord
        public async Task<bool> postLandLord(landLoard landLoard)
        {
            // throw new NotImplementedException();
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
            }
            return true;
        }


        // update businessAccount
        public async Task<bool> PutBusinessAccount(int id, BusinessAccount businessAccount)
        {
            //_context.Entry(businessAccount).State = EntityState.Modified;
            _context.BusinessAccount.Update(businessAccount);
            await _context.SaveChangesAsync();

            return true;
        }
    
    }
}
