using System.Collections.Generic;
using Verbs.Model;

namespace Verbs.Data.Interfaces
{
    public interface IVerbDataProvider
    {
        IEnumerable<Verb> GetAllVerbs();
        IEnumerable<Verb> GetVerbs(string tense, string mode);
    }
}