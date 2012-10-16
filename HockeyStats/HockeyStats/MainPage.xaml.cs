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
        }

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
            if (pnlStats.Visibility == Windows.UI.Xaml.Visibility.Visible)
            {
                var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
                openPicker.FileTypeFilter.Add(".csv");

                var file = await openPicker.PickSingleFileAsync();
                if (file != null)
                {
                    var thing = await file.OpenSequentialReadAsync();
                    CsvSerializer csv = new CsvSerializer();
                    using (StreamReader sr = new StreamReader(thing.AsStreamForRead()))
                    {
                        var foo = csv.Deserialize<Player>(sr.ReadToEnd());
                        pnlStats.Players.Clear();
                        pnlStats.Players.AddRange(foo);
                    }
                }
            }
            else
            {
                var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
                openPicker.FileTypeFilter.Add(".csv");

                var file = await openPicker.PickSingleFileAsync();
                if (file != null)
                {
                    var thing = await file.OpenSequentialReadAsync();

                    var gameText = string.Empty;
                    var teamText = string.Empty;

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
                            }
                        }
                    }

                    CsvSerializer csv = new CsvSerializer();
                    var games = csv.Deserialize<Game>(gameText);
                    var teams = csv.Deserialize<Team>(teamText);

                    pnlStandings.Games.AddRange(games);
                    pnlStandings.Teams.AddRange(teams);
                }
            }
            pnlStats.loadLists();
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
                    sw.Write(csv.Serialize(pnlStats.Players));
                }
            }
            else
            {
                //TODO make this not crappy.
//                throw new Exception();
            }
            
        }

        async private void btnExport_Click(object sender, RoutedEventArgs e)
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
                    foreach (var p in pnlStats.Players)
                    {
                        sb.AppendLine(p.ToHtmlRow());
                    }
                    sb.AppendLine("</tbody>");
                    sb.AppendLine("</table>");

                    sw.Write(sb.ToString());
                }
            }
            else
            {
                //TODO make this not crappy.
                //                throw new Exception();
            }
        }

        private void btnStats_Click_1(object sender, RoutedEventArgs e)
        {
            pnlStandings.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            pnlStats.Visibility = Windows.UI.Xaml.Visibility.Visible;
            btnNews.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void btnStandings_Click_1(object sender, RoutedEventArgs e)
        {
            pnlStats.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            pnlStandings.Visibility = Windows.UI.Xaml.Visibility.Visible;
            btnNews.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
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
    }
}
