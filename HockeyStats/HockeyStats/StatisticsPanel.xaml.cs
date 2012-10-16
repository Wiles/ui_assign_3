using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
            cbTeam.ItemsSource = teams;
        }

        private ObservableCollection<Player> players { get; set; }
        public ObservableCollection<Player> Players
        {
            get
            {
                return players;
            }
            set
            {
                players = value;
                gvPlayers.ItemsSource = players;
                players.CollectionChanged += (s, e) =>
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
        }

        private ObservableCollection<Team> teams { get; set; }
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
        
        async private void btnAdd_Click_1(object sender, RoutedEventArgs e)
        {
            int goals;
            int assists;
            int penalty;

            if (string.IsNullOrEmpty(tbName.Text))
            {
                var dialog = new MessageDialog("Each player must have a name.");
                await dialog.ShowAsync();
                return;
            }

            if( Players.Select(t => t).Where(t => t.Name.Equals(tbName.Text)).Count() != 0)
            {
                var dialog = new MessageDialog("A player with that name already exists.");
                await dialog.ShowAsync();
                return;
            }

            if (string.IsNullOrEmpty((string)cbTeam.SelectedItem))
            {
                var dialog = new MessageDialog("Each player must have a team.");
                await dialog.ShowAsync();
                return;
            }

            if (!Int32.TryParse(tbGoals.Text, out goals))
            {
                var dialog = new MessageDialog("Goals cannot be empty. Enter 0 for no goals.");
                await dialog.ShowAsync();
                return;
            }

            if (!Int32.TryParse(tbAssists.Text, out assists))
            {
                var dialog = new MessageDialog("Assists cannot be empty. Enter 0 for no assists.");
                await dialog.ShowAsync();
                return;
            }

            if (!Int32.TryParse(tbPenalties.Text, out penalty))
            {
                var dialog = new MessageDialog("Penality minutes cannot be empty. Enter 0 for no penality minutes.");
                await dialog.ShowAsync();
                return;
            }

            var player = Players.FirstOrDefault(p => p.Name.ToLower() == tbName.Text.ToLower()
                    && p.Team.ToLower() == ((string)cbTeam.SelectedItem).ToLower());

            if (player == null)
            {
                player = new Player(tbName.Text, ((string)cbTeam.SelectedItem), goals, assists, penalty);
                this.Players.Add(player);
            }
            else
            {
                player.AddGoals(goals);
                player.AddAssists(assists);
                player.AddPenaltyMinutes(penalty);
                gvPlayers.ItemsSource = Players;
            }
            cbTeam.SelectedIndex = -1;
            tbName.Text = String.Empty;
            tbGoals.Text = String.Empty;
            tbAssists.Text = String.Empty;
            tbPenalties.Text = String.Empty;
            loadNames();
            loadTeams();
        }

        private void tbName_GotFocus(object sender, RoutedEventArgs e)
        {
            lbName.Visibility = loadNames() ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void tbName_LostFocus(object sender, RoutedEventArgs e)
        {
            lbName.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public void loadLists()
        {
            loadNames();
            loadTeams();
        }

        private bool loadNames()
        {
            var b = false;
            lbName.Items.Clear();

            if (string.IsNullOrEmpty(tbName.Text))
            {
                return false;
            }

            foreach(Player p in Players)
            {
                if(AutoCompleteCheck(p.Name, tbName.Text))
                {
                    lbName.Items.Add(p.Name);
                    b = true;
                }
            }

            return b;
        }

        private bool loadTeams()
        {
            var b = false;
            cbTeam.SelectedIndex = -1;

            foreach (var team in teams.Select(p => p.Color).Distinct())
            {
                cbTeam.Items.Add(team);
            }

            return b;
        }

        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbName.Visibility = loadNames() ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void lbName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbName.SelectedItem != null)
            {
                tbName.Text = (String)lbName.SelectedItem;
            }
        }

        private void gvPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gvPlayers.SelectedItem != null)
            {
                tbName.Text = ((Player)gvPlayers.SelectedItem).Name;
                cbTeam.SelectedIndex = cbTeam.Items.IndexOf(((Player)gvPlayers.SelectedItem).Team);
            }

            lbName.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private bool AutoCompleteCheck(string name, string search)
        {
            if (name == search)
            {
                return false;
            }

            var array = name.ToLower().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var str in array)
            {
                if (str.StartsWith(search.ToLower()))
                {
                    return true;
                }
            }

            if (name.ToLower().StartsWith(search.ToLower()))
            {
                return true;
            }

            return false;
        }

        private bool AutoCompleteCheck(string name, string search, string team)
        {
            //p.Name.ToLower().Contains(tbName.Text) && !lbName.Items.Contains(p.Team)
            var array = name.ToLower().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var str in array)
            {
                if (str.StartsWith(search.ToLower()) && !lbName.Items.Contains(team))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
