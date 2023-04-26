using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObcMessenger.Models.Osu;

public class Level
{
    [JsonProperty("current")]
    public int Current { get; set; }
    [JsonProperty("progress")]
    public int Progress { get; set; }
}
