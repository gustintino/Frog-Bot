using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObcMessenger.Models.Osu;

public class UserStatistics
{
    [JsonProperty("grade_counts")]
    public GradeCounts GradeCounts { get; set; }
    [JsonProperty("hit_accuracy")]
    public float HitAccuracy { get; set; }
    [JsonProperty("is_ranked")]
    public bool IsRanked { get; set; }
    [JsonProperty("level")]
    public Level Level { get; set; }
    [JsonProperty("maximum_combo")]
    public int MaximumCombo{ get; set; }
    [JsonProperty("play_count")]
    public long PlayCount { get; set; }
    [JsonProperty("play_time")]
    public object PlayTime { get; set; }
    [JsonProperty("pp")]
    public float PP { get; set; }
    [JsonProperty("global_rank")]
    public int GlobalRank { get; set; }
    [JsonProperty("ranked_score")]
    public long RankedScore { get; set; }
    [JsonProperty("replays_watched_by_others")]
    public int ReplaysWatched { get; set; }
    [JsonProperty("total_hits")]
    public long TotalHits { get; set; }
    [JsonProperty("total_score")]
    public long TotalScore { get; set; }
    [JsonProperty("user")]
    public User User { get; set; }
}
