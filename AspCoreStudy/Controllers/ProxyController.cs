using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreStudy.Controllers
{
    [ApiController]
    [Route("proxy")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ProxyController(HttpClient httpClient) : ControllerBase
    {
        private readonly HttpClient _httpClient = httpClient;

        [HttpPost("login")]
        public async Task<IActionResult> ProxyLogin(object requestBody)
        {
            string apiUrl = "https://localhost:7018/api/accountapi/login";

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            using var response = await _httpClient.PostAsync(apiUrl, jsonContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            var statusCode = (int)response.StatusCode;

            return StatusCode(statusCode, JsonSerializer.Deserialize<object>(responseContent));
        }
    }
}