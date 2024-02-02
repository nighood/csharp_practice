using System;
using Agent;
using Question;
using QuestionExecutor;
using System.Text;

namespace llmriddles
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            string apiKey = args[0];
            if (apiKey == null) {
                Console.WriteLine("Please set OPENAI_API_KEY environment variable.");
                return;
            }
            ChatGPT agent = new ChatGPT(apiKey, modelName: "gpt-3.5-turbo-instruct");
            LLMQuestionExecutor executor = new LLMQuestionExecutor(agent);
            IQuestion question = new Level21();

            Console.WriteLine(question.GetQuestionText());
            string userInput = Console.ReadLine();
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(userInput);
            string userMessage = Encoding.UTF8.GetString(utf8Bytes);

            ExecutionResult result = executor.Execute(question, userMessage);

            string answer = result.success ? "正确的" : "错误的";
            Console.WriteLine("你的回答是{0}, 因为{1}。智能体的实际回答为：{2}", answer, result.explanation, result.answerText);
        }
    }
}
