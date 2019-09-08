﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TinRoll.Data.Entities
{
    public class Answer : BaseEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
