﻿using System;
using System.Threading.Tasks;

namespace QMS.Storage.Interfaces
{
    /// <summary>
    /// Interface that defines Read operations on files
    /// </summary>
    public interface IReadFile
    {
        Task<byte[]> ReadFile(string cmsType, string id, string fieldName, string lang);
    }

   
}