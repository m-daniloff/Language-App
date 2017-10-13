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
        public IEnumerable<Verb> GetAllVerbs()
        {
            // ???
            throw new NotImplementedException();
        }

        public IEnumerable<Verb> GetVerbsForTenseAndMode(string verbTense, string mode)
        {
            using (StreamReader reader = new StreamReader(@"C:\Temp\out.json"))
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
        

        public void Dispose()
        {
            
        }
    }
}
