namespace CameraIA
{
    partial class Interface
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
            this.button1 = new System.Windows.Forms.Button();
            this.picture = new System.Windows.Forms.PictureBox();
            this.outputText = new System.Windows.Forms.TextBox();
            this.imagePath = new System.Windows.Forms.Label();
            this.predictionTXT = new System.Windows.Forms.Label();
            this.percentageTXT = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 628);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Predict";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // picture
            // 
            this.picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picture.Location = new System.Drawing.Point(12, 12);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(503, 335);
            this.picture.TabIndex = 1;
            this.picture.TabStop = false;
            // 
            // outputText
            // 
            this.outputText.Location = new System.Drawing.Point(12, 599);
            this.outputText.Multiline = true;
            this.outputText.Name = "outputText";
            this.outputText.Size = new System.Drawing.Size(503, 23);
            this.outputText.TabIndex = 2;
            // 
            // imagePath
            // 
            this.imagePath.AutoSize = true;
            this.imagePath.Location = new System.Drawing.Point(12, 390);
            this.imagePath.Name = "imagePath";
            this.imagePath.Size = new System.Drawing.Size(38, 15);
            this.imagePath.TabIndex = 3;
            this.imagePath.Text = "label1";
            // 
            // predictionTXT
            // 
            this.predictionTXT.AutoSize = true;
            this.predictionTXT.Location = new System.Drawing.Point(12, 414);
            this.predictionTXT.Name = "predictionTXT";
            this.predictionTXT.Size = new System.Drawing.Size(38, 15);
            this.predictionTXT.TabIndex = 4;
            this.predictionTXT.Text = "label2";
            // 
            // percentageTXT
            // 
            this.percentageTXT.AutoSize = true;
            this.percentageTXT.Location = new System.Drawing.Point(12, 439);
            this.percentageTXT.Name = "percentageTXT";
            this.percentageTXT.Size = new System.Drawing.Size(38, 15);
            this.percentageTXT.TabIndex = 5;
            this.percentageTXT.Text = "label3";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(162, 353);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(159, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Change Image";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 663);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.percentageTXT);
            this.Controls.Add(this.predictionTXT);
            this.Controls.Add(this.imagePath);
            this.Controls.Add(this.outputText);
            this.Controls.Add(this.picture);
            this.Controls.Add(this.button1);
            this.Name = "Interface";
            this.Text = "Interface";
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private PictureBox picture;
        private TextBox outputText;
        private Label imagePath;
        private Label predictionTXT;
        private Label percentageTXT;
        private Button button2;
    }
}