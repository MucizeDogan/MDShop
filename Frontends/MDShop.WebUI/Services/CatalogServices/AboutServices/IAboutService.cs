﻿using MDShop.DtoLayer.CatalogDtos.AboutDtos;

namespace MDShop.WebUI.Services.CatalogServices.AboutServices {
    public interface IAboutService {
        Task<List<ResultAboutDto>> GetAllAboutAsync();
        Task CreateAboutAsync(CreateAboutDto createAboutDto);
        Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
        Task DeleteAboutAsync(string id);
        Task<UpdateAboutDto> GetByIdAboutAsync(string id);
    }
}
