using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObcMessenger.Models.Osu;

public class GradeCounts
{
    [JsonProperty("A")]
    public int A { get; set; }
    [JsonProperty("S")]
    public int S { get; set; }
    [JsonProperty("SH")]
    public int SH { get; set; }
    [JsonProperty("SS")]
    public int SS { get; set; }
    [JsonProperty("SSH")]
    public int SSH { get; set; }
}
