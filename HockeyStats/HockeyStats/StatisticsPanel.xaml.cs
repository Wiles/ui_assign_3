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
    public sealed partial class StatisticsPanel : UserControl
    {
        public StatisticsPanel()
        {
            this.InitializeComponent();
            this.Players = new ObservableCollection<Player>();

            gvPlayers.ItemsSource = this.Players;
            this.Players.CollectionChanged += (s, e) =>
                {
                    var goals = 0;
                    var assists = 0;
                    var penalty = 0;
                    var points = 0;

                    foreach (var p in this.Players)
                    {
                        goals += p.Goals;
                        assists += p.Assists;
                        penalty += p.PenaltyMinutes;
                        points += p.Points;
                    }

                    lblGoals.Text = goals.ToString();
                    lblAssists.Text = assists.ToString();
                    lblPenalty.Text = penalty.ToString();
                    lblPoints.Text = points.ToString();
                };
        }

        public ObservableCollection<Player> Players { get; private set; }
        
        private void btnAdd_Click_1(object sender, RoutedEventArgs e)
        {
            bool error = false;
            int goals;
            int assists;
            int penalty;

            if (string.IsNullOrEmpty(tbName.Text))
            {
                // make shit red
                error = true;
            }

            if (string.IsNullOrEmpty(tbTeam.Text))
            {
                // make shit red
                error = true;
            }

            if (!Int32.TryParse(tbGoals.Text, out goals))
            {
                // red shit everywhere
                error = true;
            }

            if (!Int32.TryParse(tbAssists.Text, out assists))
            {
                // red shit everywhere
                error = true;
            }

            if (!Int32.TryParse(tbPenalties.Text, out penalty))
            {
                // red shit everywhere
                error = true;
            }

            if (error)
            {
                return;
            }

            var player = Players.FirstOrDefault(p => p.Name.ToLower() == tbName.Text.ToLower()
                    && p.Team.ToLower() == tbTeam.Text.ToLower());

            if (player == null)
            {
                player = new Player(tbName.Text, tbTeam.Text, goals, assists, penalty);
                this.Players.Add(player);
            }
            else
            {
                player.AddGoals(goals);
                player.AddAssists(assists);
                player.AddPenaltyMinutes(penalty);
                gvPlayers.ItemsSource = Players;
            }

            tbName.Text = String.Empty;
            tbTeam.Text = String.Empty;
            tbGoals.Text = String.Empty;
            tbAssists.Text = String.Empty;
            tbPenalties.Text = String.Empty;
            loadNames();
            loadTeams();
        }

        private void tbName_GotFocus(object sender, RoutedEventArgs e)
        {
            lbName.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void tbName_LostFocus(object sender, RoutedEventArgs e)
        {
            lbName.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }


        private void tbTeam_GotFocus(object sender, RoutedEventArgs e)
        {
            lbTeam.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void tbTeam_LostFocus(object sender, RoutedEventArgs e)
        {
            lbTeam.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public void loadLists()
        {
            loadNames();
            loadTeams();
        }

        private void loadNames()
        {
            lbName.Items.Clear();
            foreach(Player p in Players)
            {
                if(p.Name.ToLower().Contains(tbName.Text))
                {
                    lbName.Items.Add(p.Name);
                }
            }
        }


        private void loadTeams()
        {
            lbName.Items.Clear();
            foreach (Player p in Players)
            {
                if (p.Name.ToLower().Contains(tbName.Text) && !lbName.Items.Contains(p.Team))
                {
                    lbName.Items.Add(p.Name);
                }
            }
        }

        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadNames();
        }

        private void lbName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbName.SelectedItem != null)
            {
                tbName.Text = (String)lbName.SelectedItem;
            }
        }

        private void lbTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lbTeam.SelectedItem != null)
            {
                tbTeam.Text = (String)lbTeam.SelectedItem;
            }
        }

        private void gvPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gvPlayers.SelectedItem != null)
            {
                tbName.Text = ((Player)gvPlayers.SelectedItem).Name;
                tbTeam.Text = ((Player)gvPlayers.SelectedItem).Team;
            }
        }
    }
}
