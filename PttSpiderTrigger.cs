// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.RegularExpressions;
using AngleSharp;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace PttSpider
{
    public class PttSpiderTrigger
    {
        public PttSpiderTrigger(ILogger<PttSpiderTrigger> logger,
        CosmosDbServices cosmosDbServices,
        LineNotifyServices lineNotifyServices,
        LineBotService lineBotService
        )
        {
            _logger = logger;
            _cosmosDbServices = cosmosDbServices;
            _lineNotifyServices = lineNotifyServices;
            _lineBotService = lineBotService;
        }
        private readonly string _pttNotifyUrl = "https://www.ptt.cc";
        private readonly ILogger<PttSpiderTrigger> _logger;
        private readonly CosmosDbServices _cosmosDbServices;
        private readonly LineNotifyServices _lineNotifyServices;
        private readonly LineBotService _lineBotService;

        [FunctionName("PttSpiderTrigger")]
        public async Task Run([TimerTrigger("*/10 * * * * *")] TimerInfo myTimer)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var allRule = await _cosmosDbServices.GetSearchRule();// await GetSearchRule();
            foreach (var rule in allRule)
            {
                var document = await context.OpenAsync($"{_pttNotifyUrl}/bbs/{rule.BoardName}/index.html");
                var allRows = document.QuerySelectorAll("div.title > a");
                foreach (var row in allRows)
                {
                    //Regex regex = new Regex(rule.SearchRegx);
                    if (Regex.IsMatch(row.InnerHtml, "(?i)" + rule.SearchRegx))
                    {
                        var blog = new Blog()
                        {
                            Id = Guid.NewGuid()!.ToString(),
                            CategoryId = "ptt",
                            Title = row.InnerHtml,
                            Url = _pttNotifyUrl + row.Attributes["href"]!.Value,
                            UserId = rule.UserId,
                            CatchTime = DateTime.Now,
                        };
                        _logger.LogInformation($"Match Post: {blog.Title}");
                        if (!await _cosmosDbServices.CheckIsCatch(blog))
                        {
                            var user = await _cosmosDbServices.GetUser(rule.UserId!);
                            if (user == null)
                            {
                                _logger.LogInformation($"User not found");
                                continue;
                            }
                            if (string.IsNullOrEmpty(user.LineBotUserId))
                            {
                                _logger.LogInformation($"User {user.Id} not bind LineBotUserId");
                                continue;
                            }
                            // await _lineNotifyServices.SendLineNotify(user!, blog.Title + " " + blog.Url);
                            await _lineBotService.PushMessageAsync(user!, blog.Title + " " + blog.Url);
                            await _cosmosDbServices.LogCatch(blog);
                        }

                    }
                }
            }
        }
    }
}
