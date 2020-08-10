
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Comments.Model {
	public class CommentsService {
		public void CreateDatabase() {
			SQLiteConnection.CreateFile("CommentsDb.sqlite");
			SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=CommentsDb.sqlite;Version=3;");
			m_dbConnection.Open();
			string sql = "create table Comments (CommentId int, CommentText varchar(2000), CreatedByUserId int, CreatedDate datetime)";
			SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
			command.ExecuteNonQuery();

			sql = "insert into Comments (CommentId, CommentText, UserId, CreatedTimestamp) values (1, 'This is a great comment, huh.', 3000, getdate())";
			command.ExecuteNonQuery();
			sql = "insert into Comments (CommentId, CommentText, UserId, CreatedTimestamp) values (2, 'Here is me saying some stuff.', 1000, getdate())";
			command.ExecuteNonQuery();
			sql = "insert into Comments (CommentId, CommentText, UserId, CreatedTimestamp) values (3, 'I did not like it.', 3000, getdate())";
			command.ExecuteNonQuery();
			sql = "insert into Comments (CommentId, CommentText, UserId, CreatedTimestamp) values (4, 'Some other stuff being said here.', 6363, getdate())";
			command.ExecuteNonQuery();
		}

		public async Task<IEnumerable<Comment>> GetComments(int userId = 0) {
			List<Comment> comments = new List<Comment>();

			SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=CommentsDb.sqlite;Version=3;");
			m_dbConnection.Open();

			string sql = "select * from Comments";
			if (userId > 0) {
				sql = sql + " where UserId = " + userId.ToString();
			}
			sql = sql + " order by CreatedTimestamp desc";
			SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
			SQLiteDataReader reader = command.ExecuteReader();

			while (await reader.ReadAsync()) {
				comments.Add(new Comment() {
					CommentId = reader.GetInt32(reader.GetOrdinal("CommentId")),
					CommentText = reader.IsDBNull(reader.GetOrdinal("CommentText")) ? null : reader.GetString(reader.GetOrdinal("CommentText")),
					UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
					CreatedTimestamp = reader.IsDBNull(reader.GetOrdinal("CreatedTimestamp")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("CreatedTimestamp"))
				});
			}
			return comments.AsEnumerable();
		}

		public async Task<Comment> AddComment(Comment comment) {

			SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=CommentsDb.sqlite;Version=3;");
			m_dbConnection.Open();

			string sql = "insert into Comments(CommentId, CommentText, UserId, CreatedTimestamp) values (" + comment.CommentId + ", " + comment.CommentText + ", " + comment.UserId + ", " + comment.CreatedTimestamp + ")";
			SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
			SQLiteDataReader reader = command.ExecuteReader();

			while (await reader.ReadAsync()) {
				
			}
			return comment;
		}
	}
}

