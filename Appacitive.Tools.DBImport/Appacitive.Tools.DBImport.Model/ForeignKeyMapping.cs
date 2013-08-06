using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport.Model
{
    [Serializable]
    public class ForeignKeyMapping
    {
        public ForeignKeyMapping()
        {
            this.AddPropertiesToRelation=new List<Property>();
        }

        public string ForeignKeyName { get; set; }

        public bool KeepNameAsIs { get; set; }

        public string AppacitiveRelationName { get; set; }

        public string Description { get; set; }

        public int RestrictManySideMultiplicityTo { get; set; }    //  -1 for *

        public string OneSideLabel { get; set; }

        public string ManySideLabel { get; set; }

        public List<Property> AddPropertiesToRelation { get; set; }  //   these become relation properties
    }
}
