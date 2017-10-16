using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Verbs.Data.Interfaces;
using Verbs.Model;

namespace Verbs.DataProvider.Tests
{
    public class VerbsDataProviderTests
    {
        //
        private IEnumerable<Verb> GetTestVerbs()
        {
            Inflections inflections = new Inflections();
            inflections.Yo = "abro";
            inflections.El = "abre";
            inflections.Tu = "abres";
            inflections.Ellos = "abren";
            inflections.Nos = "abremos";
            inflections.Vos = "abreis";

            var verb = new Verb();
            verb.Name = "abrir";
            verb.Modo = "Indicativo";
            verb.Tense = "Presente";
            verb.Inflections = inflections;
            yield return verb;
        }

        public VerbsDataProviderTests()
        {
            
        }

        [Test]
        public void ShouldLoad()
        {
             Mock<IVerbDataProvider> _dataProviderMock;
            _dataProviderMock = new Mock<IVerbDataProvider>();

            _dataProviderMock.Setup(dp => dp.GetVerbs("Presente", "Indicativo")).Returns(GetTestVerbs());
            var result = _dataProviderMock.Object;
            //Assert.AreEqual(result.Count, 1);
        }
    }
}
