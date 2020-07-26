using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace InfoCardsAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoCardsController : ControllerBase
    {
        private readonly ICardSaveLoaderFactory _loaderFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<InfoCardsController> _logger;

        public InfoCardsController(ICardSaveLoaderFactory loaderFactory, IConfiguration configuration, ILogger<InfoCardsController> logger)
        {
            _loaderFactory = loaderFactory;
            _configuration = configuration;
            _logger = logger;
        }

        //Get all cards
        //GET: api/InfoCards
        [HttpGet]
        public async Task<ActionResult<List<InfoCard>>> Get()
        {
            try
            {
                return new JsonResult(await Task.Run(() => InfoCard.LoadAllFiles(_configuration["ContentPath"], _loaderFactory.GetNewLoader())));
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Get card by id
        // GET: api/InfoCards/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<InfoCard>> Get(int id)
        {
            try
            {
                return new JsonResult(await Task.Run(() => InfoCard.LoadFromFile(_configuration["ContentPath"], id.ToString(), _loaderFactory.GetNewLoader())));
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Edit card
        // POST: api/InfoCards
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] InfoCard value)
        {
            try
            {
                await Task.Run(() => value.SaveToFile(_configuration["ContentPath"], _loaderFactory.GetNewLoader(), false));
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Add card
        // PUT: api/InfoCards
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] InfoCard value)
        {
            try
            {
                await Task.Run(() => value.SaveToFile(_configuration["ContentPath"], _loaderFactory.GetNewLoader(), true));
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Delete card
        // DELETE: api/InfoCards/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await Task.Run(() => FileUtils.DeleteFile(_configuration["ContentPath"] + id + _loaderFactory.GetLoaderExtension()));
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
