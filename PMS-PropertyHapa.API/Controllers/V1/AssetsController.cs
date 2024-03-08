﻿using MagicVilla_VillaAPI.Repository.IRepostiory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PMS_PropertyHapa.Models;
using PMS_PropertyHapa.Models.DTO;
using System.Net;
using PMS_PropertyHapa.Shared.Email;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Web;
using System.Security.Claims;
using System.Net.Http.Headers;
using PMS_PropertyHapa.Models.Roles;
using PMS_PropertyHapa.Models.Entities;

namespace PMS_PropertyHapa.API.Controllers.V1
{
    [Route("api/v1/AssetsAuth")]
    [ApiController]
    //  [ApiVersionNeutral]
    public class AssetsController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        protected APIResponse _response;

        public AssetsController(IUserRepository userRepo, UserManager<ApplicationUser> userManager)
        {
            _userRepo = userRepo;
            _response = new();
            _userManager = userManager;
        }

        [HttpGet("Error")]
        public async Task<IActionResult> Error()
        {
            throw new FileNotFoundException();
        }

        [HttpGet("ImageError")]
        public async Task<IActionResult> ImageError()
        {
            throw new BadImageFormatException("Fake Image Exception");
        }


        #region PropertyTypeCrud 



        [HttpGet("Assets")]
        public async Task<ActionResult<AssetDTO>> GetAllAssets()
        {
            try
            {
                var assets = await _userRepo.GetAllAssetsAsync();

                if (assets != null)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Result = assets;
                    return Ok(_response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("No asset found with this id.");
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        //[HttpGet("PropertyType/{tenantId}")]
        //public async Task<IActionResult> GetPropertyTypeById(string tenantId)
        //{
        //    try
        //    {
        //        var propertyTypeDto = await _userRepo.GetPropertyTypeByIdAsync(tenantId);

        //        if (propertyTypeDto != null)
        //        {
        //            _response.StatusCode = HttpStatusCode.OK;
        //            _response.IsSuccess = true;
        //            _response.Result = propertyTypeDto;
        //            return Ok(_response);
        //        }
        //        else
        //        {
        //            _response.StatusCode = HttpStatusCode.NotFound;
        //            _response.IsSuccess = false;
        //            _response.ErrorMessages.Add("No propertyType found with this id.");
        //            return NotFound(_response);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.StatusCode = HttpStatusCode.NotFound;
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages.Add("Error Occured");
        //        return NotFound(_response);
        //    }
        //}


        //[HttpGet("GetSinglePropertyType/{propertytypeId}")]
        //public async Task<IActionResult> GetSinglePropertyType(int propertytypeId)
        //{
        //    try
        //    {
        //        var tenantDto = await _userRepo.GetSinglePropertyTypeByIdAsync(propertytypeId);

        //        if (tenantDto != null)
        //        {
        //            _response.StatusCode = HttpStatusCode.OK;
        //            _response.IsSuccess = true;
        //            _response.Result = tenantDto;
        //            return Ok(_response);
        //        }
        //        else
        //        {
        //            _response.StatusCode = HttpStatusCode.NotFound;
        //            _response.IsSuccess = false;
        //            _response.ErrorMessages.Add("No user found with this id.");
        //            return NotFound(_response);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.StatusCode = HttpStatusCode.NotFound;
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages.Add("Error Occured");
        //        return NotFound(_response);
        //    }
        //}


        [HttpPost("Asset")]
        public async Task<ActionResult<bool>> CreateAssets(AssetDTO asset)
        {
            try
            {
                var isSuccess = await _userRepo.CreateAssetAsync(asset);

                if (isSuccess == true)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Result = isSuccess;
                   
                }
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        //[HttpPut("PropertyType/{PropertyTypeId}")]
        //public async Task<ActionResult<bool>> UpdatePropertyType(int PropertyTypeId, PropertyTypeDto tenant)
        //{
        //    try
        //    {
        //        tenant.PropertyTypeId = PropertyTypeId; // Ensure tenantId is set
        //        var isSuccess = await _userRepo.UpdatePropertyTypeAsync(tenant);
        //        return Ok(isSuccess);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}

        //[HttpDelete("PropertyType/{propertytypeId}")]
        //public async Task<ActionResult<bool>> DeletePropertyType(int propertytypeId)
        //{
        //    try
        //    {
        //        var isSuccess = await _userRepo.DeletePropertyTypeAsync(propertytypeId);
        //        return Ok(isSuccess);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}
        #endregion





    }
}