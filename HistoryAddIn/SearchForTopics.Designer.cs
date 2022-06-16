namespace HistoryAddIn
{
    partial class SearchForTopics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForTopics));
            this.txtbxTopic = new System.Windows.Forms.TextBox();
            this.lblTopic = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.numSearchCounter = new System.Windows.Forms.NumericUpDown();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.numSearchCounter)).BeginInit();
            this.SuspendLayout();
            // 
            // txtbxTopic
            // 
            this.txtbxTopic.Location = new System.Drawing.Point(12, 39);
            this.txtbxTopic.Name = "txtbxTopic";
            this.txtbxTopic.Size = new System.Drawing.Size(165, 24);
            this.txtbxTopic.TabIndex = 0;
            this.txtbxTopic.TextChanged += new System.EventHandler(this.txtbxTopic_TextChanged);
            // 
            // lblTopic
            // 
            this.lblTopic.AutoSize = true;
            this.lblTopic.Location = new System.Drawing.Point(44, 19);
            this.lblTopic.Name = "lblTopic";
            this.lblTopic.Size = new System.Drawing.Size(42, 17);
            this.lblTopic.TabIndex = 3;
            this.lblTopic.Text = "Topic";
            this.lblTopic.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(190, 19);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(232, 17);
            this.lblSearch.TabIndex = 4;
            this.lblSearch.Text = "How many pages should be searched?\r\n";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(175, 69);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 42);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // numSearchCounter
            // 
            this.numSearchCounter.Location = new System.Drawing.Point(275, 39);
            this.numSearchCounter.Name = "numSearchCounter";
            this.numSearchCounter.Size = new System.Drawing.Size(42, 24);
            this.numSearchCounter.TabIndex = 6;
            this.numSearchCounter.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSearchCounter.ValueChanged += new System.EventHandler(this.numSearchCounter_ValueChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 82);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(410, 23);
            this.progressBar1.TabIndex = 7;
            this.progressBar1.Visible = false;
            // 
            // SearchForTopics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(445, 144);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.numSearchCounter);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.lblTopic);
            this.Controls.Add(this.txtbxTopic);
            this.Font = new System.Drawing.Font("Modern No. 20", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SearchForTopics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search For Topics";
            ((System.ComponentModel.ISupportInitialize)(this.numSearchCounter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtbxTopic;
        private System.Windows.Forms.Label lblTopic;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.NumericUpDown numSearchCounter;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}