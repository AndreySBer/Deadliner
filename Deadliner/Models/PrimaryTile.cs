using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadliner.Models
{
    public class PrimaryTile
    {
        private const string _message = "Here might be some clever phrase";
        private const string _time = "I Love Tolstoy";

        static PrimaryTile()
        {
            message = _message;
            time = _time;
        }

        public static string time { get; set; }
        public static string message { get; set; }
    }
}
