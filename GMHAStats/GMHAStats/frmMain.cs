using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GMHAStats
{
    public partial class frmMain : Form
    {
        int statItemIndex = -1;
        bool bChanged = false;
        Dictionary<string, clsStatItem> dStats = new Dictionary<string, clsStatItem>();

        public frmMain()
        {
            InitializeComponent();
        }

        private void updateListview()
        {
            try
            {
                lvStats.Items.Clear();
                var kvps = from si in dStats
                           orderby si.Value descending
                           select new KeyValuePair<string, clsStatItem>(si.Key, si.Value);

                foreach (KeyValuePair<string, clsStatItem> kvp in kvps)
                {
                    clsStatItem si = (clsStatItem)kvp.Value;
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = si.Name;
                    lvi.SubItems.Add(si.Team);
                    lvi.SubItems.Add(si.Goals.ToString());
                    lvi.SubItems.Add(si.Assists.ToString());
                    lvi.SubItems.Add(si.PenaltyMin.ToString());
                    lvi.SubItems.Add((si.Goals + si.Assists).ToString());
                    lvStats.Items.Add(lvi);
                }
            }
            catch (Exception ex)
            {
                clsError.SendError(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int goals = 0;
                int assists = 0;
                int penalty = 0;
                Int32.TryParse(tbGoals.Text, out goals);
                Int32.TryParse(tbAssists.Text, out assists);
                Int32.TryParse(tbPenalty.Text, out penalty);

                if (dStats.ContainsKey(tbName.Text + tbTeam.Text))
                {
                    clsStatItem si = dStats[tbName.Text + tbTeam.Text];
                    si.TodayGoals += goals;
                    si.TodayAssists += assists;
                    si.TodayPenalty += penalty;
                }
                else
                {
                    clsStatItem si = new clsStatItem();
                    si.Name = tbName.Text;
                    si.Team = tbTeam.Text;
                    si.TodayGoals = goals;
                    si.TodayAssists = assists;
                    si.TodayPenalty = penalty;

                    dStats.Add(si.Name + si.Team, si);
                }

                tbName.Text = string.Empty;
                tbTeam.Text = string.Empty;
                tbGoals.Text = string.Empty;
                tbAssists.Text = string.Empty;
                tbPenalty.Text = string.Empty;

                updateListview();

                tbName.Focus();
            }
            catch (Exception ex)
            {
                clsError.SendError(ex);
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (sfDlg.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(sfDlg.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine("<table>");
                    sw.WriteLine("<tbody>");

                    var kvps = from si in dStats
                               orderby si.Value descending
                               select new KeyValuePair<string, clsStatItem>(si.Key, si.Value);

                    bool bShowAlt = false;
                    string alt = " style=\"background-color:#F4BE29\"";

                    sw.Write("<tr><th><span><span><b>Regular Season Individual Stats</b></span></span></th></tr>");
                    sw.Write("<tr style=\"background-color:#D49E09\">");
                    sw.Write("<th style=\"width: 200px\">Name</th>");
                    sw.Write("<th style=\"width: 80px\">Team</th>");
                    sw.Write("<th style=\"text-align: right; width: 40px\">G</th>");
                    sw.Write("<th style=\"text-align: right; width: 40px\">A</th>");
                    sw.Write("<th style=\"text-align: right; width: 40px\">Pts</th>");
                    sw.Write("<th style=\"text-align: right; width: 40px\">PM</th>");
                    sw.WriteLine("</tr>");

                    foreach (KeyValuePair<string, clsStatItem> kvp in kvps)
                    {
                        clsStatItem si = (clsStatItem)kvp.Value;
                        sw.WriteLine("<tr" + (bShowAlt ? alt : string.Empty) + ">");
                        sw.WriteLine("<td>" + si.Name + "</td>");
                        sw.WriteLine("<td>" + si.Team + "</td>");
                        sw.WriteLine("<td style=\"text-align: right\">" + si.Goals + "</td>");
                        sw.WriteLine("<td style=\"text-align: right\">" + si.Assists + "</td>");
                        sw.WriteLine("<td style=\"text-align: right\">" + (si.Goals + si.Assists) + "</td>");
                        sw.WriteLine("<td style=\"text-align: right\">" + si.PenaltyMin + "</td>");
                        sw.WriteLine("</tr>");
                        bShowAlt = !bShowAlt;
                    }

                    sw.WriteLine("</tbody>");
                    sw.WriteLine("</table>");

                    sw.Close();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                clsError.SendError(ex);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (sfDlg.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(sfDlg.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);

                    var kvps = from si in dStats
                               orderby si.Value descending
                               select new KeyValuePair<string, clsStatItem>(si.Key, si.Value);

                    foreach (KeyValuePair<string, clsStatItem> kvp in kvps)
                    {
                        clsStatItem si = (clsStatItem)kvp.Value;
                        sw.WriteLine(si.Name + "," + si.Team + "," + si.Goals + "," + si.Assists + "," + si.PenaltyMin);
                    }

                    sw.Close();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                clsError.SendError(ex);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ofDlg.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(ofDlg.FileName, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);

                    dStats.Clear();

                    while (!sr.EndOfStream)
                    {
                        string ln = sr.ReadLine();

                        if (ln.Length > 0)
                        {
                            string[] els = ln.Split(new char[] { ',' });
                            clsStatItem si = new clsStatItem();

                            si.Name = els[0];
                            si.Team = els[1];
                            si.Goals = Int32.Parse(els[2]);
                            si.Assists = Int32.Parse(els[3]);
                            si.PenaltyMin = Int32.Parse(els[4]);

                            dStats.Add(si.Name + si.Team, si);
                        }
                    }

                    sr.Close();
                    fs.Close();

                    updateListview();
                }
            }
            catch (Exception ex)
            {
                clsError.SendError(ex);
            }
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (bChanged)
                    bChanged = false;
                else
                {
                    statItemIndex = -1;

                    foreach (clsStatItem si in dStats.Values)
                    {
                        int len = tbName.Text.Length;
                        statItemIndex++;

                        if (si.Name.Length >= len && si.Name.Substring(0, len).CompareTo(tbName.Text) == 0)
                        {
                            bChanged = true;
                            tbName.Text = si.Name;
                            tbName.SelectionStart = len;
                            tbName.SelectionLength = si.Name.Length - len;
                            tbTeam.Text = si.Team;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsError.SendError(ex);
            }
        }

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 0x08)
                {
                    if (tbName.SelectionStart > 0 && tbName.SelectionLength > 0)
                    {
                        tbName.Text = tbName.Text.Substring(0, tbName.SelectionStart - 1);
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clsError.SendError(ex);
            }
        }

        private void tbName_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Up)
                {
                    List<clsStatItem> lsi = dStats.Values.ToList<clsStatItem>();
                    string userText = tbName.Text.Substring(0, tbName.SelectionStart);

                    for (int i = statItemIndex - 1; i >= 0; i--)
                    {
                        clsStatItem si = lsi[i];
                        int len = userText.Length;
                        statItemIndex--;

                        if (si.Name.Length >= len && si.Name.Substring(0, len).CompareTo(userText) == 0)
                        {
                            bChanged = true;
                            tbName.Text = si.Name;
                            tbName.SelectionStart = len;
                            tbName.SelectionLength = si.Name.Length - len;
                            tbTeam.Text = si.Team;
                            break;
                        }
                    }
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    List<clsStatItem> lsi = dStats.Values.ToList<clsStatItem>();
                    string userText = tbName.Text.Substring(0, tbName.SelectionStart);

                    for (int i = statItemIndex + 1; i < lsi.Count; i++)
                    {
                        clsStatItem si = lsi[i];
                        int len = userText.Length;
                        statItemIndex++;

                        if (si.Name.Length >= len && si.Name.Substring(0, len).CompareTo(userText) == 0)
                        {
                            bChanged = true;
                            tbName.Text = si.Name;
                            tbName.SelectionStart = len;
                            tbName.SelectionLength = si.Name.Length - len;
                            tbTeam.Text = si.Team;
                            break;
                        }
                    }
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                clsError.SendError(ex);
            }
        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void miVerify_Click(object sender, EventArgs e)
        {
            List<clsStatItem> lsi = dStats.Values.ToList<clsStatItem>();
            int totalGoals = 0;

            foreach (clsStatItem csi in lsi)
                totalGoals += csi.Goals;

            MessageBox.Show("Total Goals: " + totalGoals);
        }

        private void newsTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var kvps = from si in dStats
                            orderby si.Value descending
                            select new KeyValuePair<string, clsStatItem>(si.Key, si.Value);

                Dictionary<string, string> dGoals = new Dictionary<string, string>();
                Dictionary<string, string> dAssists = new Dictionary<string, string>();

                foreach (KeyValuePair<string, clsStatItem> kvp in kvps)
                {
                    clsStatItem si = (clsStatItem)kvp.Value;
                    if (si.TodayGoals > 0)
                    {
                        string text = string.Empty;
                        if (dGoals.ContainsKey(si.Team))
                            text = dGoals[si.Team] + ",";
                        text += si.Name;
                        if (si.TodayGoals > 1)
                            text += "(" + si.TodayGoals + ")";
                        dGoals[si.Team] = text;
                    }

                    if (si.TodayAssists > 0)
                    {
                        string text = string.Empty;
                        if (dAssists.ContainsKey(si.Team))
                            text = dAssists[si.Team] + ",";
                        text += si.Name;
                        if (si.TodayAssists > 1)
                            text += "(" + si.TodayAssists + ")";
                        dAssists[si.Team] = text;
                    }
                }

                string newsText = string.Empty;

                foreach (string key in dGoals.Keys)
                {
                    newsText += "Team: " + key + "\r\n";
                    newsText += "Goals: " + dGoals[key] + "\r\n";
                    if (dAssists.ContainsKey(key))
                        newsText += "Assists: " + dAssists[key] + "\r\n\r\n";
                }

                if (MessageBox.Show(newsText + "Copy to Clipboard?", "GMHA Stats", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Clipboard.SetText(newsText);
            }
            catch (Exception ex)
            {
                clsError.SendError(ex);
            }
        }
    }
}
