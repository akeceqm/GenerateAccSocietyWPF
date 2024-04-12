using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePasswordWPF.Model.DbTables
{
    public static class TestLsit
    {
        public static List<SocietyTable> GetTestSocieties()
        {
            var societies = new List<SocietyTable>
            {
                new SocietyTable { SocietyId = 1, SocietyName = "Facebook" },
                new SocietyTable { SocietyId = 2, SocietyName = "Twitter" },
                new SocietyTable { SocietyId = 3, SocietyName = "Instagram" },
                new SocietyTable { SocietyId = 4, SocietyName = "LinkedIn" }
            };

            return societies;
        }

    }
}
