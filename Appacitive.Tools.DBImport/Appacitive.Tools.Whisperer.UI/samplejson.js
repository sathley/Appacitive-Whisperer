{
    "Tables": [
      {
          "Name": "account",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "IX_Account_Name"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Description",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "CreatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "LastUpdatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Tags",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcLastUpdatedDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "accountattribute",
          "Columns": [
            {
                "Name": "AccountId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "account",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_AccountAttribute_Account"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Value",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "accountplanmapping",
          "Columns": [
            {
                "Name": "AccountId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "account",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "accountplanmapping_ibfk_2"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "PlanId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "plan",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "accountplanmapping_ibfk_1"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "StartDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "EndDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "accountusermapping",
          "Columns": [
            {
                "Name": "AccountId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "User",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "User"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "aggregate",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "BlueprintId",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "RelationId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "relation1",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_Aggregate_Relation"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "DestinationSchemaId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "schema1",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_Aggregate_Schema"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "Aggregate_Name_Unique"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "DestinationSchemaLabel",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "PropertyId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "property",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_Aggregate_Property"
                  }
                ],
                "Constraints": []
            },
            {
                "Name": "SourceId",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Source",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Type",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "CreatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "unique",
                      "Name": "Aggregate_Name_Unique"
                  }
                ],
                "Constraints": []
            },
            {
                "Name": "Attributes",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            }
          ]
      },
      {
          "Name": "announcement",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Type",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Subject",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "Body",
                "Type": "Text",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "IsSendAll",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": []
            }
          ]
      },
      {
          "Name": "announcementaccountmapping",
          "Columns": [
            {
                "Name": "AnnouncementId",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "AccountId",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "IsRead",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": []
            }
          ]
      },
      {
          "Name": "apikey",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "ApplicationId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "application",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_ApiKey_Application"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "ApiKey",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "IX_ApiKey_Key"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Description",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "IsDisabled",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcLastUpdatedDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "application",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "AccountId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "IX_Application_Name"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "unique",
                      "Name": "IX_Application_Name"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Description",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Status",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "CreatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "LastUpdatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Tags",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcLastUpdatedDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "applicationattribute",
          "Columns": [
            {
                "Name": "ApplicationId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "application",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_ApplicationAttribute_Application"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Value",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "applicationconfiguration",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "ApplicationId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "application",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_ApplicationConfiguration_Application"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Section",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Value",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "IsList",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "IsPublic",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcLastUpdatedDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "applicationusermapping",
          "Columns": [
            {
                "Name": "ApplicationId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "User",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "apptemplate",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "AccountId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "account",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_AppTemplate_Account"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Code",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "Code"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Password",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "IsPublic",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "BlueprintId",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "MarkdownUrl",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "ThumbnailUrl",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "UsedCount",
                "Type": "Int",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Tags",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcLastUpdatedDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "apptemplateattribute",
          "Columns": [
            {
                "Name": "AppTemplateId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Value",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "blueprint",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "ApplicationId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "application",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_Blueprint_Application"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "IX_Blueprint_ApplicationId_Name_Unique"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "unique",
                      "Name": "IX_Blueprint_ApplicationId_Name_Unique"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Description",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "CreatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Status",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Revision",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Tags",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "LastUpdatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcLastUpdatedDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "blueprintattribute",
          "Columns": [
            {
                "Name": "BlueprintId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "blueprint",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_BlueprintAttribute_Blueprint"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Value",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "cannedlist1",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "BlueprintId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "blueprint",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_CannedList_Blueprint"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "IX_CannedList_blueprintId_name_unique"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "unique",
                      "Name": "IX_CannedList_blueprintId_name_unique"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Description",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Revision",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "IsDynamic",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "SearchUrl",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "CountUrl",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "ListingUrl",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "CreatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "LastUpdatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Tags",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcLastUpdatedDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "ItemsCount",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "cannedlistattribute",
          "Columns": [
            {
                "Name": "ListId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "cannedlist1",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_CannedListAttribute_CannedList"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Value",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "cannedlistitem",
          "Columns": [
            {
                "Name": "ListId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "cannedlist1",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_CannedListItem_CannedList"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "IX_CannedListItem_ListId_Name_Unique"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "IX_CannedListItem_ListId_Value_Unique"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  },
                  {
                      "SequenceInIndex": 2,
                      "Type": "unique",
                      "Name": "IX_CannedListItem_ListId_Name_Unique"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Value",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "unique",
                      "Name": "IX_CannedListItem_ListId_Value_Unique"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Position",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Description",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            }
          ]
      },
      {
          "Name": "defaultplanitem",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "Id"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "Name"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Description",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Counters",
                "Type": "Text",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "UnitValue",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UnitType",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "FreeUnits",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "ReservedUnits",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "BaseCharge",
                "Type": "Decimal",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "BaseCharge_Currency",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "PerUnitCharge",
                "Type": "Decimal",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "PerUnit_Currency",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "ReservedUnitCharge",
                "Type": "Decimal",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "ReservedUnit_Currency",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            }
          ]
      },
      {
          "Name": "deployment",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "ApplicationId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "application",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_Deployment_Application"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "IX_Deployment_Name_ApplicationId_Unique"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "BlueprintId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "blueprint",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_Deployment_Blueprint"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "unique",
                      "Name": "IX_Deployment_Name_ApplicationId_Unique"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Description",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "CreatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "LastUpdatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Revision",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcLastUpdatedDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Tags",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            }
          ]
      },
      {
          "Name": "deploymentconfiguration",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "DeploymentId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "deployment",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_DeploymentConfiguration_Deployment"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Section",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Value",
                "Type": "Text",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "IsList",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "IsPublic",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcLastUpdatedDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "discount",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "Id"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "AccountId",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "Name"
                  }
                ],
                "Constraints": []
            },
            {
                "Name": "Description",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "DiscountAmount",
                "Type": "Decimal",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "DiscountAmount_Currency",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "DiscountPercent",
                "Type": "Decimal",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "DiscountMonth",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "handlers",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "DeploymentId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "deployment",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "Fk_Deployment_DeploymentId"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "Deployment_Name_Unique"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "unique",
                      "Name": "Deployment_Name_Unique"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "AccountId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "account",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "Fk_Account_AccountId"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "ApplicationId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "application",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "Fk_Application_ApplicationId"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Type",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "File",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "CreatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "LastUpdatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "UtcLastUpdatedDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "Attributes",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            }
          ]
      },
      {
          "Name": "invite",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "UX_Invite_Name"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "ShortUrl",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Token",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "MaxCount",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UsedCount",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "StartDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "EndDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Description",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "CreatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "LastUpdatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Tags",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "Attributes",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcLastUpdatedDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "invoice",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "AccountId",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Month",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Year",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "PaymentStatus",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "TotalAmount",
                "Type": "Decimal",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "PlanId",
                "Type": "Int",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "plan",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "Id"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "Name"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "IsDefault",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "BaseCharge",
                "Type": "Decimal",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "BaseCharge_Currency",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcLastUpdatedDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "planitem",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "Id"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "PlanId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "plan",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "planitem_ibfk_1"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Description",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "FreeUnits",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "ReservedUnits",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "property",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Description",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Revision",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "IsInternal",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "PropertyFlag",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "IsHashed",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "DataType",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "MinValue",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "MxValue",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "IsList",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "CannedListId",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "IsMandatory",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "IsUnique",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "IsImmutable",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "IsMultiValued",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Storage",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "DefaultValue",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "ValidationRegex",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "UnmaskedStartingN",
                "Type": "Int",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "UnmaskedEndingN",
                "Type": "Int",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "Tags",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "CreatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "LastUpdatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcLastUpdatedDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "MinLength",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "MaxLength",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": []
            }
          ]
      },
      {
          "Name": "propertyattribute",
          "Columns": [
            {
                "Name": "PropertyId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "property",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_PropertyAttribute_Relation"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Value",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "rabbitmqrequest",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Domain",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Request",
                "Type": "Text",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "raindropserver",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "InstanceId",
                "Type": "Char",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "Raindrop_Instance_Unique"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "TimeStamp",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "relation1",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "BlueprintId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "blueprint",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_Relation_Blueprint"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "IX_Relation_BlueprintId_Name_Unique"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "unique",
                      "Name": "IX_Relation_BlueprintId_Name_Unique"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Description",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Revision",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "VertexASchemaId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "schema1",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_Relation_Schema_VertexA"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "VertexALabel",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "VertexBSchemaId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "schema1",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_Relation_Schema_VertexB"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "VertexBLabel",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "AMultiplicity",
                "Type": "Int",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "BMultiplicity",
                "Type": "Int",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "Tags",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "CreatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "LastUpdatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcLastUpdatedDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "PropertiesCount",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "IndexName",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            }
          ]
      },
      {
          "Name": "relationattribute",
          "Columns": [
            {
                "Name": "RelationId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "relation1",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_RelationAttribute_Relation"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Value",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "relationproperty",
          "Columns": [
            {
                "Name": "RelationId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "relation1",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_RelationProperty_Relation"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "PropertyId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "property",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_RelationProperty_Property"
                  },
                  {
                      "SequenceInIndex": 2,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "schema1",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "BlueprintId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "blueprint",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_Schema_Blueprint"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "IX_Schema_BlueprintId_Name_Unique"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "unique",
                      "Name": "IX_Schema_BlueprintId_Name_Unique"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Description",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Revision",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "CreatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Tags",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "LastUpdatedBy",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcLastUpdatedDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "PropertiesCount",
                "Type": "BigInt",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "IndexName",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            }
          ]
      },
      {
          "Name": "schemaattribute",
          "Columns": [
            {
                "Name": "SchemaId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "schema1",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_SchemaAttribute_Schema"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Value",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "schemaproperty",
          "Columns": [
            {
                "Name": "SchemaId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "schema1",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_SchemaProperty_Schema"
                  },
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "PropertyId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "ReferenceTableName": "property",
                      "ReferenceColumnName": "Id",
                      "Type": "foreign",
                      "Name": "FK_SchemaProperty_Property"
                  },
                  {
                      "SequenceInIndex": 2,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "seed",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "AddDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "systemconfiguration",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Section",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Value",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "IsList",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "IsPublic",
                "Type": "Bit",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcLastUpdatedDate",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "tax",
          "Columns": [
            {
                "Name": "Id",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "Id"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Name",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "Name"
                  }
                ],
                "Constraints": []
            },
            {
                "Name": "Description",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "TaxAmount",
                "Type": "Decimal",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "TaxAmount_Currency",
                "Type": "VarChar",
                "Indexes": [],
                "Constraints": []
            },
            {
                "Name": "TaxPercent",
                "Type": "Decimal",
                "Indexes": [],
                "Constraints": []
            }
          ]
      },
      {
          "Name": "template",
          "Columns": [
            {
                "Name": "TemplateId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "TemplateName",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "unique",
                      "Name": "UniqueNameType"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "Type",
                "Type": "VarChar",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "unique",
                      "Name": "UniqueNameType"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      },
      {
          "Name": "templatedeploymentmapping",
          "Columns": [
            {
                "Name": "TemplateId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 1,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "DeploymentId",
                "Type": "BigInt",
                "Indexes": [
                  {
                      "SequenceInIndex": 2,
                      "Type": "primary",
                      "Name": "PRIMARY"
                  }
                ],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            },
            {
                "Name": "UtcDateCreated",
                "Type": "DateTime",
                "Indexes": [],
                "Constraints": [
                  {
                      "Type": "notnull"
                  }
                ]
            }
          ]
      }
    ]
}