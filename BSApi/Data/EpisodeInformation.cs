using System;
using System.Collections.Generic;
using System.Text;

namespace BSApi.Data
{
    class EpisodeInformation
    {
        public string series { get; set; }
        public Episode epi { get; set; }
        public List<LinkInformation> links { get; set; }
    }
}
