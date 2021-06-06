using Newtonsoft.Json;
using RestSharp;
using SuitecrmCore.Case.Models;
using SuiteCRMCore.Models;
using SuiteCRMCore.SuiteCrmIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuiteCRMCore.Models.EditCase;
namespace SuitecrmCore.SuiteCrmIntegration
{
    public static class InsertUpdateCase
    {
        public static CaseUpIn Insertcase(string sessionId, AddeditCase model, string sugarCrmUrl)
        {
            CaseUpIn result = new CaseUpIn();
            try
            {
                if (!string.IsNullOrEmpty(sessionId)&& model!=null)
                {
                    List<InsertCaseData> insertCaseDatas = new List<InsertCaseData>();
                    if(string.IsNullOrEmpty(model.id))
                    {
                        insertCaseDatas.Add(new InsertCaseData { name = "nutp_msisdn_c", value = model.nutp_msisdn_c });
                        insertCaseDatas.Add(new InsertCaseData { name = "case_category_c", value = model.case_category_c });
                        insertCaseDatas.Add(new InsertCaseData { name = "case_subcategory_c", value = model.case_subtype_c });
                        insertCaseDatas.Add(new InsertCaseData { name = "name", value = model.name });
                        insertCaseDatas.Add(new InsertCaseData { name = "description", value = model.description });
                        insertCaseDatas.Add(new InsertCaseData { name = "status", value = "Open_New" });
                    }
                    else
                    {
                        insertCaseDatas.Add(new InsertCaseData { name = "id", value = model.id });
                        insertCaseDatas.Add(new InsertCaseData { name = "nutp_msisdn_c", value = model.nutp_msisdn_c });
                        insertCaseDatas.Add(new InsertCaseData { name = "case_category_c", value = model.case_category_c });
                        insertCaseDatas.Add(new InsertCaseData { name = "case_subcategory_c", value = model.case_subtype_c });
                        insertCaseDatas.Add(new InsertCaseData { name = "name", value = model.name });
                        insertCaseDatas.Add(new InsertCaseData { name = "description", value = model.description });
                    }
                    var client = new RestClient(sugarCrmUrl);
                    var noteparamss = new
                    {
                        session = sessionId,
                        module_name = "Cases",
                        name_value_list = insertCaseDatas.ToArray()
                    };
                    var requests = new RestRequest();
                    requests.AddParameter("method", "set_entry");
                    requests.AddParameter("input_type", "JSON");
                    requests.AddParameter("response_type", "JSON");
                    requests.AddParameter("rest_data", JsonConvert.SerializeObject(noteparamss));
                    var responses = client.Execute(requests);
                    result = JsonConvert.DeserializeObject<CaseUpIn>(responses.Content.ToString());
                   var data= Editcase.EditCasebyId(sessionId,result.id,sugarCrmUrl);
                    result.id = data.case_number;

                }
                else
                {
                    result.Is_error = "Please provide valid data";
                }
            }
            catch (Exception ex)
            {
                result.Is_error = ex.Message;
            }
            return result;
        }
    }
}
