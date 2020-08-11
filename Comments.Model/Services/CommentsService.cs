
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Comments.Model {
	public class CommentsService : IDisposable {
		public CommentsService() { }
		public void CreateDatabase() {
			SQLiteConnection.CreateFile("C:\\temp\\CommentsDb.sqlite");
			using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=C:\\temp\\CommentsDb.sqlite;Version=3;")) { 
				m_dbConnection.Open();
				string sql = "create table Comments (CommentId int, CommentText varchar(2000), UserId int, CreatedTimestamp datetime)";
				SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
				command.ExecuteNonQuery();

				sql = "insert into Comments (CommentId, CommentText, UserId, CreatedTimestamp) values (1, 'This is a great comment, huh.', 3000, datetime('2019-01-01 02:34:56'))";
				command = new SQLiteCommand(sql, m_dbConnection);
				command.ExecuteNonQuery();
				sql = "insert into Comments (CommentId, CommentText, UserId, CreatedTimestamp) values (2, 'Here is me saying some stuff.', 1000, datetime('2020-04-13 12:20:32'))";
				command = new SQLiteCommand(sql, m_dbConnection);
				command.ExecuteNonQuery();
				sql = "insert into Comments (CommentId, CommentText, UserId, CreatedTimestamp) values (3, 'I did not like it.', 3000, datetime('2020-02-16 07:54:12'))";
				command = new SQLiteCommand(sql, m_dbConnection);
				command.ExecuteNonQuery();
				sql = "insert into Comments (CommentId, CommentText, UserId, CreatedTimestamp) values (4, 'Some other stuff being said here.', 6363, datetime('2020-08-05 06:17:20'))";
				command = new SQLiteCommand(sql, m_dbConnection);
				command.ExecuteNonQuery();
			}
		}

		public async Task<IEnumerable<Comment>> GetComments(int userId = 0) {
			List<Comment> comments = new List<Comment>();

			using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=C:\\temp\\CommentsDb.sqlite;Version=3;")) {

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
		}

		public async Task<Comment> AddComment(Comment comment) {

			using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=C:\\temp\\CommentsDb.sqlite;Version=3;")) {
				m_dbConnection.Open();


				string sql = "select max(CommentId) as CommentId from Comments";
				SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
				SQLiteDataReader reader = command.ExecuteReader();
				while (await reader.ReadAsync()) {
					comment.CommentId = reader.GetInt32(reader.GetOrdinal("CommentId")) + 1;
				}
				comment.CreatedTimestamp = DateTime.Now;
				Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

				sql = "insert into Comments(CommentId, CommentText, UserId, CreatedTimestamp) values (" + comment.CommentId + ", '" + comment.CommentText.Replace("'", "''") + "', " + comment.UserId + ", datetime(" + unixTimestamp.ToString() + ", 'unixepoch'))";
				command = new SQLiteCommand(sql, m_dbConnection);
				SQLiteDataReader readerAdd = command.ExecuteReader();

				while (await readerAdd.ReadAsync()) {
				
				}
				return comment;
			}
		}

		#region DISPOSE
		public void Dispose() {
			Dispose(true);

			GC.SuppressFinalize(this);
		}

		~CommentsService() {
			Dispose(false);
		}

		protected virtual void Dispose(bool disposing) {
			if (!disposing)
				return;
		}
		#endregion
	}
}

