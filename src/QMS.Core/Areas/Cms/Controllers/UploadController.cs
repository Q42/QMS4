﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QMS.Services;
using QMS.Storage.Interfaces;

namespace QMS.Core.Controllers
{
    /// <summary>
    /// Handles uploading of files
    /// </summary>
    [Area("cms")]
    [Route("[area]/upload")]
    public class UploadController : Controller
    {
        private readonly IWriteFile writeFileService;

        public UploadController(DataProviderWrapperService dataProviderService)
        {
            this.writeFileService = dataProviderService;
        }

        [HttpPost]
        [Route("images/{cmsType}/{id}/{lang?}")]
        public async Task<IActionResult> Images([FromForm]IFormFile file, [FromRoute]string cmsType, [FromRoute]string id, [FromRoute]string lang, [FromQuery]string fieldName)
        {
            string fileName = null;
            string mimeType = null;
            byte[] bytes = null;

            if (file != null)
            {
                fileName = GetFileName(file);
                mimeType = file.ContentType.ToLowerInvariant();

                //Get file bytes
                var outputStream = new MemoryStream();
                var output = file.OpenReadStream();
                output.CopyTo(outputStream);
                bytes = outputStream.ToArray();

                if (mimeType.StartsWith("image/"))
                {
                    // Only validate files that seem to be images.
                    try
                    {
                        //Image.Load(Configuration.Default, bytes);
                        var blob = await writeFileService.WriteFile(bytes, mimeType, cmsType, id, fieldName, lang).ConfigureAwait(false);

                    }
                    catch (Exception e)
                    {
                    }
                }
            }

            return View();
        }

        private static string GetFileName(IFormFile file) => file.ContentDisposition.Split(';')
                                                       .Select(x => x.Trim())
                                                       .Where(x => x.StartsWith("filename="))
                                                       .Select(x => x.Substring(9).Trim('"'))
                                                       .First();
    }
}