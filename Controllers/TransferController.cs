using Contexts;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class TransferController : ControllerBase
{
    private readonly PostgreSqlContext _postgreSqlContext;

    public TransferController(PostgreSqlContext postgreSqlContext)
    {
        _postgreSqlContext = postgreSqlContext;
    }

    [HttpGet]
    public List<TransferModel> Get()
    {
        var transfers = _postgreSqlContext.Transfers.ToList();

        return transfers;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Post([FromBody] TransferModel transfer)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _postgreSqlContext.Transfers.Add(transfer);
        await _postgreSqlContext.SaveChangesAsync();

        return CreatedAtAction(nameof(Post), new { id = transfer.Id }, transfer);
    }

}

