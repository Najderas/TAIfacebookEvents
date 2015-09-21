using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TAIfacebookEvents.Models
{
    public class Comment
    {
        public DateTime Date { get; set; }
        public string User { get; set; }
        public string Content { get; set; }

        public Comment(string user, string content)
        {
            Date = DateTime.Now;
            User = user;
            Content = content;
        }
    }
    public class CommentDBContext : DbContext
    {
        public DbSet<Comment> Comment { get; set; }
    }
}
