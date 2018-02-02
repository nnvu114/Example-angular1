using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Petronas.SampleApp.Common.Models;
using Petronas.SampleApp.Model.Classes;
using Petronas.SampleApp.Service.Interfaces.MasterData;
using Petronas.SampleApp.Service.Models.MasterData;
using Petronas.SampleApp.WebApi.Controllers;

namespace Petronas.SampleApp.WebApi.Areas.Setup
{
    /// <summary>
    /// Setup / Company Details
    /// </summary>
    [Authorize]
    [RoutePrefix("api/MasterData/Navigation")]
    public class NavigationController : BaseApiController
    {
        private readonly INavigationService _service;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="service"></param>
        public NavigationController(INavigationService service)
        {
            _service = service;
        }

        // GET api/Company/Get
        /// <summary>
        /// Get data for Company
        /// <input>PageInfo model</input>
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [Route("Get")]
        [HttpGet]
        [ResponseType(typeof(ResponseBaseModel))]
        public async Task<IHttpActionResult> Get()
        {
            var data = await _service.Get(null);
            ResponseBaseModel result = new ResponseBaseModel();
            result.Data.total = data.Count;
            result.Data.data = data;
            return Ok(result);
        }
        [Route("GetTopRight")]
        [HttpGet]
        [ResponseType(typeof(ResponseBaseModel))]
        public async Task<IHttpActionResult> TopRight()
        {
            var data = await _service.Get(null);
            ResponseBaseModel result = new ResponseBaseModel();
            result.Data.total = 2;
            result.Data.data = Newtonsoft.Json.JsonConvert.DeserializeObject(@"[
    {
                'Id': '56789',
        'IconClass': 'icon icon-pet-i-bell',
        'HeaderText': 'Notifications',
        'Items': [
            {
                'Id': 1,
                'TextContent': 'Officia tempor ullamco exercitation minim ad excepteur exercitation.',
                'CanDelete': true,
                'Url': null,
                'Action': null
            },
            {
                'Id': 2,
                'TextContent': 'Officia tempor ullamco exercitation minim ad excepteur exercitation.',
                'CanDelete': false,
                'Url': 'http://google.com',
                'Action': null
            },
            {
                'Id': 3,
                'TextContent': 'Officia tempor ullamco exercitation minim ad excepteur exercitation.',
                'CanDelete': true,
                'Url': null,
                'Action': null
            },
            {
                'Id': 4,
                'TextContent': 'Officia tempor ullamco exercitation minim ad excepteur exercitation.',
                'CanDelete': false,
                'Url': null,
                'Action': null
            }
        ],
        'More': {
            'Label': 'See more',
            'Url': '',
            'Action': ''
        }
    },
    {
        'Id': '123456789',
        'IconClass': 'icon icon-pet-i-users',
        'HeaderText': 'Guideline',
        'Items': [
            {
                'Id': 1,
                'TextContent': 'Officia tempor ullamco exercitation minim ad excepteur exercitation.',
                'CanDelete': true,
                'Url': null,
                'Action': null
            },
            {
                'Id': 2,
                'TextContent': 'Officia tempor ullamco exercitation minim ad excepteur exercitation.',
                'CanDelete': true,
                'Url': 'http://google.com',
                'Action': null
            },
            {
                'Id': 3,
                'TextContent': 'Officia tempor ullamco exercitation minim ad excepteur exercitation.',
                'CanDelete': true,
                'Url': null,
                'Action': null
            },
            {
                'Id': 4,
                'TextContent': 'Officia tempor ullamco exercitation minim ad excepteur exercitation.',
                'CanDelete': true,
                'Url': null,
                'Action': null
            }
        ],
        'More': {
            'Label': 'See more',
            'Url': '',
            'Action': ''
        }
    }
]");
            return Ok(result);
        }
        [Route("GetById")]
        [HttpGet]
        //[ResponseType(typeof(CompanyModel))]
        public async Task<IHttpActionResult> GetById([FromUri]Guid Id)
        {
            var result = await _service.GetById(Id);
            return Ok(result);
        }

        // POST api/Company/Post
        /// <summary>
        /// Create record
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [Route("Post")]
        [HttpPost]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Post([FromBody]NavigationModel model)
        {
            var result = await _service.Add(model);
            return Ok(result);
        }

        // PUT api/Company/Put
        /// <summary>
        /// Update record
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [Route("Put")]
        [HttpPut]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Put([FromBody]NavigationModel model)
        {
            var result = await _service.Update(model);
            return Ok(result);
        }

        // DELETE api/Company/Delete
        /// <summary>
        /// Delete record by id
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [Route("Delete")]
        [HttpDelete]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}