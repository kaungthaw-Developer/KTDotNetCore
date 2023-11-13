namespace KTDotNetCore.WindowsFormsApp
{
    partial class BlogDapper
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
            this.textTitle = new System.Windows.Forms.TextBox();
            this.textAuthor = new System.Windows.Forms.TextBox();
            this.textContent = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textTitle
            // 
            this.textTitle.Location = new System.Drawing.Point(28, 40);
            this.textTitle.Name = "textTitle";
            this.textTitle.Size = new System.Drawing.Size(100, 22);
            this.textTitle.TabIndex = 0;
            // 
            // textAuthor
            // 
            this.textAuthor.Location = new System.Drawing.Point(28, 98);
            this.textAuthor.Name = "textAuthor";
            this.textAuthor.Size = new System.Drawing.Size(100, 22);
            this.textAuthor.TabIndex = 1;
            // 
            // textContent
            // 
            this.textContent.Location = new System.Drawing.Point(28, 157);
            this.textContent.Name = "textContent";
            this.textContent.Size = new System.Drawing.Size(100, 22);
            this.textContent.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(28, 246);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BlogDapper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textContent);
            this.Controls.Add(this.textAuthor);
            this.Controls.Add(this.textTitle);
            this.Name = "BlogDapper";
            this.Text = "BlogDapper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textTitle;
        private System.Windows.Forms.TextBox textAuthor;
        private System.Windows.Forms.TextBox textContent;
        private System.Windows.Forms.Button button1;
    }
}