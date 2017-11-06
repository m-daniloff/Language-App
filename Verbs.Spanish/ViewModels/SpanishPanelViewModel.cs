using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Markup;
using Prism.Commands;
using Verbs.Data.Interfaces;
using Verbs.Infrastructure.Base;
using Verbs.Model;

namespace Verbs.Spanish.ViewModels
{
    public class SpanishPanelViewModel : ViewModelBase
    {
        private IVerbDataProvider _verbDataProvider;
        public SpanishPanelViewModel(IVerbDataProvider verbDataProvider)
        {
            _verbDataProvider = verbDataProvider;
            LoadVerbData();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            StartCommand = new DelegateCommand(StartCommandExecuted, ()=> CanExecuteStartCommand);
            CheckCommand = new DelegateCommand(CheckCommandExecuted, () => CanExecuteCheckCommand);
        }

        private List<string> PronounsList = new List<string>(){"Yo", "Tú", "El/Ella/Ud", "Nosotros/Nosotras", "Vosotros", "Ellos/Ellas/Uds"};


        private List<Verb> VerbList { get; set; }

        private void LoadVerbData()
        {
            var verbs = _verbDataProvider.GetVerbs("Presente", "Indicativo");
            VerbList = verbs.ToList();

            Pronoun = "Yo";
            Verb = "Tener";
            Tiempo = "Presente";
            Modo = "Indicativo";
        }

        #region Properties

        private string _answer;

        public string Answer
        {
            get => _answer;
            set
            {
                _answer = value;
                SetProperty(ref this._answer, value);
                RaiseCanExecuteChanged();
            }
        }

        private string _pronoun;
        private string _verb;
        private string _tiempo;
        private string _modo;

        public string Pronoun
        {
            get => _pronoun;
            set
            {
                SetProperty(ref this._pronoun, value);
            }
        }

        public string Verb
        {
            get => _verb;
            set
            {
                //_verb = value;
                SetProperty(ref this._verb, value);
            }
        }

        public string Tiempo
        {
            get => _tiempo;
            set
            {
                SetProperty(ref this._tiempo, value);
            }
        }

        public string Modo
        {
            get => _modo;
            set
            {
                SetProperty(ref this._modo, value);
            }
        }


        private string _elapsedTime;

        public string ElapsedTime
        {
            get => _elapsedTime;
            set
            {
                _elapsedTime = value;
                SetProperty(ref this._elapsedTime, value);
            }
        }

        public ICommand StartCommand { get; private set; }
        public ICommand CheckCommand { get; private set; }

        #endregion


        #region Commands


        private void StartCommandExecuted()
        {
            GetNextVerb();
        }
        public bool CanExecuteStartCommand => true;


        public bool CanExecuteCheckCommand
        {
            get
            {
                return !string.IsNullOrEmpty(Answer);
            }
        }

        private void CheckCommandExecuted()
        {
            //throw new NotImplementedException();

            //Have to compare whatever in the text field
            // against the verb in the table
            int n = Answer.Length;
        }

        private void RaiseCanExecuteChanged()
        {
            DelegateCommand command = CheckCommand as DelegateCommand;
            command?.RaiseCanExecuteChanged();
        }

        #endregion

        private void GetNextVerb()
        {
            Random random = new Random();
            int index = random.Next(0, VerbList.Count);

            var verb = VerbList[index];

            Verb = verb.Name;

            index = random.Next(0, PronounsList.Count);
            Pronoun = PronounsList[index];
        }
    }
}
