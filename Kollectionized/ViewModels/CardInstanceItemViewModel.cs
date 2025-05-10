using Kollectionized.Models;
using Kollectionized.Services;

namespace Kollectionized.ViewModels;

public class CardInstanceItemViewModel : CardInstanceDetailsViewModel
{
    public CardInstanceItemViewModel(PokemonCard card, CardInstance instance, CardImageService imageService)
        : base(card, instance, imageService) { }
}