using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadliner.Models
{
    public class TodoItem
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool Complete { get; set; }
        public string Title { get; set; }
        public DateTime DueTo { get; set; }
        public String UserName { get; set; }
    }
}
