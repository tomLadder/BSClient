using System.Collections.Generic;

namespace BSApi.Data
{
    public class Series
    {
        public class Data
        {
            public List<string> author { get; set; }
            public List<string> producer { get; set; }
            public List<string> director { get; set; }
            public string genre_main { get; set; }
            public List<string> genre { get; set; }
        }

        public string id { get; set; }
        public string series { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string movies { get; set; }
        public string seasons { get; set; }
        public Data data { get; set; }
    }
}