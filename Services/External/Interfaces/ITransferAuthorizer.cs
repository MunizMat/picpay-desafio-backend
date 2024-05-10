namespace Services.External.Interfaces;

public interface ITransferAuthorizer
{
    Task AuthorizeTransaction();
}
