using Microsoft.AspNetCore.Mvc;
using Contexts;
using Dtos;
using Models;
using Services.Interfaces;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class TransferController : ControllerBase
{
    private readonly ITransferService _transferService;

    public TransferController(ITransferService transferService)
    {
        _transferService = transferService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TransferDto>>> Get()
    {
        var transfers = await _transferService.GetAllTransfersAsync();

        return Ok(transfers);
    }

    [HttpPost]
    public async Task<ActionResult<TransferDto>> Post([FromBody] TransferModel transfer)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);


        try
        {
            var createdTransfer = await _transferService.CreateTransferAsync(transfer);

            return CreatedAtAction(nameof(Post), createdTransfer);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}

