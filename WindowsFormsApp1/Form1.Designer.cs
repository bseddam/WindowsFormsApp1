namespace WindowsFormsApp1
{
    partial class Form1
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
            this.btngonder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btngoster = new System.Windows.Forms.Button();
            this.btnbazayagonder = new System.Windows.Forms.Button();
            this.btnhesabla = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btngonder
            // 
            this.btngonder.Location = new System.Drawing.Point(115, 281);
            this.btngonder.Name = "btngonder";
            this.btngonder.Size = new System.Drawing.Size(163, 23);
            this.btngonder.TabIndex = 0;
            this.btngonder.Text = "kecmeyenleri bazaya gonder";
            this.btngonder.UseVisualStyleBackColor = true;
            this.btngonder.Click += new System.EventHandler(this.btngonder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(385, 286);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(115, 46);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(379, 150);
            this.dataGridView1.TabIndex = 2;
            // 
            // btngoster
            // 
            this.btngoster.Location = new System.Drawing.Point(115, 228);
            this.btngoster.Name = "btngoster";
            this.btngoster.Size = new System.Drawing.Size(163, 23);
            this.btngoster.TabIndex = 3;
            this.btngoster.Text = "göstər";
            this.btngoster.UseVisualStyleBackColor = true;
            this.btngoster.Click += new System.EventHandler(this.btngoster_Click);
            // 
            // btnbazayagonder
            // 
            this.btnbazayagonder.Location = new System.Drawing.Point(371, 227);
            this.btnbazayagonder.Name = "btnbazayagonder";
            this.btnbazayagonder.Size = new System.Drawing.Size(123, 23);
            this.btnbazayagonder.TabIndex = 4;
            this.btnbazayagonder.Text = "bazaya göndər";
            this.btnbazayagonder.UseVisualStyleBackColor = true;
            this.btnbazayagonder.Click += new System.EventHandler(this.btnbazayagonder_Click);
            // 
            // btnhesabla
            // 
            this.btnhesabla.Location = new System.Drawing.Point(241, 358);
            this.btnhesabla.Name = "btnhesabla";
            this.btnhesabla.Size = new System.Drawing.Size(123, 23);
            this.btnhesabla.TabIndex = 6;
            this.btnhesabla.Text = "illər üzrə hesabla";
            this.btnhesabla.UseVisualStyleBackColor = true;
            this.btnhesabla.Click += new System.EventHandler(this.btnhesabla_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(27, 17);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(581, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork_1);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged_1);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 393);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnhesabla);
            this.Controls.Add(this.btnbazayagonder);
            this.Controls.Add(this.btngoster);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btngonder);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btngonder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btngoster;
        private System.Windows.Forms.Button btnbazayagonder;
        private System.Windows.Forms.Button btnhesabla;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}

