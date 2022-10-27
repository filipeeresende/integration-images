using BonifiQ.Domain.Dto;
using BonifiQ.Domain.Entities;

namespace BonifiQ.Domain.Utils
{
    public static class PhotoUtils
    {
        public static void UpdateExistingPhoto(PhotoApiResponse updatedPhoto, Photo photo)
        {
            photo.ThumbnailUrl = updatedPhoto.ThumbnailUrl;
            photo.AlbumId = updatedPhoto.AlbumId;
            photo.DateIncluded = DateTime.Now;
            photo.Url = updatedPhoto.Url;
            photo.Title = updatedPhoto.Title;
        }

        public static Photo AddNewPhoto(PhotoApiResponse updatedPhoto)
        {
            return new Photo
            {
                Id = updatedPhoto.Id,
                AlbumId = updatedPhoto.AlbumId,
                Title = updatedPhoto.Title,
                Url = updatedPhoto.Url,
                ThumbnailUrl = updatedPhoto.ThumbnailUrl,
                DateIncluded = DateTime.Now
            };
        }
        public static PhotoResponse MapReturn(Photo photo)
        {
            return new PhotoResponse
            {
                Id = photo.Id,
                AlbumId = photo.AlbumId,
                Title = photo.Title,
                Url = photo.Url,
                ThumbnailUrl = photo.ThumbnailUrl,

            };
        }

        public static IList<PhotoResponse> MapReturnList(IList<Photo> photos)
        {
            var response = new List<PhotoResponse>();

            foreach (Photo photo in photos)
            {
                var newPhotoResponse = new PhotoResponse
                {
                    Id = photo.Id,
                    AlbumId = photo.AlbumId,
                    Title = photo.Title,
                    Url = photo.Url,
                    ThumbnailUrl = photo.ThumbnailUrl,

                };

                response.Add(newPhotoResponse);
            }

            return response;
        }
    }
}
