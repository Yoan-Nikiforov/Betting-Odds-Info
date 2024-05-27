namespace UltraPlayTask.Business.Constants
{
    public class PreviewBetTypes
    {
        public const string MatchWinnerBetName = "Match Winner";
        public const string MapAdvantageBetName = "Map Advantage";
        public const string TotalMapsPlayedBetName = "Total Maps Played";

        public static List<string> AllBetTypes()
        {
            return new List<string> { MatchWinnerBetName, MapAdvantageBetName, TotalMapsPlayedBetName };
        }
    }
}
