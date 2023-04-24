using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace QuizGenerator.Model.Game
{
    internal class Answer
    {
        private string _content;
        private bool _isCorrect;

        public string Content 
        { 
            get => _content;
            set
            {
                _content = value;
                OnChange?.Invoke();
            } 
        }

        public bool IsCorrect
        {
            get => _isCorrect;
            set
            {
                _isCorrect = value;
                OnChange?.Invoke();
            }
        }

        [JsonIgnore]
        public bool IsValid => !string.IsNullOrEmpty(Content);

        public event Action OnChange;
    }
}
