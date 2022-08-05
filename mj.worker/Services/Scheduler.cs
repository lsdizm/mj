using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using mj.connect;
using mj.model;

public class Scheduler : IScheduler
{
    private readonly ILogger<Scheduler> _logger;
    private readonly IDataBases _databases;
    private readonly IDataAPI _dataapi;

    public Scheduler(ILogger<Scheduler> logger,
        IDataAPI dataapi,
        IDataBases databases)
    {
        _databases = databases;
        _dataapi = dataapi;
        _logger = logger;
    }
    
    public async Task RunScheduleAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var delayTime = 24*60*60*1000;
            await PostAsyncApi().ConfigureAwait(false);            
            await Task.Delay(delayTime, stoppingToken).ConfigureAwait(false);
        }
    }

    private async Task PostAsyncApi()
    {
        var rc_date_fr = DateTime.Now.AddDays(-7);
        var rc_date_to = DateTime.Now.AddDays(7);

        var result = await _dataapi.GetRaceResult(rc_date_fr, rc_date_to).ConfigureAwait(false);

        if (result.Any()) 
        {
            using (var connection = _databases.Connect()) 
            {
                await connection.OpenAsync().ConfigureAwait(false);

                foreach (var item in result) 
                {
                    var sql = $"insert into RACE_RESULT (chaksun, diffTot, divide, hrName, hrno, jkName, jkNo, meet, noracefl, prow, prowName, prtr, prtrName, rankKind, rc10dusu, rcAge, rcBudam, rcChul, rcCode, rcDate, rcDiff2, rcDiff3, rcDiff4, rcDiff5, rcDist, rcFrflag, rcGrade, rcHrfor, rcHrnew, rcNo, rcNrace, rcOrd, rcP1Odd, rcP2Odd, rcP3Odd, rcP4Odd, rcP5Odd, rcP6Odd, rcP8Odd, rcPlansu, rcRank, rcSex, rcSpcbu, rcTime, rcVtdusu, rundayth, track, weath, wgHr) values ({item.chaksun}, {item.diffTot}, {item.divide}, '{item.hrName}', '{item.hrno}', '{item.jkName}', '{item.jkNo}', '{item.meet}', '{item.noracefl}', {item.prow}, '{item.prowName}', '{item.prtr}', '{item.prtrName}', {item.rankKind}, {item.rc10dusu}, '{item.rcAge}', {item.rcBudam}, {item.rcChul}, '{item.rcCode}', {item.rcDate}, {item.rcDiff2}, {item.rcDiff3}, {item.rcDiff4}, {item.rcDiff5}, {item.rcDist}, '{item.rcFrflag}', '{item.rcGrade}', '{item.rcHrfor}', '{item.rcHrnew}', {item.rcNo}, '{item.rcNrace}', {item.rcOrd}, {item.rcP1Odd}, {item.rcP2Odd}, {item.rcP3Odd}, {item.rcP4Odd}, {item.rcP5Odd}, {item.rcP6Odd}, {item.rcP8Odd}, {item.rcPlansu}, '{item.rcRank}', '{item.rcSex}', {item.rcSpcbu}, {item.rcTime}, {item.rcVtdusu}, {item.rundayth}, '{item.track}', '{item.weath}', {item.wgHr});";
                    await _databases.ExecuteAsync(sql, connection).ConfigureAwait(false);
                }

                await _databases.SaveLog(connection, "RACE_RESULT", DateTime.Now, 
                    string.Format("{0} ~ {1} : {2}", rc_date_fr.ToString("yyyyMMdd"), rc_date_to.ToString("yyyyMMdd"), result.Count().ToString()));
            }   
        }
    }


    private async Task PostAsync()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://193.122.127.59:7030/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        try
        {

            var response = await client.PostAsync("schedulers?remark=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), null).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
        }
    }

    

    /*
        https://apis.data.go.kr/B551015/API186/SeoulRace?pageNo=1&numOfRows=10&rc_date_fr=20220701&rc_date_to=20220720&serviceKey=gKTNtNTmRwLKq8JD1zkpfaggw28u5FJ%2F%2BCZ3PpQxX15sOjBrSoWWMf2oSe3dG%2BJqsIcXim5EW5xlTx1jxGqKgA%3D%3D
    */

}