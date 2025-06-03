using OpenAI;
using OpenAI.Chat;
using OpenAI.Audio;
using System.ClientModel;
using System.Diagnostics;

namespace ja_learner
{
    internal class GptCaller
    {
        private static OpenAIClient client;
        private static OpenAIClient ttsClient;

        public static void Initialize()
        {
            OpenAIClientOptions options = new()
            {
                Endpoint = new Uri(Program.APP_SETTING.GPT.ApiUrl)
            };
            Debug.WriteLine(Program.APP_SETTING.GPT.ApiUrl);
            client = ttsClient = new(
                new ApiKeyCredential(Program.APP_SETTING.GPT.ApiKey),
                options
            );

            if (Program.APP_SETTING.GPT.TtsApiKey != string.Empty || Program.APP_SETTING.GPT.TtsApiUrl != string.Empty)
            {
                OpenAIClientOptions ttsOptions = new()
                {
                    Endpoint = new Uri(Program.APP_SETTING.GPT.TtsApiUrl)
                };
                ttsClient = new(
                    new ApiKeyCredential(Program.APP_SETTING.GPT.TtsApiKey),
                    ttsOptions
                );
            }
        }

        public static void SetProxy(bool useProxy)
        {
            if (useProxy)
            {

            }
            else
            {

            }
        }
        public static Task<ClientResult<BinaryData>> Speech(string text)
        {
            return client.GetAudioClient("tts-1").GenerateSpeechAsync(text, Program.APP_SETTING.GPT.Voice);
        }

        public static async void Chat(string systemPrompt, string userPrompt, Action<string> streamCallback)
        {
            ChatClient chatClient = client.GetChatClient(Program.APP_SETTING.GPT.Model);
            AsyncCollectionResult<StreamingChatCompletionUpdate> completionUpdates = chatClient.CompleteChatStreamingAsync(systemPrompt + "\n" + userPrompt);
            await foreach (StreamingChatCompletionUpdate completionUpdate in completionUpdates)
            {
                if (completionUpdate.ContentUpdate.Count > 0)
                {
                    streamCallback(completionUpdate.ContentUpdate[0].Text);
                }
            }
        }

        public static async void CreateTranslateConversation(string userPrompt, Action<string> streamCallback)
        {
            string systemPrompt = Program.APP_SETTING.GPT.TranslatePrompt;
            if (UserConfig.useExtraPrompt)
            {
                systemPrompt += UserConfig.ExtraPrompt;
            }
            Chat(systemPrompt, userPrompt, streamCallback);
        }

        public static async void CreateInterpretConversation(string userPrompt, Action<string> streamCallback)
        {
            string systemPrompt = Program.APP_SETTING.GPT.ExplainPrompt;
            if (UserConfig.useExtraPrompt)
            {
                systemPrompt += UserConfig.ExtraPrompt;
            }
            Chat(systemPrompt, userPrompt, streamCallback);
        }
    }

}
