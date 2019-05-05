using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnibrewProject.Model;

namespace UnibrewProject.ViewModel.HelperClasses.SaveClasses
{
    public static class DbCommunication
    {
        private static string _restUrl = "https://ubrest.azurewebsites.net/api/testmoments";

        public static bool Post(TESTmoment momenter)
        {
            bool connectionOk = false;
            
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (HttpClient client = new HttpClient(handler))
            {
                String jsonStr = JsonConvert.SerializeObject(momenter);
                StringContent content = new StringContent(jsonStr, Encoding.ASCII, "application/json");

                try
                {
                    Task<HttpResponseMessage> postAsync = client.PostAsync(_restUrl, content);
                    
                    HttpResponseMessage resp = postAsync.Result;
                    if (resp.IsSuccessStatusCode)
                    {
                        //Fetch object.id
                        connectionOk = true;
                        //jsonString = resp.Headers.Location;
                        Task<TESTmoment> jsonStrings = resp.Content.ReadAsAsync<TESTmoment>(); //right!
                        MomentID = jsonStrings.Result.Id; //JsonConvert.DeserializeObject<TESTmoment>(jsonStrings).Id;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);

                }
            }

            return connectionOk;
        }

        public static bool Put(TESTmoment tMoment, int id)
        {
            bool ok = true;

            using (HttpClient client = new HttpClient())
            {
                String jsonStr = JsonConvert.SerializeObject(tMoment);
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                try
                {
                    Task<HttpResponseMessage> putAsync = client.PutAsync(_restUrl + "/" + id, content);

                    HttpResponseMessage resp = putAsync.Result;
                }
                catch (Exception e)
                {
                    ok = false;
                    Console.WriteLine(e);
                }
            }


            return ok;
        }

        public static int MomentID { get; set; }

    }
}
