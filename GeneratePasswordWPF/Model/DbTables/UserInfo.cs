﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePasswordWPF.Model.DbTables
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string SocietyName { get; set; }
        public int SocietyId { get; set; }
        public string Description { get; set; }
    }
}
