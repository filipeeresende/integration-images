using BonifiQ.Domain.Dto;
using BonifiQ.Domain.Entities;
using BonifiQ.Domain.Interfaces.CrossTalk;
using BonifiQ.Domain.Interfaces.Repositories;
using BonifiQ.Domain.Interfaces.services;
using BonifiQ.Domain.Services;
using BonifiQ.Domain.Settings.ErrorHandler.ErrorStatusCodes;
using BonifQ.Tests.Repositories;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BonifQ.Tests
{
    public class AlbumServiceTests
    {
        private readonly IAlbumService _albumService;
        private readonly Mock<IPhotosRepository> _photosRepository;  
        private readonly Mock<IImageRequests> _httpRequest;  

        public AlbumServiceTests()
        {
            _photosRepository = new Mock<IPhotosRepository>();
            _httpRequest = new Mock<IImageRequests>();
            _albumService = new AlbumService(_photosRepository.Object, _httpRequest.Object);
        }

        [Fact]
        public async Task GetAlbumPhotosByAlbumId_WhenPhotosIsUpdated_Success()
        {
            IList<Photo> albumPhotosMock = PhotosRepositoryMock.ListAlbumPhotosObjectMock();

            _photosRepository.Setup(x => x.GetAlbumPhotosByAlbumIdAsync(It.IsAny<int>()))
                .ReturnsAsync(albumPhotosMock); 

            IList<PhotoResponse> response = await _albumService
                .GetAlbumPhotosByAlbumId(It.IsAny<int>());

            Assert.Equal(albumPhotosMock.Count, response.Count);
        }

        [Fact]
        public async Task GetAlbumPhotosByAlbumId_WhenPhotoIsNotUpdated_Success()
        {
            IList<Photo> albumPhotosMock = PhotosRepositoryMock
                .ListAlbumNotUpdatedPhotosObjectMock();

            _photosRepository.Setup(x => x.GetAlbumPhotosByAlbumIdAsync(It.IsAny<int>()))
                .ReturnsAsync(albumPhotosMock);

            PhotoApiResponse httpRequestMock = PhotosRepositoryMock.GetPhotoResponseObjectMock();

            _httpRequest.Setup(x => x.GetPhotoById(It.IsAny<int>()))
                .ReturnsAsync(httpRequestMock);

            IList<PhotoResponse> response = await _albumService
                .GetAlbumPhotosByAlbumId(It.IsAny<int>());

            Assert.Equal(albumPhotosMock.Count, response.Count);
        }

        [Fact]
        public async Task GetAlbumPhotosByAlbumId_WhenPhotoNotFound_Error()
        {
            _photosRepository.Setup(x => x.GetAlbumPhotosByAlbumIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<Photo>());

            _httpRequest.Setup(x => x.GetAllPhotosByAlbumId(It.IsAny<int>()))
                .ReturnsAsync(new List<PhotoApiResponse>());

            Task act() => _albumService.GetAlbumPhotosByAlbumId(It.IsAny<int>());

            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async Task GetAlbumPhotosByAlbumId_WhenPhotoNotFoundAndApiReturnsOk_Success()
        {
            IList<PhotoApiResponse> httpResponseMock = PhotosRepositoryMock
                .ListPhotoResponseObjectMock();

            _photosRepository.Setup(x => x.GetAlbumPhotosByAlbumIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<Photo>());

            _httpRequest.Setup(x => x.GetAllPhotosByAlbumId(It.IsAny<int>()))
                .ReturnsAsync(httpResponseMock); 

            IList<PhotoResponse> response = await _albumService
                .GetAlbumPhotosByAlbumId(It.IsAny<int>());

            _photosRepository.Verify(x => x.InsertPhotoAsync(It.IsAny<Photo>()), Times.Exactly(2));
        }
    }
}