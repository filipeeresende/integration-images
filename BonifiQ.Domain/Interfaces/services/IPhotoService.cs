using BonifiQ.Domain.Dto;
using BonifiQ.Domain.Entities;

namespace BonifiQ.Domain.Interfaces.services
{
    public interface IPhotoService
    {
        Task<PhotoResponse> GetPhotoById(int id);
    }
}
