using Dynamic_Form_Generation_Json.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
