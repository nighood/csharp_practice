using System;
using OpenAI_API;

namespace Agent {
public interface IAgent {
    public string Chat(string userMessage);
}

public class ChatGPT : IAgent{
    private string apiKey;
    private string modelName;
    private float temperature;
    private OpenAIAPI client;

    public ChatGPT(string apiKey, string modelName, float temperature = 0.7f) {
        this.apiKey = apiKey;
        this.modelName = modelName;
        this.temperature = temperature;
        this.client = new OpenAIAPI(apiKey);
    }

    public string Chat(string userMessage) {
        var task = client.Completions.CreateCompletionAsync(userMessage, temperature: this.temperature, model: this.modelName);
        task.Wait();
        return task.Result.ToString();
    }
}
}
