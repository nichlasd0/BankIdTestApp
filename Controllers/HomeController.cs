using BankIdTestApp.Helpers;
using BankIdTestApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BankIdTestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            ViewBag.Check = "false";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ThisDevice(string ssn)
        {

            var remoteIp = HttpContext.Connection.RemoteIpAddress;
            var ip = remoteIp.IsIPv4MappedToIPv6 ? remoteIp.MapToIPv4().ToString() : remoteIp.ToString();
            var response = await Bankid.QrCode(ssn, ip);

            ViewBag.ThisDevice = "bankid:///?autostarttoken=" + response.autoStartToken;


            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> QrCode(string ssn)
        {
            var remoteIp = HttpContext.Connection.RemoteIpAddress;
            var ip = remoteIp.IsIPv4MappedToIPv6 ? remoteIp.MapToIPv4().ToString() : remoteIp.ToString();

            var response = await Bankid.QrCode(ssn, ip);
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode("bankid:///?autostarttoken=" + response.autoStartToken,
                    QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                using (Bitmap bitMap = qrCode.GetGraphic(20))
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }

            return View("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Index(string ssn)
        {
            var remoteIp = HttpContext.Connection.RemoteIpAddress;
            var ip = remoteIp.IsIPv4MappedToIPv6 ? remoteIp.MapToIPv4().ToString() : remoteIp.ToString();

            var response = await Bankid.CertAsync(ssn, ip);

            ViewBag.Check = "true";

            return View(response);
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
