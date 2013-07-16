using System;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport
{
    public class AppacitiveWhisperer
    {
        public AppacitiveWhisperer(string apiKey, string bId)
        {
            this.ApiKey = apiKey;
            this.BlueprintId = bId;
        }

        public string ApiKey { get; set; }

        public string BlueprintId { get; set; }

        public void Whisper(AppacitiveInput input)
        {
            input.CannedLists.ForEach(c=>CreateCannedList(c));
            input.Schemata.ForEach(s=>CreateSchema(s));
            input.Relations.ForEach(r=>CreateRelation(r));
        }

        public CreateResult CreateSchema(Schema schema)
        {
            throw new NotImplementedException();
        }

        public CreateResult CreateRelation(Relation relation)
        {
            throw new NotImplementedException();
        }

        public CreateResult CreateCannedList(CannedList cannedList)
        {
            throw new NotImplementedException();
        }
    }
}
