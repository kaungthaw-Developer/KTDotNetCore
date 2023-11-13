namespace KTDotNetCore.WindowsFormsApp
{
    partial class BlogEf
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
            this.btnSave = new System.Windows.Forms.Button();
            this.textAuthor = new System.Windows.Forms.TextBox();
            this.textContent = new System.Windows.Forms.TextBox();
            this.textTitle = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(26, 299);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 65);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // textAuthor
            // 
            this.textAuthor.Location = new System.Drawing.Point(103, 25);
            this.textAuthor.Name = "textAuthor";
            this.textAuthor.Size = new System.Drawing.Size(100, 22);
            this.textAuthor.TabIndex = 1;
            // 
            // textContent
            // 
            this.textContent.Location = new System.Drawing.Point(103, 71);
            this.textContent.Name = "textContent";
            this.textContent.Size = new System.Drawing.Size(100, 22);
            this.textContent.TabIndex = 2;
            // 
            // textTitle
            // 
            this.textTitle.Location = new System.Drawing.Point(103, 131);
            this.textTitle.Name = "textTitle";
            this.textTitle.Size = new System.Drawing.Size(100, 22);
            this.textTitle.TabIndex = 3;
            // 
            // BlogEf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textTitle);
            this.Controls.Add(this.textContent);
            this.Controls.Add(this.textAuthor);
            this.Controls.Add(this.btnSave);
            this.Name = "BlogEf";
            this.Text = "BlogEf";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox textAuthor;
        private System.Windows.Forms.TextBox textContent;
        private System.Windows.Forms.TextBox textTitle;
    }
}

