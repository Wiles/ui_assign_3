using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HockeyStats
{
    class Game
    {
        public DateTime Date { get; set; }

        public int Visitor { get; set; }

        public int Home { get; set; }

        public int VisitorScore { get; set; }

        public int HomeScore { get; set; }
    }
}
