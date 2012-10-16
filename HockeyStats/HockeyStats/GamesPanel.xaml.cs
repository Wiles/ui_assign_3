using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HockeyStats
{
    public sealed partial class GamesPanel : UserControl
    {
        public GamesPanel()
        {
            this.InitializeComponent();
        }
        
        private ObservableCollection<Game> games;
        public ObservableCollection<Game> Games
        {
            get
            {
                return games;
            }
            set
            {
                games = value;
                games.CollectionChanged += (s, e) =>
                {
                    models = new ObservableCollection<GameModel>();

                    foreach (var game in games.OrderByDescending(g => g.Date))
                    {
                        models.Add(CreateModel(game));
                    }

                    gvGames.ItemsSource = models;
                };
            }
        }

        private ObservableCollection<Team> teams;
        public ObservableCollection<Team> Teams
        {
            get
            {
                return teams;
            }
            set
            {
                teams = value;
            }
        }

        private ObservableCollection<GameModel> models { get; set; }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var game = new Game();

            var error = false;

            if (!dtpGame.Value.HasValue)
            {
                error = true;
            }

            if (error)
            {
                return;
            }

            game.Date = dtpGame.Value.Value;
        }

        private GameModel CreateModel(Game game)
        {
            var model = new GameModel();

            var home = Teams.FirstOrDefault(t => t.Number == game.Home);
            var visitor = Teams.FirstOrDefault(t => t.Number == game.Visitor);

            if (home != null)
                model.Home = home.Name;
            if (visitor != null)
                model.Visitor = visitor.Name;

            model.Date = game.Date.ToString("d MMMM yyyy");
            model.HomeScore = game.HomeScore;
            model.VisitorScore = game.VisitorScore;

            return model;
        }
    }

    class GameModel
    {
        public string Date { get; set; }

        public string Home { get; set; }

        public string Visitor { get; set; }

        public int VisitorScore { get; set; }

        public int HomeScore { get; set; }

        public string WinningTeam
        {
            get
            {
                if (VisitorScore > HomeScore)
                    return "Visitor";
                else if (HomeScore > VisitorScore)
                    return "Home";
                else
                    return "Tie";
            }
        }
    }
}
