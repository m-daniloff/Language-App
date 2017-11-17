using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verbs.Model
{
    public class Verb
    {
       // public string Name { get; set; }
        public string Tense { get; set; }
        public string Modo { get; set; }
        public Inflections Inflections { get; set; }

        public string GetInflected(string pronoun)
        {
            if (pronoun == "Yo")
                return Inflections.Yo;
            if (pronoun == "Tu")
                return Inflections.Tu;
            if (pronoun == "El")
                return Inflections.El;
            if (pronoun == "Nos")
                return Inflections.Nos;
            if (pronoun == "Vos")
                return Inflections.Vos;
            return Inflections.Ellos;
        }
    }
}
