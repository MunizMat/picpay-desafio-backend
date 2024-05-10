using Contexts;
using Dtos;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.External.Interfaces;
using Services.Interfaces;

namespace Services;

public class TransferService : ITransferService
{
    private readonly PostgreSqlContext _postgreSqlContext;
    private readonly ITransferAuthorizer _transferAuthorizer;

    public TransferService(PostgreSqlContext postgreSqlContext, ITransferAuthorizer transferAuthorizer)
    {
        _postgreSqlContext = postgreSqlContext;
        _transferAuthorizer = transferAuthorizer;
    }

    public async Task<TransferDto> CreateTransferAsync(TransferModel transfer)
    {

        var payer = await _postgreSqlContext.Users.FindAsync(transfer.PayerId);

        if (payer is null)
            throw new ArgumentException("Payer does not exist");

        if (payer.AccountBalance < transfer.Amount)
            throw new ArgumentException("Insufficient account balance for this transfer");

        try
        {
            await _transferAuthorizer.AuthorizeTransaction();
        }
        catch (HttpRequestException)
        {
            throw new InvalidOperationException("Failed to authorize your transaction");
        }

        _postgreSqlContext.Transfers.Add(transfer);
        await _postgreSqlContext.SaveChangesAsync();

        return new TransferDto
        {
            Amount = transfer.Amount,
            CreatedAt = transfer.CreatedAt,
            Id = transfer.Id,
            PayeeId = transfer.PayeeId,
            PayerId = transfer.PayerId
        };
    }

    public async Task<List<TransferDto>> GetAllTransfersAsync()
    {
        var transfers = await _postgreSqlContext.Transfers
            .Select(t => new TransferDto
            {
                Amount = t.Amount,
                CreatedAt = t.CreatedAt,
                Id = t.Id,
                PayeeId = t.PayeeId,
                PayerId = t.PayerId
            })
          .ToListAsync();

        return transfers;
    }
}
