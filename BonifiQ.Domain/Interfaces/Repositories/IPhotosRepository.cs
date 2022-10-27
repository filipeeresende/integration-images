using BonifiQ.Domain.Dto;
using BonifiQ.Domain.Entities;

namespace BonifiQ.Domain.Interfaces.Repositories
{
    public  interface IPhotosRepository
    {
        Task<Photo> GetPhotoByIdAsync(int id);
        Task CommitChangesAsync();
        Task InsertPhotoAsync(Photo newPhoto);
        Task<IList<Photo>> GetAlbumPhotosByAlbumIdAsync(int id);
    }
}
