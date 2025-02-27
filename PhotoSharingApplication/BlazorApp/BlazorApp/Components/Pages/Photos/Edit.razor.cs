using BlazorApp.Core.Interfaces;
using BlazorApp.Core.Models;
using Microsoft.AspNetCore.Components;


namespace BlazorApp.Components.Pages.Photos
{
    public partial class Edit: ComponentBase
    {
        [Inject] public IPhotoRepository _PhotoRepository { get; set; }
        [Inject] public NavigationManager _NavigationManager { get; set; }

        [SupplyParameterFromForm(FormName = "PhotoForm")]
        Photo? photo { get; set; }

        [SupplyParameterFromForm(FormName = "PhotoForm")]
        FileModel fileModel { get; set; }

        [Parameter]
        public int? Id { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            fileModel ??= new FileModel();
            if (Id != null)
            {
                photo ??= await _PhotoRepository.GetPhotoByIdAsync(Id ?? 0);
            }
            else
            {
                photo ??= new Photo();
            }
        }

        async Task Save()
        {
            if (fileModel.File is not null )
            {
                using var memoryStream = new MemoryStream();
                await fileModel.File.OpenReadStream().CopyToAsync(memoryStream);
                photo.PhotoFile = memoryStream.ToArray();
                photo.ImageMimeType = fileModel.File.ContentType;
            }

            if (photo.Id == 0)
            {
                await _PhotoRepository.AddAsync(photo);
            }
            else
            {
                await _PhotoRepository.UpdatePhotoAsync(photo);
            }
            _NavigationManager.NavigateTo(uri: "/");
        }
    }
}
