using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppTest.Models
{
    public class UserReview
    {
        public int id { get; set; }
        public int page { get; set; }
        public List<UserReviewResult> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class UserReviewResult
    {
        public string author { get; set; }
        public Author_Details author_details { get; set; }
        public string content { get; set; }
        public DateTime created_at { get; set; }
        public string id { get; set; }
        public DateTime updated_at { get; set; }
        public string url { get; set; }
    }

    public class Author_Details
    {
        public string name { get; set; }
        public string username { get; set; }
        public string avatar_path { get; set; }
        public string rating { get; set; }
    }

}
