using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace HockeyStats
{
    public class Team : INotifyPropertyChanged
    {
        [CsvColumn(0)]
        public string id { get; set; }

        [CsvColumn(1)]
        public int Number { get; set; }

        [CsvColumn(2)]
        public string Name { get; set; }

        [CsvColumn(3)]
        public string Color { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Ties { get; set; }

        public int GoalsScored { get; set; }

        public int GoalsAllowed { get; set; }

        public int[] aRecord { get; set; }

        public int Games { get { return Wins + Losses + Ties; } }

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

        public string DiffString 
        {
            get
            {
                return AverageGoalsScoredPerGame.ToString("0.000");
            }
        }

        public string ToHtmlRow(String color)
        {
                        return String.Format(
@"<tr{0}>
<td>{1}</td>
<td style=""text-align: right"">{2}</td>
<td style=""text-align: right"">{3}</td>
<td style=""text-align: right"">{4}</td>
<td style=""text-align: right"">{5}</td>
<td style=""text-align: right""><b>{6}</b></td>
<td style=""text-align: right"">{7}</td>
<td style=""text-align: right"">{8}</td>
<td style=""text-align: right"">{9}</td>
</tr>", (color != null)?" style =\"background-color:" + color +"\"":"", Name, Games, Wins, Losses, Ties, Points, GoalsScored, GoalsAllowed, DiffString);
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

        public override string ToString()
        {
            return this.Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
