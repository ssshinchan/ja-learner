using OpenAI_API;
using OpenAI_API.Chat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Markdig;
using System.Web;

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
            var chat = GptCaller.CreateInterpretConversation(textBoxSentence.Text);

            GptCaller.StreamResponse(chat, res =>
            {
                markdownContent += res;
                webBrowserResult.Document.InvokeScript("setMarkdown", new object[] { Markdown.ToHtml(markdownContent) });
            });

            buttonInterpret.Enabled = true;
        }
    }
}
