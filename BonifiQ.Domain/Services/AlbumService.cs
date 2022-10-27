using BonifiQ.Domain.Dto;
using BonifiQ.Domain.Entities;
using BonifiQ.Domain.Interfaces.CrossTalk;
using BonifiQ.Domain.Interfaces.Repositories;
using BonifiQ.Domain.Interfaces.services;
using BonifiQ.Domain.Settings.ErrorHandler.ErrorStatusCodes;
using BonifiQ.Domain.Utils;

namespace BonifiQ.Domain.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IPhotosRepository _photosRepository;
        private readonly IImageRequests _httpRequest;
        public AlbumService(IPhotosRepository photosRepository, IImageRequests httpRequest)
        {
            _httpRequest = httpRequest;
            _photosRepository = photosRepository;
        }

        public async Task<IList<PhotoResponse>> GetAlbumPhotosByAlbumId(int id)
        {
            var albumPhotos = new List<Photo>();

            IList<Photo> photos = await _photosRepository.GetAlbumPhotosByAlbumIdAsync(id);

            foreach (Photo photo in photos)
            {
                if (DateTime.Now.Subtract(photo.DateIncluded.Value).TotalMinutes > 10)
                {
                    PhotoApiResponse updatedPhoto = await _httpRequest.GetPhotoById(photo.Id);

                    PhotoUtils.UpdateExistingPhoto(updatedPhoto, photo);
                }

                albumPhotos.Add(photo);
            }

            if (!photos.Any())
            {
                IList<PhotoApiResponse> updatedPhotos = await _httpRequest
                    .GetAllPhotosByAlbumId(id);

                if (!updatedPhotos.Any())
                    throw new NotFoundException("Este album não foi encontrado");

                foreach (PhotoApiResponse photo in updatedPhotos)
                {
                    Photo newPhoto = PhotoUtils.AddNewPhoto(photo);

                    await _photosRepository.InsertPhotoAsync(newPhoto);

                    albumPhotos.Add(newPhoto);
                }
            }

            await _photosRepository.CommitChangesAsync();

            return PhotoUtils.MapReturnList(albumPhotos);
        }
    }
}
