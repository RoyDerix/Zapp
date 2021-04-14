
using System;
using System.Net;
using System.Text;
using System.IO;
using System.Text.Json;
using Zapp.Models;


using Nancy.Json;
using System.Threading.Tasks;

namespace Zapp
{
    public class ApiService
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

        public User authUser(AuthUser authUser)
        {
            string json = JsonSerializer.Serialize(authUser);
            string result = putData(Config.authUser, json);
            User user = new JavaScriptSerializer().Deserialize<User>(result);

            return (user);
        }

        public Opdracht createOpdracht(OpdrachtPost opdracht)
        {
            string json = JsonSerializer.Serialize(opdracht);
            string result = putData(Config.saveOpdrachten, json);
            Opdracht newOpdracht = new JavaScriptSerializer().Deserialize<Opdracht>(result);

            return (newOpdracht);
        }

        public OpdrachtenLijst getApiOpdrachten()
        {
            string result = fetchData(Config.getOpdrachten);
            var opdrachtenlijst = new JavaScriptSerializer().Deserialize<OpdrachtenLijst>(result);

            return (opdrachtenlijst);
        }

        public Klant createKlant(KlantPost klant)
        {
            string json = JsonSerializer.Serialize(klant);
            string result = putData(Config.saveKlanten, json);
            Klant newKlant = new JavaScriptSerializer().Deserialize<Klant>(result);

            return (newKlant);
        }

        public KlantenLijst getApiKlanten()
        {
            string result = fetchData(Config.getKlanten);
            var klantenlijst = new JavaScriptSerializer().Deserialize<KlantenLijst>(result);

            return (klantenlijst);
        }

        public Taak createTaak(TaakPost taak)
        {
            string json = JsonSerializer.Serialize(taak);
            string result = putData(Config.saveTaken, json);
            Taak newTaak = new JavaScriptSerializer().Deserialize<Taak>(result);

            return (newTaak);
        }

        public TakenLijst getApiTaken()
        {
            string result = fetchData(Config.getTaken);
            var allTaken = new JavaScriptSerializer().Deserialize<TakenLijst>(result);

            return (allTaken);
        }

    }
}
