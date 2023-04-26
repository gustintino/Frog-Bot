using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObcMessenger.Models.Osu;

public class Country
{
    [JsonProperty("code")]
    public string Code { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
}
