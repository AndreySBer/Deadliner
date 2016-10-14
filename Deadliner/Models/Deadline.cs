using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadliner.Models
{
    public class Deadline
    {
        //string name;
        public Deadline(string description)
        {
            Title = description;
        }

        public Deadline()
        {
        }

        public override string ToString()
        {
            return Title;
        }

        public string ToFile()
        {
            return Title;
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueTo { get; set; }

    }
}
