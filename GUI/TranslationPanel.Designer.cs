namespace ja_learner.GUI
{
    partial class TranslationPanel
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            textBoxSentence = new TextBox();
            webBrowserResult = new WebBrowser();
            buttonInterpret = new Button();
            SuspendLayout();
            // 
            // textBoxSentence
            // 
            textBoxSentence.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxSentence.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxSentence.Location = new Point(3, 3);
            textBoxSentence.Multiline = true;
            textBoxSentence.Name = "textBoxSentence";
            textBoxSentence.Size = new Size(545, 76);
            textBoxSentence.TabIndex = 0;
            // 
            // webBrowserResult
            // 
            webBrowserResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            webBrowserResult.Location = new Point(3, 114);
            webBrowserResult.Name = "webBrowserResult";
            webBrowserResult.Size = new Size(545, 153);
            webBrowserResult.TabIndex = 2;
            webBrowserResult.DocumentText = getHtmlTemplate();
            // 
            // buttonInterpret
            // 
            buttonInterpret.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonInterpret.Location = new Point(3, 85);
            buttonInterpret.Name = "buttonInterpret";
            buttonInterpret.Size = new Size(545, 23);
            buttonInterpret.TabIndex = 3;
            buttonInterpret.Text = "分析";
            buttonInterpret.UseVisualStyleBackColor = true;
            buttonInterpret.Click += buttonInterpret_Click;
            // 
            // TranslationPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(buttonInterpret);
            Controls.Add(webBrowserResult);
            Controls.Add(textBoxSentence);
            Name = "TranslationPanel";
            Size = new Size(551, 270);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private string getHtmlTemplate()
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ 
            font-family: 'Microsoft YaHei UI', Arial, sans-serif;
            font-size: 20px;
        }}
        h1, h2, h3, h4, h5, h6, p, ol {{ 
            margin-top: 0;
            margin-bottom: 0;
        }}
    </style>
    <script>
        function setMarkdown(markdown) {{
            document.body.innerHTML = markdown;
        }}
    </script>
</head>
<body>
</body>
</html>";
        }

        private TextBox textBoxSentence;
        private WebBrowser webBrowserResult;
        private Button buttonInterpret;
    }
}
