using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObcMessenger.Models.Osu;

public class Rankings
{
    [JsonProperty("beatmapsets")]
    public Beatmapsets Beatmapsets { get; set; }
    [JsonProperty("cursor")]
    public Cursor Cursor { get; set; }
    [JsonProperty("ranking")]
    public UserStatistics[] Ranking { get; set; }
    [JsonProperty("spotlight")]
    public Spotlight Spotlight { get; set; }
    [JsonProperty("total")]
    public int Total { get; set; }
}
