﻿namespace QuizFactory.Data.Common
{
    using QuizFactory.Data.Common.Interfaces;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class DeletableEntity : AuditInfo, IDeletableEntity
    {
        [Display(Name = "Deleted?")]
        [Editable(false)]
        [Index]
        public bool IsDeleted { get; set; }

        [Display(Name = "Deleted on")]
        [Editable(false)]
        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
    }
}
