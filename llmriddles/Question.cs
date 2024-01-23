using System;
using System.Collections.Generic;
using Agent;
using System.Linq;

namespace Question {

    public enum QuestionLanguage {
        English,
        Chinese,
    };

    public interface IQuestion {
        public string GetQuestionText();
        public (bool, string) Check(string userText, string answerText, QuestionLanguage language);
    }

    public static class Utility
    {
        public static int CountWords(string text, bool containPunctuation = false)
        {
            List<string> chineseWords = new List<string>();
            List<string> englishWords = new List<string>();
            List<string> otherWords = new List<string>();
            List<char> tempEnglishWords = new List<char>();

            foreach (char c in text)
            {
                if ('\u4e00' <= c && c <= '\u9fff')
                {
                    chineseWords.Add(c.ToString());
                    if (tempEnglishWords.Count > 0)
                    {
                        englishWords.Add(new string(tempEnglishWords.ToArray()));
                        tempEnglishWords.Clear();
                    }
                }
                else
                {
                    if (char.IsLetter(c))
                    {
                        tempEnglishWords.Add(c);
                    }
                    else
                    {
                        if (tempEnglishWords.Count > 0)
                        {
                            englishWords.Add(new string(tempEnglishWords.ToArray()));
                            tempEnglishWords.Clear();
                        }
                        otherWords.Add(c.ToString());
                    }
                }
            }

            if (containPunctuation)
            {
                return chineseWords.Count + englishWords.Count + otherWords.Count;
            }
            else
            {
                return chineseWords.Count + englishWords.Count;
            }
        }

        public static string WordReverseText(string inputText, QuestionLanguage language)
        {
            if (language == QuestionLanguage.Chinese)
            {
                char[] charArray = inputText.ToCharArray();
                Array.Reverse(charArray);
                return new string(charArray);
            }
            else
            {
                string[] words = inputText.Split(' ');
                Array.Reverse(words);
                return string.Join(' ', words);
            }
        }

        public static string GetLLMCallbackResponse(string reversedUserText)
        {
            // 调用语言智能体的回调函数，返回反转后的回答
            // 这里后面再加
            return string.Empty;
        }
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

    public class Level12 : IQuestion
    {
        private string questionText = "请你输入三个字（及）以内的问题，使智能体的回答在30个字以上。";

        public string GetQuestionText()
        {
            return questionText;
        }
        public (bool, string) Check(string userText, string answerText, QuestionLanguage language)
        {
            const int maxWords = 3;
            const int minAnswerLength = 30;

            userText = userText.Trim();
            answerText = answerText.Trim();

            int userWordCount = Utility.CountWords(userText);
            int answerWordCount = Utility.CountWords(answerText);

            if (userWordCount > maxWords)
            {
                string errorMessage = (language == QuestionLanguage.Chinese) ? "用户的问题长度应该在三个字及以内" : "Question should be within three words.";
                return (false, errorMessage);
            }
            else if (answerWordCount <= minAnswerLength)
            {
                string errorMessage = (language == QuestionLanguage.Chinese) ? "大语言智能体的答案应该超过30个字" : "Answer should be more than 30 words.";
                return (false, errorMessage);
            }
            else
            {
                return (true, null);
            }
        }
    }

    public class Level13 : IQuestion
    {
        private string questionText = "请你输入三个字（及）以内的问题，使智能体的回答在100个字以上。";

        public string GetQuestionText()
        {
            return questionText;
        }
        public (bool, string) Check(string userText, string answerText, QuestionLanguage language)
        {
            const int maxWords = 3;
            const int minAnswerLength = 100;

            userText = userText.Trim();
            answerText = answerText.Trim();

            int userWordCount = Utility.CountWords(userText);
            int answerWordCount = Utility.CountWords(answerText);

            if (userWordCount > maxWords)
            {
                string errorMessage = (language == QuestionLanguage.Chinese) ? "用户的问题长度应该在三个字及以内" : "Question should be within three words.";
                return (false, errorMessage);
            }
            else if (answerWordCount <= minAnswerLength)
            {
                string errorMessage = (language == QuestionLanguage.Chinese) ? "大语言智能体的答案应该超过100个字" : "Answer should be more than 30 words.";
                return (false, errorMessage);
            }
            else
            {
                return (true, null);
            }
        }
    }

    public class Level14 : IQuestion
    {
        private string questionText = "请你输入一个字（及）以内的问题，使智能体的回答小于20个字。";
        public string GetQuestionText()
        {
            return questionText;
        }
        public (bool, string) Check(string userText, string answerText, QuestionLanguage language)
        {
            const int maxWords = 1;
            const int maxAnswerLength = 20;

            userText = userText.Trim();
            answerText = answerText.Trim();

            int userWordCount = Utility.CountWords(userText);
            int answerWordCount = Utility.CountWords(answerText);

            if (userWordCount > maxWords)
            {
                string errorMessage = (language == QuestionLanguage.Chinese) ? "用户的问题长度应该在一个字及以内" : "Question should be within three words.";
                return (false, errorMessage);
            }
            else if (answerWordCount >= maxAnswerLength)
            {
                string errorMessage = (language == QuestionLanguage.Chinese) ? "大语言智能体的答案应该小于20个字" : "Answer should be more than 30 words.";
                return (false, errorMessage);
            }
            else
            {
                return (true, null);
            }
        }
    }

    public class Level15 : IQuestion
    {
        // 这个问题暂时用不了
        private string questionText = "请输入一个本身不是回文串的问题，使无论正着问还是倒着问，智能体的回答是一样的。";
        public ChatGPT agent; 

        public ChatGPT Agent {
            get { return agent; }
            set { agent = value; }
        }

        public Level15(ChatGPT agent) {
            // 需要agent继续回答的问题
            this.agent = agent;
        }

        public string GetQuestionText()
        {
            return questionText;
        }
        public (bool, string) Check(string userText, string answerText, QuestionLanguage language)
        {
            userText = userText.Trim();
            answerText = answerText.Trim();
            string reversedUserText = Utility.WordReverseText(userText, language);
            string secondAnswerText = this.agent.Chat(reversedUserText);

            if (userText == reversedUserText)
            {
                return (false, language == QuestionLanguage.Chinese ? "用户的问题不能是回文串" : "Question should not be a palindrome string.");
            }

            if (secondAnswerText != answerText)
            {
                string errorMessage = language == QuestionLanguage.Chinese ?
                    $"正着问和倒着问时，智能体的回答应该是一样的\n 问题：{userText}\n 正着问回答：{answerText}\n 反着问回答：{secondAnswerText}" :
                    $"The model's answer should be the same when asked forward or backward.\n Question: {userText}\n Forward-Asking answer: {answerText}\n Backward-Asking answer: {secondAnswerText}";

                return (false, errorMessage);
            }

            return (true, null);
        }
    }

    public class Level16 : IQuestion
    {
        private string questionText = "请输入一个大于一的正整数作为问题，使回答里包含至少比它大一千的数。";

        public string GetQuestionText()
        {
            return questionText;
        }
        public (bool, string) Check(string userText, string answerText, QuestionLanguage language)
        {
            userText = userText.Trim();

            if (!int.TryParse(userText, out int value) || value <= 1)
            {
                return (false, $"输入内容{userText}，并非一个大于1的正整数");
            }

            List<int> numbers = GetNumbers(answerText);

            foreach (int number in numbers)
            {
                if (number >= value + 1000)
                {
                    return (true, $"检测到输出中数字{number}，满足要求");
                }
            }

            return (false, $"未在输出中检测到不少于{value + 1000}的数字");
        }

        private List<int> GetNumbers(string text)
        {
            List<int> numbers = new List<int>();
            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                if (int.TryParse(word, out int number))
                {
                    numbers.Add(number);
                }
            }

            return numbers;
        }
    }

    public class Level17 : IQuestion
    {
        private string questionText = "请提一个不包含“狗”这个字的问题，但是模型的回答中至少出现3次“狗”这个字。";

        public string GetQuestionText()
        {
            return questionText;
        }
        public (bool, string) Check(string userText, string answerText, QuestionLanguage language)
        {
            userText = userText.Trim();
            answerText = answerText.Trim();

            if (userText.Contains("狗"))
            {
                return (false, "问题不得包含“狗”字");
            }

            int dogCount = CountOccurrences("狗", answerText);

            if (dogCount >= 3)
            {
                return (true, $"“狗”字的出现次数为{dogCount}次");
            }
            else
            {
                return (false, $"“狗”字的出现次数为{dogCount}次，未达到3次");
            }
        }

        private int CountOccurrences(string word, string text)
        {
            int count = 0;
            int index = 0;
            while ((index = text.IndexOf(word, index)) != -1)
            {
                index += word.Length;
                count++;
            }
            return count;
        }
    
    }

    public class Level18 : IQuestion
    {
        private string questionText = "请提一个不包含“狗”这个字的10个字以内的问题，使回答中“狗”这个字出现的次数至少是问题字数的2倍。";

        public string GetQuestionText()
        {
            return questionText;
        }
        public (bool, string) Check(string userText, string answerText, QuestionLanguage language)
        {
            userText = userText.Trim();

            if (userText.Length > 10)
            {
                return (false, "问题不得超过10个字");
            }

            if (userText.Contains("狗"))
            {
                return (false, "问题不得包含“狗”字");
            }

            int dogCount = CountOccurrences("狗", answerText);

            if (dogCount >= userText.Length * 2)
            {
                return (true, $"“狗”字的出现次数为{dogCount}次");
            }
            else
            {
                return (false, $"“狗”字的出现次数为{dogCount}次，未达到{userText.Length * 2}次");
            }
        }

        private int CountOccurrences(string word, string text)
        {
            int count = 0;
            int index = 0;
            while ((index = text.IndexOf(word, index)) != -1)
            {
                index += word.Length;
                count++;
            }
            return count;
        }
    
    }

    public class Level19 : IQuestion
    {
        // 这个问题暂时用不了
        private string questionText = "请在不提及1,4,5这三个字符的情况下让智能体回答114514，不要有多余的字符。";
        public bool needAgent = true;

        public string GetQuestionText()
        {
            return questionText;
        }
        public (bool, string) Check(string userText, string answerText, QuestionLanguage language)
        {
            userText = userText.Trim();

            if (userText.Contains("1") || userText.Contains("4") || userText.Contains("5"))
            {
                return (false, "请不要在提问中出现1、4、5三个数字");
            }

            bool isCorrect = answerText.Trim() == "114514";

            return (isCorrect, null);
        }
    }

    public class Level20 : IQuestion
    {
        private string questionText = "请你输入一个至少10个字的提问，使模型的回答和提问完全一样。";
        public bool needAgent = true;

        public string GetQuestionText()
        {
            return questionText;
        }
        public (bool, string) Check(string userText, string answerText, QuestionLanguage language)
        {
            userText = userText.Trim();
            answerText = answerText.Trim();

            if (Utility.CountWords(userText) < 10)
            {
                return (false, "用户的问题长度应该至少10个字");
            }

            if (answerText == userText)
            {
                return (true, null);
            }
            else
            {
                return (false, null);
            }
        }
    }

    public class Level21 : IQuestion
    {
        private string questionText = "- 请输入一个大于一的正整数作为问题，使回答里包含和它刚好相差1的数。";
        public bool needAgent = true;

        public string GetQuestionText()
        {
            return questionText;
        }
        public (bool, string) Check(string userText, string answerText, QuestionLanguage language)
        {
            userText = userText.Trim();
            answerText = answerText.Trim();

            if (!CheckIfIsNumber(userText))
            {
                return (false, "问题应该是一个正整数");
            }
            else if (int.Parse(userText) == 1)
            {
                return (false, "问题应该是一个大于1的正整数");
            }
            else if (!ContainsNumberWithDifferenceOfOne(userText, answerText))
            {
                return (false, "回答中应该包含一个与问题相差1的数字");
            }
            else
            {
                return (true, null);
            }
        }

        private bool CheckIfIsNumber(string text)
        {
            return int.TryParse(text, out int number) && number > 0;
        }

        private bool ContainsNumberWithDifferenceOfOne(string userText, string answerText)
        {
            int questionNumber = int.Parse(userText);
            int[] numbers = GetNumbers(answerText);
            return numbers.Contains(questionNumber - 1) || numbers.Contains(questionNumber + 1);
        }

        private int[] GetNumbers(string text)
        {
            List<int> numbers = new List<int>();
            string[] words = text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                if (int.TryParse(word, out int number))
                {
                    numbers.Add(number);
                }
            }
            return numbers.ToArray();
        }
    }
}
