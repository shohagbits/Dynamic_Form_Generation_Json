using System.Collections.Generic;

namespace Dynamic_Form_Generation_Json.Data
{
    public class DataTypeService
    {
        public string Type { get; set; }
        public static List<DataTypeService> GetDataType()
        {
            return new List<DataTypeService>()
            {
                new DataTypeService(){ Type="ALPHANUM"},
                new DataTypeService(){ Type="COMBO"},
                new DataTypeService(){ Type="DIGIT"}
            };
        }
    }
}
