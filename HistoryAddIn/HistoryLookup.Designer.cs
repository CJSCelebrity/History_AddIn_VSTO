namespace HistoryAddIn
{
    partial class History_Lookup : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public History_Lookup()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabHistory = this.Factory.CreateRibbonTab();
            this.grpTools = this.Factory.CreateRibbonGroup();
            this.btnSearchForTopics = this.Factory.CreateRibbonButton();
            this.tabHistory.SuspendLayout();
            this.grpTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabHistory
            // 
            this.tabHistory.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tabHistory.Groups.Add(this.grpTools);
            this.tabHistory.Label = "Resources For History";
            this.tabHistory.Name = "tabHistory";
            // 
            // grpTools
            // 
            this.grpTools.Items.Add(this.btnSearchForTopics);
            this.grpTools.Label = "Tools";
            this.grpTools.Name = "grpTools";
            // 
            // btnSearchForTopics
            // 
            this.btnSearchForTopics.Image = global::HistoryAddIn.Properties.Resources.search;
            this.btnSearchForTopics.Label = "Search for Topics";
            this.btnSearchForTopics.Name = "btnSearchForTopics";
            this.btnSearchForTopics.ShowImage = true;
            this.btnSearchForTopics.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSearchForTopics_Click);
            // 
            // History_Lookup
            // 
            this.Name = "History_Lookup";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tabHistory);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.History_Lookup_Load);
            this.tabHistory.ResumeLayout(false);
            this.tabHistory.PerformLayout();
            this.grpTools.ResumeLayout(false);
            this.grpTools.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tabHistory;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpTools;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSearchForTopics;
    }

    partial class ThisRibbonCollection
    {
        internal History_Lookup History_Lookup
        {
            get { return this.GetRibbon<History_Lookup>(); }
        }
    }
}
