using ClinicVet.PetCare.Infra.Data.Services.Models.v1;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ClinicVet.PetCare.Infra.Data.Services.Extensions.v1;

public static class CacheExtensions
{
    public static async Task<string> DistributedCacheAsync(
          this IDistributedCache distributedCache,
          CacheSettings cacheSettings,
          string key,
          Object result ,
          CancellationToken cancellationToken)
    {
        var returnCache = result switch
        {
            null => string.Empty,
            string s => s,
            _ => JsonSerializer.Serialize(result)
        };

        await distributedCache.SetStringAsync(key, returnCache,
        new DistributedCacheEntryOptions
        {
           AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheSettings.MinutesToExpireToken!.Value)
        },
        cancellationToken);

        return returnCache ;
    }
}