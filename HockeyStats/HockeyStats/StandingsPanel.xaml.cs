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
    public sealed partial class StandingsPanel : UserControl
    {
        public StandingsPanel()
        {
            this.InitializeComponent();
        }

        private ObservableCollection<Game> games { get; set; }
        public ObservableCollection<Game> Games
        {
            get
            {
                return games;
            }
            set
            {
                games = value;
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
                gvStandings.ItemsSource = teams;
            }
        }

        async private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text) || string.IsNullOrWhiteSpace(tbColor.Text))
            {
                return;
            }

            if(Teams.Select(t => t).Where(t => t.Name.Equals(tbName)) != null || Teams.Select(t => t).Where(t => t.Color.Equals(tbColor)) != null)
            {

                var dialog = new MessageDialog("A Team with that name or color already exists.");
                await dialog.ShowAsync();
                return;
            }

            var team = new Team();
            team.id = "T";
            team.Name = tbName.Text;
            team.Color = tbColor.Text;

            this.Teams.Add(team);
            
            tbName.Text = string.Empty;
            tbColor.Text = string.Empty;
        }

        public void RefreshStats()
        {
            var list = this.Teams.ToList();
            this.Teams.Clear();
            this.Teams.AddRange(list);
        }
    }
}
