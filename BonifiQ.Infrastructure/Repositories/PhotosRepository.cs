using BonifiQ.Domain.Dto;
using BonifiQ.Domain.Entities;
using BonifiQ.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BonifiQ.Infrastructure.Repositories
{
    public class PhotosRepository : IPhotosRepository
    {
        private readonly BonifiQContext _context;
        public PhotosRepository(BonifiQContext context)
        {
            _context = context;
        }

        public async Task<List<Photo>> GetAlbumPhotosByAlbumIdAsync(int id)
        {
            return await (from P in _context.Photos
                          where P.AlbumId == id
                          select new Photo
                          {
                              Id = P.Id,
                              AlbumId = P.AlbumId,
                              Title = P.Title,
                              Url = P.Url,
                              ThumbnailUrl = P.ThumbnailUrl,
                              DateIncluded = P.DateIncluded
                          }).ToListAsync();
        }
        public async Task<Photo> GetPhotoByIdAsync(int id)
        {
            return await _context.Photos
               .Where(x => x.Id == id)
               .Select(x => new Photo
               {
                   Id = x.Id,
                   AlbumId = x.AlbumId,
                   Title = x.Title,
                   Url = x.Url,
                   ThumbnailUrl = x.ThumbnailUrl,
                   DateIncluded = x.DateIncluded
               }).FirstOrDefaultAsync();

        }

        public async Task CommitChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task InsertPhotoAsync(Photo newPhoto)
        {
            await _context.AddAsync(newPhoto);
        }
    }
}
