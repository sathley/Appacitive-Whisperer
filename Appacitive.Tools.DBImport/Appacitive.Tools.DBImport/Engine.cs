using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport
{
    public static class Engine
    {
        public static AppacitiveInput AppacitizeDatabase(this Database database)
        {
            var result = new AppacitiveInput();
            foreach (var table in database.Tables)
            {
                var schema = new Schema();
                schema.Name = table.Name;

            }
        }
    }

    public class AppacitiveInput
    {
        public AppacitiveInput()
        {
            this.Schemata = new List<Schema>();
            this.Relations = new List<Relation>();
            this.CannedLists = new List<CannedList>();
        }

        public List<Schema> Schemata { get; set; }

        public List<Relation> Relations { get; set; }

        public List<CannedList> CannedLists { get; set; } 
    }
}
