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
    public sealed partial class StandingsPanel : UserControl
    {
        public StandingsPanel()
        {
            this.InitializeComponent();
            this.Games = new ObservableCollection<Game>();
            this.Teams = new ObservableCollection<Team>();
            this.Teams.CollectionChanged += (s, e) =>
                {
                };
        }

        public ObservableCollection<Game> Games { get; private set; }

        public ObservableCollection<Team> Teams { get; private set; }
        
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
    }
}
