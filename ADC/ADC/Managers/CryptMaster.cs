using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADC.Managers
{
    internal class CryptMaster    {
        public string standardSaltHash(string toHash)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(4);
            return BCrypt.Net.BCrypt.HashPassword(toHash, salt);
        }

        public bool verifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
