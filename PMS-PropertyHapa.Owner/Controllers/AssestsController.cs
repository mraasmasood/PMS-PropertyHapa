﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PMS_PropertyHapa.Models;
using PMS_PropertyHapa.Models.DTO;
using PMS_PropertyHapa.MigrationsFiles.Data;
using PMS_PropertyHapa.Models.Roles;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PMS_PropertyHapa.Models.Entities;
using PMS_PropertyHapa.Owner.Controllers;
using PMS_PropertyHapa.Owner.Services.IServices;
using PMS_PropertyHapa.Shared.Enum;
using PMS_PropertyHapa.Shared.ImageUpload;

namespace PMS_PropertyHapa.Owner.Controllers
{
    public class AssestsController : Controller
    {

        private readonly IAuthService _authService;

        public AssestsController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        public async Task<IActionResult> AddAsset([FromBody] AssetDTO assetDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _authService.CreateAssetAsync(assetDTO);
                return Ok(new { success = true, message = "Asset added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while adding the asset." });
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateAsset([FromBody] AssetDTO assetDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _authService.UpdateAssetAsync(assetDTO);
                return Ok(new { success = true, message = "Asset added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "An error occurred while adding the asset." });
            }
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteAsset(int propertyId)
        {
            await _authService.DeleteAssetAsync(propertyId);
            return Json(new { success = true, message = "Tenant deleted successfully" });
        }


        [HttpGet]
        public async Task<IActionResult> GetAssets()
        {
            try
            {
                var asset = await _authService.GetAllAssetsAsync();
                return Ok(asset);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching assets: {ex.Message}");
            }
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddAssest()
        {
            return View();
        }
    }
}
