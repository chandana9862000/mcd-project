﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S3bucketDemo.Models;
using S3bucketDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3bucketDemo.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AWSS3FileController : Controller
    {
        private readonly IAWSS3FileService _AWSS3FileService;
        public AWSS3FileController(IAWSS3FileService AWSS3FileService)
        {
            this._AWSS3FileService = AWSS3FileService;
        }
        [Route("uploadFile")]
        [HttpPost]
        public async Task<IActionResult> UploadFileAsync(UploadFileName uploadFileName)
        {
            var result = await _AWSS3FileService.UploadFile(uploadFileName);
            return Ok(new { isSucess = result });
        }
        [Route("filesList")]
        [HttpGet]
        public async Task<IActionResult> FilesListAsync()
        {
            var result = await _AWSS3FileService.FilesList();
            return Ok(result);
        }
        [Route("getFile/{fileName}")]
        [HttpGet]
        public async Task<IActionResult> GetFile(string fileName)
        {
            try
            {
                var result = await _AWSS3FileService.GetFile(fileName);
                return File(result, "image/png");
            }
            catch
            {
                return Ok("NoFile");
            }

        }
        [Route("updateFile")]
        [HttpPut]
        public async Task<IActionResult> UpdateFile(UploadFileName uploadFileName, string fileName)
        {
            var result = await _AWSS3FileService.UpdateFile(uploadFileName, fileName);
            return Ok(new { isSucess = result });
        }
        [Route("deleteFile/{fileName}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            var result = await _AWSS3FileService.DeleteFile(fileName);
            return Ok(new { isSucess = result });
        }
    }
}
    

