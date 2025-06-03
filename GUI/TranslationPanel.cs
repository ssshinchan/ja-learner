namespace ja_learner.GUI
{
    public partial class TranslationPanel : UserControl
    {
        public TranslationPanel()
        {
            InitializeComponent();
        }

        public void UpdateText(string text)
        {
            textBoxSentence.Text = text;
        }

        private void buttonInterpret_Click(object sender, EventArgs e)
        {
            var markdownContent = "";
            buttonInterpret.Enabled = false;
            GptCaller.CreateInterpretConversation(textBoxSentence.Text, res =>
            {
                markdownContent += res;
                webBrowserResult.Document.InvokeScript("setMarkdown", new object[] { Markdown.ToHtml(markdownContent) });
            });

            buttonInterpret.Enabled = true;
        }
    }
}
