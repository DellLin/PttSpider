// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations.Schema;

namespace PttSpider.Models;
public class SearchRule
{
    [Column("id")]
    [JsonProperty(PropertyName = "id")]
    public string? Id { get; set; }

    [Column("boardName")]
    [JsonProperty(PropertyName = "boardName")]
    public string? BoardName { get; set; }

    [Column("searchRegx")]
    [JsonProperty(PropertyName = "searchRegx")]
    public string? SearchRegx { get; set; }

    [Column("userId")]
    [JsonProperty(PropertyName = "userId")]
    public string? UserId { get; set; }

}
