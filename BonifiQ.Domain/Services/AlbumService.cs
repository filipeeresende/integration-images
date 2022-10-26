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
        private readonly IPhotosRepository _imageRepository;
        private readonly IImageRequests _httpRequest;
        public AlbumService(IPhotosRepository imageRepository, IImageRequests httpRequest)
        {
            _httpRequest = httpRequest;
            _imageRepository = imageRepository;
        }

        public async Task<IList<PhotoResponse>> GetAlbumPhotosByAlbumId(int id)
        {
            var photoAlbum = new List<PhotoResponse>(); 

            IList<Photo> photos = await _imageRepository.GetAlbumPhotosByAlbumIdAsync(id);

            foreach (Photo photo in photos)
            {
                if (DateTime.Now.Subtract(photo.DateIncluded.Value).TotalMinutes > 10)
                {
                    PhotoApiResponse updatedPhoto = await _httpRequest.GetPhotoById(photo.Id);

                    PhotoUtils.UpdateExistingPhoto(updatedPhoto, photo);

                    await _imageRepository.CommitChangesAsync();

                    photoAlbum.Add(PhotoUtils.MapReturn(photo));
                }
            }

            if (!photos.Any())
            {
                List<PhotoApiResponse> updatedPhotos = await _httpRequest.GetAllPhotosByAlbumId(id);

                if (updatedPhotos.Count == 0)
                    throw new NotFoundException("Este album não foi encontrado");

                foreach (PhotoApiResponse photo in updatedPhotos)
                {
                    Photo newPhoto = PhotoUtils.AddNewPhoto(photo);

                    await _imageRepository.InsertPhotoAsync(newPhoto);

                    photoAlbum.Add(PhotoUtils.MapReturn(newPhoto));
                }

                await _imageRepository.CommitChangesAsync();
            }
            return photoAlbum;
        }
    }
}
