using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace FOIKnjiznica.Classes
{
    public static class Clanovi
    {
        public static int id { get; set; } = 4;
        public static string hrEduPersonUniqueID { get; set; } = "stiven@foi.hr";
        public static string ime { get; set; } = "Stiven";
        public static string prezime { get; set; } = "Drvoderić";
        public static string mobitelID { get; set; } = "00012223111223";
        public static List<Publikacije> listaFavorita { get; set; }

        public async static void DohvatiFavorite()
        {
            HttpClient client = new HttpClient();

            try
            {
                var response = await client.GetStringAsync("http://foiknjiznica1.azurewebsites.net/api/Favoriti/" + id);
                var favoriti = JsonConvert.DeserializeObject<List<Classes.Publikacije>>(response);
                listaFavorita = favoriti;
            }
            catch (Exception socketException) when (socketException is System.Net.Sockets.SocketException || socketException is HttpRequestException)
            {
                
            }
            finally
            {
                client.Dispose();
            } 
        }

    }
}
