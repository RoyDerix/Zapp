using System;


namespace Zapp
{
    public static class Config
    {

        public static string baseUrl = "https://cockpit.educom.nu/api/collections";
        public static string token = "bd216b5098452677789be90f7c4f4f";

        public static string getOpdrachten = "/get/royZapp";
        public static string saveOpdrachten = "/save/royZapp";
        public static string removeOpdrachten = "/remove/royZapp";

        public static string getKlanten = "/get/royZappKlant";
        public static string saveKlanten = "/save/royZappKlant";
        public static string removeKlanten = "/remove/royZappKlant";

        public static string getTaken = "/get/royZappTaak";
        public static string saveTaken = "/save/royZappTaak";
        public static string removeTaken = "/remove/royZappTaak";


        public static string CreateUrl(string url)
        {
            string requestUrl = String.Format("{0}{1}?token={2}", Config.baseUrl, url, Config.token);
            return (requestUrl);

        }

        public static void Log(string log)
        {
            Console.WriteLine(String.Format("*** {0} ***", log));
        }

    }
}