namespace picToMCF {
    partial class Form1 {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbOutFolderPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.rbSourcePath = new System.Windows.Forms.RadioButton();
            this.rbSelectFolder = new System.Windows.Forms.RadioButton();
            this.gbOutfolder = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDatapackName = new System.Windows.Forms.TextBox();
            this.cboxCreateNew = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbCustomName = new System.Windows.Forms.RadioButton();
            this.rbSourceName = new System.Windows.Forms.RadioButton();
            this.tbDestFileName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbVer17 = new System.Windows.Forms.RadioButton();
            this.rbVer16 = new System.Windows.Forms.RadioButton();
            this.rbVer15 = new System.Windows.Forms.RadioButton();
            this.rbVer13 = new System.Windows.Forms.RadioButton();
            this.rbVer12 = new System.Windows.Forms.RadioButton();
            this.cboxPreview = new System.Windows.Forms.CheckBox();
            this.gbOutfolder.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(18, 494);
            this.progressBar.Maximum = 1000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(475, 26);
            this.progressBar.TabIndex = 1;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(228, 479);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(41, 12);
            this.lblProgress.TabIndex = 2;
            this.lblProgress.Text = "待機中";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(51, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(355, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "ウィンドウ内に画像ファイルをドロップすると変換が始まります。";
            // 
            // tbOutFolderPath
            // 
            this.tbOutFolderPath.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbOutFolderPath.Location = new System.Drawing.Point(11, 62);
            this.tbOutFolderPath.Name = "tbOutFolderPath";
            this.tbOutFolderPath.ReadOnly = true;
            this.tbOutFolderPath.Size = new System.Drawing.Size(380, 20);
            this.tbOutFolderPath.TabIndex = 4;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(398, 61);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(48, 23);
            this.btnBrowse.TabIndex = 5;
            this.btnBrowse.Text = "設定";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // rbSourcePath
            // 
            this.rbSourcePath.AutoSize = true;
            this.rbSourcePath.Checked = true;
            this.rbSourcePath.Location = new System.Drawing.Point(11, 18);
            this.rbSourcePath.Name = "rbSourcePath";
            this.rbSourcePath.Size = new System.Drawing.Size(198, 16);
            this.rbSourcePath.TabIndex = 6;
            this.rbSourcePath.TabStop = true;
            this.rbSourcePath.Text = "変換元ファイルと同じ場所に出力する";
            this.rbSourcePath.UseVisualStyleBackColor = true;
            this.rbSourcePath.CheckedChanged += new System.EventHandler(this.rbDefault_CheckedChanged);
            // 
            // rbSelectFolder
            // 
            this.rbSelectFolder.AutoSize = true;
            this.rbSelectFolder.Location = new System.Drawing.Point(11, 40);
            this.rbSelectFolder.Name = "rbSelectFolder";
            this.rbSelectFolder.Size = new System.Drawing.Size(152, 16);
            this.rbSelectFolder.TabIndex = 7;
            this.rbSelectFolder.Text = "指定したフォルダに出力する";
            this.rbSelectFolder.UseVisualStyleBackColor = true;
            // 
            // gbOutfolder
            // 
            this.gbOutfolder.Controls.Add(this.label2);
            this.gbOutfolder.Controls.Add(this.tbDatapackName);
            this.gbOutfolder.Controls.Add(this.cboxCreateNew);
            this.gbOutfolder.Controls.Add(this.groupBox3);
            this.gbOutfolder.Controls.Add(this.groupBox2);
            this.gbOutfolder.Controls.Add(this.groupBox1);
            this.gbOutfolder.Location = new System.Drawing.Point(12, 59);
            this.gbOutfolder.Name = "gbOutfolder";
            this.gbOutfolder.Size = new System.Drawing.Size(492, 391);
            this.gbOutfolder.TabIndex = 8;
            this.gbOutfolder.TabStop = false;
            this.gbOutfolder.Text = "出力オプション";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 333);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "フォルダ名（データパック名）：";
            // 
            // tbDatapackName
            // 
            this.tbDatapackName.Location = new System.Drawing.Point(6, 353);
            this.tbDatapackName.Name = "tbDatapackName";
            this.tbDatapackName.Size = new System.Drawing.Size(321, 19);
            this.tbDatapackName.TabIndex = 16;
            this.tbDatapackName.Text = "geoglyph";
            this.tbDatapackName.Leave += new System.EventHandler(this.tbDatapackName_Leave);
            // 
            // cboxCreateNew
            // 
            this.cboxCreateNew.AutoSize = true;
            this.cboxCreateNew.Checked = true;
            this.cboxCreateNew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxCreateNew.Location = new System.Drawing.Point(6, 310);
            this.cboxCreateNew.Name = "cboxCreateNew";
            this.cboxCreateNew.Size = new System.Drawing.Size(143, 16);
            this.cboxCreateNew.TabIndex = 15;
            this.cboxCreateNew.Text = "フォルダごと新規作成する";
            this.cboxCreateNew.UseVisualStyleBackColor = true;
            this.cboxCreateNew.CheckedChanged += new System.EventHandler(this.cboxCreateNew_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbSourcePath);
            this.groupBox3.Controls.Add(this.btnBrowse);
            this.groupBox3.Controls.Add(this.rbSelectFolder);
            this.groupBox3.Controls.Add(this.tbOutFolderPath);
            this.groupBox3.Location = new System.Drawing.Point(6, 29);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(475, 100);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "出力先";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbCustomName);
            this.groupBox2.Controls.Add(this.rbSourceName);
            this.groupBox2.Controls.Add(this.tbDestFileName);
            this.groupBox2.Location = new System.Drawing.Point(6, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(475, 100);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "出力ファイル名";
            // 
            // rbCustomName
            // 
            this.rbCustomName.AutoSize = true;
            this.rbCustomName.Location = new System.Drawing.Point(6, 38);
            this.rbCustomName.Name = "rbCustomName";
            this.rbCustomName.Size = new System.Drawing.Size(100, 16);
            this.rbCustomName.TabIndex = 13;
            this.rbCustomName.Text = "自分で指定する";
            this.rbCustomName.UseVisualStyleBackColor = true;
            // 
            // rbSourceName
            // 
            this.rbSourceName.AutoSize = true;
            this.rbSourceName.BackColor = System.Drawing.SystemColors.Control;
            this.rbSourceName.Checked = true;
            this.rbSourceName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rbSourceName.Location = new System.Drawing.Point(6, 18);
            this.rbSourceName.Name = "rbSourceName";
            this.rbSourceName.Size = new System.Drawing.Size(175, 16);
            this.rbSourceName.TabIndex = 12;
            this.rbSourceName.TabStop = true;
            this.rbSourceName.Text = "変換元のファイル名と同名にする";
            this.rbSourceName.UseVisualStyleBackColor = false;
            this.rbSourceName.CheckedChanged += new System.EventHandler(this.rbSourceName_CheckedChanged);
            // 
            // tbDestFileName
            // 
            this.tbDestFileName.BackColor = System.Drawing.SystemColors.Window;
            this.tbDestFileName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbDestFileName.Location = new System.Drawing.Point(6, 61);
            this.tbDestFileName.Name = "tbDestFileName";
            this.tbDestFileName.Size = new System.Drawing.Size(309, 19);
            this.tbDestFileName.TabIndex = 10;
            this.tbDestFileName.Text = "geoglyph";
            this.tbDestFileName.Leave += new System.EventHandler(this.tbDestFileName_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbVer17);
            this.groupBox1.Controls.Add(this.rbVer16);
            this.groupBox1.Controls.Add(this.rbVer15);
            this.groupBox1.Controls.Add(this.rbVer13);
            this.groupBox1.Controls.Add(this.rbVer12);
            this.groupBox1.Location = new System.Drawing.Point(6, 241);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 45);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "対象のMinecraftバージョン";
            // 
            // rbVer17
            // 
            this.rbVer17.AutoSize = true;
            this.rbVer17.Location = new System.Drawing.Point(360, 20);
            this.rbVer17.Name = "rbVer17";
            this.rbVer17.Size = new System.Drawing.Size(55, 16);
            this.rbVer17.TabIndex = 15;
            this.rbVer17.TabStop = true;
            this.rbVer17.Text = "1.17～";
            this.rbVer17.UseVisualStyleBackColor = true;
            // 
            // rbVer16
            // 
            this.rbVer16.AutoSize = true;
            this.rbVer16.Checked = true;
            this.rbVer16.Location = new System.Drawing.Point(277, 20);
            this.rbVer16.Name = "rbVer16";
            this.rbVer16.Size = new System.Drawing.Size(63, 16);
            this.rbVer16.TabIndex = 14;
            this.rbVer16.TabStop = true;
            this.rbVer16.Text = "1.16.2～";
            this.rbVer16.UseVisualStyleBackColor = true;
            // 
            // rbVer15
            // 
            this.rbVer15.AutoSize = true;
            this.rbVer15.Location = new System.Drawing.Point(179, 20);
            this.rbVer15.Name = "rbVer15";
            this.rbVer15.Size = new System.Drawing.Size(83, 16);
            this.rbVer15.TabIndex = 13;
            this.rbVer15.Text = "1.15～1.16.1";
            this.rbVer15.UseVisualStyleBackColor = true;
            // 
            // rbVer13
            // 
            this.rbVer13.AutoSize = true;
            this.rbVer13.Location = new System.Drawing.Point(84, 20);
            this.rbVer13.Name = "rbVer13";
            this.rbVer13.Size = new System.Drawing.Size(75, 16);
            this.rbVer13.TabIndex = 12;
            this.rbVer13.Text = "1.13～1.14";
            this.rbVer13.UseVisualStyleBackColor = true;
            // 
            // rbVer12
            // 
            this.rbVer12.AutoSize = true;
            this.rbVer12.Location = new System.Drawing.Point(16, 20);
            this.rbVer12.Name = "rbVer12";
            this.rbVer12.Size = new System.Drawing.Size(43, 16);
            this.rbVer12.TabIndex = 11;
            this.rbVer12.Text = "1.12";
            this.rbVer12.UseVisualStyleBackColor = true;
            // 
            // cboxPreview
            // 
            this.cboxPreview.AutoSize = true;
            this.cboxPreview.Location = new System.Drawing.Point(17, 472);
            this.cboxPreview.Name = "cboxPreview";
            this.cboxPreview.Size = new System.Drawing.Size(169, 16);
            this.cboxPreview.TabIndex = 9;
            this.cboxPreview.Text = "地上絵イメージをプレビューする";
            this.cboxPreview.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 549);
            this.Controls.Add(this.cboxPreview);
            this.Controls.Add(this.gbOutfolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "地上絵コマンドジェネレータ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.gbOutfolder.ResumeLayout(false);
            this.gbOutfolder.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbOutFolderPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.RadioButton rbSourcePath;
        private System.Windows.Forms.RadioButton rbSelectFolder;
        private System.Windows.Forms.GroupBox gbOutfolder;
        private System.Windows.Forms.CheckBox cboxPreview;
        private System.Windows.Forms.RadioButton rbVer12;
        private System.Windows.Forms.RadioButton rbVer13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbDestFileName;
        private System.Windows.Forms.RadioButton rbCustomName;
        private System.Windows.Forms.RadioButton rbSourceName;
        private System.Windows.Forms.CheckBox cboxCreateNew;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbVer16;
        private System.Windows.Forms.RadioButton rbVer15;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDatapackName;
        private System.Windows.Forms.RadioButton rbVer17;
    }
}

