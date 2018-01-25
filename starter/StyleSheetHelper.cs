using System;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Linq;
using System.Reflection;
using Xamarin.Forms.StyleSheets;
using System.Threading.Tasks;
using static System.Web.HttpUtility;

namespace EasyChat.Helpers
{
    public static class StyleSheetHelper
    {
        public static StyleSheet GetStyleSheet()
        {
            if (!AppConstants.UseAzureFunction)
                return GetEmbeddedStyleSheet();

            var path = GetLocalStyleSheetPath();

            if (File.Exists(path))
            {
                var fileStream = File.OpenRead(path);
                return StyleSheet.FromReader(new StreamReader(fileStream));
            }

            return GetEmbeddedStyleSheet();
        }

        public static async Task UpdateStyleSheet()
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri($"https://{AppConstants.AzureFunctionAppName}.azurewebsites.net/api/{AppConstants.AzureFunctionName}?code={AppConstants.AzureFunctionCode}&incommingColor={UrlEncode(Settings.Current.IncommingColor)}&outgoingColor={UrlEncode(Settings.Current.OutgoingColor)}");
                var request = await client.GetAsync(uri);
                if (!request.IsSuccessStatusCode) return;

                var css = await request.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(css)) return;

                var path = GetLocalStyleSheetPath();
                if (File.Exists(path))
                    File.Delete(path);

                using (var sw = File.CreateText(path))
                {
                    var lines = css.Split('\n');
                    foreach (var line in lines)
                        sw.WriteLine(line);
                }
            }
        }

        private static StyleSheet GetEmbeddedStyleSheet() =>
            StyleSheet.FromAssemblyResource(typeof(StyleSheetHelper).Assembly, "EasyChat.Style.style.css");

        private static string GetLocalStyleSheetPath() =>
            Path.Combine(GetWorkingPath(), "style.css");

        private static string GetWorkingPath() =>
            Environment.GetFolderPath(Environment.SpecialFolder.Personal);
    }
}
