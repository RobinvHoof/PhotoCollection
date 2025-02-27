using BlazorApp.Core.Interfaces;
using BlazorApp.Core.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.Pages.Photos
{
    public partial class Details: ComponentBase
    {
        [Inject] public IPhotoRepository _PhotoRepository { get; set; }

        [Parameter]
        public int Id { get; set; }

        public Photo? photo { get; set; }

        protected override async Task OnInitializedAsync()
        {
            photo = await _PhotoRepository.GetPhotoByIdAsync(Id);
        }
    }
}
