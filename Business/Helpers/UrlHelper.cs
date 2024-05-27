namespace UltraPlayTask.Business.Helpers
{
    public class UrlHelper
    {
        private readonly string _baseEndpoint;

        public UrlHelper(string baseEndpoint)
        {
            _baseEndpoint = baseEndpoint;
        }

        public string ConstructUrl(string endpoint, Dictionary<string, string> queryParams)
        {
            var url = $"{_baseEndpoint}/{endpoint}";

            if (queryParams.Any())
            {
                var queryString = string.Join("&", queryParams.Select(qp => $"{qp.Key}={qp.Value}"));
                url += $"?{queryString}";
            }

            return url;
        }
    }
}