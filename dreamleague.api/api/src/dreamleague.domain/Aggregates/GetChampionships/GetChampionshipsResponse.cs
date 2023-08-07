using dreamleague.domain.Entities.Championships;

namespace dreamleague.domain.Aggregates.GetChampionships
{
    public class GetChampionshipsResponse
    {
        public IEnumerable<Championship> Championships { get; set; }
    }
}
