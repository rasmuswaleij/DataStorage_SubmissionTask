using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos
{
    public class ProjectRegistrationForm
    {
        public string ProjectName { get; set; } = null!;
        public string Service { get; set; } = null!;

        public string ProjectManager { get; set; } = null!;

        public string Status { get; set; } = null!;
    }
}
