using System;
using System.Text.Json.Serialization;

namespace Discuss.Domain.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string HashPass { get; set; }
    }
}