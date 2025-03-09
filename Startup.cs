// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(PttSpider.Startup))]
namespace PttSpider;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var endPoint = Environment.GetEnvironmentVariable("COSMOS_ENDPOINT", EnvironmentVariableTarget.Process)!;
        var key = Environment.GetEnvironmentVariable("COSMOS_KEY", EnvironmentVariableTarget.Process)!;
        System.Console.WriteLine(endPoint);
        System.Console.WriteLine(key);
        builder.Services.AddDbContext<DellServiceContext>(
            options => options.UseCosmos(endPoint, key, "MyDataBase")
        );
        builder.Services.AddSingleton<CosmosDbServices>();
        builder.Services.AddSingleton<LineNotifyServices>();
        builder.Services.AddSingleton<LineBotService>();
    }
}

