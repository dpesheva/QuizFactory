using AutoMapper;
using QuizFactory.Data.Models;
using QuizFactory.Mvc.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizFactory.Mvc.Areas.Admin.ViewModels
{
    public class CategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Created on")]
        [Editable(false)]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Modified on")]
        [Editable(false)]
        public DateTime? ModifiedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {

            configuration.CreateMap<Category, CategoryViewModel>().ReverseMap();
        }
    }
}