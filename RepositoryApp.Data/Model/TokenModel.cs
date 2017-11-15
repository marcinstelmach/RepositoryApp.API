﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryApp.Data.Model
{
    public class TokenModel
    {
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
