using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadliner.Models
{
    public class Deadline
    {
        string name;
        public Deadline(string description)
        {
            name = description;
        }

        public Deadline()
        {
        }

        public override string ToString()
        {
            return name;
        }

        public string ToFile()
        {
            return name;
        }
    }
}
