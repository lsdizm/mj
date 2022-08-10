using mj.model;

namespace mj.connect {
    public interface IDataAPI
    {
        Task<List<RaceApiResult>> GetRaceResult(DateTime fromDate, DateTime toDate);
        Task<List<HorseResult>> GetHorceResult(string meet, string rank);
    }
}
