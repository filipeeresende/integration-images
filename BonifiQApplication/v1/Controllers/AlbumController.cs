using BonifiQ.Application.v1.Controller.Base;
using BonifiQ.Domain.Interfaces.services;
using Microsoft.AspNetCore.Mvc;

namespace BonifiQ.Application.v1.Controller
{
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/album/")]
    public class ImageController : MainController
    {
        private readonly IAlbumService _albumService;

        public ImageController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbumPhotosByAlbumId(int id)
        {
            return Ok(await _albumService.GetAlbumPhotosByAlbumId(id));
        }
    }
}
