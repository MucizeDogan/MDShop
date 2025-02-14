// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MDShop.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] {
           new ApiResource("ResourceCatalog") {Scopes={"CatalogFullPermission","CatalogReadPermission"}},
           new ApiResource("ResourceDiscount"){Scopes={"DiscountFullPermission"} },
           new ApiResource("ResourceOrder"){Scopes={"OrderFullPermisson"}},
           new ApiResource("ResourceCargo"){Scopes={"CargoFullPermission"} },
           new ApiResource("ResourceBasket"){Scopes={"BasketFullPermission"} },
           new ApiResource("ResourceComment"){Scopes={"CommentFullPermission"} },
           new ApiResource("ResourcePayment"){Scopes={ "PaymentFullPermission" } },
           new ApiResource("ResourceImage"){Scopes={ "ImageFullPermission" } },
           new ApiResource("ResourceOcelot"){Scopes={"OcelotFullPermission"} },
           //new ApiResource("ResourceMessage"){Scopes={"MessageFullPermission"} },
           new ApiResource(IdentityServerConstants.LocalApi.ScopeName)

        };
        // ApiResources ismindeki property imde yapacağım şey şu ApiResurces çağrıldığı zaman ben herbir mikroservisim için o mikroservise erişim sağlanılacak olan bir key vereceğim
        // ResourceCatalog ismindeki key e sahip olan mikroservis kullanıcısı CatalogFullPermission işlemini gerçekleştirebilir.

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[] {
            new IdentityResources.OpenId(), //Identity Resources e sahip olan kişi herkese açık olan id ye erişim sağlayacak.
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };
        //IdentityResources ifadesiyle token ını aldığım kullanıcının o token içerisinde hangi ilgilere erişim sağlayacağını bildirmiş oldum.

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[] {
            new ApiScope("CatalogFullPermission","Full authority for catalog operations"), //Token ı alan kişi CatalogFullPermission a sahip ise sahip olan kişinin yapapileceği işlemleri belirtiyoruz.
            new ApiScope("CatalogReadPermission","Reading authority for catalog operations"), // CatalogReadPermission buna sahipse okuma yetkisi var
            
            new ApiScope("DiscountFullPermission","Full authority for discount operations"),
            new ApiScope("OrderFullPermisson","Full authority for order operations"),
            new ApiScope("CargoFullPermission","Full authority for cargo operations"),
            new ApiScope("BasketFullPermission","Full authority for basket operations"),
            new ApiScope("CommentFullPermission","Full authority for comment operations"),
            new ApiScope("PaymentFullPermission","Full authority for payment operations"),
            new ApiScope("ImageFullPermission","Full authority for image operations"),
            new ApiScope("OcelotFullPermission","Full authority for ocelot operations"),
            //new ApiScope("MessageFullPermission","Full authority for message operations"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<Client> Clients => new Client[] {
            //1. Client Visitor (Visitor un sahip olacağı izinleri oluşturacağız.)
            new Client {
                ClientId = "MDShopVisitorId",
                ClientName = "MDShop Visitor User",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("mdshopsecret".Sha256())},
                AllowedScopes={"CatalogReadPermission", "CatalogFullPermission", "OcelotFullPermission", "CommentFullPermission", "ImageFullPermission"}, // Visitor hangi yetkilere sahip olsun.

            },

            //2. Client Manager
            new Client {
                ClientId = "MDShopManagerId",
                ClientName = "MDShop Manager User",
                AllowedGrantTypes =GrantTypes.ResourceOwnerPassword, //GrantTypes.ResourceOwnerPassword,
                ClientSecrets = {new Secret("mdshopsecret".Sha256())},
                AllowedScopes = { "CatalogReadPermission", "CatalogFullPermission", "BasketFullPermission", "OcelotFullPermission", "CommentFullPermission", "PaymentFullPermission", "ImageFullPermission" }
            },

            // Admin
            new Client {
                ClientId = "MDShopAdminId",
                ClientName = "MDShop Admin User",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, //GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("mdshopsecret".Sha256())},
                AllowedScopes = { "CatalogReadPermission", "CatalogFullPermission", "DiscountFullPermission", "OrderFullPermisson", "CargoFullPermission", "OcelotFullPermission", 
                    "BasketFullPermission", "CommentFullPermission", "PaymentFullPermission", "ImageFullPermission",

                IdentityServerConstants.LocalApi.ScopeName,
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
                },
                AccessTokenLifetime = 600

            }
        };
    }
}