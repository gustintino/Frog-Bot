using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObcMessenger.Models.Osu;

public class Cover
{
    [JsonProperty("custom_url")]
    public object CustomUrl { get; set; }
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("url")]
    public string Url { get; set; }
}
