using System;

namespace mj.connect {
    public class DataBases : IDataBases
    {
        public DataBases()
        {
        }

        // TO-DO connection 정보 관리 방안
        // TO-DO Search 공통화, 파라미터 바인딩 방안.
        public MySql.Data.MySqlClient.MySqlConnection Connect()
        {
            var _connectionString =  "host=152.70.232.248;port=3306;user id=mj;password=!Dhfkzmffkdnem1;database=mj;";
            return new MySql.Data.MySqlClient.MySqlConnection(_connectionString);
        }

        public async Task<int> ExecuteAsync(string sql, MySql.Data.MySqlClient.MySqlConnection connection)
        {
            var command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);
            var result = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            return result;
        }
    }
}

