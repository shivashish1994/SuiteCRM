using Newtonsoft.Json;
using RestSharp;
using SuiteCRMCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuiteCRMCore.Models.EditCase;
namespace SuiteCRMCore.SuiteCrmIntegration
{
    public static class Editcase
    {
        public static AddeditCase EditCasebyId(string sessionId,string caseId, string sugarCrmUrl)
        {
            AddeditCase editCase = new AddeditCase();
           
            var client = new RestClient(sugarCrmUrl);
            var editparamss = new
            {
                session = sessionId,
                module_name = "Cases",
                id = caseId,
                select_fields=string.Empty,
                link_name_to_fields_array=string.Empty,
                track_view=true
            };
            var requests = new RestRequest();
            requests.AddParameter("method", "get_entry");
            requests.AddParameter("input_type", "JSON");
            requests.AddParameter("response_type", "JSON");
            requests.AddParameter("rest_data", JsonConvert.SerializeObject(editparamss));
            var responses = client.Execute(requests);
            var requestData = JsonConvert.DeserializeObject<EditCaseModel>(responses.Content.ToString());
            editCase.id = requestData.entry_list.FirstOrDefault().id;
            editCase.nutp_msisdn_c = requestData.entry_list.FirstOrDefault().name_value_list.nutp_msisdn_c.value;
            editCase.case_category_c = requestData.entry_list.FirstOrDefault().name_value_list.case_category_c.value;
            editCase.case_subtype_c = requestData.entry_list.FirstOrDefault().name_value_list.case_subcategory_c.value;
            editCase.name = requestData.entry_list.FirstOrDefault().name_value_list.name.value;
            editCase.description = requestData.entry_list.FirstOrDefault().name_value_list.description.value;
            editCase.status = requestData.entry_list.FirstOrDefault().name_value_list.status.value;
            editCase.resolution = requestData.entry_list.FirstOrDefault().name_value_list.resolution.value;
            editCase.date= requestData.entry_list.FirstOrDefault().name_value_list.date_entered.value;
            editCase.case_number = requestData.entry_list.FirstOrDefault().name_value_list.case_number.value;
            return editCase;
        }
}
}