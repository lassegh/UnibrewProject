﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnibrewProject.Model;

namespace UnibrewProject.ViewModel.HelperClasses
{
    public static class DbCommunication
    {
        private static string _restUrl = "https://ubrest.azurewebsites.net/api/testmoments";

        public static int Post(TESTmoment momenter)
        {
            int id = -1;

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
                        //Fetch object
                        String jsonString = postAsync.Result.Content.ToString();
                        id = JsonConvert.DeserializeObject<TESTmoment>(jsonString).Id;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);

                }
            }

            return id;
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

    }
}
