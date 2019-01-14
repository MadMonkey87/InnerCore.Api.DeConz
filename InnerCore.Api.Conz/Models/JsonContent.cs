using System.Text;
using System.Net.Http;

namespace InnerCore.Api.DeConz.Models
{
    public class JsonContent : StringContent
    {
        public JsonContent(string content)
            : base(content, Encoding.UTF8, "application/json")
        {
        }
    }
}
