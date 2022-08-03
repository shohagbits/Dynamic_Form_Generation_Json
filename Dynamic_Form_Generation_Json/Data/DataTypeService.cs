using System.Collections.Generic;

namespace Dynamic_Form_Generation_Json.Data
{
    public class FieldDataType
    {
        public string Type { get; set; }
    }
    public class DataTypeService
    {
        public static List<FieldDataType> GetDataTypes()
        {
            return new List<FieldDataType>()
            {
                new FieldDataType(){ Type="ALPHANUM"},
                new FieldDataType(){ Type="COMBO"},
                new FieldDataType(){ Type="DIGIT"}
            };
        }
        public static string GetHtmlInputType(string type)
        {
            switch (type)
            {
                case "ALPHANUM":
                    return "text";
                case "DIGIT":
                    return "number";
                case "COMBO":
                    return "doropdown";
                default:
                    return "text";
            };
        }
    }
}
