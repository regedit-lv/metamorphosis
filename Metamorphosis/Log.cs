using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metamorphosis
{
    enum Error
    {
        UnexpectedToken,
        NotFound,
        UnknownToken,
        WrongFormat
    }

    class Log
    {
        public static void Error(string token, Error error, string text)
        {
            Console.WriteLine("Error " + error.ToString() + "on token \"" + token + "\": " + text);
        }
    }
}
