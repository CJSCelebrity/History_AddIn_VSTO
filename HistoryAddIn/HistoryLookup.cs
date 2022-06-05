using Microsoft.Office.Tools.Ribbon;

namespace HistoryAddIn
{
    public partial class History_Lookup
    {
        private void History_Lookup_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void btnSearchForTopics_Click(object sender, RibbonControlEventArgs e)
        {
            SearchForTopics searchForm = new SearchForTopics();
            searchForm.Show();
        }
    }
}
