using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ScratchPad
{
    class Program
    {
        static void Main(string[] args)
        {
           // Test();
     //       ReadBack();
            //ReadBackImport();
        }

        static Dictionary<string, List<Verb>> dictionary = new Dictionary<string, List<Verb>>();
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

        //private static void ReadBackImport()
        //{
        //    using (StreamReader reader = new StreamReader(@"C:\Temp\Verbs.json"))
        //    {
        //        string json = reader.ReadToEnd();
        //        var result = JsonConvert.DeserializeObject<List<ImportVerb>>(json);
        //    }
        //}

        //private static void Test()
        //{
        //    SpanishVerb v1 = new SpanishVerb
        //    {
        //        Verb = "beber",
        //        Suffix = "er",
        //        Translation = "To drink",
        //        IrregularForms = null
        //    };

        //    SpanishVerb v2 = new SpanishVerb
        //    {
        //        Verb = "aceptar",
        //        Suffix = "ar",
        //        Translation = "To accept",
        //        IrregularForms = null
        //    };

        //    SpanishVerb v3 = new SpanishVerb
        //    {
        //        Verb = "abrir",
        //        Suffix = "ir",
        //        Translation = "To open",
        //        IrregularForms = new Dictionary<string, string>()
        //        {
        //            {"Participio","abierto"}
        //        }
        //    };

        //    List<SpanishVerb> list = new List<SpanishVerb>();
        //    list.Add(v1);
        //    list.Add(v2);
        //    list.Add(v3);

        //    JsonSerializer serializer = new JsonSerializer();
        //    serializer.NullValueHandling = NullValueHandling.Ignore;
        //    serializer.Formatting = Formatting.Indented;
        //    using (StreamWriter sw = new StreamWriter(@"c:\temp\out.json"))
        //    {
        //        using (JsonWriter writer = new JsonTextWriter(sw))
        //        {
        //            serializer.Serialize(writer, list);

        //        }
        //    }
        //}

        //private static void ReadBack()
        //{
        //    using (StreamReader reader = new StreamReader(@"C:\Temp\out.json"))
        //    {
        //        string json = reader.ReadToEnd();
        //        var result = JsonConvert.DeserializeObject<List<SpanishVerb>>(json);
        //    }
        //}
    }


    public class Verb
    {
        public string Name { get; set; }
        public string Tense { get; set; }
        public string Modo { get; set; }
        public Inflections Inflections { get; set; }
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
