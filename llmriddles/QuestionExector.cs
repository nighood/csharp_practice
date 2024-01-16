using System;
using Agent;
using Question;

namespace QuestionExecutor {

public struct ExecutionResult {
    public string answerText;
    public bool success;
    public string explanation;
}

public class LLMQuestionExecutor {
    private QuestionLanguage language;
    private IAgent agent;
    private string successText;
    private string failureText;

    public LLMQuestionExecutor(IAgent agent) {
        this.language = QuestionLanguage.Chinese;
        this.agent = agent;
        if (language == QuestionLanguage.Chinese) {
            this.successText = "智能体的回答符合题目的要求";
            this.failureText = "智能体的回答不符合题目的要求";
        } else {
            this.successText = "The answer of the agent meets the requirements of the question";
            this.failureText = "The answer of the agent does not meet the requirements of the question";
        }
    }

    public ExecutionResult Execute(IQuestion question, string userText) {
        string answerText = agent.Chat(userText);
        
        (bool success, string explanation) info = question.Check(userText, answerText, language);

        ExecutionResult result = new ExecutionResult();
        result.answerText = answerText;
        result.success = info.success;
        if (info.explanation.Equals("")) {
            if (info.success) {
                result.explanation = successText;
            } else {
                result.explanation = failureText;
            }
        } else {
            result.explanation = info.explanation;
        }
        return result;
    } 
}

}
