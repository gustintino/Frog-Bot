﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObcMessenger.Models.Osu;

public class CredentialsResponse
{
    [JsonProperty("token_type")]
    public string TokenType { get; set; }
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
}
