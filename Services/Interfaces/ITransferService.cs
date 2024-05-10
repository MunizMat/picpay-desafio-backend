using Dtos;
using Models;

namespace Services.Interfaces;

public interface ITransferService
{
    Task<TransferDto> CreateTransferAsync(TransferModel transfer);
    Task<List<TransferDto>> GetAllTransfersAsync();
}
