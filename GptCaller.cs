using OpenAI_API;
using OpenAI_API.Chat;
using System.Net;

namespace ja_learner
{
    internal class GptCaller
    {
        private static OpenAIAPI api;
        private static OpenAIAPI ttsApi;
        private static IHttpClientFactory defaultFactory;
        private static IHttpClientFactory proxyFactory;

        public static void Initialize()
        {
            api = new(Program.APP_SETTING.GPT.ApiKey) { ApiUrlFormat = Program.APP_SETTING.GPT.ApiUrl };
            ttsApi = api;
            if (Program.APP_SETTING.GPT.TtsApiKey != string.Empty || Program.APP_SETTING.GPT.TtsApiUrl != string.Empty)
            {
                ttsApi = new(Program.APP_SETTING.GPT.TtsApiKey) { ApiUrlFormat = Program.APP_SETTING.GPT.TtsApiUrl };
            }
            defaultFactory = api.HttpClientFactory;
            proxyFactory = new MyHttpClientFactory(Program.APP_SETTING.HttpProxy);
        }

        public static void SetProxy(bool useProxy)
        {
            if (useProxy)
            {
                api.HttpClientFactory = proxyFactory;
                ttsApi.HttpClientFactory = proxyFactory;
            }
            else
            {
                api.HttpClientFactory = defaultFactory;
                ttsApi.HttpClientFactory = defaultFactory;
            }
        }

        public static Conversation CreateTranslateConversation(string text)
        {
            Conversation conversation = api.Chat.CreateConversation();
            conversation.Model = Program.APP_SETTING.GPT.Model;
            conversation.AppendSystemMessage(Program.APP_SETTING.GPT.TranslatePrompt);
            if (UserConfig.useExtraPrompt)
            {
                AddExtraSystemPrompt(conversation);
            }
            conversation.AppendUserInput($"{text}");
            return conversation;
        }

        public static Conversation CreateInterpretConversation(string text)
        {
            Conversation conversation = api.Chat.CreateConversation();
            conversation.Model = Program.APP_SETTING.GPT.Model;
            conversation.AppendSystemMessage(Program.APP_SETTING.GPT.ExplainPrompt);
            if (UserConfig.useExtraPrompt)
            {
                AddExtraSystemPrompt(conversation);
            }
            conversation.AppendUserInput($"{text}");
            return conversation;
        }

        public static Task<Stream> CreateTextToSpeechStream(string text)
        {
            return ttsApi.TextToSpeech.GetSpeechAsStreamAsync(
                text, Program.APP_SETTING.GPT.Voice, Program.APP_SETTING.GPT.VoiceSpeed);
        }

        private static void AddExtraSystemPrompt(Conversation conversation)
        {
            if (UserConfig.ExtraPrompt.Length > 0)
            {
                conversation.AppendSystemMessage(UserConfig.ExtraPrompt);
            }
        }

        async public static void StreamResponse(Conversation conversation, Action<string> callback)
        {
            try
            {
                await foreach (var res in conversation.StreamResponseEnumerableFromChatbotAsync())
                {
                    callback(res);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    class MyHttpClientFactory : IHttpClientFactory
    {
        private string proxy;
        public MyHttpClientFactory(string proxy)
        {
            this.proxy = proxy;
        }
        HttpClient IHttpClientFactory.CreateClient(string name)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                Proxy = new WebProxy($"http://{proxy}")
            };
            var client = new HttpClient(handler);
            return client;
        }
    }

}
