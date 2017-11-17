using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verbs.Data.Interfaces;
using Verbs.Model;

namespace Verbs.DataProvider
{
    public class VerbsDataProvider : IVerbDataProvider
    {
        private readonly Func<IDataService> _dataServiceCreator;

        public VerbsDataProvider(Func<IDataService> dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        }

        [Obsolete]
        public IEnumerable<Verb> GetAllVerbs()
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.GetAllVerbs();
            }
        }

        [Obsolete]
        public IEnumerable<Verb> GetVerbs(string tense, string mode)
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.GetVerbsForTenseAndMode(tense, mode);
            }
        }

        public IEnumerable<VerbWrapper> GetAllVerbWrappers()
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.GetAllVerbWrappers();
            }
        }
    }
}
