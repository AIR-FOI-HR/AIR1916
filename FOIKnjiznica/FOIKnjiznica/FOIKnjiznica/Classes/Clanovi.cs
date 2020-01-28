using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FOIKnjiznica.Classes
{
    public static class Clanovi
    {
        public static int id { get; set; } = 4;
        public static string hrEduPersonUniqueID { get; set; } = "stiven@foi.hr";
        public static string mobitelID { get; set; } = "00012223111223";
        public static List<Publikacije> listaFavorita { get; set; }

        public static List<Mobitel> ListaMobitela { get; set; }

        public async static void DohvatiFavorite()
        {
            HttpClient client = new HttpClient();

            try
            {
                var response = await client.GetStringAsync("http://foiknjiznica2.azurewebsites.net/api/Favoriti/" + id);
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

        public async static Task<List<Mobitel>> DohvatiMobiteleSvihClanova()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://foiknjiznica2.azurewebsites.net/api/Mobitel/");
            var mobiteli = JsonConvert.DeserializeObject<List<Mobitel>>(response);
            ListaMobitela = mobiteli;
            return ListaMobitela;
        }
    }

    /// <summary>
    /// Pomoćna klasa kako bi mogli dohvatiti id mobitela u pozivu api-a.
    /// </summary>o.
    public class Mobitel
    {
        public int Id { get; set; }
        public string MobitelId { get; set; }
    }
}
