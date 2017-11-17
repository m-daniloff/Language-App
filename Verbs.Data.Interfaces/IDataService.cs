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
        [Obsolete]
        IEnumerable<Verb> GetAllVerbs();
        [Obsolete]
        IEnumerable<Verb> GetVerbsForTenseAndMode(string verbTense, string mode);

        IEnumerable<VerbWrapper> GetAllVerbWrappers();

        IEnumerable<VerbWrapper> GetVerbWrappersForTenseAndMode(string verbTense, string mode);
    }
}
