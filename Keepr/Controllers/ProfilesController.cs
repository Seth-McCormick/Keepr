using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _ps;

        private readonly VaultsService _vs;

        public ProfilesController(ProfilesService ps, VaultsService vs)
        {
            _ps = ps;
            _vs = vs;
        }

        [HttpGet("{id}")]

        public ActionResult<Account> GetProfile(string id)
        {
            try
            {
                Account profile = _ps.GetProfile(id);
                return Ok(profile);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("{id}/keeps")]

        public ActionResult<List<Keep>> GetKeeps(string id)
        {
            try
            {
                List<Keep> keeps = _ps.GetKeeps(id);
                return Ok(keeps);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        [HttpGet("{id}/vaults")]

        public ActionResult<List<Vault>> GetVaults(string id)
        {
            try
            {
                List<Vault> vaults = _ps.GetVaults(id);
                return Ok(vaults);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }
    }
}