using BlogProject.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Entities.Entities
{
    public class User : CoreEntity
    {
        public User()
        {
            this.Posts = new List<Post>();
            this.Comments = new List<Comment>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public string LastIPAddress { get; set; }

        //Navigation Property

        public virtual List<Post> Posts { get; set; }
        public virtual List<Comment> Comments { get; set; }


    }
}
