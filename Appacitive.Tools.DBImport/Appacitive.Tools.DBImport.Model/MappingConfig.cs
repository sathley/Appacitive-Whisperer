﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport.Model
{
    [Serializable]
    public class MappingConfig
    {
        public Input Input { get; set; }

        public List<TableMapping> TableMappings { get; set; } 
    }

    [Serializable]
    public class TableMapping
    {
        //  basic
        public string TableName { get; set; }

        public bool KeepNameAsIs { get; set; }

        public string AppacitiveName { get; set; }    //  Name of schema this table transforms into

        public string Description { get; set; }

        //  Make CannedList details
        public bool MakeCannedList { get; set; }

        public string CannedListKeyColumn { get; set; }

        public string CannedListValueColumn { get; set; }   //  Other columns get ignored.

        public string CannedListDescriptionColumn { get; set; }

        //  For many-to-many relationship
        public bool IsJunctionTable { get; set; }   // Is it a mapping table ?

        public string JunctionTableRelationName { get; set; }

        public string JunctionTableRelationDescription { get; set; }

        public string JunctionsSideAColumn { get; set; }

        public string JunctionsSideBColumn { get; set; }    //  other columns become relation properties

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

        //  Other notes -
        //  Self referencing foriegn keys become self relations.
        //  If table is made cannedlist, other tables which have foreign key to CannedListKeyColumn will get a property of datatype 'cannedlist'.
    }

    [Serializable]
    public class PropertyMapping
    {
        public string ColumnName { get; set; }

        public bool KeepNameAsIs { get; set; }

        public string AppacitivePropertyName { get; set; }

        public string Description { get; set; }
    }

    [Serializable]
    public class ForeignKeyMapping
    {
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
