using BonifiQ.Domain.Constants;
using BonifiQ.Domain.Dto;
using BonifiQ.Domain.Interfaces.CrossTalk;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace BonifiQ.Infrastructure.CrossTalk
{
    public class ImageRequests : IImageRequests
    {
        private readonly IConfiguration _configuration;
        private readonly string _endpoint;

        public ImageRequests(IConfiguration configuration)
        {
            _configuration = configuration;
            _endpoint = _configuration.GetValue<string>(AppSettingsConstants.EndPointUrl);
        }

        public async Task<PhotoApiResponse> GetPhotoById(int id)
        {
            PhotoApiResponse photo = null;

            using (var cliente = new RestClient(_endpoint)) 
            {
                var request = new RestRequest($"{HttpRequestsConstants.Photos}/{id}", Method.Get);
                var response = await cliente.ExecuteAsync(request);

                if (response.IsSuccessStatusCode)
                    photo = JsonConvert.DeserializeObject<PhotoApiResponse>(response.Content);
            }

            return photo;
        }

        public async Task<IList<PhotoApiResponse>> GetAllPhotosByAlbumId(int id)
        {
            List<PhotoApiResponse> photo = null;

            using (var cliente = new RestClient(_endpoint))
            {
                var request = new RestRequest($"{HttpRequestsConstants.albums}/{id}/{HttpRequestsConstants.Photos}", Method.Get);
                var response = await cliente.ExecuteAsync(request);

                if (response.IsSuccessStatusCode)
                    photo = JsonConvert.DeserializeObject<List<PhotoApiResponse>>(response.Content);
            }
            return photo;
        }
    }
}
