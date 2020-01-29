using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FOIKnjiznicaAzureFunkcije
{
    public class PovijestPublikacije
    {
        public int id { get; set; }
        public int kopijaId { get; set; }
        public int vrsta_statusaId { get; set; }
        public int clanoviId { get; set; }

        public DateTime datum { get; set; }
        public DateTime datum_do { get; set; }
        public string nazivPublikacije { get; set; }
        public string nazivStatusa { get; set; }
    }

    public static class AutomatskoPrekidanjeRezervacije
    {
        [FunctionName("AutomatskoPrekidanjeRezervacije")]
        public static async void Run([TimerTrigger("0 0 8 * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Prekid rezervacije function executed at: {DateTime.Now}");

            NotificationHubClient hub =
            NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://foiknjiznicanotificationhub-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=EdMFKv2S17MgtwqY/u57G7capol0MKbGaBZ1VokQRmM=",
                                                           "FoiKnjiznicaNotificationHub");

            string connString = "Server=tcp:foiknjiznica2.database.windows.net,1433;Initial Catalog=foiknjiznica;Persist Security Info=False;User ID=foi;Password=admin123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            List<int> listaIDKopija = new List<int>();
            List<int> listaIDKorisnika = new List<int>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                var text = $"SELECT kopija_id FROM Kopija_Publikacije";

                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                listaIDKopija.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }

                foreach (var idKopije in listaIDKopija)
                {
                    var naredba = $"SELECT TOP 1 ClanoviId FROM Stanje_Publikacije WHERE KopijaID = '{idKopije}' AND CONVERT(date,datum_do) = CONVERT(DATE,dateadd(day,-1,GETDATE())) AND Vrsta_StatusaId IN(3) ORDER BY datum DESC";

                    // Za testiranje je potrebno promijeniti dateadd funkciju tako da se doda današnjem danu broj dana za koju rezervaciju treba probati
                    //var naredba = $"SELECT TOP 1 ClanoviId,id FROM Stanje_Publikacije WHERE KopijaID = '{idKopije}' AND CONVERT(date,datum_do) = CONVERT(DATE,dateadd(day,3,GETDATE()))  AND Vrsta_StatusaId IN(3) ORDER BY datum DESC";

                    using (SqlCommand cmd = new SqlCommand(naredba, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                   //log.LogInformation($"Korisnik {reader.GetInt32(0).ToString()} se prekida publikacija {idKopije.ToString()} ");

                                    var httpClient = new HttpClient();
                                    var Json = JsonConvert.SerializeObject(new PovijestPublikacije() {clanoviId = reader.GetInt32(0), kopijaId = idKopije, id = reader.GetInt32(1) });
                                    var content = new StringContent(Json, Encoding.UTF8, "application/json");
                                    log.LogInformation(Json);
                                    var odgovor = await httpClient.PostAsync("http://foiknjiznica2.azurewebsites.net/api/PrekidRezervacije", content);

                                    listaIDKorisnika.Add(reader.GetInt32(0));
                                }
                            }
                        }
                    }
                }

                foreach (var korisnik in listaIDKorisnika)
                {
                    var naredbaSlanje = $"select mobitelID from Clanovi where id ='{korisnik}'";

                    using (SqlCommand cmd = new SqlCommand(naredbaSlanje, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    string androidAlert = "{\"data\":{ \"message\":\"Istekla Vam je rezervacija!\"}}";
                                    //log.LogInformation($"Saljem {androidAlert} korisniku {reader.GetString(0)}");
                                    hub.SendFcmNativeNotificationAsync(androidAlert, reader.GetString(0)).Wait();
                                }
                            }
                        }
                    }
                }

            }
        }
    }
}
