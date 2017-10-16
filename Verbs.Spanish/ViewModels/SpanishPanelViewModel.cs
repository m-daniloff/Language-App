﻿using System;
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
        }


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
                _pronoun = value;
                SetProperty(ref this._pronoun, value);
            }
        }

        public string Verb
        {
            get => _verb;
            set
            {
                _verb = value;
                SetProperty(ref this._verb, value);
            }
        }

        public string Tiempo
        {
            get => _tiempo;
            set
            {
                _tiempo = value;
                SetProperty(ref this._tiempo, value);
            }
        }

        public string Modo
        {
            get => _modo;
            set
            {
                _modo = value;
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

        #endregion


        #region Commands


        private void StartCommandExecuted()
        {
            Random random = new Random();
            int index = random.Next(0, VerbList.Count);

            var verb = VerbList[index];

        }
        public bool CanExecuteStartCommand => true;
        #endregion
    }
}
