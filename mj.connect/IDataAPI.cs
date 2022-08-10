using mj.model;

namespace mj.connect {
    public interface IDataAPI
    {
        Task<List<RaceResultApi>> GetRaceResult(DateTime fromDate, DateTime toDate);
        Task<List<RaceApi>> GetRaceResultDetail(DateTime fromDate, DateTime toDate);
        Task<List<HorseApi>> GetHorceResult(string meet, string rank);
    }
}
