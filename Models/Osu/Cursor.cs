using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObcMessenger.Models.Osu;

public class Cursor
{
    [JsonProperty("page")]
    public int Page { get; set; }
}
