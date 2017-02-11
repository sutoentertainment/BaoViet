using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoVietCore.Models
{
    public class MercuryResponse
    {
        public string title { get; set; }
        public string content { get; set; }
        public DateTime? date_published { get; set; }
        public string lead_image_url { get; set; }
        public string dek { get; set; }
        public string url { get; set; }
        public string domain { get; set; }
        public string excerpt { get; set; }
        public int word_count { get; set; }
        public string direction { get; set; }
        public int total_pages { get; set; }
        public int rendered_pages { get; set; }
        public object next_page_url { get; set; }
    }

}
