using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Comments.Model;

namespace Comments.Api.Int.v1
{
	[RoutePrefix("api/v1/comment")]
	public class CommentsController : ApiController {

		private readonly CommentsService commentsSvc;

		[HttpGet]
		[Route("")]
		public async Task<IHttpActionResult> GetComments(int userId = 0) {
			IEnumerable<Comment> commentsTask = await commentsSvc.GetComments(userId);

			return Ok(commentsTask);
		}



		[HttpPost]
		[Route("")]
		public async Task<IHttpActionResult> SaveComment([FromBody] Comment comment) {
			Comment commentsTask = await commentsSvc.AddComment(comment);

			return Ok(commentsTask);
		}
	}
}
