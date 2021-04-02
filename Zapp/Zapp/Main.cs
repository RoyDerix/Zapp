
using System;
using System.Net;
using System.Text;
using System.IO;
using System.Text.Json;
using Zapp.Models;


using Nancy.Json;
namespace Zapp
{
    public class Main
    {
        /**
         * Generic: fetchData
         * Return: (unserialized) JSON String
         */
        private string fetchData(string url)
        {
            string result;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Config.CreateUrl(url));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

            result = readStream.ReadToEnd();

            response.Close();
            readStream.Close();

            return (result);

        }

        /**
         * Generic: putData
         * Return: (unserialized) JSON String
         */
        private string putData(string url, string json)
        {
            string result;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Config.CreateUrl(url));
            request.ContentType = "application/json";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
            }


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return (result);
        }


        /// Specifics

        public Klant createKlant(KlantPost klant)
        {
            string json = JsonSerializer.Serialize(klant);
            string result = putData(Config.saveKlanten, json);
            Klant newKlant = new JavaScriptSerializer().Deserialize<Klant>(result);

            return (newKlant);
        }

        public KlantenLijst getKlanten()
        {
            string result = fetchData(Config.getKlanten);
            var allKlanten = new JavaScriptSerializer().Deserialize<KlantenLijst>(result);

            return (allKlanten);
        }

        public async void saveDbKlanten()
        {
            var klantenlijst = getKlanten();

            var alleKlanten = klantenlijst.entries;

            foreach (var klant in alleKlanten)
            {
                try
                {
                    await App.Database.SaveKlant(klant);
                }
                catch
                {
                    await App.Database.UpdateKlant(klant);
                }
            }
        }

    }
}
