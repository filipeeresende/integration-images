using BonifiQ.Application.v1.Controller.Base;
using BonifiQ.Domain.Dto;
using BonifiQ.Domain.Interfaces.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BonifiQ.Application.v1.Controller
{
    [ApiVersion("1.0")]
    [Route("/v{version:apiVersion}/photo/")]
    public class PhotoController : MainController
    {
        private readonly IPhotoService _photoService;
        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [SwaggerOperation("This method searches for photos by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(PhotoResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "This image was not found.", typeof(string))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhotoById(int id)
        {
            return Ok(await _photoService.GetPhotoById(id));
        }
    }
}
