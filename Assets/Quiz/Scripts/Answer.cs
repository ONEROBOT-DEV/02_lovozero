namespace Quiz
{
    public struct Answer
    {
        public readonly string AnswerText;
        public readonly bool IsCorrect;

        public Answer(string answerText, bool isCorrect)
        {
            AnswerText = answerText;
            IsCorrect = isCorrect;
        }
    }
}
