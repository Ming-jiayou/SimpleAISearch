using System.Net.Http;
using Microsoft.SemanticKernel;
using SimpleAISearch.Interface;
using SimpleAISearch.Models;
using SimpleAISearch.Common.Options;
using Microsoft.SemanticKernel.ChatCompletion;
namespace SimpleAISearch.Services
{
#pragma warning disable SKEXP0050
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0020
#pragma warning disable SKEXP0010
    public class SemanticKernelService : ISemanticKernelService
    {
        private readonly Kernel _kernel;
        public SemanticKernelService()
        {
            var handler = new OpenAIHttpClientHandler();
            var builder = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
              modelId: ChatAIOption.ChatModel,
              apiKey: ChatAIOption.Key,
              httpClient: new HttpClient(handler));
            var kernel = builder.Build();
            kernel.Plugins.AddFromType<TranslationFunctions>();
            _kernel = kernel;
        }

        public async Task<Query> GetAIResponse(string question)
        {
            var query = new Query { Question = question };
            var result = await _kernel.InvokePromptAsync(question);
            var str = result.ToString();
            query.Answer = str;
            return query;
        }
     
     

        public async Task<string> RunUniversalLLMFunctionCallerSampleAsync(string askText)
        {
            var handler = new OpenAIHttpClientHandler();
            var builder = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
               modelId: ChatAIOption.ChatModel,
               apiKey: ChatAIOption.Key,
               httpClient: new HttpClient(handler));
            var kernel = builder.Build();

            // Add a plugin with some helper functions we want to allow the model to utilize.
            kernel.ImportPluginFromFunctions("HelperFunctions",
             [
                 kernel.CreateFunctionFromMethod(() => DateTime.Now.ToString("R"), "GetCurrentTime", "获取当前的时间"),
                 kernel.CreateFunctionFromMethod((string cityName) =>
                 cityName switch
                 {
                    "武汉" => "38℃，晴天",
                    "北京" => "25℃，下雨",
                    "杭州" => "30℃，阴天",
                    "福州" => "35℃，晴天",
                    "厦门" => "27℃，阴天",
                     _ => "31℃，晴天",
                 }, "GetWeatherForCity", "获取指定城市的当前天气情况。"),
                 kernel.CreateFunctionFromMethod((int id) =>
                 {
                   List<Product> Products = new List<Product>()
                   {
                       new Product{Id = 1,Name = "iPhone 15",Address = "湖北武汉"},
                       new Product{Id = 2,Name = "小米",Address = "广东深圳"},
                       new Product{Id = 3,Name = "华为",Address = "广东广州"},
                       new Product{Id = 4,Name = "vivo",Address = "福建福州"},
                       new Product{Id = 5,Name = "oppo",Address = "福建厦门"}
                   };
                   var product = Products.Find(p => p.Id == id);
                   return product;
                    
                 }, "GetProductById", "根据id获取订单。"),
            ]);
         
            ChatHistory history = new ChatHistory();
            history.AddUserMessage(askText);
            UniversalLLMFunctionCaller planner = new(kernel);
            string result = await planner.RunAsync(askText);

            history.AddAssistantMessage(result);
            return result;
        }

        public async Task<string> RunTranslationAIAgentSampleAsync(string askText)
        {           
            ChatHistory history = new ChatHistory();
            history.AddUserMessage(askText);
            UniversalLLMFunctionCaller planner = new(_kernel);
            string result = await planner.RunAsync(askText);

            history.AddAssistantMessage(result);
            return result;
        }

        public async Task<string> RunWebSpiderSampleAsync(string query)
        {
            var handler = new OpenAIHttpClientHandler();
            var builder = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
               modelId: ChatAIOption.ChatModel,
               apiKey: ChatAIOption.Key,
               httpClient: new HttpClient(handler));
            var kernel = builder.Build();

            kernel.Plugins.AddFromType<WebSpider>();

            //var AskText = """
            //    SimpleRAG是什么？
            //    """;
            ChatHistory history = new ChatHistory();
            history.AddUserMessage(query);
            UniversalLLMFunctionCaller planner = new(kernel);
            string result = await planner.RunAsync(query);

            history.AddAssistantMessage(result);
            return result;
        }
    }
}
