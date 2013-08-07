using System;
using System.Collections.Generic;

namespace Appacitive.Tools.DBImport.Model
{
    [Serializable]
    public class TableMapping
    {
        //  basic details
        public string TableName { get; set; }

        public bool KeepNameAsIs { get; set; }

        public string AppacitiveName { get; set; }    //  Name of schema this table transforms into

        public string Description { get; set; }

        //  Make CannedList details
        public bool MakeCannedList { get; set; }

        public string CannedListKeyColumn { get; set; }

        public string CannedListValueColumn { get; set; }

        public string CannedListDescriptionColumn { get; set; }     //  Other columns get ignored.

        //  For many-to-many relationship
        public bool IsJunctionTable { get; set; }   // Is it a mapping table ?

        public string JunctionTableRelationName { get; set; }

        public string JunctionTableRelationDescription { get; set; }

        public string JunctionsSideAColumn { get; set; }

        public string JunctionsSideBColumn { get; set; }    //  other columns become relation properties

        public string JunctionSideAfKeyIndexName { get; set; }

        public string JunctionSideBfKeyIndexName { get; set; }

        public int JunctionaSideAMultiplicity { get; set; } //  -1 for *

        public int JunctionaSideBMultiplicity { get; set; }

        public string JunctionALabel { get; set; }

        public string JunctionBLabel { get; set; }

        //  misc.
        public List<string> IgnoreColumns { get; set; }  //  names of columns to be ignored alltogether

        public List<string> IgnoreForeignKeyConstraints { get; set; }      //  names of foreign key constraints to be ignored alltogether

        public List<string> IgnoreUniqueKeyConstraints { get; set; }      //  names of unique key constraints to be ignored alltogether

        public List<Property> AddPropertiesToSchema { get; set; }    //  add some extra properties to schema

        public List<PropertyMapping> PropertyMappings { get; set; }  //  if only basic editing is required

        public List<ForeignKeyMapping> ForeignKeyMappings { get; set; }  // describes how foreign keys map to relations

        public TableMapping()
        {
            this.IgnoreColumns=new List<string>();
            this.IgnoreForeignKeyConstraints=new List<string>();
            this.IgnoreUniqueKeyConstraints=new List<string>();
            this.AddPropertiesToSchema=new List<Property>();
            this.PropertyMappings=new List<PropertyMapping>();
            this.ForeignKeyMappings=new List<ForeignKeyMapping>();
        }
        //  Other notes -
        //  Self referencing foreign keys become self relations.
        //  If table is made cannedlist, other tables which have foreign key to CannedListKeyColumn will get a property of datatype 'cannedlist'.
    }
}