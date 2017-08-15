namespace ImageProcessing.WinForms
{
    partial class FormTest
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
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.gbBlurFilter = new System.Windows.Forms.GroupBox();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.rbMedianFilter = new System.Windows.Forms.RadioButton();
            this.rbGaussianBlur = new System.Windows.Forms.RadioButton();
            this.rbBoxFilter = new System.Windows.Forms.RadioButton();
            this.btnApply = new System.Windows.Forms.Button();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtImageName = new System.Windows.Forms.TextBox();
            this.lblKernel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarKernelSize = new System.Windows.Forms.TrackBar();
            this.btnOpenImg = new System.Windows.Forms.Button();
            this.btnSaveImg = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.gbBlurFilter.SuspendLayout();
            this.gbSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarKernelSize)).BeginInit();
            this.SuspendLayout();
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(12, 173);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(655, 545);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 1;
            this.pbImage.TabStop = false;
            // 
            // gbBlurFilter
            // 
            this.gbBlurFilter.Controls.Add(this.rbNone);
            this.gbBlurFilter.Controls.Add(this.rbMedianFilter);
            this.gbBlurFilter.Controls.Add(this.rbGaussianBlur);
            this.gbBlurFilter.Controls.Add(this.rbBoxFilter);
            this.gbBlurFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gbBlurFilter.Location = new System.Drawing.Point(12, 12);
            this.gbBlurFilter.Name = "gbBlurFilter";
            this.gbBlurFilter.Size = new System.Drawing.Size(191, 146);
            this.gbBlurFilter.TabIndex = 11;
            this.gbBlurFilter.TabStop = false;
            this.gbBlurFilter.Text = "Blur Filter";
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Checked = true;
            this.rbNone.Location = new System.Drawing.Point(15, 120);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(65, 20);
            this.rbNone.TabIndex = 3;
            this.rbNone.TabStop = true;
            this.rbNone.Text = "NONE";
            this.rbNone.UseVisualStyleBackColor = true;
            this.rbNone.Visible = false;
            // 
            // rbMedianFilter
            // 
            this.rbMedianFilter.AutoSize = true;
            this.rbMedianFilter.Location = new System.Drawing.Point(15, 89);
            this.rbMedianFilter.Name = "rbMedianFilter";
            this.rbMedianFilter.Size = new System.Drawing.Size(103, 20);
            this.rbMedianFilter.TabIndex = 2;
            this.rbMedianFilter.Text = "Median Filter";
            this.rbMedianFilter.UseVisualStyleBackColor = true;
            this.rbMedianFilter.CheckedChanged += new System.EventHandler(this.ChangedChecked_RadioButtons);
            // 
            // rbGaussianBlur
            // 
            this.rbGaussianBlur.AutoSize = true;
            this.rbGaussianBlur.Location = new System.Drawing.Point(15, 59);
            this.rbGaussianBlur.Name = "rbGaussianBlur";
            this.rbGaussianBlur.Size = new System.Drawing.Size(109, 20);
            this.rbGaussianBlur.TabIndex = 1;
            this.rbGaussianBlur.Text = "Gaussian Blur";
            this.rbGaussianBlur.UseVisualStyleBackColor = true;
            this.rbGaussianBlur.CheckedChanged += new System.EventHandler(this.ChangedChecked_RadioButtons);
            // 
            // rbBoxFilter
            // 
            this.rbBoxFilter.AutoSize = true;
            this.rbBoxFilter.Location = new System.Drawing.Point(15, 29);
            this.rbBoxFilter.Name = "rbBoxFilter";
            this.rbBoxFilter.Size = new System.Drawing.Size(81, 20);
            this.rbBoxFilter.TabIndex = 0;
            this.rbBoxFilter.Text = "Box Filter";
            this.rbBoxFilter.UseVisualStyleBackColor = true;
            this.rbBoxFilter.CheckedChanged += new System.EventHandler(this.ChangedChecked_RadioButtons);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(513, 90);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(154, 31);
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.label2);
            this.gbSettings.Controls.Add(this.txtImageName);
            this.gbSettings.Controls.Add(this.lblKernel);
            this.gbSettings.Controls.Add(this.label1);
            this.gbSettings.Controls.Add(this.trackBarKernelSize);
            this.gbSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gbSettings.Location = new System.Drawing.Point(209, 12);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(298, 146);
            this.gbSettings.TabIndex = 13;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(6, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Image Name:";
            // 
            // txtImageName
            // 
            this.txtImageName.Location = new System.Drawing.Point(6, 115);
            this.txtImageName.Name = "txtImageName";
            this.txtImageName.Size = new System.Drawing.Size(286, 22);
            this.txtImageName.TabIndex = 3;
            // 
            // lblKernel
            // 
            this.lblKernel.AutoSize = true;
            this.lblKernel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblKernel.Location = new System.Drawing.Point(93, 27);
            this.lblKernel.Name = "lblKernel";
            this.lblKernel.Size = new System.Drawing.Size(27, 15);
            this.lblKernel.TabIndex = 2;
            this.lblKernel.Text = "3x3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kernel Size:";
            // 
            // trackBarKernelSize
            // 
            this.trackBarKernelSize.Location = new System.Drawing.Point(6, 49);
            this.trackBarKernelSize.Maximum = 20;
            this.trackBarKernelSize.Minimum = 1;
            this.trackBarKernelSize.Name = "trackBarKernelSize";
            this.trackBarKernelSize.Size = new System.Drawing.Size(286, 45);
            this.trackBarKernelSize.TabIndex = 0;
            this.trackBarKernelSize.Value = 1;
            this.trackBarKernelSize.Scroll += new System.EventHandler(this.trackBarKernelSize_Scroll);
            // 
            // btnOpenImg
            // 
            this.btnOpenImg.Location = new System.Drawing.Point(513, 18);
            this.btnOpenImg.Name = "btnOpenImg";
            this.btnOpenImg.Size = new System.Drawing.Size(154, 31);
            this.btnOpenImg.TabIndex = 14;
            this.btnOpenImg.Text = "Open Image";
            this.btnOpenImg.UseVisualStyleBackColor = true;
            this.btnOpenImg.Click += new System.EventHandler(this.btnOpenImg_Click);
            // 
            // btnSaveImg
            // 
            this.btnSaveImg.Location = new System.Drawing.Point(513, 54);
            this.btnSaveImg.Name = "btnSaveImg";
            this.btnSaveImg.Size = new System.Drawing.Size(154, 31);
            this.btnSaveImg.TabIndex = 15;
            this.btnSaveImg.Text = "Save Image";
            this.btnSaveImg.UseVisualStyleBackColor = true;
            this.btnSaveImg.Click += new System.EventHandler(this.btnSaveImg_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(513, 127);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(154, 31);
            this.btnReset.TabIndex = 16;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 731);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSaveImg);
            this.Controls.Add(this.btnOpenImg);
            this.Controls.Add(this.gbSettings);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.gbBlurFilter);
            this.Controls.Add(this.pbImage);
            this.Name = "FormTest";
            this.Text = "Blur Filter";
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.gbBlurFilter.ResumeLayout(false);
            this.gbBlurFilter.PerformLayout();
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarKernelSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.GroupBox gbBlurFilter;
        private System.Windows.Forms.RadioButton rbMedianFilter;
        private System.Windows.Forms.RadioButton rbGaussianBlur;
        private System.Windows.Forms.RadioButton rbBoxFilter;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.Label lblKernel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarKernelSize;
        private System.Windows.Forms.Button btnOpenImg;
        private System.Windows.Forms.Button btnSaveImg;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtImageName;
    }
}

