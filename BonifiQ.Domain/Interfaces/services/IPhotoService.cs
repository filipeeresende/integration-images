using BonifiQ.Domain.Entities;

namespace BonifiQ.Domain.Interfaces.services
{
    public interface IPhotoService
    {
        Task<Photo> GetPhotoById(int id);
    }
}
