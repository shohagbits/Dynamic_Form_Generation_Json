using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dynamic_Form_Generation_Json.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Body
    {
        [JsonProperty("esp-das-reply")]
        public EspDasReply EspDasReply { get; set; }
    }

    public class Connector
    {
        public string id { get; set; }
    }

    public class DATACONTEXT
    {
        public HEADER HEADER { get; set; }
        public RECORDSET RECORDSET { get; set; }
    }

    public class Device
    {
        public string id { get; set; }
        public string type { get; set; }
    }

    public class Envelope
    {
        public Body Body { get; set; }
    }

    public class EspDasReply
    {
        public Partner partner { get; set; }
        public Device device { get; set; }
        public string external_reference_no { get; set; }
        public MTML MTML { get; set; }
    }

    public class GETDELIVERYOPTIONTEMPLATE
    {
        public string PRODUCT { get; set; }
        public object CATEGORY { get; set; }
        public int T_INDEX { get; set; }
        public string DESCRIPTION { get; set; }
    }

    public class HEADER
    {
        public string ACCOUNT_NUM { get; set; }
        public string DATA_MORE { get; set; }
        public int DATA_NUM_RECS { get; set; }
        public string NAME { get; set; }
    }

    public class MTML
    {
        public REPLY REPLY { get; set; }
    }

    public class Partner
    {
        public string id { get; set; }
        public System system { get; set; }
    }

    public class RECORDSET
    {
        public List<GETDELIVERYOPTIONTEMPLATE> GETDELIVERYOPTIONTEMPLATE { get; set; }
    }

    public class REPLY
    {
        public DATACONTEXT DATA_CONTEXT { get; set; }
    }

    public class Root
    {
        public Envelope Envelope { get; set; }
    }

    public class System
    {
        public string id { get; set; }
        public string name { get; set; }
        public int version { get; set; }
        public string ip_address { get; set; }
        public Connector connector { get; set; }
    }

}
