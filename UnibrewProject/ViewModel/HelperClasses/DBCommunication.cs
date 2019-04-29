﻿using System;
using System.Collections.Generic;
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
        private static string _restUrl = "https://ubrest.azurewebsites.net/";

        public static bool Post(TESTmoment momenter)
        {
            bool ok = false;

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
                        ok = true;
                    }
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
