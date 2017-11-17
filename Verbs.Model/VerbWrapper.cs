using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verbs.Model
{
    public class VerbWrapper
    {
        public List<Verb> VerbForms { get; set; }

        public string Name { get; set; }
        public string Translation { get; set; }
        public string Suffix { get; set; }

        public bool Irregular { get; set; }
    }
}
