using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soccer
{
    public class SoccerMatch
    {
        public IList<Team> Teams;
        public int score;
        public Referee Referee;

        public override string ToString()
        {
            return string.Format("{0} (Coach: {1}) vs. {2} (Coach: {3}) - Ref: {4}", Teams[0].TeamName, Teams[0].Coach.ToString()
                , Teams[1].TeamName, Teams[1].Coach.ToString(), Referee.ToString());

        }
    }
}
