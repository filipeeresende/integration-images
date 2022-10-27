using BonifiQ.Domain.Dto;
using BonifiQ.Domain.Entities;
using System;
using System.Collections.Generic;

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
                DateIncluded = DateTime.Now,
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

        public static PhotoApiResponse GetPhotoResponseObjectMock()
        {
            return new PhotoApiResponse
            {
                Id = 1,
                AlbumId = 2,
                ThumbnailUrl = "APIThumbnailUrlTeste",
                Title = "APITítuloTeste",
                Url = "APIUrlTeste",
            };
        }

        public static IList<PhotoApiResponse> ListPhotoResponseObjectMock()
        {
            return new List<PhotoApiResponse>
            {
                new PhotoApiResponse
                {
                    Id = 2,
                    AlbumId = 2,
                    ThumbnailUrl = "APIThumbnailUrlTeste",
                    Title = "APITítuloTeste",
                    Url = "APIUrlTeste",
                },
                new PhotoApiResponse
                {
                    Id = 2,
                    AlbumId = 2,
                    ThumbnailUrl = "APIThumbnailUrlTeste",
                    Title = "APITítuloTeste",
                    Url = "APIUrlTeste",
                }
            };
        }

        public static IList<Photo> ListAlbumPhotosObjectMock()
        {
            return new List<Photo>
            {
                new  Photo
                {
                    Id = 1,
                    AlbumId = 1,
                    DateIncluded = DateTime.Now,
                    ThumbnailUrl = "ThumbnailUrlTeste",
                    Title = "TítuloTeste",
                    Url = "UrlTeste"
                },
                new  Photo
                {
                    Id = 2,
                    AlbumId = 1,
                    DateIncluded = DateTime.Now,
                    ThumbnailUrl = "ThumbnailUrlTeste",
                    Title = "TítuloTeste",
                    Url = "UrlTeste"
                }
            };
        }

        public static IList<Photo> ListAlbumNotUpdatedPhotosObjectMock()
        {
            return new List<Photo>
            {
                new  Photo
                {
                    Id = 1,
                    AlbumId = 1,
                    DateIncluded = new DateTime(2022, 10, 23),
                    ThumbnailUrl = "ThumbnailUrlTeste",
                    Title = "TítuloTeste",
                    Url = "UrlTeste"
                },
                new  Photo
                {
                    Id = 2,
                    AlbumId = 1,
                    DateIncluded = new DateTime(2022, 10, 23),
                    ThumbnailUrl = "ThumbnailUrlTeste",
                    Title = "TítuloTeste",
                    Url = "UrlTeste"
                }
            };
        }
    }
}
