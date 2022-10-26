using BonifiQ.Domain.Dto;
using BonifiQ.Domain.Entities;
using BonifiQ.Domain.Interfaces.CrossTalk;
using BonifiQ.Domain.Interfaces.Repositories;
using BonifiQ.Domain.Interfaces.services;
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

        public async Task<IList<Photo>> GetAlbumPhotosByAlbumId(int id)
        {
            var photoAlbum = new List<Photo>(); //vai virar DTO

            IList<Photo> photos = await _imageRepository.GetAlbumPhotosByAlbumIdAsync(id);

            foreach (Photo photo in photos)
            {
                if (DateTime.Now.Subtract(photo.DateIncluded.Value).TotalMinutes > 10)
                {
                    PhotoResponse updatedPhoto = await _httpRequest.GetPhotoById(photo.Id);

                    PhotoUtils.UpdateExistingPhoto(updatedPhoto, photo);

                    await _imageRepository.CommitChangesAsync();

                    photoAlbum.Add(photo);
                }
            }

            if (!photos.Any())
            {
                List<PhotoResponse> updatedPhotos = await _httpRequest.GetAllPhotosByAlbumId(id);

                foreach (PhotoResponse photo in updatedPhotos)
                {
                    Photo newPhoto = PhotoUtils.AddNewPhoto(photo);

                    await _imageRepository.InsertPhotoAsync(newPhoto);

                    photoAlbum.Add(newPhoto);
                }

                await _imageRepository.CommitChangesAsync();
            }

            return photoAlbum;
        }
    }
}
