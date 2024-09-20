using Microsoft.SemanticKernel;
using SimpleAISearch.Common.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAISearch.Models
{
    internal class WebSpider
    {
        private readonly Kernel _kernel;

        public WebSpider()
        {
            var handler = new OpenAIHttpClientHandler();
            var builder = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
               modelId: ChatAIOption.ChatModel,
               apiKey: ChatAIOption.Key,
               httpClient: new HttpClient(handler));
            _kernel = builder.Build();
        }

        [KernelFunction, Description("根据关键词搜索网络数据")]
        public async Task<string> GetWebData(
          [Description("要搜索的主题")] string query)
        {
            // 定义 API URL 和查询参数
            string apiUrl = "https://api.peckot.com/DuckDuckGoSearch";
            //string apiUrl2 = "http://duckduckgo.com/?q=example";        
            string format = "json";
            int amount = 10;

            // 构建完整的请求 URL
            string requestUrl = $"{apiUrl}?keyword={Uri.EscapeDataString(query)}&amount={amount}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // 发送 GET 请求
                    HttpResponseMessage response = await client.GetAsync(requestUrl);

                    // 确保请求成功
                    response.EnsureSuccessStatusCode();

                    // 读取响应内容
                    string responseBody = await response.Content.ReadAsStringAsync();

                    string skPrompt = """
                            {{$input}}

                            根据这些信息回答问题{{$question}}
                            """;
                    var result = await _kernel.InvokePromptAsync(skPrompt, new() { ["input"] = responseBody, ["question"] = query });
                    var str = result.ToString();
                    return str;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"请求失败: {e.Message}");
                    return "获取失败";
                }
            }
        }

        [KernelFunction, Description("获取网页的主要内容")]
        public async Task<string> GetWebPage(
          [Description("要获取的网页的URL")] string url
)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // 发送 GET 请求
                    HttpResponseMessage response = await client.GetAsync(url);

                    // 确保请求成功
                    response.EnsureSuccessStatusCode();

                    // 读取响应内容
                    string responseBody = await response.Content.ReadAsStringAsync();

                    string skPrompt = """
                            {{$input}}

                            总结上面网页的主要内容
                            """;
                    var result = await _kernel.InvokePromptAsync(skPrompt, new() { ["input"] = responseBody });
                    var str = result.ToString();
                    return str;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"请求失败: {e.Message}");
                    return "获取失败";
                }
            }
        }
    }
}
