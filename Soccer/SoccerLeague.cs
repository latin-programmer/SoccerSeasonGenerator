using System;
using System.Collections.Generic;
using System.Linq;

namespace Soccer
{
    public class SoccerLeague
    {
        private readonly Random _RandomGenerator; 

        public SoccerLeague(IEnumerable<string> teams)
        {
            Teams = new List<Team>();
            Referees = new List<Referee>();
            Matches = new List<SoccerMatch>();

            foreach (var team in from teamName in teams
                                 let teamCoach = new Coach {FirstName = "Mister", LastName = teamName.Split()[0]}
                                 select new Team() {TeamName = teamName, Coach = teamCoach})
            {
                Teams.Add(team);
            }

            var ref1 = new Referee { FirstName = "Joe", LastName = "Strict" };
            var ref2 = new Referee { FirstName = "Edgar", LastName = "Fair" };

            Referees.Add(ref1);
            Referees.Add(ref2);
            _RandomGenerator = new Random();
        }

        public IList<Team> Teams { get; set; }
        public IList<Referee> Referees { get; set; }
        public IList<SoccerMatch> Matches { get; set; }

        public void AddMatch(string teamA, string teamB)
        {
            var team1 = (from t in Teams where t.TeamName.Equals(teamA) select t).First();
            var team2 = (from t in Teams where t.TeamName.Equals(teamB) select t).First();
            var refIndex = _RandomGenerator.Next(0, 2);
            IList<Team> teams = new List<Team>();
            teams.Add(team1);
            teams.Add(team2);
            var match = new SoccerMatch() { Teams = teams, Referee = Referees[refIndex] };
            Matches.Add(match);             
        }
    }
}
