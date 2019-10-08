namespace PixivDaily
{
    partial class Pixiv
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.Button_BGW = new System.Windows.Forms.Button();
            this.TextBox = new System.Windows.Forms.TextBox();
            this.Button_images = new System.Windows.Forms.Button();
            this.BGWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(13, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.Value = new System.DateTime(2019, 8, 7, 0, 0, 0, 0);
            // 
            // Button_BGW
            // 
            this.Button_BGW.Location = new System.Drawing.Point(155, 39);
            this.Button_BGW.Name = "Button_BGW";
            this.Button_BGW.Size = new System.Drawing.Size(60, 46);
            this.Button_BGW.TabIndex = 1;
            this.Button_BGW.Text = "BGW";
            this.Button_BGW.UseVisualStyleBackColor = true;
            this.Button_BGW.Click += new System.EventHandler(this.Button_BGW_Click);
            // 
            // TextBox
            // 
            this.TextBox.Location = new System.Drawing.Point(13, 122);
            this.TextBox.Multiline = true;
            this.TextBox.Name = "TextBox";
            this.TextBox.Size = new System.Drawing.Size(200, 182);
            this.TextBox.TabIndex = 2;
            // 
            // Button_images
            // 
            this.Button_images.Location = new System.Drawing.Point(12, 39);
            this.Button_images.Name = "Button_images";
            this.Button_images.Size = new System.Drawing.Size(57, 46);
            this.Button_images.TabIndex = 3;
            this.Button_images.Text = "images";
            this.Button_images.UseVisualStyleBackColor = true;
            this.Button_images.Click += new System.EventHandler(this.Button_images_Click);
            // 
            // BGWorker
            // 
            this.BGWorker.WorkerReportsProgress = true;
            this.BGWorker.WorkerSupportsCancellation = true;
            this.BGWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGWorker_DoWork);
            this.BGWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BGWorker_ProgressChanged);
            this.BGWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BGWorker_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 93);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(177, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(196, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "%";
            // 
            // Pixiv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 316);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Button_images);
            this.Controls.Add(this.TextBox);
            this.Controls.Add(this.Button_BGW);
            this.Controls.Add(this.dateTimePicker1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Pixiv";
            this.Text = "Pixiv";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button Button_BGW;
        private System.Windows.Forms.Button Button_images;
        private System.ComponentModel.BackgroundWorker BGWorker;
        private System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.TextBox TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

