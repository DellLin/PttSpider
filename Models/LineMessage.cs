
public class LineMessage
{
    [JsonProperty("to")]
    public string To { get; set; } = null!;

    [JsonProperty("messages")]
    public List<Message> Messages { get; set; } = null!;

    public class Message
    {
        [JsonProperty("type")]
        public string Type { get; set; } = null!;

        [JsonProperty("altText")]
        public string AltText { get; set; } = null!;

        [JsonProperty("template")]
        public Template Template { get; set; } = null!;

        [JsonProperty("text")]
        public string Text { get; set; } = null!;
    }

    public class Template
    {
        [JsonProperty("type")]
        public string Type { get; set; } = null!;

        [JsonProperty("text")]
        public string Text { get; set; } = null!;

        [JsonProperty("actions")]
        public List<Action> Actions { get; set; } = null!;
    }

    public class Action
    {
        [JsonProperty("type")]
        public string Type { get; set; } = null!;

        [JsonProperty("label")]
        public string Label { get; set; } = null!;

        [JsonProperty("uri")]
        public string Uri { get; set; } = null!;
    }
}
