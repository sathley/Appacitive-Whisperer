using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Appacitive.Tools.DBImport.Model;

namespace Appacitive.Tools.DBImport.Tests
{
    public class SerializationFixtures
    {
        public void SerializeSchema()
        {
            var schema = new Schema()
                             {
                                 Name = "Test name",
                                 Description = "Test desc.",
                                 CreatedBy = "john doe",
                                 Properties = new List<Property>()
                             };
            var whisperer = new AppacitiveWhisperer("","","");
            //var jsonSchema = whisperer.AssembleSchema(schema);

        }
    }
}
