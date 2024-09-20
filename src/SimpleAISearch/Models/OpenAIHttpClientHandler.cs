using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SimpleAISearch.Common.Options;

namespace SimpleAISearch.Models
{
    public class OpenAIHttpClientHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            UriBuilder uriBuilder;
            string chatUrl = ChatAIOption.Endpoint;
            Uri uri1 = new Uri(chatUrl);
            string scheme1 = uri1.Scheme;
            string host1 = uri1.Host;
            int port1 = uri1.Port;         
            string platform = ChatAIOption.Platform;
            switch (request.RequestUri?.LocalPath)
            {
                case "/v1/chat/completions":
                    switch (platform)
                    {
                        case "ZhiPu":
                            uriBuilder = new UriBuilder(request.RequestUri)
                            {
                                // 这里是你要修改的 URL
                                Scheme = scheme1,
                                Host = host1,
                                Port = port1,
                                Path = "api/paas/v4/chat/completions",
                            };
                            request.RequestUri = uriBuilder.Uri;
                            break;
                        case "TaiChu":
                            uriBuilder = new UriBuilder(request.RequestUri)
                            {
                                // 这里是你要修改的 URL
                                Scheme = scheme1,
                                Host = host1,
                                Port = port1,
                                Path = "maas/v1/chat/completions",
                            };
                            request.RequestUri = uriBuilder.Uri;
                            break;
                        case "DashScope":
                            uriBuilder = new UriBuilder(request.RequestUri)
                            {
                                // 这里是你要修改的 URL
                                Scheme = scheme1,
                                Host = host1,
                                Port = port1,
                                Path = "compatible-mode/v1/chat/completions",
                            };
                            request.RequestUri = uriBuilder.Uri;
                            break;
                        default:
                            uriBuilder = new UriBuilder(request.RequestUri)
                            {
                                // 这里是你要修改的 URL
                                Scheme = scheme1,
                                Host = host1,
                                Port = port1,
                                Path = "v1/chat/completions",
                            };
                            request.RequestUri = uriBuilder.Uri;
                            break;
                    }
                    break;            
            }

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            return response;
        }
    }
}
