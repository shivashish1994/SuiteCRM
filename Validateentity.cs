using Newtonsoft.Json;
using RestSharp;
using SuiteCRMCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuiteCRMCore.SuiteCrmIntegration
{
    public class Validateentity
    {
        public static bool ValidateMSDISDN(string sessionId, string sugarCrmUrl,string NRIC,string MSDISDN)
        {
            //,string NRIC,string MSISDN
            bool Is_Found = false;
            try
            {
                NUTPCrmModel nUTPCrmModel = new NUTPCrmModel();
                if (!string.IsNullOrEmpty(sessionId))
                {

                    var client = new RestClient(sugarCrmUrl);
                    var noteparamss = new
                    {
                        session = sessionId,
                        module_name = "NUTPP_NUTP_PICs_Module",
                        query = string.Empty,
                        order_by = string.Empty,
                        offset = 0,
                        select_fields = string.Empty,
                        link_name_to_fields_array = string.Empty,
                        max_results = 1000,
                        deleted = false,
                        favorites = false
                    };
                    var requests = new RestRequest();
                    requests.AddParameter("method", "get_entry_list");
                    requests.AddParameter("input_type", "JSON");
                    requests.AddParameter("response_type", "JSON");
                    requests.AddParameter("rest_data", JsonConvert.SerializeObject(noteparamss));
                    var responses = client.Execute(requests);
                    nUTPCrmModel= JsonConvert.DeserializeObject<NUTPCrmModel>(responses.Content.ToString());
                    if (nUTPCrmModel.entry_list.Where(x => x.name_value_list.phone_mobile.value == MSDISDN).FirstOrDefault() != null)
                    {
                        Is_Found= true;
                    }
                    else if(nUTPCrmModel.entry_list.Where(x => x.name_value_list.phone_other.value == MSDISDN).FirstOrDefault() != null)
                    {
                        Is_Found = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Is_Found;
        }
    }
}
