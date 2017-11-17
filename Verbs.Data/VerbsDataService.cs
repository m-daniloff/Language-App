using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Verbs.Data.Interfaces;
using Verbs.Model;

namespace Verbs.Data
{
    public class VerbsDataService : IDataService
    {
        [Obsolete]
        public IEnumerable<Verb> GetAllVerbs()
        {
            // ???
            throw new NotImplementedException();
        }

        [Obsolete]
        public IEnumerable<Verb> GetVerbsForTenseAndMode(string verbTense, string mode)
        {
            using (StreamReader reader = new StreamReader(@"..\..\..\Data\data.json"))
            {
                string json = reader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<Dictionary<string, List<Verb>>>(json);

                foreach (var item in result)
                {
                    var verbList = item.Value;
                    foreach (var verbItem in verbList)
                    {
                        if (verbItem.Tense == verbTense && verbItem.Modo == mode)
                            yield return verbItem;
                    }
                }
            }
        }

        public IEnumerable<VerbWrapper> GetAllVerbWrappers()
        {
            using (StreamReader reader = new StreamReader(@"..\..\..\Data\data1.json"))
            {
                string json = reader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<Dictionary<string, VerbWrapper>>(json);

                foreach (var item in result)
                {
                    yield return item.Value;
                }
            }
        }

        public IEnumerable<VerbWrapper> GetVerbWrappersForTenseAndMode(string verbTense, string mode)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            
        }
    }
}
