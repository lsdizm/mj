namespace mj.connect {
    public interface IDataBases 
    {
        MySql.Data.MySqlClient.MySqlConnection Connect();
        Task<int> ExecuteAsync(string sql, MySql.Data.MySqlClient.MySqlConnection connection);

        Task<List<string>> SelectAllSqls();
        Task<List<T>> SelectAsync<T>(string sqlId, object jsonParameters);
    }
}
