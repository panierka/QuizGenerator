using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuizGenerator.Model.Game
{
    internal class Question : INotifyPropertyChanged
    {
        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
                OnPropertyChanged(nameof(IsValid));
            }
        }

        public List<Answer> Answers { get; set; }

        private const int NUMBER_OF_ANSWERS = 4;

        public event PropertyChangedEventHandler PropertyChanged;

        public Question()
        {
            Answers = new();

            for (int i = 0; i < NUMBER_OF_ANSWERS; i++)
            {
                Answers.Add(new());
            }

            BindOnChangeEventToAnswers();
        }

        public void BindOnChangeEventToAnswers()
        {
            foreach(var answer in Answers)
            {
                answer.OnChange += () => OnPropertyChanged(nameof(IsValid));
            }
        }

        [JsonIgnore]
        public bool IsValid => !string.IsNullOrEmpty(Content)
            && Answers.All(x => x.IsValid)
            && Answers.Any(x => x.IsCorrect);


        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new(name));
        }
    }
}
