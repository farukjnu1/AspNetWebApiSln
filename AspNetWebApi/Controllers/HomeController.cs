using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspNetWebApi.Attributes;
using AspNetWebApi.DA.ViewModels.Operation;

namespace AspNetWebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            
            return View();
        }

        public ActionResult Test(string vin)
        {
            ClientVehicleVM oClientVehicle = new ClientVehicleVM();
            // https://carvx.jp/search/new?chassis_number=NKE165-7072856
            // https://www.carjam.co.nz/japan-history:car?chassis=
            string baseURL = "https://www.carjam.co.nz";
            string relativeURL = "/japan-history:car";
            string queryString = "?chassis=" + vin;
            string address = baseURL + relativeURL + queryString;
            string Url = address.ToLower();
            string resultAsString = "";
            try 
            {
                using (WebClient client = new WebClient())
                {
                    /*ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                           | SecurityProtocolType.Tls11
                           | SecurityProtocolType.Tls12
                           | SecurityProtocolType.Ssl3;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);*/

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    byte[] result = client.DownloadData(Url);
                    resultAsString = System.Text.Encoding.UTF8.GetString(result);

                    string[] lines = resultAsString.Split(new string[] { "\r\n", "\r", "\n" },StringSplitOptions.None);
                    
                    bool isYear = false, isChassis = false, isBody = false, isEngine = false, isTranmission = false;
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Contains("japan-year") == true && isYear == false)
                        {
                            isYear = true;
                            oClientVehicle.From = lines[i].Replace(@"<span class=""japan-year"">", "").Replace(@"</span>", "").Trim();
                            oClientVehicle.Manufacturer = lines[i + 1].Replace(@"<span class=""japan-make"">", "").Replace(@"</span>", "").Trim();
                            oClientVehicle.Model = lines[i + 2].Replace(@"<span class=""japan-model"">", "").Replace(@"</span>", "").Trim();
                        }
                        if (lines[i].Contains("Chassis/VIN:") == true && isChassis == false)
                        {
                            isChassis = true;
                            oClientVehicle.Vin = lines[i + 1].Replace(@"<td>", "").Replace(@"</td>", "").Trim();
                        }
                        if (lines[i].Contains("Body:") == true && isBody == false)
                        {
                            isBody = true;
                            oClientVehicle.Body = lines[i + 1].Replace(@"<td>", "").Replace(@"</td>", "").Trim();
                        }
                        if (lines[i].Contains("Engine:") == true && isEngine == false)
                        {
                            isEngine = true;
                            oClientVehicle.Engine = lines[i + 1].Replace(@"<td>", "").Replace(@"</td>", "").Trim();
                        }
                        if (lines[i].Contains("Transmission:") == true && isTranmission == false)
                        {
                            isTranmission = true;
                            oClientVehicle.Transmission = lines[i + 1].Replace(@"<td>", "").Replace(@"</td>", "").Trim();
                        }
                    }
                }
            }
            catch (Exception ex) 
            {

            }
            return Content(resultAsString);
        }

    }
}
