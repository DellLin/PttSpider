// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Azure.Cosmos;

namespace PttSpider.Services;

public class CosmosDbServices
{

    private readonly CosmosClient _client = new(
                accountEndpoint: Environment.GetEnvironmentVariable("COSMOS_ENDPOINT")!,
                authKeyOrResourceToken: Environment.GetEnvironmentVariable("COSMOS_KEY")!
            );
    private readonly Database _database;
    public CosmosDbServices()
    {
        _database = _client.GetDatabase(id: "MyDataBase");
    }
    public async Task<Account?> GetUser(string userId)
    {
        Account? result = new();
        var container = _database.GetContainer("account");
        var query = new QueryDefinition(
            query: "SELECT * FROM account a WHERE a.id=@userId"
        )
        .WithParameter("@userId", userId); ;
        using var feed = container.GetItemQueryIterator<Account>(query);
        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            result = response.FirstOrDefault();
        }
        return result;
    }
    public async Task<List<SearchRule>> GetSearchRule()
    {
        List<SearchRule> result = new();
        var container = _database.GetContainer("PttSpider");
        var query = new QueryDefinition(
            query: "SELECT * FROM PttSpider"
        );
        using var feed = container.GetItemQueryIterator<SearchRule>(query);
        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            foreach (var item in response)
            {
                result.Add(item);
            }
        }
        return result;
    }
    public async Task LogCatch(Blog blog)
    {
        var container = _database.GetContainer("PttSpiderCatch");
        await container.CreateItemAsync<Blog>(blog);
        return;
    }

    public async Task<bool> CheckIsCatch(Blog blog)
    {
        var container = _database.GetContainer("PttSpiderCatch");
        var query = new QueryDefinition(
            query: "SELECT p.title FROM PttSpiderCatch p WHERE p.url=@url"
        )
        .WithParameter("@url", blog.Url);
        using var feed = container.GetItemQueryIterator<SearchRule>(query);
        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            if (response.Count > 0)
            {
                return true;
            }
        }
        return false;
    }
}
