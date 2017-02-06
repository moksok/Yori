using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain.Media
{
    public class MediaDomain
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public string UserId { get; set; }

        public string DataType { get; set; }

        public string Url { get; set; }
    }
}