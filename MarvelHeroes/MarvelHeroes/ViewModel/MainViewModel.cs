using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Extended;
using MarvelHeroes.Models;
using Xamarin.Forms;

namespace MarvelHeroes
{
    class MainViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;
        private const int limit = 3;
        private  int offset = 0;
        private const int MaxCharacters = 1491;
        readonly MarvelDataLoader _data = new MarvelDataLoader();

        public InfiniteScrollCollection<Character> CharactersList { get; }
       
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public  MainViewModel()
        {
            CharactersList = new InfiniteScrollCollection<Character>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;
                    offset = CharactersList.Count;
                    var items = await _data.GetMarvelCharactersAsync(offset, limit);
                    IsBusy = false;
                    return items;
                },
               OnCanLoadMore = () =>
                {
                    return CharactersList.Count < MaxCharacters;
                }
            };
           
             DownloadDataAsync();
        }

        private async Task DownloadDataAsync()
        {
            var items = await _data.GetMarvelCharactersAsync(0,limit);
            CharactersList.AddRange(items);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}