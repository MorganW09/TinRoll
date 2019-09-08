﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TinRoll.Data.Entities
{
    public class Question : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
