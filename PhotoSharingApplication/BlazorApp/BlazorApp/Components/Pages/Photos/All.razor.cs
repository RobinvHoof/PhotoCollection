using BlazorApp.Core.Interfaces;
using BlazorApp.Core.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.Pages.Photos
{
    public partial class All : ComponentBase
    {
        [Inject] public IPhotoRepository _photoRepository { get; set;}

        public List<Photo>? photos;

        protected override async Task OnInitializedAsync()
        {
            photos = (await _photoRepository.GetAllAsync()).ToList();
        }
    }
}
