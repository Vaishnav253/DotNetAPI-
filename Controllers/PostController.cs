using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly DataContextDapper _dapper;
        public PostController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        [HttpGet("Posts")]
        public IEnumerable<Post> GetPosts()
        {
            string sql = @"SELECT [PostId],
                    [UserId],
                    [PostTitle],
                    [PostContent],
                    [PostCreated],
                    [PostUpdated] 
                FROM TutorialAppSchema.Posts";

            return _dapper.LoadData<Post>(sql);
        }

        [HttpGet("PostSingle/{postId}")]
        // public IEnumerable<Post> GetPostSingle(int postId)
        public Post GetPostSingle(int postId)
        {
            string sql = @"SELECT [PostId],
                    [UserId],
                    [PostTitle],
                    [PostContent],
                    [PostCreated],
                    [PostUpdated] 
                FROM TutorialAppSchema.Posts
                    WHERE PostId = " + postId.ToString();

            return _dapper.LoadDataSingle<Post>(sql);
        }

        [HttpGet("PostsByUser/{userId}")]
        public IEnumerable<Post> GetPostsByUser(int userId)
        {
            string sql = @"SELECT [PostId],
                    [UserId],
                    [PostTitle],
                    [PostContent],
                    [PostCreated],
                    [PostUpdated] 
                FROM TutorialAppSchema.Posts
                    WHERE UserId = " + userId.ToString();

            return _dapper.LoadData<Post>(sql);
        }

        [HttpGet("MyPosts")]
        public IEnumerable<Post> GetMyPosts()
        {
            string sql = @"SELECT [PostId],
                    [UserId],
                    [PostTitle],
                    [PostContent],
                    [PostCreated],
                    [PostUpdated] 
                FROM TutorialAppSchema.Posts
                    WHERE UserId = " + this.User.FindFirst("userId")?.Value;

            return _dapper.LoadData<Post>(sql);
        }

        [HttpGet("PostsBySearch/{searchParam}")]
        public IEnumerable<Post> PostsBySearch(string searchParam)
        {
            string sql = @"SELECT [PostId],
                    [UserId],
                    [PostTitle],
                    [PostContent],
                    [PostCreated],
                    [PostUpdated] 
                FROM TutorialAppSchema.Posts
                    WHERE PostTitle LIKE '%" + searchParam + "%'" +
                        " OR PostContent LIKE '%" + searchParam + "%'";

            return _dapper.LoadData<Post>(sql);
        }

        static string EscapeSingleQuote(string input)
        {
            string output = input.Replace("'", "''");
 
            return output;
        }

        [HttpPost("Post")]
        public IActionResult AddPost(PostToAddDto postToAdd)
        {
            string sql = @"
            INSERT INTO TutorialAppSchema.Posts(
                [UserId],
                [PostTitle],
                [PostContent],
                [PostCreated],
                [PostUpdated]) VALUES (" + this.User.FindFirst("userId")?.Value
                + ",'" + EscapeSingleQuote(postToAdd.PostTitle)
                + "','" + EscapeSingleQuote(postToAdd.PostContent)
                + "', GETDATE(), GETDATE() )";

            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Faled to create new posts!");    
        }
        
        // [AllowAnonymous]
        [HttpPut("Post")]
        public IActionResult EditPost(PostToEditDto postToEdit)
        {
            string sql = @"
            UPDATE TutorialAppSchema.Posts 
                SET PostContent = '" + EscapeSingleQuote(postToEdit.PostContent) + 
                "', PostTitle = '" + EscapeSingleQuote(postToEdit.PostTitle) + 
                @"', PostUpdated = GETDATE()
                WHERE PostId = " + postToEdit.PostId.ToString() +
                "AND UserId = " + this.User.FindFirst("userId")?.Value;

            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Faled to edit post!");    
        }

        [HttpDelete("Post/{postId}")]
        public IActionResult DeletePost(int postId)
        {
            string sql = @"DELETE FROM TutorialAppSchema.Posts 
                WHERE PostId = " + postId.ToString() +
                    " AND UserId = " + this.User.FindFirst("userId")?.Value;
            
            Console.WriteLine("Generated SQL Query for Deletion: " + sql);

            if(_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Faled to delete posts!");
        }
    }
}