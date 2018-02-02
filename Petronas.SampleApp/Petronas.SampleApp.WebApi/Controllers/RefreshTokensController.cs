using System.Threading.Tasks;
using System.Web.Http;
using Petronas.SampleApp.Repository.Interfaces.Shared;

namespace Petronas.SampleApp.WebApi.Controllers
{
    /// <summary>
    /// Refresh token management
    /// </summary>
    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {
        private readonly IAuthRepository _repo;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="repo"></param>
        public RefreshTokensController(IAuthRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get all refresh tokens
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles ="Admin")]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_repo.GetAllRefreshTokens());
        }

        //[Authorize(Users = "Admin")]
        /// <summary>
        /// Delete token by tokenId
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var result = await _repo.RemoveRefreshToken(tokenId);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token Id does not exist");

        }

        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
