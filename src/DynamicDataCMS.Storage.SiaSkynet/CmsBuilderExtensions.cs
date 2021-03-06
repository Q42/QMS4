﻿using Microsoft.Extensions.DependencyInjection;
using System;
using DynamicDataCMS.Storage.Interfaces;
using DynamicDataCMS.Core;
using DynamicDataCMS.Core.Models;
using DynamicDataCMS.Storage.SiaSkynet.Models;

namespace DynamicDataCMS.Storage.SiaSkynet
{
    /// <summary>
    /// Configure all services in the DynamicDataCMS.Storage.SiaSkynet package
    /// </summary>
    public static class CmsBuilderExtensions
    {
        public static CmsBuilder ConfigureSiaSkynet(this CmsBuilder builder)
        {
            return builder.ConfigureSiaSkynet(() => new StorageConfiguration { WriteFiles = true, ReadFiles = true });
        }

        public static CmsBuilder ConfigureSiaSkynet(this CmsBuilder builder, Func<StorageConfiguration> storageConfigFunc)
        {
            var services = builder.Services;
            var Configuration = builder.Configuration;

            services.Configure<SkynetConfig>(Configuration.GetSection(nameof(SkynetConfig)));

            StorageConfiguration storageConfig = storageConfigFunc();

            if (storageConfig.ReadFiles)
                services.AddTransient<IReadFile, CmsFileStorageService>();
            if (storageConfig.WriteFiles)
                services.AddTransient<IWriteFile, CmsFileStorageService>();
            if (storageConfig.ReadCmsItems)
                services.AddSingleton<IReadCmsItem, CmsItemStorageService>();
            if (storageConfig.WriteCmsItems)
                services.AddSingleton<IWriteCmsItem, CmsItemStorageService>();

            return builder;
        }
    }
}
