using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnibrewProject.Model;

namespace UnibrewProject.ViewModel.HelperClasses.DbCommunication
{
    public class DbComFinishedItems
    {
        private static DbComFinishedItems _comFinishedItems;
        private static readonly string _restUrl = "https://quayzer.azurewebsites.net/api/finisheditems";

        private DbComFinishedItems()
        {
            FinishedItemsList = GetAll();
        }

        private List<FinishedItems> GetAll()
        {
            List<FinishedItems> finishedItemsList = new List<FinishedItems>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    Task<string> resTask = client.GetStringAsync(_restUrl);
                    String jsonStr = resTask.Result;

                    finishedItemsList = JsonConvert.DeserializeObject<List<FinishedItems>>(jsonStr);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Kunne ikke hentes -kontroller forbindelse til server");
                    Console.WriteLine(e);
                }

            }


            return finishedItemsList;
        }

        public List<FinishedItems> FinishedItemsList { get; set; }
        public static DbComFinishedItems ComFinishedItems
        {
            get
            {
                if (_comFinishedItems == null)
                {
                    _comFinishedItems = new DbComFinishedItems();
                }

                return _comFinishedItems;

            }
            
        }
    }
}
