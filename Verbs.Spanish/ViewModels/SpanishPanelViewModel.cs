using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
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
        private Timer _timer;
        public SpanishPanelViewModel(IVerbDataProvider verbDataProvider)
        {
            _verbDataProvider = verbDataProvider;
            LoadVerbData();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            CanExecuteStartCommand = true;
            StartCommand = new DelegateCommand(StartCommandExecuted, ()=> CanExecuteStartCommand);
            CheckCommand = new DelegateCommand(CheckCommandExecuted, () => CanExecuteCheckCommand);
            KeyCommand = new DelegateCommand<string>(OnKeyCommand);

        }

        private List<string> PronounsList = new List<string>(){"Yo", "Tu", "El", "Nos", "Vos", "Ellos"};


        private List<VerbWrapper> VerbList { get; set; }

        private void LoadVerbData()
        {
            //test
            VerbList = _verbDataProvider.GetAllVerbWrappers().ToList();
            // end test
            //var verbs = _verbDataProvider.GetVerbs("Presente", "Indicativo");
            //VerbList = verbs.ToList();

            Pronoun = "Yo";
            Verb = "Tener";
            Tiempo = "Presente";
            Modo = "Indicativo";

            //GetNextVerb();
        }

        #region Properties

        private string _answer;

        public string Answer
        {
            get => _answer;
            set
            {
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

        private string _translation;
        public string Translation
        {
            get { return _translation; }
            set { SetProperty(ref this._translation, value); }
            
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

        private VerbWrapper _selectedVerb;

        public VerbWrapper SelectedVerb
        {
            get => _selectedVerb;
            set { SetProperty(ref this._selectedVerb, value); }
        }


        private int _elapsedTime;
        private int _correctCount;
        private int _attemptsCount;

        public int ElapsedTime
        {
            get => _elapsedTime;
            set
            {
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
        public ICommand KeyCommand { get; private set; }

        #endregion


        #region Commands


        private void StartCommandExecuted()
        {
            CanExecuteStartCommand = false;

            _timer = new Timer(OnTimer, null, 0, 1000);
            GetNextVerb();
        }

        private void OnTimer(object state)
        {
            ElapsedTime++;
        }

        public bool CanExecuteStartCommand { get; set; }


        public bool CanExecuteCheckCommand
        {
            get
            {
                return !string.IsNullOrEmpty(Answer) && !CanExecuteStartCommand;
            }
        }

        private void CheckCommandExecuted()
        {

            AttemptsCount++;

            var expected = GetExpectedVerb().GetInflected(Pronoun);

            if (expected == Answer)
            {
                CorrectCount++;
                Answer = string.Empty;
                GetNextVerb();
            }
        }

        private void OnKeyCommand(string arg)
        {
            Answer += arg;
        }

        private bool CanExecuteKeyCommand
        {
            get { return true; }
        }

        private Verb GetExpectedVerb()
        {
            return SelectedVerb.VerbForms.FirstOrDefault(v => v.Tense == Tiempo && v.Modo == Modo);

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
            Translation = SelectedVerb.Translation;

            index = random.Next(0, PronounsList.Count);
            Pronoun = PronounsList[index];
        }
    }
}
