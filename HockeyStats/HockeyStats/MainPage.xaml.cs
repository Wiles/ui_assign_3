using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
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
        ObservableCollection<Player> Players { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            this.Players = new ObservableCollection<Player>();
            gvPlayers.ItemsSource = this.Players;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
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

            if (error) return;

            Player p = new Player(tbName.Text, tbTeam.Text, goals, assists, penalty);
            tbName.Text = String.Empty;
            tbTeam.Text = String.Empty;
            tbGoals.Text = String.Empty;
            tbAssists.Text = String.Empty;
            tbPenalties.Text = String.Empty;
            
            this.Players.Add(p);
        }

        private class Player
        {
            [CsvColumn(0)]
            public String Name { get; set; }

            [CsvColumn(1)]
            public String Team { get; set; }

            [CsvColumn(2)]
            public Int32 Goals { get; set; }

            [CsvColumn(3)]
            public Int32 Assists { get; set; }

            [CsvColumn(4)]
            public Int32 PenaltyMinutes { get; set; }

            public int Points
            {
                get
                {
                    return this.Goals + this.Assists;
                }
            }

            public Player()
            {
            }

            public Player(String name, String team, Int32 goals, Int32 assists, Int32 penaltyMinutes)
            {
                this.Name = name;
                this.Team = team;
                this.Goals = goals;
                this.Assists = assists;
                this.PenaltyMinutes = penaltyMinutes;
            }

            public string ToHtmlRow()
            {
                return String.Format(
@"<tr>
<td>{0}</td>
<td>{1}</td>
<td style=""text-align: right"">{2}</td>
<td style=""text-align: right"">{3}</td>
<td style=""text-align: right"">{5}</td>
<td style=""text-align: right"">{4}</td>
</tr>", Name, Team, Goals, Assists, PenaltyMinutes, Points);
            }
        }

        async private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.FileTypeFilter.Add(".csv");

            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                var thing = await file.OpenSequentialReadAsync();
                CsvSerializer csv = new CsvSerializer();
                StreamReader sr = new StreamReader(thing.AsStreamForRead());
                var foo = csv.Deserialize<Player>(sr.ReadToEnd());
                Players.Clear();
                foreach (Player p in foo)
                {
                    Players.Add(p);
                }
            }
            else
            {
                //TODO make this not crappy.
                //throw new Exception();
            }
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
                    sw.Write(csv.Serialize(Players));
                }
            }
            else
            {
                //TODO make this not crappy.
//                throw new Exception();
            }
            
        }

        private void gvPlayers_ItemClick(object sender, ItemClickEventArgs e)
        {
            var a = e.ClickedItem;

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
                    foreach(var p in Players)
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
    }
}
