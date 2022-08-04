﻿using Dynamic_Form_Generation_Json.Data;
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

            //using (StreamReader r = new StreamReader("./Data/Templates/FinalData.json"))
            //using (StreamReader r = new StreamReader("./Data/Templates/DeliveryServiceTemplate-LK-LKR.json"))
            using (StreamReader r = new StreamReader("./Data/Templates/DeliveryServiceTemplate-IN-INR.json"))
            {
                readJsonData = r.ReadToEnd();
            }
            Root fields = JsonConvert.DeserializeObject<Root>(readJsonData);
            //FormGeneration fields = JsonConvert.DeserializeObject<FormGeneration>(readJsonData);

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

            var recordsTemplates = fields.Envelope.Body.EspDasReply.MTML.REPLY.DATA_CONTEXT.RECORDSET.GETDELIVERYOPTIONTEMPLATE.OrderBy(a => a.T_INDEX).ToList();

            var dataTypesList = DataTypeService.GetDataTypes();
            int dropDownItemCount = 0;
            int appendDropdownItemCount = 0;
            var dropdownLabel = String.Empty;
            var dropdownId = String.Empty;
            var dropdownDesign = String.Empty;
            var maxSizeLength = String.Empty;

            StringBuilder aDynamicFormDesign = new StringBuilder();
            StringBuilder dropdownOptionList = new StringBuilder();
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
                    var isRequired = descriptionArray[4].Contains("required") ? "required" : "";

                    var inputType = String.Empty;
                    var placeholder = String.Empty;

                    var singlePropertyDesign = String.Empty;
                    var option = String.Empty;

                    if (dropDownItemCount == 0)
                    {
                        foreach (var type in dataTypesList)
                        {
                            var typeOfField = descriptionArray.Count() > 6 ? descriptionArray[6] : "";
                            if (!String.IsNullOrWhiteSpace(typeOfField))
                            {
                                inputType = DataTypeService.GetHtmlInputType(descriptionArray[6]);
                                break;
                            }
                        }
                    }
                    else if (dropDownItemCount > 0)
                    {
                        appendDropdownItemCount++;
                        option = String.Format(@"<option value='{0}'>{1}</option>", descriptionArray[2].Trim(), descriptionArray[3].Trim());
                        dropdownOptionList.AppendFormat(option);

                        //Finally Apped Dropdown Into Form
                        if (appendDropdownItemCount == dropDownItemCount)
                        {
                            dropdownDesign = String.Format(@"<div class='form-group row'>
                                                            <label for='{1}' class='col-sm-2 col-form-label {3}'>{0}</label>
                                                            <div class='col-sm-10'>
                                                                <select class='form-select form-control' id='{1}' name='{1}' aria-label='Default select example' {3}>
                                                                    <option selected>--Select--</option>
                                                                    {2}
                                                                </select>
                                                            </div>
                                                        </div>", dropdownLabel, dropdownId, dropdownOptionList.ToString(), isRequired);
                            aDynamicFormDesign.AppendFormat(dropdownDesign);
                            dropDownItemCount = 0;
                            appendDropdownItemCount = 0;
                            dropdownOptionList = new StringBuilder();
                            dropdownDesign = String.Empty;
                        }
                    }

                    if (!String.IsNullOrWhiteSpace(inputType) && inputType == "upperstring" && appendDropdownItemCount == 0)
                    {
                        maxSizeLength = descriptionArray[2];
                        singlePropertyDesign = String.Format(@"<div class='form-group row'>
                                                            <label for='{1}' class='col-sm-2 col-form-label {5}'>{0}</label>
                                                            <div class='col-sm-10'>
                                                                <input type='{2}' class='form-control text-uppercase' id='{1}' name='{1}' placeholder='{3}' maxlength='{4}' {5} >
                                                            </div>
                                                        </div>", descriptionArray[1], descriptionArray[0], "text", placeholder, maxSizeLength, isRequired);
                        aDynamicFormDesign.AppendFormat(singlePropertyDesign);
                    }
                    else if (!String.IsNullOrWhiteSpace(inputType) && inputType != "doropdown" && appendDropdownItemCount == 0)
                    {
                        maxSizeLength = descriptionArray[2];

                        singlePropertyDesign = String.Format(@"<div class='form-group row'>
                                                            <label for='{1}' class='col-sm-2 col-form-label {5}'>{0}</label>
                                                            <div class='col-sm-10'>
                                                                <input type='{2}' class='form-control' id='{1}' name='{1}' placeholder='{3}' maxlength='{4}' {5}>
                                                            </div>
                                                        </div>", descriptionArray[1], descriptionArray[0], inputType, placeholder, maxSizeLength, isRequired);
                        aDynamicFormDesign.AppendFormat(singlePropertyDesign);
                    }
                    else if (!String.IsNullOrWhiteSpace(inputType) && inputType == "doropdown" && appendDropdownItemCount == 0)
                    {
                        int count = descriptionArray.Length - 2;
                        dropDownItemCount = Int32.Parse(descriptionArray[count]);

                        dropdownLabel = descriptionArray[1];
                        dropdownId = descriptionArray[0];
                    }
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
