using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
//using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections.Specialized;
using System.Configuration;
using Newtonsoft.Json;
using AspNetWebApi.DA.ViewModels.System;
using System.Web.Configuration;
using System.Text;

namespace AspNetWebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class MyAuth : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                var re = actionContext.Request;
                var headers = re.Headers;
                if (headers.Contains("Token"))
                {
                    string token = headers.GetValues("Token").First();
                    NameValueCollection myKeys = ConfigurationManager.AppSettings;
                    string VSS = myKeys["VSS"];
                    var strUserPayload = JsonWebToken.Decode(token, VSS);
                    var oUserPayload = JsonConvert.DeserializeObject<UserPayload>(strUserPayload);
                    var TokenExpireDate = oUserPayload.CreateDate.AddMinutes(oUserPayload.TokenExpire);
                    if (TokenExpireDate < DateTime.Now)
                    {
                        //actionContext.Response = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.OK);
                        /*HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                        var stringPayload = JsonConvert.SerializeObject(new { message = "Session expired, please login"});
                        var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                        response.Content = httpContent;
                        actionContext.Response = response;*/
                    }
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
            catch
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}