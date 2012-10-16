using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HockeyStats
{
    public class Team
    {
        public int Number { get; set; }

        public string Name { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Ties { get; set; }

        public int GoalsScored { get; set; }

        public int GoalsAllowed { get; set; }

        public int[] aRecord { get; set; }

        public double AverageGoalsAllowedPerGame
        {
            get
            {
                int games = Wins + Losses + Ties;
                if (games == 0)
                    return 0;
                else
                    return (double)GoalsAllowed / (double)games;
            }
        }

        public double AverageGoalsScoredPerGame
        {
            get
            {
                return (GoalsScored + GoalsAllowed > 0) ? (double)GoalsScored / (double)(GoalsScored + GoalsAllowed) : 0;
            }
        }

        public int Points
        {
            get { return (Wins * 2) + Ties; }
        }

        public int CompareTo(object obj)
        {
            Team t = (Team)obj;

            int ret = Points.CompareTo(t.Points);

            if (ret == 0)
                ret = Wins.CompareTo(t.Wins);

            if (ret == 0)
                ret = aRecord[t.Number - 1];

            if (ret == 0)
                ret = AverageGoalsScoredPerGame.CompareTo(t.AverageGoalsScoredPerGame);

            if (ret == 0)
                ret = (-1) * GoalsAllowed.CompareTo(t.GoalsAllowed);

            return ret;
        }
    }
}
