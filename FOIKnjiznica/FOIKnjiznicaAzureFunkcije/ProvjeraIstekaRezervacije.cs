using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FOIKnjiznicaAzureFunkcije
{
    public static class ProvjeraIstekaRezervacije
    {
        [FunctionName("ProvjeraIstekaRezervacije")]
        public static void Run([TimerTrigger("0 0 8 * * *")]TimerInfo myTimer, ILogger log)
        {         
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

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
                    var naredba = $"SELECT TOP 1 ClanoviId FROM Stanje_Publikacije WHERE KopijaID = '{idKopije}' AND CONVERT(date,datum_do) = CONVERT(DATE,GETDATE()) AND Vrsta_StatusaId IN(2,3) ORDER BY datum DESC";
                   
                    // Za testiranje je potrebno promijeniti dateadd funkciju tako da se doda današnjem danu broj dana za koju rezervaciju/posudbu treba probati
                   //var naredba = $"SELECT TOP 1 ClanoviId FROM Stanje_Publikacije WHERE KopijaID = '{idKopije}' AND CONVERT(date,datum_do) = CONVERT(DATE,dateadd(day,5,GETDATE()))  AND Vrsta_StatusaId IN(2,3) ORDER BY datum DESC";

                    using (SqlCommand cmd = new SqlCommand(naredba, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    //log.LogInformation($"Korisnik {reader.GetInt32(0).ToString()} mora vratiti publikaciju {idKopije.ToString()} ");
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
                                    string androidAlert = "{\"data\":{ \"message\":\"Imate rezervaciju/posudbu koja vam istièe danas!\"}}";
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
