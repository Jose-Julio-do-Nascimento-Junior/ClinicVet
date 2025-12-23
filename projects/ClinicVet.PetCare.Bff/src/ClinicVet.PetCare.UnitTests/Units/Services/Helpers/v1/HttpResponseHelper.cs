using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ClinicVet.PetCare.UnitTests.Units.Services.Helpers.v1;

public sealed class HttpResponseHelper
{
    public static HttpResponseMessage CreateJsonResponse(
        object content,
        HttpStatusCode statusCode = HttpStatusCode.OK,
        IEnumerable<string>? notifications = null,
        JsonSerializerSettings? jsonSettings = null)
    {
        var envelope = new Dictionary<string, object?>
        {
            ["Content"] = content,
            ["Notifications"] = notifications ?? Enumerable.Empty<string>()
        };

        var json = JsonConvert.SerializeObject(envelope, jsonSettings ?? new JsonSerializerSettings());

        return new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
    }
}