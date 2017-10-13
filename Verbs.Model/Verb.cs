using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verbs.Model
{
    public class Verb
    {
        public string Name { get; set; }
        public string Tense { get; set; }
        public string Modo { get; set; }
        public Inflections Inflections { get; set; }
    }
}
