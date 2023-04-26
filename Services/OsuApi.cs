using ObcMessenger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ObcMessenger.Models.Osu;
using Newtonsoft.Json;
using ObcMessenger.Enums.Osu;

namespace ObcMessenger.Services;

public class OsuApi
{
    int m_osuClientId;
    string m_osuClientSecret;
    HttpClient m_httpClient;

    CredentialsResponse m_osuCredentials;

    public OsuApi()
    {
        m_httpClient = new HttpClient();
        m_httpClient.BaseAddress = new Uri("https://osu.ppy.sh/api/v2/");
        m_httpClient.DefaultRequestHeaders.Accept.Clear();
        m_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<bool> Connect(int id, string secret)
    {
        m_osuCredentials = await RefreshCredentials(id, secret);

        return m_osuCredentials != null;
    }

    public async Task<Rankings> GetRankings(GameMode mode, RankingType rankingType, string country, int page)
    {
        var p = $"?cursor[page]={page}";
        var c = string.IsNullOrEmpty(country) ? "" : $"&country={country}";
        var res = await PerformGet<Rankings>($"rankings/{mode.ToString().ToLowerInvariant()}/{rankingType.ToString().ToLowerInvariant()}{p}{c}");
        return res;
    }

    public async Task<User> GetUser(string userName, GameMode mode)
    {
        var res = await PerformGet<User>($"users/{userName}/{mode.ToString().ToLowerInvariant()}?key=username||id");
        return res;
    }

    async Task<CredentialsResponse> RefreshCredentials(int id, string secret)
    {
        CredentialsResponse res;

        try
        {
            res = await PerformPost<CredentialsResponse>("https://osu.ppy.sh/oauth/token", new CredentialsRequest()
            {
                ClientId = id,
                ClientSecret = secret
            });
        }
        catch (AccessViolationException)
        {
            return null;
        }

        m_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(res.TokenType, res.AccessToken);

        return res;
    }

    async Task<T> PerformPost<T>(string endpoint, object data)
    {
        var res = await m_httpClient.PostAsync(endpoint,
            new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));

        if (res.IsSuccessStatusCode)
        {
            var content = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        throw new AccessViolationException("[OSU] GET failed: " + res.ReasonPhrase);
    }

    async Task<T> PerformGet<T>(string endpoint)
    {
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, endpoint);
        httpRequestMessage.Content = new StringContent("", Encoding.ASCII, "application/json");
        var res = await m_httpClient.SendAsync(httpRequestMessage);

        if (res.IsSuccessStatusCode)
        {
            var content = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        throw new AccessViolationException($"[OSU] GET failed ({res.StatusCode}): {res.ReasonPhrase}");
    }
}
