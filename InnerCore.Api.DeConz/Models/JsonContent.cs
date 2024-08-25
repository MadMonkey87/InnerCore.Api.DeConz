using System.Net.Http;
using System.Text;

namespace InnerCore.Api.DeConz.Models
{
    public class JsonContent : StringContent
    {
        public JsonContent(string content)
            : base(content, Encoding.UTF8, "application/json") { }
    }
}
