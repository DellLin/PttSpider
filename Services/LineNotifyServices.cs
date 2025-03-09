// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Net.Http.Headers;

namespace PttSpider.Services;

public class LineNotifyServices
{
    private readonly string _lineNotifyUrl = "https://notify-api.line.me/api/notify";
    public async Task<string> SendLineNotitfy(Account account, string message)
    {
        try
        {
            var form = new Dictionary<string, string>(){
                    {"message", message}
                };
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", account.LineNotifyAccessToken); ;
            var responseMessage = await client.PostAsync(_lineNotifyUrl, new FormUrlEncodedContent(form));
            var result = responseMessage.Content.ReadAsStringAsync().Result;
            return result;
        }
        catch
        {
            throw;
        }
    }
}
