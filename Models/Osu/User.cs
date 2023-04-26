using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObcMessenger.Models.Osu;

public class User
{
    [JsonProperty("avatar_url")]
    public string AvatarUrl { get; set; }
    [JsonProperty("country")]
    public Country Country { get; set; }
    [JsonProperty("country_code")]
    public string CountryCode { get; set; }
    [JsonProperty("cover")]
    public Cover Cover { get; set; }
    [JsonProperty("default_group")]
    public string DefaultGroup { get; set; }
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("is_active")]
    public bool IsActive { get; set; }
    [JsonProperty("is_bot")]
    public bool IsBot { get; set; }
    [JsonProperty("is_online")]
    public bool IsOnline { get; set; }
    [JsonProperty("is_supporter")]
    public bool IsSupporter { get; set; }
    [JsonProperty("last_visit")]
    public DateTime? LastVisit { get; set; }
    [JsonProperty("pm_friends_only")]
    public bool PmFriendsOnly { get; set; }
    [JsonProperty("profile_colour")]
    public string ProfileColour { get; set; }
    [JsonProperty("username")]
    public string Username { get; set; }
    [JsonProperty("statistics")]
    public UserStatistics Statistics { get; set; }
}
