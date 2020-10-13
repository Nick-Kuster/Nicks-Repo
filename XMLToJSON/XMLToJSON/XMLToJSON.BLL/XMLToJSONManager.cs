using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace XMLToJSON.BLL
{
    public class XMLToJSONManager
    {
        private const string SettingsFileName = "XMLToJsonUserSettings.config";

        public async Task SendToEndpoint()
        {
            
            UserSettingsManager<XMLToJSONUserSettings> settingsManager = new UserSettingsManager<XMLToJSONUserSettings>(SettingsFileName);
            var settings = settingsManager.LoadSettings();

            var file = settings.FilePath;
            string xml = "";

            if (File.Exists(file))
            {
                xml = File.ReadAllText(file);
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string jsonText = JsonConvert.SerializeXmlNode(doc);
            
            await Task.Delay(3000);

            var client = new RestClient(settings.Endpoint);
            // request.AddHeader("authorization", "Bearer " + _fireBaseService.AuthToken);
            //string url = string.Format("{0}batchs", MyUrl);
            //RestClient client = new RestClient(url);
            //RestRequest getRequest = new RestRequest(Method.GET);
            //getRequest.AddHeader("Accept", "application/json");
            //getRequest.AddHeader("Authorization", "token " + MyToken);
            //getRequest.AddParameter("name", MyName, ParameterType.QueryString);

            //IRestResponse getResponse = client.Execute(getRequest);
            client.Authenticator = new HttpBasicAuthenticator("username", "password");
            var request = new RestRequest("address/update")
                                .AddJsonBody(jsonText);

            var response = await client.PostAsync<string>(request);

        }

        public void SaveJSONToXML()
        {
            UserSettingsManager<XMLToJSONUserSettings> settingsManager = new UserSettingsManager<XMLToJSONUserSettings>(SettingsFileName);
            var settings = settingsManager.LoadSettings();

            var file = settings.FilePath;
            var path = Path.GetDirectoryName(file);
            var fullPath = Path.Combine(path, "SampleJSON.json");
            if (File.Exists(fullPath))
            {
                XmlDocument xml = JsonConvert.DeserializeXmlNode(File.ReadAllText(fullPath));
                xml.Save(Path.Combine(path, "SampleXML.xml"));
            }           
        }
    }
}
