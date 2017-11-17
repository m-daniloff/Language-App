using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ScratchPad
{
    class Program
    {
        private static Dictionary<string, string> _translationDictionary = new Dictionary<string, string>();
        static void Main(string[] args)
        {
            //ReadAnotherOne();
            
            //CsvTestEx();

            ReadBack();
        }

        static Dictionary<string, List<Verb>> dictionary = new Dictionary<string, List<Verb>>();

        
        private static void CsvTestEx()
        {
            Dictionary<string, VerbWrapper> dictionary = new Dictionary<string, VerbWrapper>();

            string filename = @"C:\Temp\jehle_dcomtois_updates.csv";
            var contents = File.ReadAllText(filename, Encoding.UTF7).Split('\n');
            var csv = from line in contents
                select line.Split(',').ToArray();

            foreach (var row in csv)
            {
                var result = ProcessLine(row[0]);
                if (row[0].Length > 0)
                {
                    if (!dictionary.ContainsKey(result.Name))
                        dictionary.Add(result.Name,
                            new VerbWrapper()
                            {
                                Name = result.Name,
                                Translation = GetTranslation(result.Name),
                                Suffix = GetSuffix(result.Name),
                                Irregular = false,
                                VerbForms = new List<Verb> {result}
                            });
                    else
                    {
                        VerbWrapper val = dictionary[result.Name];
                        val.VerbForms.Add(result);
                        dictionary[result.Name] = val;
                        //val.Add(result);
                        //dictionary[result.Name] = val;
                    }
                }
            }

            int count = dictionary.Count;



            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;
            using (StreamWriter sw = new StreamWriter(@"c:\temp\outEx.json"))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, dictionary);

                }
            }

            Console.ReadKey();
        }
        private static void CsvTest()
        {
            Dictionary<string, List<Verb>> dictionary = new Dictionary<string, List<Verb>>();

            string filename = @"C:\Temp\jehle_dcomtois_updates.csv";
            var contents = File.ReadAllText(filename, Encoding.UTF7).Split('\n');
            var csv = from line in contents
                select line.Split(',').ToArray();

            foreach (var row in csv)
            {
                var result = ProcessLine(row[0]);
                if (row[0].Length > 0)
                {
                    if (!dictionary.ContainsKey(result.Name))
                        dictionary.Add(result.Name, new List<Verb> { result });
                    else
                    {
                        List<Verb> val = dictionary[result.Name];
                        val.Add(result);
                        dictionary[result.Name] = val;
                    }
                }
            }

            int count = dictionary.Count;



            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;
            using (StreamWriter sw = new StreamWriter(@"c:\temp\out.json"))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, dictionary);

                }
            }

            Console.ReadKey();
        }

        private static Verb ProcessLine(string line)
        {
            var columns = line.Split(':');
            Console.WriteLine(columns.Length);

            Verb t = new Verb();

            t.Name = columns[1];
            t.Modo = columns[2];
            t.Tense = columns[3];

            Inflections inflections = new Inflections();
            inflections.Yo = columns[4];
            inflections.Tu = columns[5];
            inflections.El = columns[6];
            inflections.Nos = columns[7];
            inflections.Vos = columns[8];
            inflections.Ellos = columns[9];

            t.Inflections = inflections;

            return t;
        }

        private static void ReadBackImport()
        {
            using (StreamReader reader = new StreamReader(@"C:\Temp\out.json"))
            {
                string json = reader.ReadToEnd();
                var result = JsonConvert.DeserializeObject<Dictionary<string, List<Verb>>>(json);
            }
        }

        private static void Test()
        {
            SpanishVerb v1 = new SpanishVerb
            {
                Verb = "beber",
                Suffix = "er",
                Translation = "To drink",
                IrregularForms = null
            };

            SpanishVerb v2 = new SpanishVerb
            {
                Verb = "aceptar",
                Suffix = "ar",
                Translation = "To accept",
                IrregularForms = null
            };

            SpanishVerb v3 = new SpanishVerb
            {
                Verb = "abrir",
                Suffix = "ir",
                Translation = "To open",
                IrregularForms = new Dictionary<string, string>()
                {
                    {"Participio","abierto"}
                }
            };

            List<SpanishVerb> list = new List<SpanishVerb>();
            list.Add(v1);
            list.Add(v2);
            list.Add(v3);

            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;
            using (StreamWriter sw = new StreamWriter(@"c:\temp\out.json"))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, list);

                }
            }
        }

        private static string GetSuffix(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string suffix = input.Substring(input.Length - 2);

            return suffix;
        }

        private static string GetTranslation(string input)
        {
            if (_translationDictionary.ContainsKey(input))
                return _translationDictionary[input];

            return "Notranslation";
        }


        private static void ReadBack()
        {
            Dictionary<string, VerbWrapper> source = new Dictionary<string, VerbWrapper>();
            using (StreamReader reader = new StreamReader(@"C:\Temp\outEx.json"))
            {
                string json = reader.ReadToEnd();
                source = JsonConvert.DeserializeObject<Dictionary<string, VerbWrapper>>(json);
            }

            Dictionary<string, VerbWrapper2> target = new Dictionary<string, VerbWrapper2>();

            foreach (var input in source)
            {
                VerbWrapper vw = input.Value;
                List<Verb> lst = vw.VerbForms;

                List<Verb2> lst2 = new List<Verb2>();

                foreach (var l in lst)
                {
                    var v2 = new Verb2();
                    v2.Modo = l.Modo;
                    v2.Tense = l.Tense;
                    v2.Inflections = l.Inflections;

                    lst2.Add(v2);
                }

                VerbWrapper2 vw2 = new VerbWrapper2();
                vw2.Name = vw.Name;
                vw2.Irregular = vw.Irregular;
                vw2.Suffix = vw.Suffix;
                vw2.Translation = vw.Translation;
                vw2.VerbForms = lst2;
                target.Add(input.Key,  vw2);
            }

            int n = target.Count;

            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;
            using (StreamWriter sw = new StreamWriter(@"c:\temp\Better.json"))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, target);

                }
            }

            n = target.Count;
        }


        private static void ReadAnotherOne()
        {
            string filename = @"C:\Temp\trans.csv";
            var contents = File.ReadAllText(filename, Encoding.UTF7).Split('\n');
           
            foreach (var line in contents)
            {
                var reg = new Regex("\".*?\"");
                var matches = reg.Matches(line);
                string spanish = matches[0].ToString().TrimStart('\"').TrimEnd('\"');
                string english = matches[1].ToString().TrimStart('\"').TrimEnd('\"');

                if (!_translationDictionary.ContainsKey(spanish))
                {
                    _translationDictionary.Add(spanish, english);
                }
            }
        }
    }


    public class Verb
    {
        public string Name { get; set; }
        public string Tense { get; set; }
        public string Modo { get; set; }
        public Inflections Inflections { get; set; }
    }

    public class VerbWrapper
    {
        public List<Verb> VerbForms { get; set; }
        public string Name { get; set; }
        public string Translation { get; set; }
        public string Suffix { get; set; }

        public bool Irregular { get; set; }
    }


    //CONVERT
    public class Verb2
    {
        public string Tense { get; set; }
        public string Modo { get; set; }
        public Inflections Inflections { get; set; }
    }

    public class VerbWrapper2
    {
        public List<Verb2> VerbForms { get; set; }
        public string Name { get; set; }
        public string Translation { get; set; }
        public string Suffix { get; set; }

        public bool Irregular { get; set; }
    }

    public class Inflections
    {
        public string Yo { get; set; }
        public string Tu { get; set; }
        public string El { get; set; }
        public string Nos { get; set; }
        public string Vos { get; set; }
        public string Ellos { get; set; }
    }
    public class SpanishVerb
    {
        public string Verb { get; set; }
        public string Translation { get; set; }
        public string Suffix { get; set; }
        public Dictionary<string, string> IrregularForms { get; set; }
    }

    public class ImportVerb
    {
        public string e_present { get; set; }
        public string e_present3p { get; set; }
        public string e_past { get; set; }
        public string e_pparticiple { get; set; }
        public string e_gerund { get; set; }
        public string english_disambiguation { get; set; }
        public Dictionary<string, string> IrregularForms { get; set; }
        public string english_template_type { get; set; }
    }

}
