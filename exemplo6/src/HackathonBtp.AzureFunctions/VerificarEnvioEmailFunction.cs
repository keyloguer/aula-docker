using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackathonBtp.AzureFunctions
{
    public static class VerificarEnvioEmailFunction
    {
        [FunctionName("VerificarEnvioEmailFunction")]
        public static async Task Run([TimerTrigger("0 0 */3 * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            //var cfg = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json").Build();

            HttpClient client = new HttpClient();

            //var api = cfg.GetSection("Hackathon");

            await client.PostAsync("https://hackathonbtp.azurewebsites.net/btp/email/", null);

        }   
    }
}
