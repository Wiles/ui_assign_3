using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HockeyStats
{
    public class Game
    {
        [CsvColumn(0)]
        public string id { get; set; }

        [CsvColumn(1)]
        public DateTime Date { get; set; }

        [CsvColumn(4)]
        public int Visitor { get; set; }
        
        [CsvColumn(2)]
        public int Home { get; set; }

        [CsvColumn(5)]
        public int VisitorScore { get; set; }

        [CsvColumn(3)]
        public int HomeScore { get; set; }

        public string ToHtmlRow(string color, ICollection<Team> teams)
        {
            String HomeTeam = Home.ToString();
            String VisitingTeam = Visitor.ToString();

            foreach (Team team in teams)
            {
                if(team.Number == Home)
                {
                    HomeTeam = team.Name;
                } 
                else if (team.Number == Visitor)
                {
                    VisitingTeam = team.Name;
                }
            }
                        return String.Format(
@"<tr{0}>
<td>{1}</td>
<td style=""text-align: right"">{2}</td>
<td></td>
<td>{3}</td>
<td style=""text-align: right"">{4}</td>
</tr>", (color != null) ? " style =\"background-color:" + color + "\"" : "", HomeTeam, HomeScore, VisitingTeam, VisitorScore);
        }
    }
}
