using BonifiQ.Domain.Entities;

namespace BonifiQ.Domain.Interfaces.services
{
    public interface IAlbumService
    {
        Task<IList<Photo>> GetAlbumPhotosByAlbumId(int id);
    }
}
