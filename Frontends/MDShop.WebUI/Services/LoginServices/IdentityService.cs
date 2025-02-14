using IdentityModel.Client;
using MDShop.DtoLayer.IdentityDtos.LoginDtos;
using MDShop.WebUI.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using IdentityServer4.Models;
using System.Security.Claims;

namespace MDShop.WebUI.Services.LoginServices {
    public class IdentityService : IIdentityService {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClientSettings _clientSettings;
        private readonly ServiceApiSettings _serviceApiSettings;

        public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings) {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _clientSettings = clientSettings.Value;
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public async Task<bool> GetRefreshToken() {
            var discoveryEndpoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest {
                Address = _serviceApiSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy {
                    RequireHttps = false
                }
            });//buradan 5001/connect/token a istekte bulunacağız ancak biz https yerine http yaptığımız için bununla ilgili ayar yapmamız gerekiyor

            var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            RefreshTokenRequest refreshTokenRequest = new RefreshTokenRequest() {
                ClientId = _clientSettings.MDShopManagerClient.ClientId,
                ClientSecret = _clientSettings.MDShopManagerClient.ClientSecret,
                RefreshToken = refreshToken,
                Address = discoveryEndpoint.TokenEndpoint
            };

            var token = await _httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

            var authenticationToken = new List<AuthenticationToken>()
            {
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.AccessToken,
                    Value = token.AccessToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.RefreshToken,
                    Value = token.RefreshToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.ExpiresIn,
                    Value=DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
                }
            };

            var result = await _httpContextAccessor.HttpContext.AuthenticateAsync();

            var properties = result.Properties;
            properties.StoreTokens(authenticationToken);

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Principal, properties);

            return true;
        }

        public async Task<bool> SignIn(SignInDto signInDto) {
            // Discovery Endpoint aslında bir dağıtım sağlıyor. bunun üzerinden gideğimiz adresi vs belirliyor olacağız.
            var discoveryEndpoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest {
                Address= _serviceApiSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy {
                    RequireHttps = false
                }
            });//buradan 5001/connect/token a istekte bulunacağız ancak biz https yerine http yaptığımız için bununla ilgili ayar yapmamız gerekiyor

            var passwordTokenRequest = new PasswordTokenRequest {
                ClientId = _clientSettings.MDShopManagerClient.ClientId,
                ClientSecret = _clientSettings.MDShopManagerClient.ClientSecret,
                UserName = signInDto.Username,
                Password = signInDto.Password,
                Address = discoveryEndpoint.TokenEndpoint
            };

            var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

            var userInfoRequest = new UserInfoRequest {
                Token = token.AccessToken,
                Address = discoveryEndpoint.UserInfoEndpoint
            };

            var userValues = await _httpClient.GetUserInfoAsync(userInfoRequest);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userValues.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authenticationProperties = new AuthenticationProperties();

            authenticationProperties.StoreTokens(new List<AuthenticationToken>() {
                new AuthenticationToken {
                    Name=OpenIdConnectParameterNames.AccessToken,
                    Value = token.AccessToken
                },
                new AuthenticationToken {
                    Name=OpenIdConnectParameterNames.RefreshToken,
                    Value = token.RefreshToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.ExpiresIn,
                    Value=DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
                }
            });

            authenticationProperties.IsPersistent = false;

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

            return true;
        }
    }
}
