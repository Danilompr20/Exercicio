using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Program
{
    public static async Task Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await getTotalScoredGoalsAsync(teamName, year);



        teamName = "Chelsea";
        year = 2014;
        totalGoals = await getTotalScoredGoalsAsync(teamName, year);



        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async Task<int> getTotalScoredGoalsAsync(string team, int year)
    {
        int totalGoals = await GetAPI(team, year);
        var teste = totalGoals;
        return totalGoals;
    }
    public static async Task<int> GetAPI(string team, int year, int page = 1, int totalGoals = 0)
    {

        using (HttpClient httpClient = new HttpClient())
        {

            HttpResponseMessage response = await httpClient.GetAsync($"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}&page={page}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var json = Newtonsoft.Json.Linq.JObject.Parse(jsonResponse);
                var data = json["data"];

                if (data.Count() > 0)
                {
                    foreach (var match in data)
                    {
                        totalGoals += match["team1goals"].Value<int>();
                    }
                    page += 1;
                    await GetAPI(team, year, page, totalGoals);
                }
                else
                    Console.WriteLine("Team " + team + " scored " + totalGoals.ToString() + " goals in " + year);
                return totalGoals;
            }
            else
                return totalGoals;
        }
    }

}