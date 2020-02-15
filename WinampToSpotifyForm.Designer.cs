using System.Drawing;

namespace winamptospotifyforms
{
    partial class WinampToSpotify
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            selectFolderButton = new System.Windows.Forms.Button();
            folderNameText = new System.Windows.Forms.TextBox();
            accesstokenTxt = new System.Windows.Forms.TextBox();
            resultTxt = new System.Windows.Forms.TextBox();
            getAccessTokenBtn = new System.Windows.Forms.Button();
            openFolderDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            SuspendLayout();
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Location = new Point(13, 86);
            webView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            webView.Name = "webView";
            webView.Size = new Size(642, 424);
            webView.Source = new System.Uri("about:blank", System.UriKind.Absolute);
            webView.TabIndex = 0;
            webView.ZoomFactor = 1D;
            // 
            // selectFolderButton
            // 
            selectFolderButton.Location = new Point(13, 20);
            selectFolderButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            selectFolderButton.Name = "selectFolderButton";
            selectFolderButton.Size = new Size(155, 20);
            selectFolderButton.TabIndex = 1;
            selectFolderButton.Text = "Select Folder";
            selectFolderButton.UseVisualStyleBackColor = true;
            selectFolderButton.Click += selectButton_Click;
            // 
            // folderNameText
            // 
            folderNameText.Location = new Point(181, 20);
            folderNameText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            folderNameText.Name = "folderNameText";
            folderNameText.Size = new Size(475, 23);
            folderNameText.TabIndex = 2;
            // 
            // accesstokenTxt
            // 
            accesstokenTxt.Location = new Point(673, 54);
            accesstokenTxt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            accesstokenTxt.Name = "accesstokenTxt";
            accesstokenTxt.Size = new Size(56, 23);
            accesstokenTxt.TabIndex = 3;
            accesstokenTxt.Visible = false;
            // 
            // resultTxt
            // 
            resultTxt.Location = new Point(13, 86);
            resultTxt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            resultTxt.Multiline = true;
            resultTxt.Name = "resultTxt";
            resultTxt.Size = new Size(642, 424);
            resultTxt.TabIndex = 4;
            resultTxt.Visible = false;
            // 
            // getAccessTokenBtn
            // 
            getAccessTokenBtn.Location = new Point(13, 47);
            getAccessTokenBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            getAccessTokenBtn.Name = "getAccessTokenBtn";
            getAccessTokenBtn.Size = new Size(155, 26);
            getAccessTokenBtn.TabIndex = 5;
            getAccessTokenBtn.Text = "Get Access Token";
            getAccessTokenBtn.UseVisualStyleBackColor = true;
            getAccessTokenBtn.Click += getAccessToken_Click;
            // 
            // openFolderDialog
            // 
            openFolderDialog.FileName = "openFolderDialog";
            openFolderDialog.Filter = "mp3 Files|*.mp3";
            // 
            // WinampToSpotify
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new Size(772, 527);
            Controls.Add(getAccessTokenBtn);
            Controls.Add(resultTxt);
            Controls.Add(accesstokenTxt);
            Controls.Add(folderNameText);
            Controls.Add(selectFolderButton);
            Controls.Add(webView);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Name = "WinampToSpotify";
            Text = "Winamp To Spotify";
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private System.Windows.Forms.Button selectFolderButton;
        private System.Windows.Forms.TextBox folderNameText;
        private System.Windows.Forms.TextBox accesstokenTxt;
        private System.Windows.Forms.TextBox resultTxt;
        private System.Windows.Forms.Button getAccessTokenBtn;
        private System.Windows.Forms.OpenFileDialog openFolderDialog;
    }
}

