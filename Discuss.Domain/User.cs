﻿using System;

namespace Discuss.Domain
{
    public class User
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string HashPass { get; set; }
    }
}