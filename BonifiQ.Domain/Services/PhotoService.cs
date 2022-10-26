using BonifiQ.Domain.Dto;
using BonifiQ.Domain.Entities;
using BonifiQ.Domain.Interfaces.CrossTalk;
using BonifiQ.Domain.Interfaces.Repositories;
using BonifiQ.Domain.Interfaces.services;
using BonifiQ.Domain.Settings.ErrorHandler.ErrorStatusCodes;
using BonifiQ.Domain.Utils;

namespace BonifiQ.Domain.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotosRepository _photosRepository;
        private readonly IImageRequests _httpRequest;
        public PhotoService(IPhotosRepository photosRepository, IImageRequests httpRequest)
        {
            _photosRepository = photosRepository;
            _httpRequest = httpRequest;
        }

        public async Task<PhotoResponse> GetPhotoById(int id)
        {
            Photo photo = await _photosRepository.GetPhotoByIdAsync(id);

            if (photo != null)
            {
                if (DateTime.Now.Subtract(photo.DateIncluded.Value).TotalHours > 24)
                {
                    PhotoApiResponse updatedPhoto = await _httpRequest.GetPhotoById(id);

                    PhotoUtils.UpdateExistingPhoto(updatedPhoto, photo);

                    await _photosRepository.CommitChangesAsync();
                }

                 return PhotoUtils.MapReturn(photo);
            }

            PhotoApiResponse apiPhoto = await _httpRequest.GetPhotoById(id);

            if (apiPhoto == null)
                throw new NotFoundException("Esta imagem não foi encontrada");

                Photo newPhoto = PhotoUtils.AddNewPhoto(apiPhoto);

                await _photosRepository.InsertPhotoAsync(newPhoto);

                await _photosRepository.CommitChangesAsync();

                return PhotoUtils.MapReturn(newPhoto);

        }
    }
}
