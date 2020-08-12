using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comments.Model
{
    public class Comment
    {
		public int? CommentId { get; set; }
		public string CommentText { get; set; }
		public DateTime? CreatedTimestamp { get; set; }
	}
}
