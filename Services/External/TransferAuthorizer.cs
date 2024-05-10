using Services.External.Interfaces;

namespace Services.External;

public class TransferAuthorizer : ITransferAuthorizer
{
    private readonly HttpClient _httpClient;

    public TransferAuthorizer(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task AuthorizeTransaction()
    {
        var response = await _httpClient.GetAsync("/");

        response.EnsureSuccessStatusCode();

    }
}
