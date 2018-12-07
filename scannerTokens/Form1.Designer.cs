namespace scannerTokens
{
    partial class tokens
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
            this.FileDataBox = new System.Windows.Forms.TextBox();
            this.loadFileButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TokensButton = new System.Windows.Forms.Button();
            this.ScannerGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.restart = new System.Windows.Forms.Button();
            this.ParserTreeView = new System.Windows.Forms.TreeView();
            this.parseButton = new System.Windows.Forms.Button();
            this.parseLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ScannerGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // FileDataBox
            // 
            this.FileDataBox.Location = new System.Drawing.Point(13, 82);
            this.FileDataBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.FileDataBox.Multiline = true;
            this.FileDataBox.Name = "FileDataBox";
            this.FileDataBox.Size = new System.Drawing.Size(322, 255);
            this.FileDataBox.TabIndex = 4;
            // 
            // loadFileButton
            // 
            this.loadFileButton.Location = new System.Drawing.Point(107, 60);
            this.loadFileButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(126, 22);
            this.loadFileButton.TabIndex = 5;
            this.loadFileButton.Text = "Load Code File";
            this.loadFileButton.UseVisualStyleBackColor = true;
            this.loadFileButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(22, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 48);
            this.label1.TabIndex = 6;
            this.label1.Text = "Please write your code, \r\nor upload the code file.\r\n- Reserved words start with a" +
    " Capital Letter.\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // TokensButton
            // 
            this.TokensButton.Location = new System.Drawing.Point(91, 343);
            this.TokensButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TokensButton.Name = "TokensButton";
            this.TokensButton.Size = new System.Drawing.Size(107, 23);
            this.TokensButton.TabIndex = 7;
            this.TokensButton.Text = "See Tokens";
            this.TokensButton.UseVisualStyleBackColor = true;
            this.TokensButton.Click += new System.EventHandler(this.TokensButton_Click);
            // 
            // ScannerGridView
            // 
            this.ScannerGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ScannerGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.ScannerGridView.Location = new System.Drawing.Point(343, 15);
            this.ScannerGridView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ScannerGridView.Name = "ScannerGridView";
            this.ScannerGridView.RowHeadersWidth = 30;
            this.ScannerGridView.Size = new System.Drawing.Size(457, 345);
            this.ScannerGridView.TabIndex = 8;
            this.ScannerGridView.Visible = false;
            this.ScannerGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ScannerGridView_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Lexeme";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Token Name";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // restart
            // 
            this.restart.Location = new System.Drawing.Point(13, 343);
            this.restart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.restart.Name = "restart";
            this.restart.Size = new System.Drawing.Size(70, 23);
            this.restart.TabIndex = 9;
            this.restart.Text = "Restart";
            this.restart.UseVisualStyleBackColor = true;
            this.restart.Click += new System.EventHandler(this.restart_Click);
            // 
            // ParserTreeView
            // 
            this.ParserTreeView.Location = new System.Drawing.Point(830, 42);
            this.ParserTreeView.Name = "ParserTreeView";
            this.ParserTreeView.Size = new System.Drawing.Size(340, 313);
            this.ParserTreeView.TabIndex = 10;
            // 
            // parseButton
            // 
            this.parseButton.Location = new System.Drawing.Point(205, 343);
            this.parseButton.Name = "parseButton";
            this.parseButton.Size = new System.Drawing.Size(106, 23);
            this.parseButton.TabIndex = 11;
            this.parseButton.Text = "See Parse Tree";
            this.parseButton.UseVisualStyleBackColor = true;
            this.parseButton.Click += new System.EventHandler(this.parseButton_Click);
            // 
            // parseLabel
            // 
            this.parseLabel.AutoSize = true;
            this.parseLabel.Location = new System.Drawing.Point(971, 26);
            this.parseLabel.Name = "parseLabel";
            this.parseLabel.Size = new System.Drawing.Size(69, 13);
            this.parseLabel.TabIndex = 12;
            this.parseLabel.Text = "Parse Tree";
            // 
            // tokens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Brown;
            this.ClientSize = new System.Drawing.Size(1240, 378);
            this.Controls.Add(this.parseLabel);
            this.Controls.Add(this.parseButton);
            this.Controls.Add(this.ParserTreeView);
            this.Controls.Add(this.restart);
            this.Controls.Add(this.ScannerGridView);
            this.Controls.Add(this.TokensButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loadFileButton);
            this.Controls.Add(this.FileDataBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "tokens";
            this.Text = "Scanner";
            this.Load += new System.EventHandler(this.tokens_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ScannerGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FileDataBox;
        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button TokensButton;
        private System.Windows.Forms.DataGridView ScannerGridView;
        private System.Windows.Forms.Button restart;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.TreeView ParserTreeView;
        private System.Windows.Forms.Button parseButton;
        private System.Windows.Forms.Label parseLabel;
    }
}

