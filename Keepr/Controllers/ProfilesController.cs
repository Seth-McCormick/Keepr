using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
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

        private readonly AccountService _as;

        public ProfilesController(ProfilesService ps, AccountService @as)
        {
            _ps = ps;
            _as = @as;
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Profile>> GetProfile(string id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                id = userInfo.Id;
                Profile profile = _ps.GetProfile(id);
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