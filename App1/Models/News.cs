using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class News
    {
        public int NewsId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishedAt { get; set; }

    }
}
