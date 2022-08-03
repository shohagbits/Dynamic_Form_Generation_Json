using Dynamic_Form_Generation_Json.Data;
using Dynamic_Form_Generation_Json.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            var model = new FormGeneration()
            {
                RECORDSET = new RECORDSET()
                {
                    GETDELIVERYOPTIONTEMPLATE = new List<GETDELIVERYOPTIONTEMPLATE>()
                    {
                        new GETDELIVERYOPTIONTEMPLATE() { PRODUCT = "UNIVERSAL ADDRESS DLV          H2H",CATEGORY = "USAEPT",  T_INDEX = 2, DESCRIPTION = "receiver.bank_account.routing_number;ABA Routing Number; 9; 1231; ABA Routing Number required; 0; ALPHANUM; NL; 9;" },
                        new GETDELIVERYOPTIONTEMPLATE() { PRODUCT = "UNIVERSAL ADDRESS DLV          H2H",CATEGORY = "USAEPT", T_INDEX = 63, DESCRIPTION = "receiver.mobile_phone.phone_number.national_number;Receiver Phone Number; 10; 1479; Receiver Phone required; 0; DIGIT; NL; 10;" }
                    }
                }
            };
            var serializeData = JsonSerializer.Serialize(model);
            FormGeneration myDeserializedClass = JsonSerializer.Deserialize<FormGeneration>(serializeData);
            var recordsTemplates = myDeserializedClass.RECORDSET.GETDELIVERYOPTIONTEMPLATE.OrderBy(a => a.T_INDEX).ToList();

            var dataTypesList = DataTypeService.GetDataType();

            StringBuilder aDynamicFormDesign = new StringBuilder();
            for (int i = 0; i < recordsTemplates.Count; i++)
            {
                var product = recordsTemplates[i].PRODUCT;
                var category = recordsTemplates[i].CATEGORY;
                var t_index = recordsTemplates[i].T_INDEX;
                var description = recordsTemplates[i].DESCRIPTION;

                var singlePropertyDesign = String.Format(@"<div class='form-group row'>
                                                            <label for='' class='col-sm-2 col-form-label'>{0}</label>
                                                            <div class='col-sm-10'>
                                                                <input type='text' class='form-control' id='' placeholder='{1}'>
                                                            </div>
                                                        </div>", product, category);
                aDynamicFormDesign.AppendFormat(singlePropertyDesign);
            }
            ViewBag.FormDesignFields = aDynamicFormDesign.ToString();
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
