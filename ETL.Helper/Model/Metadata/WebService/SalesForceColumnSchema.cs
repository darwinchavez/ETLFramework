using ETL.Helper.Controller;
using ETL.Services.SFDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public class SalesForceColumnSchema
    {
        public SalesForceColumnSchema(Field fieldDefinition)
        {
            if (fieldDefinition == null)
                throw new ArgumentNullException("fieldDefinition");

            string[] propertiesToCopy = new string[] {"type", "autoNumber", "byteLength", "calculated", "calculatedFormula", "cascadeDelete", "cascadeDeleteSpecified", "caseSensitive", "controllerName", "createable",
                                                        "custom", "defaultValueFormula", "defaultedOnCreate", "dependentPicklist", "dependentPicklistSpecified", "deprecatedAndHidden", "digits", "displayLocationInDecimal",
                                                        "displayLocationInDecimalSpecified", "encrypted", "encryptedSpecified", "externalId", "externalIdSpecified", "extraTypeInfo", "filterable", "groupable", "highScaleNumber",
                                                        "highScaleNumberSpecified", "htmlFormatted", "htmlFormattedSpecified", "idLookup", "inlineHelpText", "label", "length", "mask", "maskType", "name1", "name", "namePointing",
                                                        "namePointingSpecified", "nillable", "permissionable", "precision", "queryByDistance", "referenceTarget", "referenceTo", "relationshipName", "relationshipOrder", "relationshipOrderSpecified",
                                                        "restrictedDelete", "restrictedDeleteSpecified", "restrictedPicklist", "scale", "sortable", "sortableSpecified", "unique", "updateable", "writeRequiresMasterRead", "writeRequiresMasterReadSpecified"
                                                        };
            CommonController.CopyProperties(fieldDefinition, this, propertiesToCopy.ToList());
            type = (SF_FieldType) fieldDefinition.type;
        }

        public bool autoNumber { get; set; }
        public int byteLength { get; set; }
        public bool calculated { get; set; }
        public string calculatedFormula { get; set; }
        public bool cascadeDelete { get; set; }
        public bool cascadeDeleteSpecified { get; set; }
        public bool caseSensitive { get; set; }
        public string controllerName { get; set; }
        public bool createable { get; set; }
        public bool custom { get; set; }
        public string defaultValueFormula { get; set; }
        public bool defaultedOnCreate { get; set; }
        public bool dependentPicklist { get; set; }
        public bool dependentPicklistSpecified { get; set; }
        public bool deprecatedAndHidden { get; set; }
        public int digits { get; set; }
        public bool displayLocationInDecimal { get; set; }
        public bool displayLocationInDecimalSpecified { get; set; }
        public bool encrypted { get; set; }
        public bool encryptedSpecified { get; set; }
        public bool externalId { get; set; }
        public bool externalIdSpecified { get; set; }
        public string extraTypeInfo { get; set; }
        public bool filterable { get; set; }
        public FilteredLookupInfo filteredLookupInfo { get; set; }
        public bool groupable { get; set; }
        public bool highScaleNumber { get; set; }
        public bool highScaleNumberSpecified { get; set; }
        public bool htmlFormatted { get; set; }
        public bool htmlFormattedSpecified { get; set; }
        public bool idLookup { get; set; }
        public string inlineHelpText { get; set; }
        public string label { get; set; }
        public int length { get; set; }
        public string mask { get; set; }
        public string maskType { get; set; }
        public string name1 { get; set; }
        public bool name { get; set; }
        public bool namePointing { get; set; }
        public bool namePointingSpecified { get; set; }
        public bool nillable { get; set; }
        public bool permissionable { get; set; }
        public PicklistEntry[] picklistValues { get; set; }
        public int precision { get; set; }
        public bool queryByDistance { get; set; }
        public string referenceTarget { get; set; }
        public string[] referenceTo { get; set; }
        public string relationshipName { get; set; }
        public int relationshipOrder { get; set; }
        public bool relationshipOrderSpecified { get; set; }
        public bool restrictedDelete { get; set; }
        public bool restrictedDeleteSpecified { get; set; }
        public bool restrictedPicklist { get; set; }
        public int scale { get; set; }
        public SF_SoapType soapType { get; set; }
        public bool sortable { get; set; }
        public bool sortableSpecified { get; set; }
        public SF_FieldType type { get; set; }
        public bool unique { get; set; }
        public bool updateable { get; set; }
        public bool writeRequiresMasterRead { get; set; }
        public bool writeRequiresMasterReadSpecified { get; set; }
    }

    public partial class PicklistEntry
    {
        public bool active { get; set; }
        public bool defaultValue { get; set; }
        public string label { get; set; }
        public byte[] validFor { get; set; }
        public string value { get; set; }
    }

    public partial class FilteredLookupInfo
    {
        public string[] controllings { get; set; }
        public bool dependent { get; set; }
        public bool optionalFilter { get; set; }
    }
}
