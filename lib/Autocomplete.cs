using System.Text.Json;

namespace ao3.lib
{
    public class Autocomplete
    {
        public static async Task<IEnumerable<string>> AutocompleteAsync(string query)
        {
            query = Uri.EscapeDataString(query);
            var url = $"https://archiveofourown.org/autocomplete/fandom?term={query}";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            using HttpResponseMessage response = await client.GetAsync(url);


            var content = await response.Content.ReadAsStringAsync();

            List<Dictionary<string, string>> json = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(content)!;

            return json.Select(i => i["name"]);

        }
    }
}
