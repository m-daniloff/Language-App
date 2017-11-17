using System;
using System.Collections.Generic;
using Verbs.Model;

namespace Verbs.Data.Interfaces
{
    public interface IVerbDataProvider
    {
        [Obsolete]
        IEnumerable<Verb> GetAllVerbs();
        [Obsolete]
        IEnumerable<Verb> GetVerbs(string tense, string mode);

        IEnumerable<VerbWrapper> GetAllVerbWrappers();
    }
}