using MarvelHeroes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarvelHeroes.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CharacterItemDetails : ContentPage
	{
		public CharacterItemDetails (Character selectedCharacter)
		{
			InitializeComponent ();

            heroImage.Source = selectedCharacter.thumbnail.large;
            heroName.Text = selectedCharacter.name;
            heroDescription.Text = "Description: " + selectedCharacter.description;
            var comics = selectedCharacter.comics.items.Select(e => e.name);
            var series = selectedCharacter.series.items.Select(e => e.name );
            var stories = selectedCharacter.stories.items.Select(e => e.name);
            var events = selectedCharacter.events.items.Select(e => e.name);
            heroComics.Text = "Comics: " + string.Join(", ", comics);
            heroSeries.Text = "Series: " + string.Join(", ", series);
            heroStories.Text = "Stories: " + string.Join(", ", stories);
            heroEvents.Text = "Events: " + string.Join(", ", events);


        }
	}
}