using Microsoft.AspNetCore.Mvc;
using Contexts;
using Dtos;
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
    public List<TransferDto> Get()
    {
        var transfers = _postgreSqlContext.Transfers
            .Select(t => new TransferDto
            {
                Amount = t.Amount,
                CreatedAt = t.CreatedAt,
                Id = t.Id,
                PayeeId = t.PayeeId,
                PayerId = t.PayerId
            })
            .ToList();

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

