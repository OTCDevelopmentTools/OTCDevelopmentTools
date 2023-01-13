namespace Bird.Client.Mtchmkr.Portable.Models
{
    public class SearchMlodel
    {
        public int[] Skills { get; set; }

        public string Name { get; set; }

        public double Stake { get; set; }

        public Location Location {get;set; }

        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }

    }
}