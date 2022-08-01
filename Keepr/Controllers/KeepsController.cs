using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _ks;

        public KeepsController(KeepsService ks)
        {
            _ks = ks;
        }

        [HttpGet]

        public ActionResult<List<Keep>> Get()
        {
            try
            {
                List<Keep> keeps = _ks.Get();
                return Ok(keeps);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        [HttpGet("{id}")]


        public ActionResult<Keep> GetById(int id)
        {
            try
            {
                Keep keep = _ks.GetById(id);
                return Ok(keep);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keep>> Create([FromBody] Keep keepData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                keepData.CreatorId = userInfo.Id;
                keepData.Creator = userInfo;
                Keep newKeep = _ks.Create(keepData);
                return Ok(newKeep);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<ActionResult<Keep>> Edit([FromBody] Keep keepData, int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                keepData.CreatorId = userInfo.Id;
                keepData.Id = id;
                Keep keep = _ks.Edit(keepData);
                return Ok(keep);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult<Keep>> Delete(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                _ks.Delete(id, userInfo.Id);
                return Ok(new { Message = "Deleted Keep" });
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }
    }
}