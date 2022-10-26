using BonifiQ.Application.v1.Controller.Base;
using BonifiQ.Domain.Interfaces.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhotoById(int id)
        {
            return Ok(await _photoService.GetPhotoById(id));
        }
    }
}
