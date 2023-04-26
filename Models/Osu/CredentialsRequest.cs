using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObcMessenger.Models.Osu;

public class CredentialsRequest
{
    [JsonProperty("client_id")]
    public int ClientId { get; set; }
    [JsonProperty("client_secret")]
    public string ClientSecret { get; set; }
    [JsonProperty("grant_type")]
    public string GrantType { get; set; } = "client_credentials";
    [JsonProperty("scope")]
    public string Scope { get; set; } = "public";
}
