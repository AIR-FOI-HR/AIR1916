using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Azure.NotificationHubs;
using System.Net;
using FirebaseAdmin.Messaging;

namespace FOIKnjiznicaAzureFunkcije
{
    public static class OslobodenjeKnjigeNotifikacija
    {
        [FunctionName("OslobodenjeKnjigeNotifikacija")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string connString = "Server=tcp:foiknjiznica2.database.windows.net,1433;Initial Catalog=foiknjiznica;Persist Security Info=False;User ID=foi;Password=admin123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            string nazivKnjige = req.Query["idPublikacije"]; 

            int idPublikacije = 0;

            bool postojiZapis = false;

            int brojZapisa = 0;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                var text = $"SELECT id FROM Publikacije WHERE naziv = '{nazivKnjige}'";

                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                idPublikacije = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }

            string tagString = "(";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                var text = $"SELECT mobitelID FROM Clanovi JOIN Je_Favorit ON Clanovi.id = Je_Favorit.ClanoviId WHERE PublikacijeId = '{idPublikacije}'";

                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                tagString += reader.GetString(0) + " || ";
                                postojiZapis = true;
                                brojZapisa++;
                            }
                        }
                    }
                }
            }

            if(postojiZapis == false)
            {
                return new OkObjectResult($"Nema sto za poslati");
            }

            tagString = tagString.Remove(tagString.Length - 3);
            tagString = tagString + ")";

            NotificationHubClient hub =
                NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://foiknjiznicanotificationhub-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=EdMFKv2S17MgtwqY/u57G7capol0MKbGaBZ1VokQRmM=",
                                                                       "FoiKnjiznicaNotificationHub");
            var androidAlert = "{\"data\":{ \"message\":\"Oslobodio se vas favorit "+nazivKnjige+"\"}}";

            if (brojZapisa>1)
            {
                hub.SendFcmNativeNotificationAsync(androidAlert, tagString).Wait();
            }
            else if (brojZapisa == 1)
            {
                hub.SendFcmNativeNotificationAsync(androidAlert, tagString.Substring(1,tagString.Length-2)).Wait();
            }

            return new OkObjectResult($"Ovdje želite poslati: {tagString}");
        }
    }
}
