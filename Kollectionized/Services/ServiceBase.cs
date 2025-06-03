using System;
using System.Net.Http;
using Kollectionized.Common;

namespace Kollectionized.Services;

public abstract class ServiceBase
{
    protected readonly HttpClient Client;

    protected ServiceBase(string? baseUrl = null)
    {
        var resolvedUrl = baseUrl ?? Environment.GetEnvironmentVariable("KOLLECTIONIZED_API")
            ?? Constants.DefaultApiBaseUrl;
        Client = new HttpClient { BaseAddress = new Uri(resolvedUrl) };
    }
}