// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace PttSpider.Models;
public class Blog
{
    [JsonProperty(PropertyName = "id")]
    public string? Id { get; set; }
    [JsonProperty(PropertyName = "categoryId")]
    public string? CategoryId { get; set; }
    [JsonProperty(PropertyName = "title")]
    public string? Title { get; set; }
    [JsonProperty(PropertyName = "url")]
    public string? Url { get; set; }
    [JsonProperty(PropertyName = "userId")]
    public string? UserId { get; set; }
    [JsonProperty(PropertyName = "catchTime")]
    public System.DateTime? CatchTime { get; set; }
};

