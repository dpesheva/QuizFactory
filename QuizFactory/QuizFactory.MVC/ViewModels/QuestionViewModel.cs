﻿using QuizFactory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace QuizFactory.Mvc.ViewModels
{
    public class QuestionViewModel
    {
        public static Expression<Func<QuestionDefinition, QuestionViewModel>> FromQuestionDefinition
        {
            get
            {
                return question => new QuestionViewModel
                {
                    Id = question.Id,
                    QuestionText = question.QuestionText,
                    Number = question.Number,
                    Answers = question.AnswersDefinitions.Select(a => new AnswerViewModel
                    {
                        Id = a.Id,
                        Text = a.Text,
                        IsCorrect = a.IsCorrect,
                        Position = a.Position
                    }).ToList()
                };
            }
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Question")]
        public string QuestionText { get; set; }

        [Required]
        public int Number { get; set; }
        
        public ICollection<AnswerViewModel> Answers { get; set; }
    }
}