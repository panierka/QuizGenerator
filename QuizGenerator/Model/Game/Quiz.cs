using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace QuizGenerator.Model.Game
{
    internal class Quiz
    {
        public string Name { get; set; } = "Mój Quiz";

        public string Description { get; set; } = "Oto mój quiz.";

        public ObservableCollection<Question> Questions { get; set; } = new();

        public Quiz()
        {

        }

        public Question AddNewQuestion()
        {
            Question question = new();
            Questions.Add(question);

            return question;
        }

        public Quiz TrimmedCopy()
        {
            Quiz copy = new();

            copy.Name = Name;
            copy.Description = Description;
            copy.Questions = new(Questions.Where(x => x.IsValid));

            return copy;
        }
    }
}
