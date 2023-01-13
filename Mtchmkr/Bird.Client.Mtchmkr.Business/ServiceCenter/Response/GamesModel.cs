using System;
namespace Bird.Client.Mtchmkr.Business.ServiceCenter.Response
{
    public class GamesModel
    {
        public Guid gameId { get; set; }
        public string name { get; set; }
        public int maxFrame { get; set; }
        public DateTime createdDate { get; set; }
    }

    public class LocationModel
    {
        public Guid locationId { get; set; }
        public string location { get; set; }
        public DateTime createdDate { get; set; }
    }
}
