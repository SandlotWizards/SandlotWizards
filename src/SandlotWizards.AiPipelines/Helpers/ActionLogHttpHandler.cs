using SandlotWizards.ActionLogger;
using System.Diagnostics;

public class ActionLogHttpHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();

        ActionLog.Global.Info($"[Http] Sending {request.Method} request to {request.RequestUri}");

        HttpResponseMessage response;
        try
        {
            response = await base.SendAsync(request, cancellationToken);
        }
        catch (Exception ex)
        {
            ActionLog.Global.Error($"[Http] Request to {request.RequestUri} failed: {ex.Message}");
            throw;
        }

        stopwatch.Stop();

        ActionLog.Global.Info($"[Http] Received {(int)response.StatusCode} from {request.RequestUri} in {stopwatch.ElapsedMilliseconds}ms");

        return response;
    }
}
