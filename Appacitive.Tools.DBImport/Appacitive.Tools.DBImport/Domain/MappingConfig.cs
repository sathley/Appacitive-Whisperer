﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appacitive.Tools.DBImport
{
    public class MappingConfig
    {
        public Input Input { get; set; }

        public IEnumerable<TableMapping> TableMappings { get; set; } 
    }

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

        public string JunctionsSideAColumn { get; set; }

        public string JunctionsSideBColumn { get; set; }    //  other columns become relation properties

        public int JunctionaSideAMultiplicity { get; set; }

        public int JunctionaSideBMultiplicity { get; set; }

        public string JunctionALabel { get; set; }

        public string JunctionBLabel { get; set; }

        //  misc.
        public IEnumerable<string> IgnoreColumns { get; set; }  //  names of columns to be ignored alltogether
            
        public IEnumerable<string> IgnoreForeignKeyConstraints { get; set; }      //  names of foreign key constraints to be ignored alltogether

        public IEnumerable<string> IgnoreUniqueKeyConstraints { get; set; }      //  names of unique key constraints to be ignored alltogether

        public IEnumerable<Property> AddPropertiesToSchema { get; set; }    //  add some extra properties to schema

        public IEnumerable<PropertyMapping> PropertyMappings { get; set; }  //  if only basic editing is required

        public IEnumerable<ForeignKeyMapping> ForeignKeyMappings { get; set; }  // describes how foreign keys map to relations

        //  Other notes -
        //  Self referencing foriegn keys become self relations.
        //  If table is made cannedlist, other tables which have foreign key to CannedListKeyColumn will get a property of datatype 'cannedlist'.
    }

    public class PropertyMapping
    {
        public string ColumnName { get; set; }

        public bool KeepNameAsIs { get; set; }

        public string AppacitivePropertyName { get; set; }

        public string Description { get; set; }
    }

    public class ForeignKeyMapping
    {
        public string ForeignKeyName { get; set; }

        public bool KeepNameAsIs { get; set; }

        public string AppacitiveRelationName { get; set; }

        public string Description { get; set; }

        public int RestrictManySideMultiplicityTo { get; set; }    //  -1 for *

        public string OneSideLabel { get; set; }

        public string ManySideLabel { get; set; }

        public IEnumerable<Property> AddPropertiesToForeignKeyRelation { get; set; }  //   these become relation properties
    }
}
