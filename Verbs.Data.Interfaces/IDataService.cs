using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verbs.Model;

namespace Verbs.Data.Interfaces
{
    public interface IDataService : IDisposable
    {
        IEnumerable<Verb> GetAllVerbs();
        IEnumerable<Verb> GetVerbsForTenseAndMode(string verbTense, string mode);
    }
}
