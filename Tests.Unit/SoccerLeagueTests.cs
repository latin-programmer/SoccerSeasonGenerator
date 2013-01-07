using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Soccer;

namespace Tests.Unit
{
    [TestClass]
    public class SoccerLeagueTests
    {
        [TestMethod]
        public void TestConstructorInitializesTeams()
        {
            //arrange
            var teams = new List<string> {"Orange Crush", "Yellow Hornets"};

            //act
            var soccerLeague = new SoccerLeague(teams.ToArray());

            //assert
            var teamsList =
                (from team in soccerLeague.Teams
                 where (team.TeamName == "Orange Crush" || team.TeamName == "Yellow Hornets")
                 select team);


            Assert.IsTrue(teamsList.Count() == 2,
                          string.Format("Expected league to have two teams and it had {0}", teamsList.Count()));
        }

        [TestMethod]
        public void TestConstructorInitializesReferees()
        {
            //arrange
            var teams = new List<string> { "Orange Crush", "Yellow Hornets" };

            //act
            var soccerLeague = new SoccerLeague(teams.ToArray());

            //assert
           Assert.IsTrue(soccerLeague.Referees.Count() == 2,
                          string.Format("Expected league to two refs and it had {0}", soccerLeague.Referees.Count()));
        }

        [TestMethod]
        public void TestConstructorInitializesMatches()
        {
            //arrange
            var teams = new List<string> { "Orange Crush", "Yellow Hornets" };

            //act
            var soccerLeague = new SoccerLeague(teams.ToArray());

            //assert
            Assert.IsTrue(soccerLeague.Matches != null, "Expected matches to be initialized and they were not.");
        }
    }
}
