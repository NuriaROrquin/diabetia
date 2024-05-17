﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Domain.Services
{
    public interface IApiAmazonService
    {
        Task<OcrRequest> GetChFromDocument(ImageTextract imageTextract);
        public Task<TextractResult> GetTextFromDocument(ImageTextract imageTextract);
    }
}
