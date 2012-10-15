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
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.FileTypeFilter.Add(".csv");

            var file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                var thing = await file.OpenSequentialReadAsync();
                CsvSerializer csv = new CsvSerializer();
                StreamReader sr = new StreamReader(thing.AsStreamForRead());
                var foo = csv.Deserialize<Player>(sr.ReadToEnd());
                pnlStats.Players.Clear();
                foreach (Player p in foo)
                {
                    pnlStats.Players.Add(p);
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
                    sw.Write(csv.Serialize(pnlStats.Players));
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
        }

        private void btnStandings_Click_1(object sender, RoutedEventArgs e)
        {
            pnlStats.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            pnlStandings.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }
    }
}
