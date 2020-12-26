[RoutePrefix("api/comments")]

[HttpGet]
[Route("")]
public async Task<IHttpActionResult> GetComments(int userId = 0) {

[HttpPost]
[Route("")]
public IHttpActionResult SaveComment([FromBody] Comment comment)


public class Comment
{
    public int? CommentId { get; set; }
    public string CommentText { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedTimestamp { get; set; }
}

