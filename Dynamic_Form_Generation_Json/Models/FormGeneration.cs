using System.Collections.Generic;

namespace Dynamic_Form_Generation_Json.Models
{
    // FormGeneration myDeserializedClass = JsonConvert.DeserializeObject<FormGeneration>(myJsonResponse);
    public class GETDELIVERYOPTIONTEMPLATE
    {
        public string PRODUCT { get; set; }
        public string CATEGORY { get; set; }
        public int T_INDEX { get; set; }
        public string DESCRIPTION { get; set; }
    }

    public class RECORDSET
    {
        public List<GETDELIVERYOPTIONTEMPLATE> GETDELIVERYOPTIONTEMPLATE { get; set; }
    }

    public class FormGeneration
    {
        public RECORDSET RECORDSET { get; set; }
    }


}
