using SimpleAISearch.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAISearch.ViewModels.Pages
{
    public partial class AISearchViewModel : ObservableObject
    {
        private readonly ISemanticKernelService _semanticKernelService;
        public AISearchViewModel(ISemanticKernelService semanticKernelService)
        {
            _semanticKernelService = semanticKernelService;
        }

        [ObservableProperty]
        private string askText = """
            SimpleRAG是什么？
            """;
        
        [ObservableProperty]
        private string responseText = "";

        [ObservableProperty]
        private Visibility progressRingVisible = Visibility.Hidden;

        [RelayCommand]
        private async Task RunTranslationAIAgent()
        {
            if (ResponseText != "")
            {
                ResponseText = "";
            }
            if (AskText != "")
            {
                ProgressRingVisible = Visibility.Visible;
                var result = await _semanticKernelService.RunWebSpiderSampleAsync(AskText);
                ProgressRingVisible = Visibility.Hidden;
                int chunkSize = 4; // 每次显示的字符数
                for (int i = 0; i < result.Length; i += chunkSize)
                {
                    string chunk = result.Substring(i, Math.Min(chunkSize, result.Length - i));
                    ResponseText += chunk;
                    await Task.Delay(100);
                }
            }
            else
            {
                var uiMessageBox = new Wpf.Ui.Controls.MessageBox
                {
                    Title = "WPF UI Message Box",
                    Content =
                    "请先输入问题！",
                };

                _ = await uiMessageBox.ShowDialogAsync();
            }

        }
    }
}
