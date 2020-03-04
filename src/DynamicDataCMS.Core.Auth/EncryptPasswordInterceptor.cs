﻿using DynamicDataCMS.Core.Auth.Models;
using DynamicDataCMS.Core.Models;
using DynamicDataCMS.Core.Services.Extensions;
using DynamicDataCMS.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DynamicDataCMS.Core.Auth
{
    public class EncryptPasswordInterceptor : IWriteCmsItemInterceptor
    {
        public Task<T> InterceptAsync<T>(T item, string cmsType, Guid id, string? lang, string? currentUser) where T : CmsItem
        {
            //Only do this for the auth controller
            if(cmsType == CmsUser.DefaultCmsType)
            {
                var cmsUser = item.ToObject<CmsUser>();

                //Encrypt password
                cmsUser.PasswordEncrypted = BCrypt.Net.BCrypt.HashPassword(cmsUser.Password);
                cmsUser.Password = string.Empty; //Do not store original password

                item = cmsUser.ToObject<T>();

                return Task.FromResult(item);
            }

            return Task.FromResult(item);
        }
    }
}