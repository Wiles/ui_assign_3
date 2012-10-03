using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GMHAStandings
{
    public class clsTeam : IComparable
    {
        public int Number;
        public string Name;
        public int Wins;
        public int Losses;
        public int Ties;
        public int GF;
        public int GA;
        public int[] aRecord;

        public double GAA
        {
            get 
            {
                int games = Wins + Losses + Ties;
                if (games == 0)
                    return 0;
                else
                    return (double)GA / (double)games; 
            }
        }

        public double GAS
        {
            get
            {
                return (GF + GA > 0) ? (double)GF/(double)(GF + GA) : 0;
            }
        }

        public int Points
        {
            get { return (Wins * 2) + Ties; }
        }

        public int CompareTo(object obj)
        {
            clsTeam t = (clsTeam)obj;

            int ret = Points.CompareTo(t.Points);

            if (ret == 0)
                ret = Wins.CompareTo(t.Wins);

            if (ret == 0)
                ret = aRecord[t.Number - 1];

            if (ret == 0)
                ret = GAS.CompareTo(t.GAS);

            if (ret == 0)
                ret = (-1) * GA.CompareTo(t.GA);

            return ret;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
