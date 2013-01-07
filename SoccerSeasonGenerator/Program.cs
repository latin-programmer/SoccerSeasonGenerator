using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Soccer;

namespace SoccerSeasonGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() < 2)
            {
                Console.WriteLine("Usage: SoccerSeasonGenerator soccer_teams.txt 4");
            }
            string soccerFile = args[0];
            string[] teams = File.ReadAllLines(soccerFile);
            int rounds = int.Parse(args[1]);
            int matchesPerRound = teams.Count() / 2;
            Random generator = new Random();
            //SimpleSoccerSeasonGenerator(teams, rounds, matchesPerRound, generator);
            ImprovedSeasonGenerator(teams, rounds, matchesPerRound, generator);
            Console.Read();

        }

        static void SimpleSoccerSeasonGenerator(string[] teams, int rounds, int matchesPerRound, Random generator)
        {            
           
            for (int i = 0; i < rounds; i++)
            {
                List<string> notPickedTeams = teams.ToList();
                Console.WriteLine(string.Format("Round {0}:", i + 1));
                for (int j = 0; j < matchesPerRound; j++)
                {
                    string firstTeam = notPickedTeams[generator.Next(0, notPickedTeams.Count())];
                    Console.Write(string.Format("{0} vs ", firstTeam));
                    notPickedTeams.Remove(firstTeam);
                    string secondTeam = notPickedTeams[generator.Next(0, notPickedTeams.Count())];
                    Console.WriteLine(secondTeam);
                    notPickedTeams.Remove(secondTeam);
                }
                if (notPickedTeams.Count > 0)
                {
                    Console.WriteLine("Bye: {0}", notPickedTeams[0]);
                }
                Console.WriteLine();

            }
        }

        static void ImprovedSeasonGenerator(string[] teams, int rounds, int matchesPerRound, Random generator)
        {
            SoccerLeague soccerLeague = new SoccerLeague(teams);
            List<List<Team>> matchPool = new List<List<Team>>();

            //populate all combinations
            foreach (Team team in soccerLeague.Teams)
            {
                var opponents = 
                    (from t in soccerLeague.Teams 
                     where t.TeamName != team.TeamName 
                     select t).ToList();
                while (opponents.Count > 0)
                {
                    var matchingMatch = (from m in matchPool where m.Contains(team) && m.Contains(opponents[0]) select m);
                    if (matchingMatch.Count() > 0)
                    {
                        //match already exists
                        opponents.Remove(opponents[0]);
                    }
                    else
                    {
                        List<Team> match = new List<Team>();
                        match.Add(team);
                        match.Add(opponents[0]);
                        matchPool.Add(match);
                    }
                    
                }
            }

            var notPickedMatches = matchPool.ToArray();
            var notPicketMatchesList = notPickedMatches.ToList();
            for (int i = 0; i < rounds; i++)
            {

                List<string> notPickedTeams = teams.ToList();
                Console.WriteLine(string.Format("Round {0}:", i + 1));
                if (i == 0)
                {
                    //first round is totally random
                    for (int j = 0; j < matchesPerRound; j++)
                    {
                        string firstTeam = notPickedTeams[generator.Next(0, notPickedTeams.Count())];
                        notPickedTeams.Remove(firstTeam);
                        string secondTeam = notPickedTeams[generator.Next(0, notPickedTeams.Count())];
                        notPickedTeams.Remove(secondTeam);
                        soccerLeague.AddMatch(firstTeam, secondTeam);
                        var match = soccerLeague.Matches.Last();
                        Console.WriteLine(match.ToString());

                        //remove from not picked matches
                        var matchingMatch = (from m in notPicketMatchesList where m.Contains(match.Teams[0]) && m.Contains(match.Teams[1]) select m).First();
                        notPicketMatchesList.Remove(matchingMatch);

                    }
                }
                else
                {
                    //have some intelligence to pick teams.     
                    for (int j = 0; j < matchesPerRound; j++)
                    {
                        string firstTeam = notPickedTeams[generator.Next(0, notPickedTeams.Count())];
                        var remainingMatchCount = (from m in notPicketMatchesList
                                                   where 
                                                   ((m[0].TeamName == firstTeam || m[1].TeamName == firstTeam)
                                                   && (notPickedTeams.Contains(m[0].TeamName) && (notPickedTeams.Contains(m[1].TeamName))))
                                                   select m).Count();
                        if (remainingMatchCount == 0)
                        {
                            //reset the list 
                            notPicketMatchesList = notPickedMatches.ToList();
                        }

                    
                        var matchingMatch = (from m in notPicketMatchesList
                                             where
                                             ((m[0].TeamName == firstTeam || m[1].TeamName == firstTeam)
                                             && (notPickedTeams.Contains(m[0].TeamName) && (notPickedTeams.Contains(m[1].TeamName))))
                                             select m).First();
                        
                        soccerLeague.AddMatch(matchingMatch[0].TeamName, matchingMatch[1].TeamName);
                        notPickedTeams.Remove(firstTeam);
                        notPickedTeams.Remove(matchingMatch[1].TeamName);
                        var match = soccerLeague.Matches.Last();
                        Console.WriteLine(match.ToString());
                        notPicketMatchesList.Remove(matchingMatch);
                    }
                                       
                }
                if (notPickedTeams.Count > 0)
                {
                    Console.WriteLine("Bye: {0}", notPickedTeams[0]);
                }
                Console.WriteLine();
            }
        }
    }
}
