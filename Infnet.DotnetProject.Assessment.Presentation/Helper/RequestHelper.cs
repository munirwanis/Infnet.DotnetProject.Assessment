using RestSharp;

namespace Infnet.DotnetProject.Assessment.Presentation.Helper
{
    public class RequestHelper
    {
        private static RestClient client = new RestClient("http://localhost:51252/");

        public static T MakeRequest<T>(string uri, Method method, object body = null) where T : new()
        {
            var request = new RestRequest(uri, method);
            request.AddHeader("Content-Type", "application/json");

            if (body != null) { request.AddJsonBody(body); }

            var response = client.Execute<T>(request);
            return response.Data;
        }
    }
}