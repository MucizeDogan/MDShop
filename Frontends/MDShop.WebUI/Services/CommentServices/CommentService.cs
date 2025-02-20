using MDShop.DtoLayer.CommentDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDShop.WebUI.Services.CommentServices {
    public class CommentService : ICommentService {
        private readonly HttpClient _httpClient;
        public CommentService(HttpClient httpClient) {
            _httpClient = httpClient;
        }
        public async Task CreateCommentAsync(CreateCommentDto createCommentDto) {
            await _httpClient.PostAsJsonAsync<CreateCommentDto>("comments", createCommentDto);
        }
        public async Task DeleteCommentAsync(string id) {
            await _httpClient.DeleteAsync("comments?id=" + id);
        }
        public async Task<UpdateCommentDto> GetByIdCommentAsync(string id) {
            var responseMessage = await _httpClient.GetAsync("comments/" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateCommentDto>();
            return values;
        }
        public async Task<List<ResultCommentDto>> GetAllCommentAsync(bool isAdmin = true) {

            var responseMessage = await _httpClient.GetAsync("comments");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
            return values;

        }
        public async Task UpdateCommentAsync(UpdateCommentDto updateCommentDto) {
            await _httpClient.PutAsJsonAsync<UpdateCommentDto>("comments", updateCommentDto);
        }

        public async Task StatusChangeToTrue(string id) {
            await _httpClient.GetAsync($"comments/StatusChangeToTrue/{id}");
        }

        public async Task StatusChangeToFalse(string id) {
            await _httpClient.GetAsync($"comments/StatusChangeToFalse/{id}");
        }

        public async Task<List<ResultCommentDto>> CommentListByProductId(string id, [FromQuery] bool isAdmin = false) {
            string url = $"comments/CommentListByProductId/{id}?isadmin={isAdmin.ToString().ToLower()}";
            var responseMessage = await _httpClient.GetAsync(url);

            if (!responseMessage.IsSuccessStatusCode) {
                throw new HttpRequestException($"API isteği başarısız! Status Code: {responseMessage.StatusCode}");
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData) ?? new List<ResultCommentDto>();

            // Eğer admin değilse, verileri filtrele
            return isAdmin ? values : values.Where(x => x.Status).ToList();
        }
    }
}
