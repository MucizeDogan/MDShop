
using MDShop.WebUI.Services.LoginServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net;
using System.Net.Http.Headers;

namespace MDShop.WebUI.Handlers {
    public class ResourceOwnerPasswordTokenHandler : DelegatingHandler {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IIdentityService _identityService;

        public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor contextAccessor, IIdentityService identityService) {
            _contextAccessor = contextAccessor;
            _identityService = identityService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            var accessToken = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var res = await base.SendAsync(request,cancellationToken);

            if (res.StatusCode==HttpStatusCode.Unauthorized){
                var tokenRes = await _identityService.GetRefreshToken();

                if (tokenRes!=null) {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    res = await base.SendAsync(request, cancellationToken);
                }
            }

            if (res.StatusCode == HttpStatusCode.Unauthorized) {
                // hata mesajı
            }

            return res;
        }
    }
}
