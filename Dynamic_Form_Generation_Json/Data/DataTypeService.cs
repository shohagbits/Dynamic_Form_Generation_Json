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
                new FieldDataType(){ Type="UPRSTRING"},
                new FieldDataType(){ Type="ALPHANUM"},
                new FieldDataType(){ Type="COMBO"},
                new FieldDataType(){ Type="DIGIT"},
            };
        }
        public static string GetHtmlInputType(string type)
        {
            var inputType=type.Trim();
            switch (inputType)
            {
                case "ALPHANUM":
                    return "text";  
                case "UPRSTRING":
                    return "upperstring";
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
