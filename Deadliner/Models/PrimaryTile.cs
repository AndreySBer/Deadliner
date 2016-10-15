using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadliner.Models
{
    public class PrimaryTile
    {
        public static string IdealTime = "Нет задач";
        public static string IdealMessage = "...или они еще не загружены.";
        public string time { get; set; } = IdealTime;
        public string message { get; set; } = IdealMessage;
    }
}
