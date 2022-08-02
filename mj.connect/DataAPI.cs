using System.Web;
using System.Dynamic;
//using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using mj.model;

namespace mj.connect {
    public class DataAPI : IDataAPI
    {
        private const string serviceKey = "gKTNtNTmRwLKq8JD1zkpfaggw28u5FJ%2F%2BCZ3PpQxX15sOjBrSoWWMf2oSe3dG%2BJqsIcXim5EW5xlTx1jxGqKgA%3D%3D";
        public DataAPI()
        {
        }

        private HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://apis.data.go.kr/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private async Task<List<T>> GetFromAPI<T>(string path, Dictionary<string, string> parameters)
        {
            var result = new List<T>();
            var pageNo = 1;

            parameters.TryAdd("pageNo", "1");
            parameters.TryAdd("numOfRows", "10");
            parameters.TryAdd("serviceKey", serviceKey);

            using (var client = GetHttpClient())
            {
                while (true) 
                {
                    try
                    {
                        var queryString = string.Join("&", parameters.Select(s => string.Format("{0}={1}", s.Key, s.Value)));
                        var response = await client.GetAsync($"{path}?{queryString}").ConfigureAwait(false);
                        response.EnsureSuccessStatusCode();
                        var responseString = await response.Content.ReadAsStringAsync();                
                        var apiResult = JsonSerializer.Deserialize<ApiResult>(responseString);

                        if (apiResult == null || apiResult.response  == null || apiResult.response.body == null ||
                            apiResult.response.body.totalCount.HasValue == false)
                        {
                            break;
                        }
                        else 
                        {
                            if (apiResult.response.body.items != null && apiResult.response.body.items.item != null)
                            {
                                //result.AddRange(apiResult.response.body.items.item);
                            }
                        }
                        pageNo = pageNo + 1;
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }

                return result;
            }
        }

        public async Task<List<RaceResult>> GetRaceResult(DateTime fromDate, DateTime toDate)
        {
            var path = "B551015/API186/SeoulRace";
            var result = new List<RaceResult>();
            var pageNo = 1;
            var numOfRows = 10;
            var rc_date_fr = fromDate.ToString("yyyyMMdd");
            var rc_date_to = toDate.ToString("yyyyMMdd");

            using (var client = GetHttpClient())
            {
                while (true) 
                {
                    try
                    {
                        var response = await client.GetAsync($"{path}?pageNo={pageNo}&numOfRows={numOfRows}&rc_date_fr={rc_date_fr}&rc_date_to={rc_date_to}&serviceKey={serviceKey}").ConfigureAwait(false);
                        response.EnsureSuccessStatusCode();
                        var responseString = await response.Content.ReadAsStringAsync();                
                        var apiResult = JsonSerializer.Deserialize<ApiResult>(responseString);

                        if (apiResult == null || apiResult.response  == null || apiResult.response.body == null ||
                            apiResult.response.body.totalCount.HasValue == false)
                        {
                            break;
                        }
                        else 
                        {
                            if (apiResult.response.body.items != null && apiResult.response.body.items.item != null){
                                result.AddRange(apiResult.response.body.items.item);
                            }
                        }
                        pageNo = pageNo + 1;
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }

                return result;
            }
        }
    }
}

