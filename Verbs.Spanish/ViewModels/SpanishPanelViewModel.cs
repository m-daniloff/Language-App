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

        private List<string> PronounsList = new List<string>(){"Yo", "Tu", "El", "Nos", "Vos", "Ellos"};


        private List<Verb> VerbList { get; set; }

        private void LoadVerbData()
        {
            //test
            var result = _verbDataProvider.GetAllVerbWrappers();
            // end test
            var verbs = _verbDataProvider.GetVerbs("Presente", "Indicativo");
            VerbList = verbs.ToList();

            Pronoun = "Yo";
            Verb = "Tener";
            Tiempo = "Presente";
            Modo = "Indicativo";

            GetNextVerb();
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

        private Verb _selectedVerb;

        public Verb SelectedVerb
        {
            get => _selectedVerb;
            set { SetProperty(ref this._selectedVerb, value); }
        }


        private string _elapsedTime;
        private int _correctCount;
        private int _attemptsCount;

        public string ElapsedTime
        {
            get => _elapsedTime;
            set
            {
                _elapsedTime = value;
                SetProperty(ref this._elapsedTime, value);
            }
        }

        public int CorrectCount
        {
            get => _correctCount;
            set
            {
                SetProperty(ref this._correctCount, value);
            }
        }

        public int AttemptsCount
        {
            get => _attemptsCount;
            set
            {
                SetProperty(ref this._attemptsCount, value);
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

            AttemptsCount++;

            var expected = SelectedVerb.GetInflected(Pronoun);

            if (expected == Answer)
            {
                CorrectCount++;
            }
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

            SelectedVerb = VerbList[index];

            Verb = SelectedVerb.Name;

            index = random.Next(0, PronounsList.Count);
            Pronoun = PronounsList[index];
        }
    }
}
