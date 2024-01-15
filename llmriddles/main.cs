using System;

namespace llmriddles
{
    class Program
    {
        static void Main(string[] args)
        {
            string userMessage = args[0];
            string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            if (apiKey == null) {
                Console.WriteLine("Please set OPENAI_API_KEY environment variable.");
                return;
            }
            ChatGPT agent = new ChatGPT(apiKey, modelName: "gpt-3.5-turbo-instruct");
            string result = agent.Chat(userMessage);
            Console.WriteLine("Chat result is: {0}", result);
        }
    }
}
