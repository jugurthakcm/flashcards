using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcards
{
    public static class Variables
    {
        public static string? defaultConnection = ConfigurationManager.AppSettings.Get(
            "defaultConnection"
        );
    }
}
