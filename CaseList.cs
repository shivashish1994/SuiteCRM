using Newtonsoft.Json;
using RestSharp;
using SuitecrmCore.Case.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuitecrmCore.SuiteCrmIntegration
{
    public static class CaseList
    {
        public static CaseListGet GetListofCases(string sessionId,string sugarCrmUrl)
        {
            CaseListGet caseListGet = new CaseListGet();
            try
            {
                if(!string.IsNullOrEmpty(sessionId))
                {
                    
                    var client = new RestClient(sugarCrmUrl);
                    var noteparamss = new
                    {
                        session = sessionId,
                        module_name = "Cases",
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
                  caseListGet =JsonConvert.DeserializeObject<CaseListGet>(responses.Content.ToString());
                    List<SubmittedCaseModel> submittedCaseModels = new List<SubmittedCaseModel>();
                    if(caseListGet!=null)
                    {
                        caseListGet.entry_list.ForEach(x => 
                        {
                            int casenumber = Convert.ToInt32(x.name_value_list.case_number.value);
                            submittedCaseModels.Add(new SubmittedCaseModel 
                            {
                                caseId=x.id,
                                casenumber= casenumber,
                                category=x.name_value_list.case_category_c.value,
                                subject=x.name_value_list.name.value,
                                status=x.name_value_list.status.value

                            });
                        });
                        submittedCaseModels = submittedCaseModels.OrderByDescending(x => x.casenumber).ToList();
                    }
                    caseListGet.submittedCaseModels = submittedCaseModels;
                    return caseListGet;
                }
                else
                {
                    caseListGet.Is_error = "Please provide valid data";
                }
            }
            catch(Exception ex)
            {
                caseListGet.Is_error = ex.Message;
            }
            return caseListGet;
        }
    }
}
