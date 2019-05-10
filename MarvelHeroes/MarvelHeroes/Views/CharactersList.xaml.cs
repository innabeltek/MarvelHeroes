using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MarvelHeroes.Models;

namespace MarvelHeroes.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CharactersList : ContentPage
	{
		public CharactersList ()
		{
			InitializeComponent ();
           
		}

        private async void OnItemSelected(Object sender, ItemTappedEventArgs e)
        {
            var selectedCharacter = e.Item as Character;
            await Navigation.PushAsync(new CharacterItemDetails(selectedCharacter));
        }
	}
}