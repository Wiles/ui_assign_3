using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GMHAStandings
{
    public partial class frmMain : Form
    {
        Dictionary<int,clsTeam> dTeam = new Dictionary<int,clsTeam>();
        List<clsGame> lGame = new List<clsGame>();

        public frmMain()
        {
            InitializeComponent();
        }

        private void updateListview()
        {
            foreach (clsTeam t in dTeam.Values)
            {
                t.Wins = 0;
                t.Losses = 0;
                t.Ties = 0;
                t.GA = 0;
                t.GF = 0;
                t.aRecord = new int[dTeam.Count];
            }

            foreach (clsGame g in lGame)
            {
                clsTeam tHome = dTeam[g.Home];
                clsTeam tVisitor = dTeam[g.Visitor];

                tHome.GA += g.VisitorScore;
                tVisitor.GA += g.HomeScore;
                tHome.GF += g.HomeScore;
                tVisitor.GF += g.VisitorScore;

                if (g.HomeScore > g.VisitorScore)
                {
                    tHome.Wins++;
                    tVisitor.Losses++;
                    tHome.aRecord[tVisitor.Number - 1] += 1;
                    tVisitor.aRecord[tHome.Number - 1] -= 1;
                }
                else if (g.HomeScore < g.VisitorScore)
                {
                    tHome.Losses++;
                    tVisitor.Wins++;
                    tHome.aRecord[tVisitor.Number - 1] -= 1;
                    tVisitor.aRecord[tHome.Number - 1] += 1;
                }
                else
                {
                    tHome.Ties++;
                    tVisitor.Ties++;
                }
            }

            lvStandings.Items.Clear();
            var kvps = from t in dTeam
                       orderby t.Value descending
                       select new KeyValuePair<int, clsTeam>(t.Key, t.Value);

            foreach (KeyValuePair<int, clsTeam> kvp in kvps)
            {
                clsTeam t = (clsTeam)kvp.Value;
                ListViewItem lvi = new ListViewItem();
                lvi.Text = t.Name;
                lvi.SubItems.Add((t.Wins + t.Losses + t.Ties).ToString());
                lvi.SubItems.Add(t.Wins.ToString());
                lvi.SubItems.Add(t.Losses.ToString());
                lvi.SubItems.Add(t.Ties.ToString());
                lvi.SubItems.Add(t.Points.ToString());
                lvi.SubItems.Add(t.GF.ToString());
                lvi.SubItems.Add(t.GA.ToString());
                //lvi.SubItems.Add(t.GAA.ToString("0.00"));
                lvi.SubItems.Add(t.GAS.ToString("0.000"));
                lvStandings.Items.Add(lvi);
            }
        }

        private void updateCombos()
        {
            foreach (clsTeam t in dTeam.Values)
            {
                cbHome.Items.Add(t);
                cbVisitor.Items.Add(t);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            clsGame g = new clsGame();

            g.Date = dtpGame.Value;
            g.Home = ((clsTeam)cbHome.SelectedItem).Number;
            g.Visitor = ((clsTeam)cbVisitor.SelectedItem).Number;
            g.HomeScore = (int)udHome.Value;
            g.VisitorScore = (int)udVisitor.Value;
            lGame.Add(g);

            updateListview();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofDlg.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(ofDlg.FileName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);

                dTeam.Clear();
                lGame.Clear();

                while (!sr.EndOfStream)
                {
                    string ln = sr.ReadLine();
                    string[] els = ln.Split(new char[] { ',' });

                    if (els[0] == "T")
                    {
                        clsTeam t = new clsTeam();
                        t.Number = Int32.Parse(els[1]);
                        t.Name = els[2];
                        dTeam.Add(t.Number, t);
                    }
                    else if (els[0] == "G")
                    {
                        clsGame g = new clsGame();
                        g.Date = DateTime.Parse(els[1]);
                        g.Home = Int32.Parse(els[2]);
                        g.HomeScore = Int32.Parse(els[3]);
                        g.Visitor = Int32.Parse(els[4]);
                        g.VisitorScore = Int32.Parse(els[5]);

                        lGame.Add(g);
                    }
                }

                sr.Close();
                fs.Close();

                updateListview();
                updateCombos();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfDlg.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(sfDlg.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                foreach (clsTeam t in dTeam.Values)
                    sw.WriteLine("T," + t.Number + "," + t.Name);

                foreach (clsGame g in lGame)
                    sw.WriteLine("G," + g.Date.ToShortDateString() + "," + g.Home + "," + g.HomeScore + "," + g.Visitor + "," + g.VisitorScore);

                sw.Close();
                fs.Close();
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfDlg.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(sfDlg.FileName, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine("<table>");
                sw.WriteLine("<tbody>");
                sw.WriteLine("<tr><th><span><span><b>Regular Season Standings</b></span></span></th></tr>");
                sw.Write("<tr style=\"background-color: #D49E09\"><th style=\"width: 200px\">Team</th>");
                sw.Write("<th style=\"text-align: right; width: 30px\">G</th>");
                sw.Write("<th style=\"text-align: right; width: 30px\">W</th>");
                sw.Write("<th style=\"text-align: right; width: 30px\">L</th>");
                sw.Write("<th style=\"text-align: right; width: 30px\">T</th>");
                sw.Write("<th style=\"text-align: right; width: 30px\">PT</th>");
                sw.Write("<th style=\"text-align: right; width: 30px\">GF</th>");
                sw.Write("<th style=\"text-align: right; width: 30px\">GA</th>");
                //sw.Write("<th style=\"text-align: right; width: 50px\">GAA</th>");
                sw.Write("<th style=\"text-align: right; width: 50px\">GAS</th>");
                sw.WriteLine("</tr>");

                var kvps = from t in dTeam
                           orderby t.Value descending
                           select new KeyValuePair<int, clsTeam>(t.Key, t.Value);

                string alt = "style =\"background-color: #F4BE29\"";
                bool bShowAlt = false;

                foreach (KeyValuePair<int, clsTeam> kvp in kvps)
                {
                    clsTeam t = (clsTeam)kvp.Value;
                    sw.WriteLine("<tr " + (bShowAlt ? alt : string.Empty) + ">");
                    sw.WriteLine("<td>" + t.Name + "</td>");
                    sw.WriteLine("<td style=\"text-align: right\">" + (t.Wins + t.Losses + t.Ties).ToString() + "</td>");
                    sw.WriteLine("<td style=\"text-align: right\">" + t.Wins + "</td>");
                    sw.WriteLine("<td style=\"text-align: right\">" + t.Losses + "</td>");
                    sw.WriteLine("<td style=\"text-align: right\">" + t.Ties + "</td>");
                    sw.WriteLine("<td style=\"text-align: right\"><b>" + t.Points + "</b></td>");
                    sw.WriteLine("<td style=\"text-align: right\">" + t.GF + "</td>");
                    sw.WriteLine("<td style=\"text-align: right\">" + t.GA + "</td>");
                    //sw.WriteLine("<td style=\"text-align: right\">" + t.GAA.ToString("0.00") + "</td>");
                    sw.WriteLine("<td style=\"text-align: right\">" + t.GAS.ToString("0.000") + "</td>");
                    sw.WriteLine("</tr>");
                    bShowAlt = !bShowAlt;
                }

                sw.WriteLine("</tbody>");
                sw.WriteLine("</table>");

                sw.WriteLine("<p></p>");

                sw.WriteLine("<table>");
                sw.WriteLine("<tbody>");
                sw.WriteLine("<tr><th><span><span><b>Latest Game Results</b></span></span></th></tr>");
                sw.Write("<tr style=\"background-color:#D49E09\"><th style=\"width: 180px\">Home</th>");
                sw.Write("<th style=\"text-align: right; width: 30px\"></th>");
                sw.Write("<th style=\"width: 30px\"></th>");
                sw.Write("<th style=\"width: 180px\">Visitor</th>");
                sw.Write("<th style=\"text-align: right; width: 30px\"></th>");
                sw.WriteLine("</tr>");

                int numGames = (dTeam.Keys.Count + 1) / 2;
                int startGame = lGame.Count - numGames;

                if (startGame < 0)
                    startGame = 0;

                bShowAlt = false;
                alt = " style=\"background-color:#F4BE29\"";

                for (int i = startGame; i < lGame.Count; i++)
                {
                    clsGame g = lGame[i];
                    clsTeam th = dTeam[g.Home];
                    clsTeam tv = dTeam[g.Visitor];

                    sw.WriteLine("<tr" + (bShowAlt ? alt : string.Empty) + ">");
                    sw.WriteLine("<td>" + th.Name + "</td>");
                    sw.WriteLine("<td style=\"text-align: right\">" + g.HomeScore.ToString() + "</td>");
                    sw.WriteLine("<td></td>");
                    sw.WriteLine("<td>" + tv.Name + "</td>");
                    sw.WriteLine("<td style=\"text-align: right\">" + g.VisitorScore.ToString() + "</td>");
                    sw.WriteLine("</tr>");
                    bShowAlt = !bShowAlt;
                }

                sw.WriteLine("</tbody>");
                sw.WriteLine("</table>");

                sw.Close();
                fs.Close();
            }
        }
    }
}
