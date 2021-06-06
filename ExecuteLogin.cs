using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuitecrmCore.Login.Models;
using Newtonsoft.Json;

namespace SuitecrmCore.SuiteCrmIntegration
{
    public static class ExecuteLogin
    {
        public static string ExecuteLoginSCRM(string sugarCrmUrl,string sugarCrmUsername,string sugarCrmPassword)
        {
            try
            {
                
                string user_name = sugarCrmUsername; string password = sugarCrmPassword;
                var paramss = new
                {
                    user_auth = new
                    {
                        user_name = user_name,
                        password = password,
                        encryption = "PLAIN"
                    },

                };
                var client = new RestClient(sugarCrmUrl);
                var request = new RestRequest();
                request.AddParameter("method", "login");
                request.AddParameter("input_type", "JSON");
                request.AddParameter("response_type", "JSON");
                request.AddParameter("rest_data", JsonConvert.SerializeObject(paramss));

                var response = client.Execute(request);
                var responseData = JsonConvert.DeserializeObject<LoginSuiteCRM>(response.Content.ToString());

                return responseData.id;
            }
            catch (Exception ex)
            {

                return string.Empty;
            }
        }
    }
}
