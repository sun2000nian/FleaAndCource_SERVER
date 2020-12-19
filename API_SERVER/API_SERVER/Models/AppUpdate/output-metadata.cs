using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Models.AppUpdate
{
    public class output_metadata
    {
        public int version { get; set; }

        public ArtifactType artifactType { get; set; }

        public string applicationId { get; set; }
        public string variantName { get; set; }

        public List<Elements> elements { get; set; }
    }

    public class ArtifactType
    {
        public string type { get; set; }
        public string kind { get; set; }
    }

    public class Elements
    {
        public string type { get; set; }
        public int versionCode { get; set; }
        public string versionName { get; set; }
        public string outputFile { get; set; }
    }
}
