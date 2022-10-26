using BonifiQ.Domain.Dto;

namespace BonifiQ.Domain.Interfaces.CrossTalk
{
    public interface IImageRequests
    {
        Task<PhotoApiResponse> GetPhotoById(int id);
        Task<List<PhotoApiResponse>> GetAllPhotosByAlbumId(int id);
    }
}
