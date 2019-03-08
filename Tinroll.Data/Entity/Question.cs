using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tinroll.Data.Entity {
    public class Question : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid QuestionId {get;set;}
        public string QuestionText {get;set;}
        public Guid UserId {get;set;}
        public User User {get;set;}
    }
}