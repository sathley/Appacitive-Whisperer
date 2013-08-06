using System;
using System.Collections.Generic;

namespace Appacitive.Tools.DBImport.Model
{
    [Serializable]
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