using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using mj.connect;
using Dapper;
using MySql;

namespace mj.service.Controllers;

[ApiController]
public class QueryController : ControllerBase
{
    private readonly IDataBases _databases;
    private readonly ILogger<QueryController> _logger;
    public QueryController(IDataBases databases,
        ILogger<QueryController> logger)
    {
        _databases = databases;
        _logger = logger;
    }

    [HttpGet("queries")]
    public async Task<IActionResult> GetQuery()
    {
        var result = await _databases.SelectAllSqls().ConfigureAwait(false);
        return Ok(result);
    }

    [HttpPut("queries/{id}")]
    public async Task<IActionResult> GetQuery(string id, [FromBody]object jsonParameter )
    {
        var result = await _databases.SelectAsync<dynamic>(id, jsonParameter).ConfigureAwait(false);
        return Ok(result);

        // using (var connection = _databases.Connect()) 
        // {
        //     await connection.OpenAsync().ConfigureAwait(false);            
        //     // TO-DO : 공통화
        //     var sql = await Dapper.SqlMapper.QueryFirstAsync<string>(connection, $"select SQL_CONTENT from SQL_STORAGE where id = '{id}'").ConfigureAwait(false);

        //     if (!string.IsNullOrWhiteSpace(sql)) {
        //         var result = await Dapper.SqlMapper.QueryAsync<dynamic>(connection, sql).ConfigureAwait(false);
        //         return Ok(result);
        //     }
        //     else 
        //     {
        //         return BadRequest("empty sql data");
        //     }
        // }   
    }
}
