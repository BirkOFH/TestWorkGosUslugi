using System.Text.Json;

namespace TestWorkGosUslugi.Data
{
  public class RegionService
  {
    private static Dictionary<string, Guid> items = new();
    private static bool isUpdating = false;
    static async Task UpdateRegions()
    {
      isUpdating = true;
      var client = new HttpClient();
      var request = new HttpRequestMessage(HttpMethod.Get, "https://dom.gosuslugi.ru/nsi/api/rest/services/nsi/fias/v4/regions");
      request.Headers.Add("Accept", "application/json; charset=utf-8");
      request.Headers.Add("Accept-Language", "ru,en;q=0.9");
      request.Headers.Add("Cache-Control", "max-age=0");
      request.Headers.Add("Connection", "keep-alive");
      request.Headers.Add("Cookie", "JSESSIONID=\"xzpT0YSQNGD8ulmdU6eFfEpL_piwbcIXs4Z55GPZ.ppak-app-wf-ui-srv05:rest\"; _ym_uid=1688404400358227132; userSelectedLanguage=ru; nau=3550bd4d-3bad-f1e1-e0b1-8ea5c2dbfc98; u=1189709203; showed_onboarding_places=; cfidsgib-w-gosuslugi=vdxL/EvtP1YfsWYVujGaP/jbX2Pc40QN7V75P9wVi4MSn20Mk0EoW85BQ2dL6TVtwpO7CPM0qw2izUM4ciiPdXSE5h4hSKPREP9sVWc/tX+OphsJmeNpL6CBvpzPaUPP7AUr7cJ/jzRyQPZToTkqtt1yBllrK/hx/KO4WvY=; gsscgib-w-gosuslugi=GshypbCoKkysuxpIOiNuoqRxLzd16nvLv38ZkW6lvHpzn4Tnv4gbe+hRdc04VSCEtIG04G0AXcLPPhiTVVvIxT5CVXlJCiKnaHTkK2l1T8xwLKGZsP9AkrleB1VBPXYuNJjIykcR2CJ32HWO6fA+oOcAqPO59NmAlFgpHUgStHVjb8LOhrwJzcXjuWp6P4e0bHVH6+olk/ZShz+r+oXQshM01Y5K4sr/60vlu+wrzuSLu/3VXFOjF2N96ndwLHu4; TS01033236=01474e7625e8dc26c4ba79945194cac9e721d0d649d133e0fc66718f3ccaf6cfec34dc72adc819cc8e3c2396cdebb3dde70f534565; _ym_d=1704813460; userSelectedRegion=41000000000; _idp_authn_id=phone%3A%252B7(988)3524615; __zzatgib-w-gosuslugi=MDA0dC0cTApcfEJcdGswPi17CT4VHThHKHIzd2VrTXdcdF4IJkhabxU6MU0XK0N1HRwaSk5fbxt7Il8qCCRjNV8ZQ2pODWk3XBQ8dWU+SG15MTxoIGJIXR9EUT9IXl1JEjJiEkBATUcNN0BeN1dhMA8WEU1HFT1WUk9DKGsbcVhXL3AkF0hSfjsWa25HZ0dXTBdfQjs4WEERdVw+SXZ6KURnHWI5VRELEhdEXlxVaXVnGUxAVy8NLjheLW8eZVBeJUlaUwguIBN7ZxUeQE8bUAg0NmJwVycrESZUP0cZSmVOewldYxM4RCEJdj0/GxA6JKvjIA==; fgsscgib-w-gosuslugi=KID28b2de9515bfe6df8692abd31ca0ab0328a68; prev-gu-role=P; redirect_order_id=3767643866; 10700_on=false; _ym_visorc=b; route_pafo-saiku=c74cc883b187536d03cd9d27dbb214c6; suimSessionGuid=3b3c4110-72a8-424b-a81a-853dcdef6344; route_rest=9ab4234711eff9f497e7da0986c6cfd0; route_suim=9cec376355a82e1c2092d0f01142afd1; route_pafo-reports=3eed4a51ebe9ecbd752731228dd302a5; _ym_isad=2");
      request.Headers.Add("DNT", "1");
      //request.Headers.Add("If-Modified-Since", "0");
      request.Headers.Add("Referer", "https://dom.gosuslugi.ru/");
      request.Headers.Add("Request-GUID", "9eb752de-444c-4bbb-ba7d-dbe637eff856");
      request.Headers.Add("Sec-Fetch-Dest", "empty");
      request.Headers.Add("Sec-Fetch-Mode", "cors");
      request.Headers.Add("Sec-Fetch-Site", "same-origin");
      request.Headers.Add("Session-GUID", "3b3c4110-72a8-424b-a81a-853dcdef6344");
      request.Headers.Add("State-GUID", "/houses");
      request.Headers.Add("User-Agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Mobile Safari/537.36");
      request.Headers.Add("sec-ch-ua", "\"Chromium\";v=\"109\", \"Not_A Brand\";v=\"99\"");
      request.Headers.Add("sec-ch-ua-mobile", "?1");
      request.Headers.Add("sec-ch-ua-platform", "\"Android\"");
      var response = await client.SendAsync(request);
      if (!response.IsSuccessStatusCode) isUpdating = false;
      response.EnsureSuccessStatusCode();
      //тут по хорошему бы не вылетать если запрос с ошибкой а как то реагировать
      var json = await response.Content.ReadAsStringAsync();
      var list = JsonSerializer.Deserialize<List<RegionItem>>(json);
      items.Clear();
      foreach (var item in list)
      {
        if (items.ContainsKey(item.regionCode))
        {
          Console.WriteLine($"Already {item.regionCode}");
        } else items.Add(item.regionCode, item.aoGuid);
      }

      isUpdating = false;

    }

    public static async Task<Guid> GetId(string cadastrNum)
    {
      var arr = cadastrNum.Split(':', StringSplitOptions.RemoveEmptyEntries);
      if (arr.Length == 0) return Guid.Empty;

      if (items.Count == 0) await UpdateRegions();
      if (items.TryGetValue(arr[0], out var res)) return res;
      return Guid.Empty;
    }
  }
}
