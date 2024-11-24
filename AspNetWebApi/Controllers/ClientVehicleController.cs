using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Http.Cors;
using AspNetWebApi.Attributes;
using AspNetWebApi.BL.Operation;
using AspNetWebApi.BL.Stores;
using AspNetWebApi.DA.EF.VssDb;
using AspNetWebApi.DA.Utilities;
using AspNetWebApi.DA.ViewModels.Operation;

namespace AspNetWebApi.Controllers
{
    [MyAuth]
    public class ClientVehicleController : ApiController
    {
        // GET: api/ClientVehicle
        public IEnumerable<ClientVehicleVM> Get([FromUri] string phone, string vehicle, int pi = 0, int ps = 10)
        {
            ClientVehicleBL _BL = new ClientVehicleBL();
            return _BL.Get(phone, vehicle, pi, ps);
        }

        public ClientVehicleVM GetById(long id)
        {
            ClientVehicleBL _BL = new ClientVehicleBL();
            return _BL.Get(id);
        }

        // POST: api/ClientVehicle
        public bool Post([FromBody] ClientVehicleVM model)
        {
            ClientVehicleBL _BL = new ClientVehicleBL();
            return _BL.Add(model);
        }

        // PUT: api/ClientVehicle/5
        public bool Put([FromBody] ClientVehicleVM model)
        {
            ClientVehicleBL _BL = new ClientVehicleBL();
            return _BL.Update(model);
        }

        [HttpGet]
        [Route("api/ClientVehicle/GetVehiclesByClient")]
        public IEnumerable<ClientVehicleVM> GetVehiclesByClient(long id)
        {
            ClientVehicleBL _BL = new ClientVehicleBL();
            return _BL.GetVehiclesByClient(id);
        }

        [HttpGet]
        [Route("api/ClientVehicle/GetManufacturer")]
        public IEnumerable<VehicleStda110UVm> GetManufacturer(string manufacturer, int offset = 0, int fetch = 20)
        {
            ClientVehicleBL _BL = new ClientVehicleBL();
            return _BL.GetManufacturer(manufacturer, offset, fetch);
        }

        [HttpGet]
        [Route("api/ClientVehicle/GetModel")]
        public IEnumerable<VehicleStda110UVm> GetModel(string manufacturer, string model, int offset = 0, int fetch = 20)
        {
            ClientVehicleBL _BL = new ClientVehicleBL();
            return _BL.GetModel(manufacturer, model, offset, fetch);
        }

        [HttpGet]
        [Route("api/ClientVehicle/GetSubModel")]
        public IEnumerable<VehicleStda110UVm> GetSubModel(string manufacturer, string model, string subModel, int offset = 0, int fetch = 20)
        {
            ClientVehicleBL _BL = new ClientVehicleBL();
            return _BL.GetSubModel(manufacturer, model, subModel, offset, fetch);
        }

        [HttpGet]
        [Route("api/ClientVehicle/GetFrom")]
        public IEnumerable<VehicleStda110UVm> GetFrom(string manufacturer, string model, string subModel, string from, int offset = 0, int fetch = 20)
        {
            ClientVehicleBL _BL = new ClientVehicleBL();
            return _BL.GetFrom(manufacturer, model, subModel, from, offset, fetch);
        }

        [HttpGet]
        [Route("api/ClientVehicle/GetTo")]
        public IEnumerable<VehicleStda110UVm> GetTo(string manufacturer, string model, string subModel, string from, string to, int offset = 0, int fetch = 20)
        {
            ClientVehicleBL _BL = new ClientVehicleBL();
            return _BL.GetTo(manufacturer, model, subModel, from, to, offset, fetch);
        }

        [HttpGet]
        [Route("api/ClientVehicle/GetCarByChassis")]
        public ClientVehicleVM GetCarByChassis([FromUri] string chassis)
        {
            ClientVehicleVM oClientVehicle = new ClientVehicleVM();
            // https://carvx.jp/search/new?chassis_number=NKE165-7072856
            // https://www.carjam.co.nz/japan-history:car?chassis=
            string baseURL = "https://www.carjam.co.nz";
            string relativeURL = "/japan-history:car";
            string queryString = "?chassis=" + chassis;
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

                    string[] lines = resultAsString.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

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
            return oClientVehicle;
        }

        [HttpGet]
        [Route("api/ClientVehicle/GetCarByVin")]
        public ClientVehicleVM GetCarByVin([FromUri] string vin)
        {
            ClientVehicleVM oClientVehicle = new ClientVehicleVM();
            // https://www.vindecoderz.com/EN/check-lookup/WDD2120472A171168
            string baseURL = "https://www.vindecoderz.com"; 
            string relativeURL = "/EN/check-lookup/";
            string queryString = vin;
            string address = baseURL + relativeURL + queryString;
            string Url = address.ToLower();
            //string resultAsString = "";

            /*try
            {
                CookieContainer cookies = new CookieContainer();
                SmsWebClient client = new SmsWebClient(cookies);
                string html = client.DownloadString(Url);
            }
            catch (Exception ex)
            {

            }*/

            /*try
            {
                WebClient client = null;
                while (client == null)
                {
                    Console.WriteLine("Trying..");
                    client = CloudflareEvader.CreateBypassedWebClient(Url);
                }
                //Console.WriteLine("Solved! We're clear to go");
                //Console.WriteLine(client.DownloadString("http://anilinkz.tv/anime-list"));
                var str = client.DownloadString(Url);
                //Console.ReadLine();
            }
            catch (Exception ex)
            {

            }*/

            /*try
            {
                using (WebClient client = new WebClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    byte[] result = client.DownloadData(Url);
                    resultAsString = System.Text.Encoding.UTF8.GetString(result);

                    string[] lines = resultAsString.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

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

            }*/
            return oClientVehicle;
        }

    }
}
