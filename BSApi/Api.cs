using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BSApi.Data;
using Newtonsoft.Json;

namespace BSApi
{
    class Api
    {
        public static List<SeriesInformation> GetSeries()
        {
            return JsonConvert.DeserializeObject<List<SeriesInformation>>(HTTPRequester.LaunchApiRequest("series"));
        }

        public static int GetSeasonCount(int seriesId)
        {
            int seasonCount = 0;
            try
            {
                seasonCount = Convert.ToInt32(GetSeason(seriesId, 1).series.seasons);
            }
            catch (Exception)
            {
                seasonCount = 0;
            }
            return seasonCount;
        }

        public static int GetSeasonCount(SeriesInformation seriesInformation)
        {
            return GetSeasonCount(Convert.ToInt32(seriesInformation.id));
        }

        public static Season GetSeason(int seriesId, int seasonId)
        {
            return JsonConvert.DeserializeObject<Season>(HTTPRequester.LaunchApiRequest($"series/{seriesId}/{seasonId}"));
        }
        public static Season GetSeason(SeriesInformation seriesInformation, int seasonId)
        {
            return GetSeason(Convert.ToInt32(seriesInformation.id), seasonId);
        }

        public static EpisodeInformation GetEpisode(int seriesId, int seasonId, int episodeId)
        {
            return JsonConvert.DeserializeObject<EpisodeInformation>(HTTPRequester.LaunchApiRequest($"series/{seriesId}/{seasonId}/{episodeId}"));
        }

        public static EpisodeInformation GetEpisode(Season season, int episodeId)
        {
            return GetEpisode(Convert.ToInt32(season.series.id), Convert.ToInt32(season.season), episodeId);
        }

        public static Link GetLink(int linkId)
        {
            return JsonConvert.DeserializeObject<Link>(HTTPRequester.LaunchApiRequest($"watch/{linkId}"));
        }

        public static Link GetLink(LinkInformation linkInformation)
        {
            return GetLink(Convert.ToInt32(linkInformation.id));
        }
    }
}
