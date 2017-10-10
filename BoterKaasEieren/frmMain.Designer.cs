namespace BoterKaasEieren
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnStart = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.comboType = new System.Windows.Forms.ComboBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.comboKleur = new System.Windows.Forms.ComboBox();
            this.radioPlayTypeEasy = new System.Windows.Forms.RadioButton();
            this.radioPlayTypeMedium = new System.Windows.Forms.RadioButton();
            this.radioPlayTypeHard = new System.Windows.Forms.RadioButton();
            this.lblAppInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(15, 71);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(10, 13);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "-";
            // 
            // comboType
            // 
            this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboType.FormattingEnabled = true;
            this.comboType.Items.AddRange(new object[] {
            "Kruis",
            "Rondje",
            "Vierkant"});
            this.comboType.Location = new System.Drawing.Point(93, 12);
            this.comboType.Name = "comboType";
            this.comboType.Size = new System.Drawing.Size(121, 21);
            this.comboType.TabIndex = 3;
            this.comboType.TabStop = false;
            // 
            // btnReset
            // 
            this.btnReset.Enabled = false;
            this.btnReset.Location = new System.Drawing.Point(12, 41);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // comboKleur
            // 
            this.comboKleur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboKleur.FormattingEnabled = true;
            this.comboKleur.Items.AddRange(new object[] {
            "Zwart",
            "Rood",
            "Blauw",
            "Paars",
            "Geel",
            "Oranje",
            "Cyan"});
            this.comboKleur.Location = new System.Drawing.Point(93, 43);
            this.comboKleur.Name = "comboKleur";
            this.comboKleur.Size = new System.Drawing.Size(121, 21);
            this.comboKleur.TabIndex = 5;
            this.comboKleur.TabStop = false;
            this.comboKleur.SelectedIndexChanged += new System.EventHandler(this.comboKleur_SelectedIndexChanged);
            // 
            // radioPlayTypeEasy
            // 
            this.radioPlayTypeEasy.AutoSize = true;
            this.radioPlayTypeEasy.Checked = true;
            this.radioPlayTypeEasy.Location = new System.Drawing.Point(221, 15);
            this.radioPlayTypeEasy.Name = "radioPlayTypeEasy";
            this.radioPlayTypeEasy.Size = new System.Drawing.Size(47, 17);
            this.radioPlayTypeEasy.TabIndex = 6;
            this.radioPlayTypeEasy.TabStop = true;
            this.radioPlayTypeEasy.Text = "easy";
            this.radioPlayTypeEasy.UseVisualStyleBackColor = true;
            // 
            // radioPlayTypeMedium
            // 
            this.radioPlayTypeMedium.AutoSize = true;
            this.radioPlayTypeMedium.Location = new System.Drawing.Point(221, 30);
            this.radioPlayTypeMedium.Name = "radioPlayTypeMedium";
            this.radioPlayTypeMedium.Size = new System.Drawing.Size(61, 17);
            this.radioPlayTypeMedium.TabIndex = 7;
            this.radioPlayTypeMedium.Text = "medium";
            this.radioPlayTypeMedium.UseVisualStyleBackColor = true;
            // 
            // radioPlayTypeHard
            // 
            this.radioPlayTypeHard.AutoSize = true;
            this.radioPlayTypeHard.Location = new System.Drawing.Point(221, 46);
            this.radioPlayTypeHard.Name = "radioPlayTypeHard";
            this.radioPlayTypeHard.Size = new System.Drawing.Size(46, 17);
            this.radioPlayTypeHard.TabIndex = 8;
            this.radioPlayTypeHard.Text = "hard";
            this.radioPlayTypeHard.UseVisualStyleBackColor = true;
            // 
            // lblAppInfo
            // 
            this.lblAppInfo.AutoSize = true;
            this.lblAppInfo.Location = new System.Drawing.Point(25, 344);
            this.lblAppInfo.Name = "lblAppInfo";
            this.lblAppInfo.Size = new System.Drawing.Size(10, 13);
            this.lblAppInfo.TabIndex = 9;
            this.lblAppInfo.Text = "-";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 357);
            this.Controls.Add(this.lblAppInfo);
            this.Controls.Add(this.radioPlayTypeHard);
            this.Controls.Add(this.radioPlayTypeMedium);
            this.Controls.Add(this.radioPlayTypeEasy);
            this.Controls.Add(this.comboKleur);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.comboType);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Boter Kaas en Eieren";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ComboBox comboType;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ComboBox comboKleur;
        private System.Windows.Forms.RadioButton radioPlayTypeEasy;
        private System.Windows.Forms.RadioButton radioPlayTypeMedium;
        private System.Windows.Forms.RadioButton radioPlayTypeHard;
        private System.Windows.Forms.Label lblAppInfo;


    }
}

