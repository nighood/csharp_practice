namespace Question {

public enum QuestionLanguage {
    English,
    Chinese,
};

public interface IQuestion {
    public string GetQuestionText();
    public (bool, string) Check(string userText, string answerText, QuestionLanguage language);
}

public class Level11 : IQuestion {
    private string questionText = "（初来乍到），请你构造一个问题使智能体的回答是一字不差的“1+1=3”（不需要引号）。";

    public string GetQuestionText() {
        return questionText;
    }
    public (bool, string) Check(string userText, string answerText, QuestionLanguage language) {
        const string standardAnswer = "1+1=3";
        bool success = answerText.Trim() == standardAnswer;
        string explanation = "";
        return (success, explanation);
    }
}

}
