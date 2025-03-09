using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

public class LineBotService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly ILogger<LineBotService> _logger;
    public LineBotService(IConfiguration configuration, ILogger<LineBotService> logger)
    {
        _configuration = configuration;
        _logger = logger;
        _httpClient = new HttpClient();
    }


    public async Task<string> GetAccessTokenAsync()
    {
        var clientId = _configuration.GetSection("LINE_BOT_CLIENT_ID").Value;
        var clientSecret = _configuration.GetSection("LINE_BOT_CLIENT_SECRET").Value;

        var data = new FormUrlEncodedContent(new[]
        {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret)
                });
        var response = await _httpClient.PostAsync("https://api.line.me/oauth2/v3/token", data);

        var responseContent = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(responseContent);
        return json["access_token"]!.ToString();

    }
    public async Task<JObject> PushMessageAsync(Account account, string message)
    {
        var url = "https://api.line.me/v2/bot/message/push";
        var retryKey = Guid.NewGuid().ToString();
        var random = new Random();
        var accessToken = await this.GetAccessTokenAsync();
        for (int i = 0; i < 3; i++)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                request.Headers.Add("X-Line-Retry-Key", retryKey);
                var LineMessage = new LineMessage()
                {
                    To = account.LineBotUserId!,
                    Messages = new List<LineMessage.Message>
                    {
                        new LineMessage.Message
                        {
                            Type = "text",
                            Text = message
                        }
                    }
                };
                request.Content = new StringContent(JsonConvert.SerializeObject(LineMessage), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return JObject.Parse(responseContent);
                }
                {
                    _logger.LogError("Failed to push message: {0}", responseContent);
                }
                await Task.Delay(random.Next(0, 200));
            }
        }
        throw new Exception("Failed to push message after 3 attempts.");
    }
}
