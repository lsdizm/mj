using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using manger.Models;

namespace manger.Controllers;

[ApiController]
[Route("minings")]
public class MiningController : ControllerBase
{
    public MiningController()
    {
    }

    [HttpGet("race/schedules")]
    public async Task<IActionResult> CreateRaceSchedules()
    {
        var result = new GeneralResult(){
            IsCompleted = true,
            ResultText = "성공했습니다."
        };

        return await Task.FromResult(Ok(result)).ConfigureAwait(false);
    }
}
