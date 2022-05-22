using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Models.Facebook.GetMyPhotos
{

    public class GetMyPhotosResponse
    {
        public Datum[] data { get; set; }
        public Paging paging { get; set; }
    }

    public class Paging
    {
        public Cursors cursors { get; set; }
        public string next { get; set; }
    }

    public class Cursors
    {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public string name { get; set; }
        public string created_time { get; set; }
        public string link { get; set; }
        public string picture { get; set; }
        public Webp_Images[] webp_images { get; set; }
    }

    public class Webp_Images
    {
        public int height { get; set; }
        public string source { get; set; }
        public int width { get; set; }
    }
}
