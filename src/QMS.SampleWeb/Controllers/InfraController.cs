﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QMS.Core.Models;
using QMS.SampleWeb.Infrastructure;

namespace QMS.SampleWeb.Controllers
{
    public class InfraController : Controller
    {
        private readonly SampleDataGenerator sampleDataGenerator;

        public InfraController(SampleDataGenerator sampleDataGenerator)
        {
            this.sampleDataGenerator = sampleDataGenerator;
        }

        [Route("_internal/testdata")]
        public async Task<IActionResult> TestData(Guid cmsItemId)
        {
            await sampleDataGenerator.ClearAndGenerate();

            return new ContentResult() { Content = "New Testdata generated" };
        }
    }
}