using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Comments.Model;

namespace Comments.Api.Int.v1
{
	[RoutePrefix("api/comments")]
	public class CommentsController : ApiController {
		public CommentsController() { }



		[HttpGet]
		[Route("")]
		public async Task<IHttpActionResult> GetComments(int userId = 0) {
			using (CommentsService commentsSvc = new CommentsService()) {
				IEnumerable<Comment> commentsTask = await commentsSvc.GetComments(userId);
				return Ok(commentsTask);
			}
		}



		[HttpPost]
		[Route("")]
		public async Task<IHttpActionResult> SaveComment([FromBody] Comment comment) {
			using (CommentsService commentsSvc = new CommentsService()) {
				Comment commentsTask = await commentsSvc.AddComment(comment);
				return Ok(commentsTask);
			}
		}
	}
}
