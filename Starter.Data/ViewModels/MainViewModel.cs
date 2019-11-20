using System;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Starter.Data.Commands;
using Starter.Data.Entities;
using Starter.Framework.Clients;
using Starter.Framework.Extensions;

namespace Starter.Data.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class MainViewModel : IMainViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool AllowSave
        {
            get
            {
                if (SelectedCat == null)
                {
                    return false;
                }

                return SelectedCat.AbilityId != 0 && SelectedCat.Name.IsNotEmpty();
            }
        }

        public bool IsCatSelected => _selectedCat != null;

        public PropertyObservable<bool> IsCreating { get; set; }

        public PropertyObservable<bool> IsLoading { get; set; }

        public PropertyObservable<bool> IsNameFocused { get; set; }

        public ICommand CreateCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public List<object> Abilities { get; set; }

        public Cat SelectedCat
        {
            get => _selectedCat;

            set
            {
                IsNameFocused.Value = false;
                IsCreating.Value = false;

                GetById(value.Id);

                IsNameFocused.Value = true;
            }
        }

        public ObservableCollection<IEntity> Cats
        {
            get => _cats;

            set
            {
                _cats = value;

                OnPropertyChanged(nameof(Cats));
            }
        }

        public MainViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;

            IsCreating = new PropertyObservable<bool>(false);
            IsLoading = new PropertyObservable<bool>(false);
            IsNameFocused = new PropertyObservable<bool>(false);

            CreateCommand = new CatCommand(Create, param => !IsCreating.Value);
            SaveCommand = new CatCommand(Save, canExecute => AllowSave);
            DeleteCommand = new CatCommand(Delete, canExecute => IsCatSelected && !IsCreating.Value);
            CancelCommand = new CatCommand(ResetSelection, canExecute => IsCatSelected || IsCreating.Value);

            Abilities = new List<object>(typeof(Ability).ToNameValueList());
            
            GetAll();
        }

        public async Task GetAll()
        {
            IsLoading.Value = true;

            Cats = new ObservableCollection<IEntity>(await _apiClient.GetAll<Cat>());

            IsLoading.Value = false;
        }

        public async Task GetById(Guid id)
        {
            IsLoading.Value = true;

            _selectedCat = await _apiClient.GetById<Cat>(id);

            OnPropertyChanged(nameof(SelectedCat));
            OnPropertyChanged(nameof(IsCatSelected));

            IsLoading.Value = false;
        }

        public void Create()
        {
            IsCreating.Value = true;
            IsNameFocused.Value = false;

            //_selectedCat = parameter;
            _selectedCat = new Cat
            {
                Id = Guid.NewGuid(),
                AbilityId = 0
            };

            IsNameFocused.Value = true;

            OnPropertyChanged(nameof(SelectedCat));
            OnPropertyChanged(nameof(IsCatSelected));
        }

        public async void Save()
        {
            IsLoading.Value = true;

            if (IsCreating.Value)
            {
                await _apiClient.Create(SelectedCat);
            }
            else
            {
                await _apiClient.Update(SelectedCat);
            }

            ResetSelection();

            await GetAll();
        }

        public async void Delete()
        {
            IsLoading.Value = true;

            await _apiClient.Delete(SelectedCat.Id);

            ResetSelection();

            await GetAll();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ResetSelection()
        {
            _selectedCat = null;
            IsCreating.Value = false;

            OnPropertyChanged(nameof(SelectedCat));
            OnPropertyChanged(nameof(IsCatSelected));
        }

        private ObservableCollection<IEntity> _cats;

        private Cat _selectedCat;

        private readonly IApiClient _apiClient;
    }
}