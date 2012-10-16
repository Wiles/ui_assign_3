using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HockeyStats
{
    public class Player
    {
        [CsvColumn(0)]
        public String Name { get; set; }

        [CsvColumn(1)]
        public String Team { get; set; }

        [CsvColumn(2)]
        public Int32 Goals { get; set; }

        [CsvColumn(3)]
        public Int32 Assists { get; set; }


        public Int32 SessionGoals { get; set; }

        public Int32 SessionAssists { get; set; }
        public Int32 SessionPenaltyMinutes { get; set; }

        [CsvColumn(4)]
        public Int32 PenaltyMinutes { get; set; }

        public int Points
        {
            get
            {
                return this.Goals + this.Assists;
            }
        }

        public Player()
        {
        }

        public Player(String name, String team, Int32 goals, Int32 assists, Int32 penaltyMinutes)
        {
            this.Name = name;
            this.Team = team;
            this.Goals = goals;
            this.Assists = assists;
            this.PenaltyMinutes = penaltyMinutes;
            this.SessionAssists = Assists;
            this.SessionGoals = goals;
            this.SessionPenaltyMinutes = penaltyMinutes;
        }

        public void AddGoals(Int32 Goals)
        {
            this.Goals += Goals;
            this.SessionGoals += Goals;
        }

        public void AddAssists(Int32 Assists)
        {
            this.Assists += Assists;
            this.SessionAssists += Assists;
        }

        public void AddPenaltyMinutes(Int32 PenaltyMinutes)
        {
            this.PenaltyMinutes += PenaltyMinutes;
            this.SessionPenaltyMinutes += PenaltyMinutes;
        }

        public string ToHtmlRow()
        {
            return String.Format(
@"<tr>
<td>{0}</td>
<td>{1}</td>
<td style=""text-align: right"">{2}</td>
<td style=""text-align: right"">{3}</td>
<td style=""text-align: right"">{5}</td>
<td style=""text-align: right"">{4}</td>
</tr>", Name, Team, Goals, Assists, PenaltyMinutes, Points);
        }
    }
}
