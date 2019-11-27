using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Wikeasy.Services
{
    public abstract class BaseHttpService
    {
        protected async Task<T> SendRequestAsync<T>(Uri url, HttpMethod httpMethod = null, IDictionary<string, string> headers = null, object requestData = null)
        {
            var result = default(T);

            // Definir le Get par default
            var method = httpMethod ?? HttpMethod.Get;

            // Serialiser les données requété
            var data = requestData == null ? null : JsonConvert.SerializeObject(requestData);

            using (var request = new HttpRequestMessage(method, url))
            {
                // Ajouter les données requété dans la requete
                if (data != null)
                {
                    request.Content = new StringContent(data, Encoding.UTF8, "application/json");
                }

                // Ajouter le header à la requete
                if (headers != null)
                {
                    foreach (var h in headers)
                    {
                        request.Headers.Add(h.Key, h.Value);
                    }
                }

                // Recuperer la reponse
                using (var handler = new HttpClientHandler())
                {
                    using (var client = new HttpClient(handler))
                    {
                        using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
                        {
                            var content = response.Content == null ? null : await response.Content.ReadAsStringAsync();

                            if (response.IsSuccessStatusCode)
                            {
                                result = JsonConvert.DeserializeObject<T>(content);
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
