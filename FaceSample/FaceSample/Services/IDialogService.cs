using Acr.UserDialogs;
using System.Threading.Tasks;

namespace FaceSample.Services
{
    public interface IDialogService : IBaseService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
        Task<string> ShowOptionsAlertAsync(string title, string cancel, string[] buttons);
        void ShowLoading(string text = "");
        void HideLoading();
        void ConfirmAlert(ConfirmConfig confirmConfig);
        void ActionSheet(ActionSheetConfig actionSheetConfig);
    }
}