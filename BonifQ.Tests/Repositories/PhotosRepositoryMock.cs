using BonifiQ.Domain.Dto;
using BonifiQ.Domain.Entities;
using System;

namespace BonifQ.Tests.Repositories
{
    public static class PhotosRepositoryMock
    {
        public static Photo GetUpdatePhotoObjectMock()
        {
            return new Photo
            {
                Id = 1,
                AlbumId = 1,
                DateIncluded = new DateTime(2022, 10, 26),
                ThumbnailUrl = "ThumbnailUrlTeste",
                Title = "TítuloTeste",
                Url = "UrlTeste"
            };
        }

        public static Photo GetNotUpdatePhotoObjectMock()
        {
            return new Photo
            {
                Id = 1,
                AlbumId = 1,
                DateIncluded = new DateTime(2022, 10, 23),
                ThumbnailUrl = "ThumbnailUrlTeste",
                Title = "TítuloTeste",
                Url = "UrlTeste"
            };
        }

        public static PhotoResponse GetPhotoResponseObjectMock()
        {
            return new PhotoResponse
            {
                Id = 1,
                AlbumId = 2,
                ThumbnailUrl = "APIThumbnailUrlTeste",
                Title = "APITítuloTeste",
                Url = "APIUrlTeste",
            };
        }
    }
}
