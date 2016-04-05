using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Helper.Model.Metadata
{
    public enum DataTypes
    {
        OleDb,
        SQLServer,
        Oracle,
        BIML,
        SSIS,
        DotNet,
        SalesForce
    }

    public enum SF_FieldType
    {
        @string,
        picklist,
        multipicklist,
        combobox,
        reference,
        base64,
        boolean,
        currency,
        textarea,
        @int,
        @double,
        percent,
        phone,
        id,
        date,
        datetime,
        time,
        url,
        email,
        encryptedstring,
        datacategorygroupreference,
        location,
        address,
        anyType,
        complexvalue
    }

    public enum SF_SoapType
    {
        [System.Xml.Serialization.XmlEnumAttribute("tns:ID")]
        tnsID,
        [System.Xml.Serialization.XmlEnumAttribute("xsd:base64Binary")]
        xsdbase64Binary,
        [System.Xml.Serialization.XmlEnumAttribute("xsd:boolean")]
        xsdboolean,
        [System.Xml.Serialization.XmlEnumAttribute("xsd:double")]
        xsddouble,
        [System.Xml.Serialization.XmlEnumAttribute("xsd:int")]
        xsdint,
        [System.Xml.Serialization.XmlEnumAttribute("xsd:string")]
        xsdstring,
        [System.Xml.Serialization.XmlEnumAttribute("xsd:date")]
        xsddate,
        [System.Xml.Serialization.XmlEnumAttribute("xsd:dateTime")]
        xsddateTime,
        [System.Xml.Serialization.XmlEnumAttribute("xsd:time")]
        xsdtime,
        [System.Xml.Serialization.XmlEnumAttribute("urn:location")]
        urnlocation,
        [System.Xml.Serialization.XmlEnumAttribute("urn:address")]
        urnaddress,
        [System.Xml.Serialization.XmlEnumAttribute("xsd:anyType")]
        xsdanyType,
        [System.Xml.Serialization.XmlEnumAttribute("urn:RelationshipReferenceTo")]
        urnRelationshipReferenceTo,
    }
}
