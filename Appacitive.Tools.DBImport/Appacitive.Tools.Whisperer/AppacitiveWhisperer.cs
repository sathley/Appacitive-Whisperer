using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Appacitive.Tools.DBImport.Logging;
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

        public string ApiKey { get; private set; }

        public string BlueprintId { get; private set; }

        public string BaseURL { get; private set; }

        public void Whisper(AppacitiveInput input)
        {
            input.CannedLists.ForEach(c=>CreateCannedList(c));
            input.Schemata.ForEach(s=>CreateSchema(s));
            input.Relations.ForEach(r=>CreateRelation(r));
        }

        private CreateResult CreateSchema(Schema schema)
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
            if (dataStream != null) dataStream.Close();
            response.Close();
            var result =  new CreateResult()
                       {
                           Code = (string)jsonResponse["status"]["code"]
                       };
            if(result.Code == "200")
                BasicLogger.Log(string.Format("Successfully created schema '{0}'",schema.Name));
            else
            {
                BasicLogger.Log(string.Format("Schema creation Failed for '{0}'",schema.Name));
                BasicLogger.Log("Request Data -");
                BasicLogger.Log(postData);
                BasicLogger.Log("Response Data -");
                BasicLogger.Log(responseFromServer);
                BasicLogger.Log("=============================================================================");
            }
            return result;
        }

        private CreateResult CreateRelation(Relation relation)
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
            if (dataStream != null) dataStream.Close();
            response.Close();
            var result = new CreateResult()
            {
                Code = (string)jsonResponse["status"]["code"]
            };
            if (result.Code == "200")
                BasicLogger.Log(string.Format("Successfully created relation '{0}'", relation.Name));
            else
            {
                BasicLogger.Log(string.Format("Relation creation Failed for '{0}'", relation.Name));
                BasicLogger.Log("Request Data -");
                BasicLogger.Log(postData);
                BasicLogger.Log("Response Data -");
                BasicLogger.Log(responseFromServer);
                BasicLogger.Log("=============================================================================");
            }
            return result;
        }

        private CreateResult CreateCannedList(CannedList cannedList)
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
            if (dataStream != null) dataStream.Close();
            response.Close();
            var result = new CreateResult()
            {
                Code = (string)jsonResponse["status"]["code"]
            };
            if (result.Code == "200")
                BasicLogger.Log(string.Format("Successfully created cannedlist '{0}'", cannedList.Name));
            else
            {
                BasicLogger.Log(string.Format("Cannedlist creation Failed for '{0}'", cannedList.Name));
                BasicLogger.Log("Request Data -");
                BasicLogger.Log(postData);
                BasicLogger.Log("Response Data -");
                BasicLogger.Log(responseFromServer);
                BasicLogger.Log("=============================================================================");
            }
            return result;
        }
    }
}
