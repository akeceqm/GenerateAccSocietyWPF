using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePasswordWPF.Model.DbTables
{
    public class userTable
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public long SocietyId { get; set; }
    }
}
