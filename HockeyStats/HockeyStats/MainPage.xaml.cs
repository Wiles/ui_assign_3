using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HockeyStats
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Teams = new ObservableCollection<Team>();
            this.Players = new ObservableCollection<Player>();
            this.Games = new ObservableCollection<Game>();
            this.Games.CollectionChanged += (s, e) => CalculateStandings();
            this.pnlStats.Players = this.Players;
            this.pnlStats.Teams = this.Teams;
            this.pnlStandings.Games = this.Games;
            this.pnlStandings.Teams = this.Teams;
            this.pnlGames.Teams = this.Teams;
            this.pnlGames.Games = this.Games;
        }

        private ObservableCollection<Player> Players { get; set; }

        private ObservableCollection<Game> Games { get; set; }

        private ObservableCollection<Team> Teams { get; set; }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        
        async private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.FileTypeFilter.Add(".csv");

            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                var thing = await file.OpenSequentialReadAsync();

                var gameText = string.Empty;
                var teamText = string.Empty;
                var playerText = string.Empty;

                using (StreamReader sr = new StreamReader(thing.AsStreamForRead()))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Length > 0)
                        {
                            if (line[0] == 'T')
                            {
                                teamText += line + Environment.NewLine;
                            }
                            else if (line[0] == 'G')
                            {
                                gameText += line + Environment.NewLine;
                            }
                            else if (line[0] == 'P')
                            {
                                playerText += line + Environment.NewLine;
                            }
                        }
                    }
                }

                CsvSerializer csv = new CsvSerializer();
                Teams.Clear();
                Players.Clear();
                Games.Clear();
                this.Teams.AddRange(csv.Deserialize<Team>(teamText));
                this.Players.AddRange(csv.Deserialize<Player>(playerText));
                this.Games.AddRange(csv.Deserialize<Game>(gameText));
            }

            pnlStats.loadLists();
        }

        void CalculateStandings()
        {
            foreach (var team in Teams)
            {
                team.Wins = 0;
                team.Losses = 0;
                team.Ties = 0;
                team.GoalsAllowed = 0;
                team.GoalsScored = 0;
            }

            foreach (Game game in this.Games)
            {
                var home = Teams.First(t => t.Number == game.Home);
                var visitor = Teams.First(t => t.Number == game.Visitor);

                home.GoalsScored += game.HomeScore;
                home.GoalsAllowed += game.VisitorScore;

                visitor.GoalsAllowed += game.HomeScore;
                visitor.GoalsScored += game.VisitorScore;

                if (game.HomeScore > game.VisitorScore)
                {
                    home.Wins += 1;
                    visitor.Losses += 1;
                }
                else if (game.VisitorScore > game.HomeScore)
                {
                    home.Losses += 1;
                    visitor.Wins += 1;
                }
                else
                {
                    home.Ties += 1;
                    visitor.Ties += 1;
                }
            }

            this.pnlStandings.RefreshStats();
        }

        async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            savePicker.FileTypeChoices.Add("Comma Separated Values", new List<string>() { ".csv" });

            var file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                var thing = await file.OpenStreamForWriteAsync();
                using (StreamWriter sw = new StreamWriter(thing.AsOutputStream().AsStreamForWrite()))
                {
                    CsvSerializer csv = new CsvSerializer();
                    sw.Write(csv.Serialize(pnlStandings.Teams));
                    sw.Write(csv.Serialize(pnlStandings.Games));
                    sw.Write(csv.Serialize(pnlStats.Players));
                }
            }
        }

        private void btnStats_Click_1(object sender, RoutedEventArgs e)
        {
            pnlStandings.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            pnlGames.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            pnlStats.Visibility = Windows.UI.Xaml.Visibility.Visible;
            tbTitle.Text = "Statistics";
            pnlStats.loadLists();

            btnStats.FontStyle = Windows.UI.Text.FontStyle.Italic;
            btnStandings.FontStyle = Windows.UI.Text.FontStyle.Normal;
            btnGames.FontStyle = Windows.UI.Text.FontStyle.Normal;
        }

        private void btnStandings_Click_1(object sender, RoutedEventArgs e)
        {
            pnlStats.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            pnlGames.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            pnlStandings.Visibility = Windows.UI.Xaml.Visibility.Visible;
            tbTitle.Text = "Standings";

            btnStats.FontStyle = Windows.UI.Text.FontStyle.Normal;
            btnStandings.FontStyle = Windows.UI.Text.FontStyle.Italic;
            btnGames.FontStyle = Windows.UI.Text.FontStyle.Normal;
        }

        private void btnGames_Click_1(object sender, RoutedEventArgs e)
        {
            pnlStats.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            pnlStandings.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            pnlGames.Visibility = Windows.UI.Xaml.Visibility.Visible;
            tbTitle.Text = "Games";

            btnStats.FontStyle = Windows.UI.Text.FontStyle.Normal;
            btnStandings.FontStyle = Windows.UI.Text.FontStyle.Normal;
            btnGames.FontStyle = Windows.UI.Text.FontStyle.Italic;
        }

        async private void btnNews_Click_1(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> dGoals = new Dictionary<string, string>();
            Dictionary<string, string> dAssists = new Dictionary<string, string>();

            foreach (Player p in pnlStats.Players)
            {
                if (p.SessionGoals > 0)
                {
                    String goals = String.Empty;
                    if(dGoals.ContainsKey(p.Team))
                    {
                        goals = dGoals[p.Team] + ",";
                    }

                    goals += p.Name;

                    if(p.SessionGoals > 1){
                        goals += String.Format(@"({0})", p.SessionGoals);
                    }

                    dGoals[p.Team] = goals;
                }

                if (p.SessionAssists > 0)
                {
                    String assists = String.Empty;
                    if (dAssists.ContainsKey(p.Team))
                    {
                        assists = dAssists[p.Team] + ",";
                    }

                    assists += p.Name;

                    if (p.SessionAssists > 1)
                    {
                        assists += String.Format(@"({0})", p.SessionAssists);
                    }

                    dAssists[p.Team] = assists;
                }
            }

            List<string> keys = new List<string>();
            keys.AddRange(dGoals.Keys);
            keys.AddRange(dAssists.Keys);
            keys = keys.Distinct().ToList();
            String newsText = String.Empty;

            foreach (String teamName in keys)
            {
                newsText += String.Format(@"Team: {0}{1}", teamName, Environment.NewLine);
                if(dGoals.ContainsKey(teamName))
                {
                    newsText += String.Format(@"Goals: {0}{1}", dGoals[teamName], Environment.NewLine);
                }

                if (dAssists.ContainsKey(teamName))
                {
                    newsText += String.Format(@"Assists: {0}{1}", dAssists[teamName], Environment.NewLine);
                }
                newsText += String.Format(@"{0}", Environment.NewLine);
            }

            string message;
            if (!string.IsNullOrWhiteSpace(newsText))
            {
                var data = new DataPackage();
                data.SetText(newsText);
                Clipboard.SetContent(data);
                message = "The News Text has been copied to the clipboard.";
            }
            else
            {
                message = "No player information available.";
            }

            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }

        async private void btnExportStandings_Click(object sender, RoutedEventArgs e)
        {

            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            savePicker.FileTypeChoices.Add("Hyper Text Markup Language", new List<string>() { ".html" });

            var file = await savePicker.PickSaveFileAsync();

            if (file != null)
            {
                var thing = await file.OpenStreamForWriteAsync();

                using (StreamWriter sw = new StreamWriter(thing.AsOutputStream().AsStreamForWrite()))
                {
                    StringBuilder sb = new StringBuilder(
@"<table>
    <tbody>
        <tr>
            <th>
                <span>
                    <span>
                        <b>Regular Season Standings</b>
                    </span>
                </span>
            </th>
        </tr>
        <tr style=""background-color: #D49E09"">
            <th style=""width: 200px"">Team</th>
            <th style=""text-align: right; width: 30px"">G</th>
            <th style=""text-align: right; width: 30px"">W</th>
            <th style=""text-align: right; width: 30px"">L</th>
            <th style=""text-align: right; width: 30px"">T</th>
            <th style=""text-align: right; width: 30px"">PT</th>
            <th style=""text-align: right; width: 30px"">GF</th>
            <th style=""text-align: right; width: 30px"">GA</th>
            <th style=""text-align: right; width: 50px"">GAS</th>
        </tr>");
                    int i = 0;



                    foreach (var p in pnlStandings.Teams.OrderByDescending(t => t.AverageGoalsScoredPerGame).OrderByDescending(t => t.Points))
                    {
                        if (i % 2 == 1)
                        {
                            sb.AppendLine(p.ToHtmlRow("#F4BE29"));
                        }
                        else
                        {
                            sb.AppendLine(p.ToHtmlRow(null));
                        }
                        i++;
                    }
                    sb.AppendLine("</tbody>");
                    sb.AppendLine("</table>");
                    sb.AppendLine("<p></p>");
                    sb.Append(
@"<table>
    <tbody>
        <tr>
            <th>
                <span>
                    <span>
                        <b>Latest Game Results</b>
                    </span>
                </span>
            </th>
        </tr>
        <tr style=""background-color:#D49E09"">
            <th style=""width: 180px"">Home</th>
            <th style=""text-align: right; width: 30px""/>
            <th style=""width: 30px""/>
            <th style=""width: 180px"">Visitor</th>
            <th style=""text-align: right; width: 30px""/>
        </tr>");
                    i = 0;
                    foreach (var p in pnlStandings.Games.OrderByDescending(t => t.Date).Take((pnlStandings.Teams.Count + 1) / 2))
                    {
                        if (i % 2 == 1)
                        {
                            sb.AppendLine(p.ToHtmlRow("#F4BE29", pnlStandings.Teams));
                        }
                        else
                        {
                            sb.AppendLine(p.ToHtmlRow(null, pnlStandings.Teams));
                        }
                        i++;
                    }
                    sb.AppendLine("</tbody>");
                    sb.AppendLine("</table>");

                    sw.Write(sb.ToString());
                }
            }
        }

        async private void btnExportStatistics_Click(object sender, RoutedEventArgs e)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            savePicker.FileTypeChoices.Add("Hyper Text Markup Language", new List<string>() { ".html" });

            var file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                var thing = await file.OpenStreamForWriteAsync();
                using (StreamWriter sw = new StreamWriter(thing.AsOutputStream().AsStreamForWrite()))
                {
                    StringBuilder sb = new StringBuilder(
@"<table>
<tbody>
<tr><th><span><span><b>Regular Season Individual Stats</b></span></span></th></tr><tr style=""background-color:#D49E09""><th style=""width: 200px"">Name</th><th style=""width: 80px"">Team</th><th style=""text-align: right; width: 40px"">G</th><th style=""text-align: right; width: 40px"">A</th><th style=""text-align: right; width: 40px"">Pts</th><th style=""text-align: right; width: 40px"">PM</th></tr>
");
                    int i = 0;
                    foreach (var p in pnlStats.Players)
                    {
                        if (i % 2 == 1)
                        {
                            sb.AppendLine(p.ToHtmlRow("#F4BE29"));
                        }
                        else
                        {
                            sb.AppendLine(p.ToHtmlRow(null));
                        }
                        i++;
                    }
                    sb.AppendLine("</tbody>");
                    sb.AppendLine("</table>");

                    sw.Write(sb.ToString());
                }
            }
        }
    }
}
