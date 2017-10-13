using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verbs.Data.Interfaces;
using Verbs.Model;

namespace Verbs.Data
{
    public class VerbsDataService : IDataService
    {
        public IEnumerable<Verb> GetAllVerbs()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Verb> GetVerbsForTenseAndMode(string verbTense, string mode)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}
