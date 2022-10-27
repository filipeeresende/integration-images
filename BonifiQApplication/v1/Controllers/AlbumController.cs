using BonifiQ.Application.v1.Controller.Base;
using BonifiQ.Domain.Dto;
using BonifiQ.Domain.Interfaces.services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation("This method searches for photos from an album by idAlbum")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(IList<PhotoResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "This Album was not found.", typeof(string))]
        [HttpGet("{id}/photos")]
        public async Task<IActionResult> GetAlbumPhotosByAlbumId(int id)
        {
            return Ok(await _albumService.GetAlbumPhotosByAlbumId(id));
        }
    }
}
