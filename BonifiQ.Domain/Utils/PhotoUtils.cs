using BonifiQ.Domain.Dto;
using BonifiQ.Domain.Entities;

namespace BonifiQ.Domain.Utils
{
    public static class PhotoUtils
    {
        public static void UpdateExistingPhoto(PhotoResponse updatedPhoto, Photo photo)
        {
            photo.ThumbnailUrl = updatedPhoto.ThumbnailUrl;
            photo.AlbumId = updatedPhoto.AlbumId;
            photo.DateIncluded = DateTime.Now;
            photo.Url = updatedPhoto.Url;
            photo.Title = updatedPhoto.Title;
        }

        public static Photo AddNewPhoto(PhotoResponse updatedPhoto)
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
    }
}
