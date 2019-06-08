using Acr.UserDialogs;
using System;
using System.Threading.Tasks;

namespace FaceSample.Services
{
    public class DialogService : IDialogService
    {
        public void ActionSheet(ActionSheetConfig actionSheetConfig)
        {
            UserDialogs.Instance.ActionSheet(actionSheetConfig);
        }

        public void ConfirmAlert(ConfirmConfig confirmConfig)
        {
            UserDialogs.Instance.Confirm(confirmConfig);
        }

        public void HideLoading()
        {
            UserDialogs.Instance.HideLoading();
        }

        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }

        public void ShowLoading(string text = "")
        {
            text = String.IsNullOrEmpty(text) ? "Loading.." : text;
            UserDialogs.Instance.ShowLoading(text, MaskType.Gradient);
        }

        public Task<string> ShowOptionsAlertAsync(string title, string cancel, string[] buttons)
        {
            return UserDialogs.Instance.ActionSheetAsync(title, "", cancel, null, buttons);
        }
    }
}