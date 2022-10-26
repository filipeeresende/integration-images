using BonifiQ.Domain.Dto;
using BonifiQ.Domain.Entities;

namespace BonifiQ.Domain.Interfaces.services
{
    public interface IAlbumService
    {
        Task<IList<PhotoResponse>> GetAlbumPhotosByAlbumId(int id);
    }
}
