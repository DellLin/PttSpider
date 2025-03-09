// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations.Schema;

namespace PttSpider.Models;
public class Account
{
    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("picture")]
    public string? Picture { get; set; }

    [JsonProperty("refreshToken")]
    public string? RefreshToken { get; set; }

    [JsonProperty("lineId")]
    public string? LineId { get; set; }

    [JsonProperty("lineName")]
    public string? LineName { get; set; }

    [JsonProperty("lineLoginAccessToken")]
    public string? LineLoginAccessToken { get; set; }

    [JsonProperty("lineLoginRefreshToken")]
    public string? LineLoginRefreshToken { get; set; }

    [JsonProperty("linePicture")]
    public string? LinePicture { get; set; }

    [JsonProperty("lineNotifyAccessToken")]
    public string? LineNotifyAccessToken { get; set; }

    [JsonProperty("googleId")]
    public string? GoogleId { get; set; }

    [JsonProperty("googleName")]
    public string? GoogleName { get; set; }

    [JsonProperty("googleAccessToken")]
    public string? GoogleAccessToken { get; set; }

    [JsonProperty("googleRefreshToken")]
    public string? GoogleRefreshToken { get; set; }

    [JsonProperty("googlePicture")]
    public string? GooglePicture { get; set; }
    [JsonProperty("lineBotUserId")]
    public string? LineBotUserId { get; set; }
    [JsonProperty("nonce")]
    public string? Nonce { get; set; }

}

