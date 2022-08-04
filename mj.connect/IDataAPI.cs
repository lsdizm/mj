using mj.model;

namespace mj.connect {
    public interface IDataAPI
    {
        Task<List<RaceResult>> GetRaceResult(DateTime fromDate, DateTime toDate);
        Task<List<HorseResult>> GetHorceResult(string meet, string rank);
    }
}
