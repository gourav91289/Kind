

[assembly: System.Runtime.Serialization.ContractNamespaceAttribute("http://schemas.datacontract.org/2004/07/Common.Payment.Common", ClrNamespace = "Common.Payment.Common1")]
namespace System.Runtime.Serialization
{
    public class ExtensionDataObject
    {
    }

    internal interface IExtensibleDataObject
    {
    }
}
namespace Common.Payment.Common1
{
    using System.Runtime.Serialization;


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "PaymentInfo", Namespace = "http://schemas.datacontract.org/2004/07/Common.Payment.Common")]
    public partial class PaymentInfo : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string STATECDField;

        private string HASHVALUEField;

        private string AMOUNTField;

        private string CIDField;

        private string SERVICECODEField;

        private string UNIQUETRANSIDField;

        private string DESCRIPTIONField;

        private string LOCALREFIDField;

        private string MERCHANTIDField;

        private string MERCHANTKEYField;

        private string PAYTYPEField;

        private string NAMEField;

        private string COMPANYNAMEField;

        private string COUNTRYField;

        private string FAXField;

        private string ADDRESS1Field;

        private string ADDRESS2Field;

        private string CITYField;

        private string STATEField;

        private string ZIPField;

        private string PHONEField;

        private string EMAILField;

        private string EMAIL1Field;

        private string EMAIL2Field;

        private string EMAIL3Field;

        private string HREFSUCCESSField;

        private string HREFFAILUREField;

        private string HREFDUPLICATEField;

        private string HREFCANCELField;

        private Common.Payment.Common1.FIELD[] ORDERATTRIBUTESField;

        private Common.Payment.Common1.LINEITEM[] LINEITEMSField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string STATECD
        {
            get
            {
                return this.STATECDField;
            }
            set
            {
                this.STATECDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
        public string HASHVALUE
        {
            get
            {
                return this.HASHVALUEField;
            }
            set
            {
                this.HASHVALUEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string AMOUNT
        {
            get
            {
                return this.AMOUNTField;
            }
            set
            {
                this.AMOUNTField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public string CID
        {
            get
            {
                return this.CIDField;
            }
            set
            {
                this.CIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public string SERVICECODE
        {
            get
            {
                return this.SERVICECODEField;
            }
            set
            {
                this.SERVICECODEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public string UNIQUETRANSID
        {
            get
            {
                return this.UNIQUETRANSIDField;
            }
            set
            {
                this.UNIQUETRANSIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
        public string DESCRIPTION
        {
            get
            {
                return this.DESCRIPTIONField;
            }
            set
            {
                this.DESCRIPTIONField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
        public string LOCALREFID
        {
            get
            {
                return this.LOCALREFIDField;
            }
            set
            {
                this.LOCALREFIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 8)]
        public string MERCHANTID
        {
            get
            {
                return this.MERCHANTIDField;
            }
            set
            {
                this.MERCHANTIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 9)]
        public string MERCHANTKEY
        {
            get
            {
                return this.MERCHANTKEYField;
            }
            set
            {
                this.MERCHANTKEYField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 10)]
        public string PAYTYPE
        {
            get
            {
                return this.PAYTYPEField;
            }
            set
            {
                this.PAYTYPEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 11)]
        public string NAME
        {
            get
            {
                return this.NAMEField;
            }
            set
            {
                this.NAMEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 12)]
        public string COMPANYNAME
        {
            get
            {
                return this.COMPANYNAMEField;
            }
            set
            {
                this.COMPANYNAMEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 13)]
        public string COUNTRY
        {
            get
            {
                return this.COUNTRYField;
            }
            set
            {
                this.COUNTRYField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 14)]
        public string FAX
        {
            get
            {
                return this.FAXField;
            }
            set
            {
                this.FAXField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 15)]
        public string ADDRESS1
        {
            get
            {
                return this.ADDRESS1Field;
            }
            set
            {
                this.ADDRESS1Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 16)]
        public string ADDRESS2
        {
            get
            {
                return this.ADDRESS2Field;
            }
            set
            {
                this.ADDRESS2Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 17)]
        public string CITY
        {
            get
            {
                return this.CITYField;
            }
            set
            {
                this.CITYField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 18)]
        public string STATE
        {
            get
            {
                return this.STATEField;
            }
            set
            {
                this.STATEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 19)]
        public string ZIP
        {
            get
            {
                return this.ZIPField;
            }
            set
            {
                this.ZIPField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 20)]
        public string PHONE
        {
            get
            {
                return this.PHONEField;
            }
            set
            {
                this.PHONEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 21)]
        public string EMAIL
        {
            get
            {
                return this.EMAILField;
            }
            set
            {
                this.EMAILField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 22)]
        public string EMAIL1
        {
            get
            {
                return this.EMAIL1Field;
            }
            set
            {
                this.EMAIL1Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 23)]
        public string EMAIL2
        {
            get
            {
                return this.EMAIL2Field;
            }
            set
            {
                this.EMAIL2Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 24)]
        public string EMAIL3
        {
            get
            {
                return this.EMAIL3Field;
            }
            set
            {
                this.EMAIL3Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 25)]
        public string HREFSUCCESS
        {
            get
            {
                return this.HREFSUCCESSField;
            }
            set
            {
                this.HREFSUCCESSField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 26)]
        public string HREFFAILURE
        {
            get
            {
                return this.HREFFAILUREField;
            }
            set
            {
                this.HREFFAILUREField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 27)]
        public string HREFDUPLICATE
        {
            get
            {
                return this.HREFDUPLICATEField;
            }
            set
            {
                this.HREFDUPLICATEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 28)]
        public string HREFCANCEL
        {
            get
            {
                return this.HREFCANCELField;
            }
            set
            {
                this.HREFCANCELField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 29)]
        public Common.Payment.Common1.FIELD[] ORDERATTRIBUTES
        {
            get
            {
                return this.ORDERATTRIBUTESField;
            }
            set
            {
                this.ORDERATTRIBUTESField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 30)]
        public Common.Payment.Common1.LINEITEM[] LINEITEMS
        {
            get
            {
                return this.LINEITEMSField;
            }
            set
            {
                this.LINEITEMSField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "FIELD", Namespace = "http://schemas.datacontract.org/2004/07/Common.Payment.Common")]
    public partial class FIELD : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string FIELDNAMEField;

        private string FIELDVALUEField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FIELDNAME
        {
            get
            {
                return this.FIELDNAMEField;
            }
            set
            {
                this.FIELDNAMEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FIELDVALUE
        {
            get
            {
                return this.FIELDVALUEField;
            }
            set
            {
                this.FIELDVALUEField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "LINEITEM", Namespace = "http://schemas.datacontract.org/2004/07/Common.Payment.Common")]
    public partial class LINEITEM : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private int ITEM_IDField;

        private string SKUField;

        private string DESCRIPTIONField;

        private decimal UNIT_PRICEField;

        private decimal QUANTITYField;

        private Common.Payment.Common1.FIELD[] ATTRIBUTESField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ITEM_ID
        {
            get
            {
                return this.ITEM_IDField;
            }
            set
            {
                this.ITEM_IDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SKU
        {
            get
            {
                return this.SKUField;
            }
            set
            {
                this.SKUField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string DESCRIPTION
        {
            get
            {
                return this.DESCRIPTIONField;
            }
            set
            {
                this.DESCRIPTIONField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public decimal UNIT_PRICE
        {
            get
            {
                return this.UNIT_PRICEField;
            }
            set
            {
                this.UNIT_PRICEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public decimal QUANTITY
        {
            get
            {
                return this.QUANTITYField;
            }
            set
            {
                this.QUANTITYField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public Common.Payment.Common1.FIELD[] ATTRIBUTES
        {
            get
            {
                return this.ATTRIBUTESField;
            }
            set
            {
                this.ATTRIBUTESField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "PreparePaymentResult", Namespace = "http://schemas.datacontract.org/2004/07/Common.Payment.Common")]
    public partial class PreparePaymentResult : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string TOKENField;

        private int ERRORCODEField;

        private string ERRORMESSAGEField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TOKEN
        {
            get
            {
                return this.TOKENField;
            }
            set
            {
                this.TOKENField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
        public int ERRORCODE
        {
            get
            {
                return this.ERRORCODEField;
            }
            set
            {
                this.ERRORCODEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string ERRORMESSAGE
        {
            get
            {
                return this.ERRORMESSAGEField;
            }
            set
            {
                this.ERRORMESSAGEField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "PaymentInfov2", Namespace = "http://schemas.datacontract.org/2004/07/Common.Payment.Common")]
    public partial class PaymentInfov2 : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string STATECDField;

        private string HASHVALUEField;

        private string AMOUNTField;

        private string CIDField;

        private string SERVICECODEField;

        private string UNIQUETRANSIDField;

        private string DESCRIPTIONField;

        private string LOCALREFIDField;

        private string MERCHANTIDField;

        private string MERCHANTKEYField;

        private string PAYTYPEField;

        private string NAMEField;

        private string COMPANYNAMEField;

        private string COUNTRYField;

        private string FAXField;

        private string ADDRESS1Field;

        private string ADDRESS2Field;

        private string CITYField;

        private string STATEField;

        private string ZIPField;

        private string PHONEField;

        private string EMAILField;

        private string EMAIL1Field;

        private string EMAIL2Field;

        private string EMAIL3Field;

        private string HREFSUCCESSField;

        private string HREFFAILUREField;

        private string HREFDUPLICATEField;

        private string HREFCANCELField;

        private Common.Payment.Common1.FIELD[] ORDERATTRIBUTESField;

        private Common.Payment.Common1.LINEITEM[] LINEITEMSField;

        private string ALTNAMEField;

        private string ALTADDRESS1Field;

        private string ALTADDRESS2Field;

        private string ALTCITYField;

        private string ALTSTATEField;

        private string ALTZIPField;

        private string ALTCOUNTRYField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string STATECD
        {
            get
            {
                return this.STATECDField;
            }
            set
            {
                this.STATECDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
        public string HASHVALUE
        {
            get
            {
                return this.HASHVALUEField;
            }
            set
            {
                this.HASHVALUEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string AMOUNT
        {
            get
            {
                return this.AMOUNTField;
            }
            set
            {
                this.AMOUNTField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public string CID
        {
            get
            {
                return this.CIDField;
            }
            set
            {
                this.CIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public string SERVICECODE
        {
            get
            {
                return this.SERVICECODEField;
            }
            set
            {
                this.SERVICECODEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public string UNIQUETRANSID
        {
            get
            {
                return this.UNIQUETRANSIDField;
            }
            set
            {
                this.UNIQUETRANSIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
        public string DESCRIPTION
        {
            get
            {
                return this.DESCRIPTIONField;
            }
            set
            {
                this.DESCRIPTIONField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
        public string LOCALREFID
        {
            get
            {
                return this.LOCALREFIDField;
            }
            set
            {
                this.LOCALREFIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 8)]
        public string MERCHANTID
        {
            get
            {
                return this.MERCHANTIDField;
            }
            set
            {
                this.MERCHANTIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 9)]
        public string MERCHANTKEY
        {
            get
            {
                return this.MERCHANTKEYField;
            }
            set
            {
                this.MERCHANTKEYField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 10)]
        public string PAYTYPE
        {
            get
            {
                return this.PAYTYPEField;
            }
            set
            {
                this.PAYTYPEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 11)]
        public string NAME
        {
            get
            {
                return this.NAMEField;
            }
            set
            {
                this.NAMEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 12)]
        public string COMPANYNAME
        {
            get
            {
                return this.COMPANYNAMEField;
            }
            set
            {
                this.COMPANYNAMEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 13)]
        public string COUNTRY
        {
            get
            {
                return this.COUNTRYField;
            }
            set
            {
                this.COUNTRYField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 14)]
        public string FAX
        {
            get
            {
                return this.FAXField;
            }
            set
            {
                this.FAXField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 15)]
        public string ADDRESS1
        {
            get
            {
                return this.ADDRESS1Field;
            }
            set
            {
                this.ADDRESS1Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 16)]
        public string ADDRESS2
        {
            get
            {
                return this.ADDRESS2Field;
            }
            set
            {
                this.ADDRESS2Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 17)]
        public string CITY
        {
            get
            {
                return this.CITYField;
            }
            set
            {
                this.CITYField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 18)]
        public string STATE
        {
            get
            {
                return this.STATEField;
            }
            set
            {
                this.STATEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 19)]
        public string ZIP
        {
            get
            {
                return this.ZIPField;
            }
            set
            {
                this.ZIPField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 20)]
        public string PHONE
        {
            get
            {
                return this.PHONEField;
            }
            set
            {
                this.PHONEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 21)]
        public string EMAIL
        {
            get
            {
                return this.EMAILField;
            }
            set
            {
                this.EMAILField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 22)]
        public string EMAIL1
        {
            get
            {
                return this.EMAIL1Field;
            }
            set
            {
                this.EMAIL1Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 23)]
        public string EMAIL2
        {
            get
            {
                return this.EMAIL2Field;
            }
            set
            {
                this.EMAIL2Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 24)]
        public string EMAIL3
        {
            get
            {
                return this.EMAIL3Field;
            }
            set
            {
                this.EMAIL3Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 25)]
        public string HREFSUCCESS
        {
            get
            {
                return this.HREFSUCCESSField;
            }
            set
            {
                this.HREFSUCCESSField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 26)]
        public string HREFFAILURE
        {
            get
            {
                return this.HREFFAILUREField;
            }
            set
            {
                this.HREFFAILUREField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 27)]
        public string HREFDUPLICATE
        {
            get
            {
                return this.HREFDUPLICATEField;
            }
            set
            {
                this.HREFDUPLICATEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 28)]
        public string HREFCANCEL
        {
            get
            {
                return this.HREFCANCELField;
            }
            set
            {
                this.HREFCANCELField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 29)]
        public Common.Payment.Common1.FIELD[] ORDERATTRIBUTES
        {
            get
            {
                return this.ORDERATTRIBUTESField;
            }
            set
            {
                this.ORDERATTRIBUTESField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 30)]
        public Common.Payment.Common1.LINEITEM[] LINEITEMS
        {
            get
            {
                return this.LINEITEMSField;
            }
            set
            {
                this.LINEITEMSField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 31)]
        public string ALTNAME
        {
            get
            {
                return this.ALTNAMEField;
            }
            set
            {
                this.ALTNAMEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 32)]
        public string ALTADDRESS1
        {
            get
            {
                return this.ALTADDRESS1Field;
            }
            set
            {
                this.ALTADDRESS1Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 33)]
        public string ALTADDRESS2
        {
            get
            {
                return this.ALTADDRESS2Field;
            }
            set
            {
                this.ALTADDRESS2Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 34)]
        public string ALTCITY
        {
            get
            {
                return this.ALTCITYField;
            }
            set
            {
                this.ALTCITYField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 35)]
        public string ALTSTATE
        {
            get
            {
                return this.ALTSTATEField;
            }
            set
            {
                this.ALTSTATEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 36)]
        public string ALTZIP
        {
            get
            {
                return this.ALTZIPField;
            }
            set
            {
                this.ALTZIPField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 37)]
        public string ALTCOUNTRY
        {
            get
            {
                return this.ALTCOUNTRYField;
            }
            set
            {
                this.ALTCOUNTRYField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "PreparePaymentv2Result", Namespace = "http://schemas.datacontract.org/2004/07/Common.Payment.Common")]
    public partial class PreparePaymentv2Result : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string TOKENField;

        private int ERRORCODEField;

        private string ERRORMESSAGEField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TOKEN
        {
            get
            {
                return this.TOKENField;
            }
            set
            {
                this.TOKENField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
        public int ERRORCODE
        {
            get
            {
                return this.ERRORCODEField;
            }
            set
            {
                this.ERRORCODEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string ERRORMESSAGE
        {
            get
            {
                return this.ERRORMESSAGEField;
            }
            set
            {
                this.ERRORMESSAGEField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "PaymentResult", Namespace = "http://schemas.datacontract.org/2004/07/Common.Payment.Common")]
    public partial class PaymentResult : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string NAMEField;

        private string PHONEField;

        private string FAXField;

        private string ADDRESS1Field;

        private string ADDRESS2Field;

        private string CITYField;

        private string STATEField;

        private string ZIPField;

        private string COUNTRYField;

        private string EMAILField;

        private string EMAIL1Field;

        private string EMAIL2Field;

        private string EMAIL3Field;

        private string LAST4NUMBERField;

        private string TOTALAMOUNTField;

        private string RECEIPTDATEField;

        private string RECEIPTTIMEField;

        private decimal ORDERIDField;

        private string AUTHCODEField;

        private string FAILMESSAGEField;

        private string LOCALREFIDField;

        private string PAYTYPEField;

        private System.DateTime ExpirationDateField;

        private string CreditCardTypeField;

        private string BillingNameField;

        private string AVSResponseField;

        private string CVVResponseField;

        private string FAILCODEField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NAME
        {
            get
            {
                return this.NAMEField;
            }
            set
            {
                this.NAMEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PHONE
        {
            get
            {
                return this.PHONEField;
            }
            set
            {
                this.PHONEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string FAX
        {
            get
            {
                return this.FAXField;
            }
            set
            {
                this.FAXField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public string ADDRESS1
        {
            get
            {
                return this.ADDRESS1Field;
            }
            set
            {
                this.ADDRESS1Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public string ADDRESS2
        {
            get
            {
                return this.ADDRESS2Field;
            }
            set
            {
                this.ADDRESS2Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public string CITY
        {
            get
            {
                return this.CITYField;
            }
            set
            {
                this.CITYField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
        public string STATE
        {
            get
            {
                return this.STATEField;
            }
            set
            {
                this.STATEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
        public string ZIP
        {
            get
            {
                return this.ZIPField;
            }
            set
            {
                this.ZIPField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 8)]
        public string COUNTRY
        {
            get
            {
                return this.COUNTRYField;
            }
            set
            {
                this.COUNTRYField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 9)]
        public string EMAIL
        {
            get
            {
                return this.EMAILField;
            }
            set
            {
                this.EMAILField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 10)]
        public string EMAIL1
        {
            get
            {
                return this.EMAIL1Field;
            }
            set
            {
                this.EMAIL1Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 11)]
        public string EMAIL2
        {
            get
            {
                return this.EMAIL2Field;
            }
            set
            {
                this.EMAIL2Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 12)]
        public string EMAIL3
        {
            get
            {
                return this.EMAIL3Field;
            }
            set
            {
                this.EMAIL3Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 13)]
        public string LAST4NUMBER
        {
            get
            {
                return this.LAST4NUMBERField;
            }
            set
            {
                this.LAST4NUMBERField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 14)]
        public string TOTALAMOUNT
        {
            get
            {
                return this.TOTALAMOUNTField;
            }
            set
            {
                this.TOTALAMOUNTField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 15)]
        public string RECEIPTDATE
        {
            get
            {
                return this.RECEIPTDATEField;
            }
            set
            {
                this.RECEIPTDATEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 16)]
        public string RECEIPTTIME
        {
            get
            {
                return this.RECEIPTTIMEField;
            }
            set
            {
                this.RECEIPTTIMEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 17)]
        public decimal ORDERID
        {
            get
            {
                return this.ORDERIDField;
            }
            set
            {
                this.ORDERIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 18)]
        public string AUTHCODE
        {
            get
            {
                return this.AUTHCODEField;
            }
            set
            {
                this.AUTHCODEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 19)]
        public string FAILMESSAGE
        {
            get
            {
                return this.FAILMESSAGEField;
            }
            set
            {
                this.FAILMESSAGEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 20)]
        public string LOCALREFID
        {
            get
            {
                return this.LOCALREFIDField;
            }
            set
            {
                this.LOCALREFIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 21)]
        public string PAYTYPE
        {
            get
            {
                return this.PAYTYPEField;
            }
            set
            {
                this.PAYTYPEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 22)]
        public System.DateTime ExpirationDate
        {
            get
            {
                return this.ExpirationDateField;
            }
            set
            {
                this.ExpirationDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 23)]
        public string CreditCardType
        {
            get
            {
                return this.CreditCardTypeField;
            }
            set
            {
                this.CreditCardTypeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 24)]
        public string BillingName
        {
            get
            {
                return this.BillingNameField;
            }
            set
            {
                this.BillingNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 25)]
        public string AVSResponse
        {
            get
            {
                return this.AVSResponseField;
            }
            set
            {
                this.AVSResponseField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 26)]
        public string CVVResponse
        {
            get
            {
                return this.CVVResponseField;
            }
            set
            {
                this.CVVResponseField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 27)]
        public string FAILCODE
        {
            get
            {
                return this.FAILCODEField;
            }
            set
            {
                this.FAILCODEField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "GetPaymentInfoResponse", Namespace = "http://schemas.datacontract.org/2004/07/Common.Payment.Common")]
    public partial class GetPaymentInfoResponse : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string NAMEField;

        private string PHONEField;

        private string FAXField;

        private string ADDRESS1Field;

        private string ADDRESS2Field;

        private string CITYField;

        private string STATEField;

        private string ZIPField;

        private string COUNTRYField;

        private string EMAILField;

        private string EMAIL1Field;

        private string EMAIL2Field;

        private string EMAIL3Field;

        private string LAST4NUMBERField;

        private string TOTALAMOUNTField;

        private string RECEIPTDATEField;

        private string RECEIPTTIMEField;

        private decimal ORDERIDField;

        private string AUTHCODEField;

        private string FAILMESSAGEField;

        private string LOCALREFIDField;

        private string PAYTYPEField;

        private System.DateTime ExpirationDateField;

        private string CreditCardTypeField;

        private string BillingNameField;

        private string AVSResponseField;

        private string CVVResponseField;

        private string FAILCODEField;

        private string ACHDATEField;

        private string TOKENField;

        private Common.Payment.Common1.NameValuePair[] extendedValuesField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NAME
        {
            get
            {
                return this.NAMEField;
            }
            set
            {
                this.NAMEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PHONE
        {
            get
            {
                return this.PHONEField;
            }
            set
            {
                this.PHONEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public string FAX
        {
            get
            {
                return this.FAXField;
            }
            set
            {
                this.FAXField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public string ADDRESS1
        {
            get
            {
                return this.ADDRESS1Field;
            }
            set
            {
                this.ADDRESS1Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public string ADDRESS2
        {
            get
            {
                return this.ADDRESS2Field;
            }
            set
            {
                this.ADDRESS2Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public string CITY
        {
            get
            {
                return this.CITYField;
            }
            set
            {
                this.CITYField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
        public string STATE
        {
            get
            {
                return this.STATEField;
            }
            set
            {
                this.STATEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
        public string ZIP
        {
            get
            {
                return this.ZIPField;
            }
            set
            {
                this.ZIPField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 8)]
        public string COUNTRY
        {
            get
            {
                return this.COUNTRYField;
            }
            set
            {
                this.COUNTRYField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 9)]
        public string EMAIL
        {
            get
            {
                return this.EMAILField;
            }
            set
            {
                this.EMAILField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 10)]
        public string EMAIL1
        {
            get
            {
                return this.EMAIL1Field;
            }
            set
            {
                this.EMAIL1Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 11)]
        public string EMAIL2
        {
            get
            {
                return this.EMAIL2Field;
            }
            set
            {
                this.EMAIL2Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 12)]
        public string EMAIL3
        {
            get
            {
                return this.EMAIL3Field;
            }
            set
            {
                this.EMAIL3Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 13)]
        public string LAST4NUMBER
        {
            get
            {
                return this.LAST4NUMBERField;
            }
            set
            {
                this.LAST4NUMBERField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 14)]
        public string TOTALAMOUNT
        {
            get
            {
                return this.TOTALAMOUNTField;
            }
            set
            {
                this.TOTALAMOUNTField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 15)]
        public string RECEIPTDATE
        {
            get
            {
                return this.RECEIPTDATEField;
            }
            set
            {
                this.RECEIPTDATEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 16)]
        public string RECEIPTTIME
        {
            get
            {
                return this.RECEIPTTIMEField;
            }
            set
            {
                this.RECEIPTTIMEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 17)]
        public decimal ORDERID
        {
            get
            {
                return this.ORDERIDField;
            }
            set
            {
                this.ORDERIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 18)]
        public string AUTHCODE
        {
            get
            {
                return this.AUTHCODEField;
            }
            set
            {
                this.AUTHCODEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 19)]
        public string FAILMESSAGE
        {
            get
            {
                return this.FAILMESSAGEField;
            }
            set
            {
                this.FAILMESSAGEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 20)]
        public string LOCALREFID
        {
            get
            {
                return this.LOCALREFIDField;
            }
            set
            {
                this.LOCALREFIDField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 21)]
        public string PAYTYPE
        {
            get
            {
                return this.PAYTYPEField;
            }
            set
            {
                this.PAYTYPEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 22)]
        public System.DateTime ExpirationDate
        {
            get
            {
                return this.ExpirationDateField;
            }
            set
            {
                this.ExpirationDateField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 23)]
        public string CreditCardType
        {
            get
            {
                return this.CreditCardTypeField;
            }
            set
            {
                this.CreditCardTypeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 24)]
        public string BillingName
        {
            get
            {
                return this.BillingNameField;
            }
            set
            {
                this.BillingNameField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 25)]
        public string AVSResponse
        {
            get
            {
                return this.AVSResponseField;
            }
            set
            {
                this.AVSResponseField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 26)]
        public string CVVResponse
        {
            get
            {
                return this.CVVResponseField;
            }
            set
            {
                this.CVVResponseField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 27)]
        public string FAILCODE
        {
            get
            {
                return this.FAILCODEField;
            }
            set
            {
                this.FAILCODEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 28)]
        public string ACHDATE
        {
            get
            {
                return this.ACHDATEField;
            }
            set
            {
                this.ACHDATEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 29)]
        public string TOKEN
        {
            get
            {
                return this.TOKENField;
            }
            set
            {
                this.TOKENField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(Order = 30)]
        public Common.Payment.Common1.NameValuePair[] extendedValues
        {
            get
            {
                return this.extendedValuesField;
            }
            set
            {
                this.extendedValuesField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "NameValuePair", Namespace = "http://schemas.datacontract.org/2004/07/Common.Payment.Common")]
    public partial class NameValuePair : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string NAMEField;

        private string VALUEField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NAME
        {
            get
            {
                return this.NAMEField;
            }
            set
            {
                this.NAMEField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string VALUE
        {
            get
            {
                return this.VALUEField;
            }
            set
            {
                this.VALUEField = value;
            }
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(Namespace = "https://common.checkout.cdc.nicusa.com", ConfigurationName = "IServiceWeb")]
public interface IServiceWeb
{

    [System.ServiceModel.OperationContractAttribute(Action = "https://common.checkout.cdc.nicusa.com/IServiceWeb/PreparePayment", ReplyAction = "https://common.checkout.cdc.nicusa.com/IServiceWeb/PreparePaymentResponse")]
    Common.Payment.Common1.PreparePaymentResult PreparePayment(Common.Payment.Common1.PaymentInfo request);

    [System.ServiceModel.OperationContractAttribute(Action = "https://common.checkout.cdc.nicusa.com/IServiceWeb/PreparePaymentv2", ReplyAction = "https://common.checkout.cdc.nicusa.com/IServiceWeb/PreparePaymentv2Response")]
    Common.Payment.Common1.PreparePaymentv2Result PreparePaymentv2(Common.Payment.Common1.PaymentInfov2 request);

    [System.ServiceModel.OperationContractAttribute(Action = "https://common.checkout.cdc.nicusa.com/IServiceWeb/QueryPayment", ReplyAction = "https://common.checkout.cdc.nicusa.com/IServiceWeb/QueryPaymentResponse")]
    Common.Payment.Common1.PaymentResult QueryPayment(string token);

    [System.ServiceModel.OperationContractAttribute(Action = "https://common.checkout.cdc.nicusa.com/IServiceWeb/GetPaymentInfo", ReplyAction = "https://common.checkout.cdc.nicusa.com/IServiceWeb/GetPaymentInfoResponse")]
    Common.Payment.Common1.GetPaymentInfoResponse GetPaymentInfo(string token);
}
