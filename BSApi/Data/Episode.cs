namespace BSApi.Data
{
    public class Episode
    {
        public string german { get; set; }
        public string english { get; set; }
        public string epi { get; set; }
        public string watched { get; set; }

        public override string ToString()
        {
            return epi + " " + german.Trim();
        }
    }
}