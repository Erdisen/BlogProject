using BlogProject.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Entities.Entities
{
    public class Category :CoreEntity
    {
        public Category()
        {
            this.Posts = new List<Post>();
        }
        public string CategoryName { get; set; }
        public string Description { get; set; }


        //Navigation Property

        public virtual List<Post> Posts { get; set; }
       
    }
}
