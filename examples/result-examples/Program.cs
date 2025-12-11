/*
This example shows usage of the result monad in a scenario when composing data from different external sources.

Using real world football standings provided by Azhari Muhammad Marzan - https://github.com/azharimm/football-standings-api.
*/

using Svan.Monads;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


// Firing off a range of queries with various outcomes
await WhichTeamWon("eng.1", 2020);
await WhichTeamWon("fra.1", 2021);
await WhichTeamWon("eng.1", 2022);
await WhichTeamWon("invalid league id", 2020);

static async Task WhichTeamWon(string leagueId, int season)
{
    var result = await GetLeague(new Query(leagueId, season))
                    .BindAsync(GetSeason)
                    .BindAsync(GetParticipants)
                    .BindAsync(GetWinner)
                    .MapAsync(winner => winner.Name)
                    .DefaultWithAsync(error => error.Reason);

    Console.WriteLine($"Winner of {season} {leagueId} is:");
    Console.WriteLine(result);
}

static Task<Result<LookupError, League>> GetLeague(Query query)
    => TryCallApi($"leagues/{query.LeagueId}")
            .MapAsync(success => new League(success["data"]["id"].ToString(), query))
            .MapErrorAsync(error => new LookupError($"Could not get league: {error.Message}"));

static Task<Result<LookupError, Season>> GetSeason(League league)
    => TryCallApi($"leagues/{league.Id}/standings?season={league.Query.Season}&sort=asc")
            .MapAsync(success => new Season(Convert.ToInt32(success["data"]["season"]), league.Query))
            .MapErrorAsync(error => new LookupError($"Could not get league: {error.Message}"));

static Task<Result<LookupError, IEnumerable<Team>>> GetParticipants(Season season)
    => TryCallApi($"leagues/{season.Query.LeagueId}/standings?season={season.Year}&sort=asc")
            .MapErrorAsync(error => new LookupError($"Could not get participants: {error.Message}"))
            .MapAsync(success =>
            {
                var participants = success["data"]["standings"]
                            .Select((item, rank) => new Team(
                                item["team"]["name"].ToString(),
                                rank + 1,
                                season.Query));

                return participants;
            });

static Result<LookupError, Team> GetWinner(IEnumerable<Team> teams)
{
    var winner = teams.FirstOrDefault(team => team.Rank == 1);
    if (winner != default)
    {
        return winner;
    }
    else
    {
        return new LookupError("No team with rank 1 found");
    }
}

static async Task<Result<Exception, JObject>> TryCallApi(string path)
{
    const string RootUrl = "https://football-standings-api.vercel.app/";

    try
    {
        var url = string.Concat(RootUrl, path);
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var parsed = JsonConvert.DeserializeObject<JObject>(json);

        return parsed;
    }
    catch (Exception ex)
    {
        return ex;
    }
}

record Query(string LeagueId, int Season);
record League(string Id, Query Query);
record Team(string Name, int Rank, Query Query);
record Season(int Year, Query Query);
record LookupError(string Reason);
