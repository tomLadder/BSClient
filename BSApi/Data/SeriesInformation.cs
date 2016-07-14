using System;
using System.Collections.Generic;
using System.Text;

namespace BSApi.Data
{
    class SeriesInformation
    {
        public string series { get; set; }
        public string id { get; set; }

        public override string ToString()
        {
            return "--> " + series;
        }
    }
}
