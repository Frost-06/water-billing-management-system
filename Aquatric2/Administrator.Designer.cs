namespace Aquatric2 {
    partial class Administrator {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Administrator));
            this.userBtn = new Guna.UI.WinForms.GunaLinkLabel();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.signinBtn = new Guna.UI2.WinForms.Guna2Button();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.passwordTxtBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.usernameTxtBox = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // userBtn
            // 
            this.userBtn.ActiveLinkColor = System.Drawing.Color.DeepSkyBlue;
            this.userBtn.AutoSize = true;
            this.userBtn.Font = new System.Drawing.Font("Euclid Square", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userBtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(159)))), ((int)(((byte)(234)))));
            this.userBtn.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.userBtn.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(159)))), ((int)(((byte)(234)))));
            this.userBtn.Location = new System.Drawing.Point(306, 566);
            this.userBtn.Name = "userBtn";
            this.userBtn.Size = new System.Drawing.Size(54, 20);
            this.userBtn.TabIndex = 20;
            this.userBtn.TabStop = true;
            this.userBtn.Text = "Client";
            this.userBtn.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(54)))), ((int)(((byte)(105)))));
            this.userBtn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.userBtn_LinkClicked);
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Euclid Square", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(234, 565);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(73, 22);
            this.guna2HtmlLabel4.TabIndex = 19;
            this.guna2HtmlLabel4.Text = "Sign in as";
            // 
            // signinBtn
            // 
            this.signinBtn.Animated = true;
            this.signinBtn.BorderRadius = 29;
            this.signinBtn.CheckedState.Parent = this.signinBtn;
            this.signinBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.signinBtn.CustomImages.Parent = this.signinBtn;
            this.signinBtn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(183)))), ((int)(((byte)(232)))));
            this.signinBtn.Font = new System.Drawing.Font("Euclid Square Medium", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.signinBtn.ForeColor = System.Drawing.Color.White;
            this.signinBtn.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(159)))), ((int)(((byte)(234)))));
            this.signinBtn.HoverState.Parent = this.signinBtn;
            this.signinBtn.Location = new System.Drawing.Point(184, 405);
            this.signinBtn.Name = "signinBtn";
            this.signinBtn.ShadowDecoration.Parent = this.signinBtn;
            this.signinBtn.Size = new System.Drawing.Size(217, 60);
            this.signinBtn.TabIndex = 16;
            this.signinBtn.Text = "Sign in";
            this.signinBtn.Click += new System.EventHandler(this.signinBtn_Click);
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Euclid Square", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(89, 289);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(157, 22);
            this.guna2HtmlLabel2.TabIndex = 15;
            this.guna2HtmlLabel2.Text = "Enter your password";
            // 
            // passwordTxtBox
            // 
            this.passwordTxtBox.Animated = true;
            this.passwordTxtBox.BackColor = System.Drawing.Color.Transparent;
            this.passwordTxtBox.BorderColor = System.Drawing.Color.White;
            this.passwordTxtBox.BorderRadius = 25;
            this.passwordTxtBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.passwordTxtBox.DefaultText = "";
            this.passwordTxtBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.passwordTxtBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.passwordTxtBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.passwordTxtBox.DisabledState.Parent = this.passwordTxtBox;
            this.passwordTxtBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.passwordTxtBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.passwordTxtBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.passwordTxtBox.FocusedState.Parent = this.passwordTxtBox;
            this.passwordTxtBox.Font = new System.Drawing.Font("Euclid Square", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordTxtBox.ForeColor = System.Drawing.Color.Black;
            this.passwordTxtBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.passwordTxtBox.HoverState.Parent = this.passwordTxtBox;
            this.passwordTxtBox.Location = new System.Drawing.Point(89, 319);
            this.passwordTxtBox.Margin = new System.Windows.Forms.Padding(5);
            this.passwordTxtBox.Name = "passwordTxtBox";
            this.passwordTxtBox.PasswordChar = '\0';
            this.passwordTxtBox.PlaceholderText = "";
            this.passwordTxtBox.SelectedText = "";
            this.passwordTxtBox.ShadowDecoration.Parent = this.passwordTxtBox;
            this.passwordTxtBox.Size = new System.Drawing.Size(394, 58);
            this.passwordTxtBox.TabIndex = 14;
            this.passwordTxtBox.TextOffset = new System.Drawing.Point(9, 0);
            this.passwordTxtBox.UseSystemPasswordChar = true;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Euclid Square", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(89, 174);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(158, 22);
            this.guna2HtmlLabel1.TabIndex = 13;
            this.guna2HtmlLabel1.Text = "Enter your username";
            // 
            // usernameTxtBox
            // 
            this.usernameTxtBox.Animated = true;
            this.usernameTxtBox.BackColor = System.Drawing.Color.Transparent;
            this.usernameTxtBox.BorderColor = System.Drawing.Color.White;
            this.usernameTxtBox.BorderRadius = 25;
            this.usernameTxtBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.usernameTxtBox.DefaultText = "";
            this.usernameTxtBox.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.usernameTxtBox.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.usernameTxtBox.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.usernameTxtBox.DisabledState.Parent = this.usernameTxtBox;
            this.usernameTxtBox.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.usernameTxtBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.usernameTxtBox.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.usernameTxtBox.FocusedState.Parent = this.usernameTxtBox;
            this.usernameTxtBox.Font = new System.Drawing.Font("Euclid Square", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameTxtBox.ForeColor = System.Drawing.Color.Black;
            this.usernameTxtBox.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.usernameTxtBox.HoverState.Parent = this.usernameTxtBox;
            this.usernameTxtBox.Location = new System.Drawing.Point(89, 204);
            this.usernameTxtBox.Margin = new System.Windows.Forms.Padding(5);
            this.usernameTxtBox.Name = "usernameTxtBox";
            this.usernameTxtBox.PasswordChar = '\0';
            this.usernameTxtBox.PlaceholderText = "";
            this.usernameTxtBox.SelectedText = "";
            this.usernameTxtBox.ShadowDecoration.Parent = this.usernameTxtBox;
            this.usernameTxtBox.Size = new System.Drawing.Size(394, 58);
            this.usernameTxtBox.TabIndex = 12;
            this.usernameTxtBox.TextOffset = new System.Drawing.Point(9, 0);
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.Animated = true;
            this.guna2ControlBox1.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
            this.guna2ControlBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.guna2ControlBox1.CustomIconSize = 12F;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.White;
            this.guna2ControlBox1.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(54)))), ((int)(((byte)(105)))));
            this.guna2ControlBox1.HoverState.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.HoverState.Parent = this.guna2ControlBox1;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(159)))), ((int)(((byte)(234)))));
            this.guna2ControlBox1.Location = new System.Drawing.Point(553, 12);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.PressedColor = System.Drawing.Color.White;
            this.guna2ControlBox1.ShadowDecoration.Parent = this.guna2ControlBox1;
            this.guna2ControlBox1.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox1.TabIndex = 21;
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.White;
            this.guna2PictureBox1.Image = global::Aquatric2.Properties.Resources.Asset_20_2x;
            this.guna2PictureBox1.Location = new System.Drawing.Point(-2, 0);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.ShadowDecoration.Parent = this.guna2PictureBox1;
            this.guna2PictureBox1.Size = new System.Drawing.Size(613, 140);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 22;
            this.guna2PictureBox1.TabStop = false;
            // 
            // Administrator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(610, 622);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.guna2PictureBox1);
            this.Controls.Add(this.userBtn);
            this.Controls.Add(this.guna2HtmlLabel4);
            this.Controls.Add(this.signinBtn);
            this.Controls.Add(this.guna2HtmlLabel2);
            this.Controls.Add(this.passwordTxtBox);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.usernameTxtBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Administrator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administrator";
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI.WinForms.GunaLinkLabel userBtn;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private Guna.UI2.WinForms.Guna2Button signinBtn;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2TextBox passwordTxtBox;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2TextBox usernameTxtBox;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
    }
}