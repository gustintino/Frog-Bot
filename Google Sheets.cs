using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frog_bot
{
    public class GoogleHelperService
    {
        public SheetsService Service { get; set; }
        const string APPLICATION_NAME = "obc sHIT";
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        public static readonly string sheet = "Registrations";
        public string SheetID = "1vSy7Jg6qdt-UmLeIVBIua51IYEs8t5sMlr1_9UWOkkY";
        GoogleCredential credential;

        public GoogleHelperService()
        {
            using (var stream = new FileStream(@"Jason.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(SheetsService.Scope.Spreadsheets);
            }

            Service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = APPLICATION_NAME
            });
        }
    }
}
