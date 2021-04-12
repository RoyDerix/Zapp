using System;


namespace Zapp
{
    public static class Config
    {

        public static string baseUrl = "https://cockpit.educom.nu/api";
        public static string token = "bd216b5098452677789be90f7c4f4f";

        public static string authUser = "/cockpit/authUser";

        public static string getOpdrachten = "/collections/get/royZapp";
        public static string saveOpdrachten = "/collections/save/royZapp";
        public static string removeOpdrachten = "/collections/remove/royZapp";

        public static string getKlanten = "/collections/get/royZappKlant";
        public static string saveKlanten = "/collections/save/royZappKlant";
        public static string removeKlanten = "/collections/remove/royZappKlant";

        public static string getTaken = "/collections/get/royZappTaak";
        public static string saveTaken = "/collections/save/royZappTaak";
        public static string removeTaken = "/collections/remove/royZappTaak";


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