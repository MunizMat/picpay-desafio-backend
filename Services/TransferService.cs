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
        if (transfer.PayeeId == transfer.PayerId)
            throw new ArgumentException("Transaction payer cannot be equal to payee");

        var payerWallet = await _postgreSqlContext.Wallets.FirstAsync(w => w.UserId == transfer.PayerId);

        if (payerWallet is null)
            throw new ArgumentNullException("Payer does not exist");

        if (payerWallet.Balance < transfer.Amount)
            throw new ArgumentException("Insufficient account balance");

        try
        {
            await _transferAuthorizer.AuthorizeTransaction();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Failed to authorize transaction", ex);
        }

        payerWallet.Balance -= transfer.Amount;
        transfer.WalletId = payerWallet.Id;

        _postgreSqlContext.Transfers.Add(transfer);

        await _postgreSqlContext.SaveChangesAsync();

        return new TransferDto
        {
            Amount = transfer.Amount,
            CreatedAt = transfer.CreatedAt,
            Id = transfer.Id,
            PayeeId = transfer.PayeeId,
            PayerId = transfer.PayerId,
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
