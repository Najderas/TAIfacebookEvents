using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TAIfacebookEvents.Models
{
    public class Event
    {
        private static int NextID = 0;

        public int ID { get; set; }
        public DateTime Start { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public List<Comment> Comments { get; set; }
        public int FacebookId { get; set; }

        public Event(DateTime start, string title, string description, string owner)
        {
            ID = NextID++;
            Start = start;
            Title = title;
            Description = description;
            Owner = owner;
            Comments = new List<Comment>();
            FacebookId = -1;
        }

        public void AddComment(string user, string comment)
        {
            Comments.Add(new Comment(user, comment));
        }
    }
    public class EventDBContext : DbContext
    {
        public DbSet<Event> Movies { get; set; }
    }
}

