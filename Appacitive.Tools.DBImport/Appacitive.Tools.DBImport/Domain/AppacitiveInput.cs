using System.Collections.Generic;

namespace Appacitive.Tools.DBImport
{
    public class AppacitiveInput
    {
        public AppacitiveInput()
        {
            this.Schemata = new List<Schema>();
            this.Relations = new List<Relation>();
            this.CannedLists = new List<CannedList>();
        }

        public IEnumerable<Schema> Schemata { get; set; }

        public IEnumerable<Relation> Relations { get; set; }

        public IEnumerable<CannedList> CannedLists { get; set; } 
    }
}