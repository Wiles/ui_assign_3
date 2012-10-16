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
        public DateTime Date { get; set; }


        [CsvColumn(1)]
        public int Visitor { get; set; }


        [CsvColumn(3)]
        public int Home { get; set; }


        [CsvColumn(2)]
        public int VisitorScore { get; set; }


        [CsvColumn(4)]
        public int HomeScore { get; set; }
    }
}
