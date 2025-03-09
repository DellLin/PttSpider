// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations.Schema;

namespace PttSpider.Models;
public class Account
{
    [Column("id")]
    [JsonProperty("id")]
    public string? Id { get; set; }

    [Column("id")]
    [JsonProperty("name")]
    public string? Name { get; set; }

    [Column("id")]
    [JsonProperty("email")]
    public string? Email { get; set; }

    [Column("id")]
    [JsonProperty("picture")]
    public string? Picture { get; set; }

    [Column("id")]
    [JsonProperty("refreshToken")]
    public string? RefreshToken { get; set; }

    [Column("id")]
    [JsonProperty("lineId")]
    public string? LineId { get; set; }

    [Column("id")]
    [JsonProperty("lineName")]
    public string? LineName { get; set; }

    [Column("id")]
    [JsonProperty("lineLoginAccessToken")]
    public string? LineLoginAccessToken { get; set; }

    [Column("id")]
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

}
