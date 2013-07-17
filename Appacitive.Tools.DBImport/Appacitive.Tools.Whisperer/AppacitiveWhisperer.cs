using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Appacitive.Tools.DBImport.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Appacitive.Tools.DBImport
{
    public class AppacitiveWhisperer
    {
        public AppacitiveWhisperer(string apiKey, string bId, string url)
        {
            this.ApiKey = apiKey;
            this.BlueprintId = bId;
            this.BaseURL = url;
        }

        public string ApiKey { get; set; }

        public string BlueprintId { get; set; }

        public string BaseURL { get; set; }

        public void Whisper(AppacitiveInput input)
        {
            input.CannedLists.ForEach(c=>CreateCannedList(c));
            input.Schemata.ForEach(s=>CreateSchema(s));
            input.Relations.ForEach(r=>CreateRelation(r));
        }

        public CreateResult CreateSchema(Schema schema)
        {
            WebRequest request = WebRequest.Create(string.Format("{0}/schema/{1}",BaseURL,BlueprintId));
            request.Method = "PUT";

            string postData = JsonConvert.SerializeObject(schema);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json; charset=UTF-8";
            request.ContentLength = byteArray.Length;
            request.Headers.Add("Appacitive-Apikey", ApiKey);
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            var jsonResponse = JObject.Parse(responseFromServer);
            
            reader.Close();
            dataStream.Close();
            response.Close();
            return new CreateResult()
                       {
                           Code = (string)jsonResponse["status"]["code"]
                       };
        }

        public CreateResult CreateRelation(Relation relation)
        {
            WebRequest request = WebRequest.Create(string.Format("{0}/relation/{1}", BaseURL, BlueprintId));
            request.Method = "PUT";

            string postData = JsonConvert.SerializeObject(relation);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json; charset=UTF-8";
            request.ContentLength = byteArray.Length;
            request.Headers.Add("Appacitive-Apikey", ApiKey);
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            var jsonResponse = JObject.Parse(responseFromServer);

            reader.Close();
            dataStream.Close();
            response.Close();
            return new CreateResult()
            {
                Code = (string)jsonResponse["status"]["code"]
            };
        }

        public CreateResult CreateCannedList(CannedList cannedList)
        {
            WebRequest request = WebRequest.Create(string.Format("{0}/list/{1}", BaseURL, BlueprintId));
            request.Method = "PUT";

            string postData = JsonConvert.SerializeObject(cannedList);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json; charset=UTF-8";
            request.ContentLength = byteArray.Length;
            request.Headers.Add("Appacitive-Apikey", ApiKey);
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            var jsonResponse = JObject.Parse(responseFromServer);

            reader.Close();
            dataStream.Close();
            response.Close();
            return new CreateResult()
            {
                Code = (string)jsonResponse["status"]["code"]
            };
        }

        public string AssembleSchema(Schema schema)
        {
            var json = JsonConvert.SerializeObject(schema);
            return json;

        }
    }
}
