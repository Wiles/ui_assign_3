using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMHAStats
{
    public class clsStatItem : IComparable
    {
        public string Name;
        public string Team;
        private int goals;
        private int assists;
        private int penaltyMin;
        public int TodayGoals;
        public int TodayAssists;
        public int TodayPenalty;

        public int Goals
        {
            get { return goals + TodayGoals; }
            set { goals = value; }
        }

        public int Assists
        {
            get { return assists + TodayAssists; }
            set { assists = value; }
        }

        public int PenaltyMin
        {
            get { return penaltyMin + TodayPenalty; }
            set { penaltyMin = value; }
        }

        public int CompareTo(object obj)
        {
            int ret = 0;

            try
            {
                clsStatItem si = (clsStatItem)obj;

                ret = (Goals + Assists).CompareTo(si.Goals + si.Assists);

                if (ret == 0)
                    ret = Goals.CompareTo(si.Goals);

                if (ret == 0)
                    ret = -PenaltyMin.CompareTo(si.PenaltyMin);
            }
            catch (Exception ex)
            {
                clsError.SendError(ex);
            }
            
            return ret;
        }
    }
}
