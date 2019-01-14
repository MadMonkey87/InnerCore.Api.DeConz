using System.Collections.Generic;
using System.Linq;

namespace InnerCore.Api.DeConz.Models
{
    /// <summary>
    /// A PUT or POST returns a list which can contain multiple success and errors
    /// </summary>
    public class DeConzResults : List<DefaultDeConzResult>
    {
        public bool HasErrors()
        {
            return this.Any(x => x.Error != null);
        }

        public IEnumerable<DefaultDeConzResult> Errors
        {
            get
            {
                return this.Where(x => x.Error != null);
            }
        }
    }
}