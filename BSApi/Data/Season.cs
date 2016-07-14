using System;
using System.Collections.Generic;
using System.Text;

namespace BSApi.Data
{
    public class Season
    {
        public Series series { get; set; }
        public int season { get; set; }
        public List<Episode> epi { get; set; }

        public int GetEpisodeCount()
        {
            return epi.Count;
        }

        public override string ToString()
        {
            return $"Season {season}";
        }
    }
}
