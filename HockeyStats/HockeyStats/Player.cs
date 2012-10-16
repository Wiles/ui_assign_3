using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HockeyStats
{
    public class Player : INotifyPropertyChanged
    {
        [CsvColumn(0)]
        public String Id { get; set; }

        [CsvColumn(1)]
        public String Name { get; set; }

        [CsvColumn(2)]
        public String Team { get; set; }

        [CsvColumn(3)]
        public Int32 Goals { get; set; }

        [CsvColumn(4)]
        public Int32 Assists { get; set; }

        public Int32 SessionGoals { get; set; }

        public Int32 SessionAssists { get; set; }

        public Int32 SessionPenaltyMinutes { get; set; }

        [CsvColumn(5)]
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
            this.Id = "P";
        }

        public Player(String name, String team, Int32 goals, Int32 assists, Int32 penaltyMinutes) : this()
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
            NotifyPropertyChanged("Goals");
        }

        public void AddAssists(Int32 Assists)
        {
            this.Assists += Assists;
            this.SessionAssists += Assists;
            NotifyPropertyChanged("Assists");
        }

        public void AddPenaltyMinutes(Int32 PenaltyMinutes)
        {
            this.PenaltyMinutes += PenaltyMinutes;
            this.SessionPenaltyMinutes += PenaltyMinutes;
            NotifyPropertyChanged("PenaltyMinutes");
        }

        public string ToHtmlRow(String color)
        {
            return String.Format(
@"<tr{0}>
<td>{1}</td>
<td>{2}</td>
<td style=""text-align: right"">{3}</td>
<td style=""text-align: right"">{4}</td>
<td style=""text-align: right"">{6}</td>
<td style=""text-align: right"">{5}</td>
</tr>", (color != null) ? " style =\"background-color:" + color + "\"" : "", Name, Team, Goals, Assists, PenaltyMinutes, Points);
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
