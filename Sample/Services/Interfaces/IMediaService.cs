using Sabio.Web.Models.Requests.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Web.Services.Interfaces
{
    public interface IMediaService
    {
        int InsertMedia(MediaAddRequest model);
    }
}
