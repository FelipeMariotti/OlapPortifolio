using System;
using System.Collections.Generic;
using System.Text;

namespace OLAPPortifolio.Application.Handlers
{
    internal class Helpers
    {
            public static string Hash(string password)
                => BCrypt.Net.BCrypt.HashPassword(password);

            public static bool Verify(string password, string hash)
                => BCrypt.Net.BCrypt.Verify(password, hash);

    }
}
