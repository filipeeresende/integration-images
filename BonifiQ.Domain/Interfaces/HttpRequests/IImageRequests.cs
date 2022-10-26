using BonifiQ.Domain.Dto;

namespace BonifiQ.Domain.Interfaces.CrossTalk
{
    public interface IImageRequests
    {
        Task<PhotoResponse> GetPhotoById(int id);
        Task<List<PhotoResponse>> GetAllPhotosByAlbumId(int id);
    }
}
