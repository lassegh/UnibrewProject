using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Newtonsoft.Json;
using UnibrewProject.Model;

namespace UnibrewProject.Model
{
    /// <summary>
    /// Generisk klasse, der håndterer al kommunikation til REST
    /// </summary>
    public class DbComGeneric
    {
        private static DbComGeneric _comGeneric = null;

        private DbComGeneric()
        {
            
        }

        private string GetUrl(Type obj) // Danner URL - ændrer URI afhængig af typen af objekt
        {
            string url = "https://quayzer.azurewebsites.net/api/"; //

            if (obj == typeof(TapOperator))
            {
                url = url + "tapoperators";
            }
            else if (obj == typeof(ProcessingItems))
            {
                url =  url + "processingitems";
            }
            else if (obj == typeof(LiquidTanks))
            {
                url = url + "liquidtanks";
            }
            else if (obj == typeof(FinishedItems))
            {
                url = url + "finisheditems";
            }

            return url;
        }

        /// <summary>
        /// Henter alle data fra tabellen
        /// </summary>
        /// <returns>Liste af objekter</returns>
        public List<T> GetAll<T>()
        {
            List<T> objectList = new List<T>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    Task<string> resTask = client.GetStringAsync(GetUrl(typeof(T)));
                    String jsonStr = resTask.Result;

                    objectList = JsonConvert.DeserializeObject<List<T>>(jsonStr);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Kunne ikke hentes -kontroller forbindelse til server");
                    Console.WriteLine(e);
                }

            }


            return objectList;
        }

        /// <summary>
        /// Henter ét bestemt objekt fra tabellen
        /// </summary>
        /// <typeparam name="T">Type af objekt</typeparam>
        /// <typeparam name="TId">Type af Id</typeparam>
        /// <param name="id">ID på objekt</param>
        /// <returns>Objekt af generisk/ønsket type</returns>
        public T GetOne<T, TId>(TId id)
        {
            T[] obj = new T[1];

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    Task<string> resTask = client.GetStringAsync(GetUrl(typeof(T)) + "/" + id);
                    String jsonStr = resTask.Result;

                    obj[0] = JsonConvert.DeserializeObject<T>(jsonStr);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Kunne ikke hentes -kontroller forbindelse til server");
                    Console.WriteLine(e);
                }

            }

            return obj[0];
        }

        /// <summary>
        /// Poster et NYT objekt til tabellen
        /// </summary>
        /// <typeparam name="T">Type på objekt</typeparam>
        /// <param name="obj">Objekt, der skal postes</param>
        /// <returns>successful bool</returns>
        public bool Post<T>(T obj)
        {
            bool ok = false;

            using (HttpClient client = new HttpClient())
            {
                String jsonStr = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(jsonStr, Encoding.ASCII, "application/json");

                try
                {
                    Task<HttpResponseMessage> postAsync = client.PostAsync(GetUrl(typeof(T)), content);

                    HttpResponseMessage resp = postAsync.Result;
                    if (resp.IsSuccessStatusCode)
                    {
                        ok = true;

                        if (obj is TapOperator) // Hvis det er tapOperator der skriver, skal ID'et gemmes.
                        {
                            Task<TapOperator> jsonStrings = resp.Content.ReadAsAsync<TapOperator>(); //right!
                            TapOperatorId = jsonStrings.Result.ID;
                        }
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

        /// <summary>
        /// Opdaterer et eksisterende objekt i tabellen
        /// </summary>
        /// <typeparam name="T">Type af objekt</typeparam>
        /// <typeparam name="TId">Type af ID</typeparam>
        /// <param name="id">ID på objekt</param>
        /// <param name="obj">Objektet, der skal opdateres</param>
        /// <returns>Successful bool</returns>
        public bool Put<TId, T>(TId id, T obj)
        {
            bool ok = false;

            using (HttpClient client = new HttpClient())
            {
                String jsonStr = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                try
                {
                    /*if (id is string)
                    {
                        
                    }*/
                    Task<HttpResponseMessage> putAsync = client.PutAsync(GetUrl(typeof(T)) + "/" + id, content);

                    HttpResponseMessage resp = putAsync.Result;
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

        /// <summary>
        /// Sletter et bestemt objekt i tabellen
        /// </summary>
        /// <typeparam name="T">Typen af objekt</typeparam>
        /// <param name="id">ID på objekt</param>
        /// <returns>Successful bool</returns>
        public bool Delete<T>(int id)
        {
            bool ok = false;

            using (HttpClient client = new HttpClient())
            {

                try
                {
                    Task<HttpResponseMessage> deleteAsync = client.DeleteAsync(GetUrl(typeof(T)) + "/" + id);

                    HttpResponseMessage resp = deleteAsync.Result;
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


        public static DbComGeneric ComGeneric
        {
            get
            {
                if (_comGeneric == null)
                {
                    _comGeneric = new DbComGeneric();
                }
                return _comGeneric;
            }
        }

        public int TapOperatorId { get; set; }

    }
}
