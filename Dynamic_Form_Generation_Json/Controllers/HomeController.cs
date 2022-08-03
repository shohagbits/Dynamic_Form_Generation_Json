using Dynamic_Form_Generation_Json.Data;
using Dynamic_Form_Generation_Json.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic_Form_Generation_Json.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var readJsonData = String.Empty;
            using (StreamReader r = new StreamReader("./Data/jsonData_Initial.json"))
            {
                readJsonData = r.ReadToEnd();
            }
            FormGeneration fields = JsonConvert.DeserializeObject<FormGeneration>(readJsonData);
            #region FormGeneration
            //var model = new FormGeneration()
            //{
            //    RECORDSET = new RECORDSET()
            //    {
            //        GETDELIVERYOPTIONTEMPLATE = new List<GETDELIVERYOPTIONTEMPLATE>()
            //        {
            //            new GETDELIVERYOPTIONTEMPLATE() { PRODUCT = "UNIVERSAL ADDRESS DLV          H2H",CATEGORY = "USAEPT",  T_INDEX = 2, DESCRIPTION = "receiver.bank_account.routing_number;ABA Routing Number; 9; 1231; ABA Routing Number required; 0; ALPHANUM; NL; 9;" },
            //            new GETDELIVERYOPTIONTEMPLATE() { PRODUCT = "UNIVERSAL ADDRESS DLV          H2H",CATEGORY = "USAEPT", T_INDEX = 63, DESCRIPTION = "receiver.mobile_phone.phone_number.national_number;Receiver Phone Number; 10; 1479; Receiver Phone required; 0; DIGIT; NL; 10;" }
            //        }
            //    }
            //};
            //var serializeData = System.Text.Json.JsonSerializer.Serialize(model);
            //FormGeneration myDeserializedClass = System.Text.Json.JsonSerializer.Deserialize<FormGeneration>(serializeData);

            #endregion

            var recordsTemplates = fields.RECORDSET.GETDELIVERYOPTIONTEMPLATE.OrderBy(a => a.T_INDEX).ToList();

            var dataTypesList = DataTypeService.GetDataTypes();
            int dropDownItemCount = 0;
            int appendDropdownItemCount = 0;
            StringBuilder aDynamicFormDesign = new StringBuilder();
            for (int i = 0; i < recordsTemplates.Count; i++)
            {
                var t_index = recordsTemplates[i].T_INDEX;
                if (t_index == 0)
                    continue;

                var descriptionArray = recordsTemplates[i].DESCRIPTION?.Split(';');
                if (descriptionArray.Length > 0)
                {
                    var labelText = descriptionArray[1];
                    var inputId = descriptionArray[0];
                    var inputType = String.Empty;
                    var placeholder = String.Empty;

                    //var category = recordsTemplates[i].CATEGORY;
                    var singlePropertyDesign = String.Empty;
                    foreach (var type in dataTypesList)
                    {
                        var typeOfField = descriptionArray.FirstOrDefault(a => !String.IsNullOrWhiteSpace(a) && a.ToUpper().Contains(type.Type));
                        if (typeOfField != null)
                        {
                            inputType = DataTypeService.GetHtmlInputType(typeOfField);
                            break;
                        }
                    }
                    if (inputType == "doropdown")
                    {
                        dropDownItemCount = Int32.Parse(descriptionArray.Last());
                        appendDropdownItemCount++;
                        singlePropertyDesign = String.Format(@"<div class='form-group row'>
                                                            <label for='{1}' class='col-sm-2 col-form-label'>{0}</label>
                                                            <div class='col-sm-10'>
                                                                <input type='{2}' class='form-control' id='{1}' name='{1}' placeholder='{3}'>
                                                            </div>
                                                        </div>", descriptionArray[1], descriptionArray[0], inputType, placeholder);

                    }
                    else
                    {
                        singlePropertyDesign = String.Format(@"<div class='form-group row'>
                                                            <label for='{1}' class='col-sm-2 col-form-label'>{0}</label>
                                                            <div class='col-sm-10'>
                                                                <input type='{2}' class='form-control' id='{1}' name='{1}' placeholder='{3}'>
                                                            </div>
                                                        </div>", descriptionArray[1], descriptionArray[0], inputType, placeholder);
                    }
                    aDynamicFormDesign.AppendFormat(singlePropertyDesign);
                }
            }
            ViewBag.FormDesignFields = aDynamicFormDesign.ToString();
            return View();
        }

        [HttpPost]
        public ActionResult DynamicForm(string formData = "")
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
