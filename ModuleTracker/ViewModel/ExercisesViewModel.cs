﻿using ModuleTracker.Domain.Models;
using ModuleTracker.Wpf.Commands;
using ModuleTracker.Wpf.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ModuleTracker.Wpf.ViewModel
{
    public class ExercisesViewModel : BaseViewModel
    {
        private readonly ObservableCollection<ExerciseListingItemViewModel> _exerciseListingItemViewModel;
        private readonly ModuleStore _moduleStore;
        private readonly Sheet _sheet;                

        public ExercisesViewModel(Sheet sheet, ModuleStore moduleStore, ModalNavigationStore modalNavigationStore)
        {
            SheetId = sheet.Id;
            ModuleId = sheet.ModuleId;
            SheetNumber = sheet.SheetNumber.ToString();

            _sheet = sheet;
            _exerciseListingItemViewModel = new ObservableCollection<ExerciseListingItemViewModel>();
            _moduleStore = moduleStore;

            OkCommand = new OkExercisesCommand(this, moduleStore, modalNavigationStore);

            AddExerciseItemViewModels();           

            _moduleStore.ExerciseUpdated += SheetStoreExerciseUpdated;
        }

        #region Properties

        public Guid SheetId { get; }
        public Guid ModuleId { get; }

        private string _sheetNumber;
        public string SheetNumber
        {
            get
            {
                return _sheetNumber;
            }
            set
            {
                _sheetNumber = value;
                OnPropertyChanged(nameof(SheetNumber));
            }
        }

        public IEnumerable<ExerciseListingItemViewModel> ExerciseListingItemViewModel =>
            _exerciseListingItemViewModel;

        #endregion

        #region Commands

        public ICommand OkCommand { get; set; }

        #endregion

        #region Actions

        public override void Dispose()
        {
            _moduleStore.ExerciseUpdated -= SheetStoreExerciseUpdated;

            base.Dispose();
        }

        private void SheetStoreExerciseUpdated(Exercise exercise)
        {
            var exerciseModel = _exerciseListingItemViewModel.FirstOrDefault(e => e.ExerciseId == exercise.Id);

            exerciseModel?.Update(exercise);
        }

        #endregion

        #region Methods
        private void AddExerciseItemViewModels()
        {
            _exerciseListingItemViewModel.Clear();

            if (_sheet != null)
            {
                foreach (var exercise in _sheet.Exercises)
                {
                    _exerciseListingItemViewModel.Add(new ExerciseListingItemViewModel(exercise));
                }
            }
        }

        #endregion




    }
}
