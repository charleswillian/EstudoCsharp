﻿using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    
    public class Login
    {
        public string email { get; set; }
        public string senha { get; set; }
        public int idade { get; set; }
        public int celular { get; set; }
    }
}
