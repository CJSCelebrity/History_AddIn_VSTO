using System;
using System.Windows.Forms;

namespace HistoryAddIn
{
    public partial class SearchForTopics : Form
    {
        public SearchForTopics()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtbxTopic_TextChanged(object sender, EventArgs e)
        {
            Common.WriteToLog("Search Topic/Phrase:" + txtbxTopic.Text);
        }

        private void numSearchCounter_ValueChanged(object sender, EventArgs e)
        {
            Common.WriteToLog("Search Counter:" + numSearchCounter.Value.ToString());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Common.CreateLogDirectory();
            try
            { /*
               * CREATE TWO ALTERNATIVES TO SEARCH FOR WORLD HISTORY AND SOUTH AFRICAN HISTORY
               */
                if (Convert.ToInt32(numSearchCounter.Value) < 1)
                {
                    MessageBox.Show("Please specify a value greater than 1 in the 'Search how many websites'", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Common.WriteToLog("Search stopped due to invalid search numbers");
                    return;
                }
                else if (txtbxTopic.Text.Length == 0)
                {
                    MessageBox.Show("Please specify a topic to search for", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Common.WriteToLog("Search stopped due to no search topic/phrase being specified");
                    return;
                }
                else 
                {
                    try
                    {
                        int counterValue = Convert.ToInt32(numSearchCounter.Value);

                        for (int i = 0; i < counterValue; i++)
                        {
                            //if (checkBoxSouthAfrica.Checked)
                            //{
                            //    Common.ScrapeSouthAfricanHistoryWeb($"https://www.sahistory.org.za/search?s={txtbxTopic.Text}#gsc.tab=0&gsc.q={txtbxTopic.Text}&gsc.page={counterValue}");
                            //}
                            //else
                            //{
                            //COMPLETE THIS SCRAPER FIRST
                                Common.ScrapeWorldHistoryWeb($"https://www.britannica.com/search?query={txtbxTopic.Text}");
                            //}
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                   
                }

            }
            catch(Exception ex)
            {
                Common.WriteToLog(ex.Message);
            }
            finally 
            {
                Dispose();
            }
        }

        private void checkBoxSouthAfrica_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxWorldHistory.Checked = false;
        }

        private void checkBoxWorldHistory_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxSouthAfrica.Checked = false;
        }
    }
}
