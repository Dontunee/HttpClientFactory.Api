using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HttpClientFactory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MyGitHubClient _typedClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory httpClientFactory,
                                        MyGitHubClient typedClient)
        {
            _logger = logger ?? throw new ArgumentNullException();
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException();
            _typedClient = typedClient ?? throw new ArgumentNullException();
        }

        [HttpGet]
        public async Task<IActionResult> GetGoogleWeatherForecasts()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var result = await client.GetStringAsync("http://www.google.com");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in GetGoogleWeatherForecasts: {ex.ToString()} ");
                return  Forbid();
            }
        }


        //Add Sample Named Client 
        [HttpPost]
        public async Task<IActionResult> GetAddressUsingNamedClient()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("GitHubClient");
                var getResult = await client.GetStringAsync("/");
                return Ok(getResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in PostDifferentAddress: {ex.ToString()} ");
                return Forbid();
            }
        }

        //Add sample typed client 
        [HttpGet]
        public async Task<IActionResult> GetAddressUsingTypedClient()
        {
            try
            {
                var result = await _typedClient.Client.GetStringAsync("/");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in GetAddressUsingTypedClient: {ex.ToString()} ");
                return Forbid();
            }
        }
    }
}
