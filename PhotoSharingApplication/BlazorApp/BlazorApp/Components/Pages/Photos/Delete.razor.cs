using System.Runtime.InteropServices;
using BlazorApp.Core.Interfaces;
using BlazorApp.Core.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.Pages.Photos
{
    public partial class Delete: ComponentBase
    {
        [Inject] public IPhotoRepository _PhotoRepository { get; set; }
        [Inject] public NavigationManager _NavigationManager { get; set; }
        [Parameter] public int Id { get; set; }

        Photo? photo;

        protected override async Task OnInitializedAsync()
        {
            photo = await _PhotoRepository.GetPhotoByIdAsync(Id);
        }

        async Task DeletePhoto()
        {
            if (photo is not null)
            {
                await _PhotoRepository.RemovePhotoAsync(photo.Id);
                _NavigationManager.NavigateTo("/");
            }
        }
    }
}
