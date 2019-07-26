using System;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Views;

namespace Cinegy.ImportTool.Model
{
    /// <summary>
    ///     Example from Laurent Bugnions Flowers.Forms mvvm Examples
    /// </summary>
    public class DialogService : IDialogService
    {
        #region IDialogService Members

        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
            afterHideCallback?.Invoke();
            return Task.CompletedTask;
        }

        public Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            MessageBox.Show(error.Message, title, MessageBoxButton.OK, MessageBoxImage.Error);
            afterHideCallback?.Invoke();
            return Task.CompletedTask;
        }

        public Task ShowMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
            return Task.CompletedTask;
        }

        public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);

            afterHideCallback?.Invoke();
            return Task.CompletedTask;
        }

        public Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            var result = MessageBox.Show(message, title, MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK;

            afterHideCallback?.Invoke(result);

            return Task.FromResult(result);
        }

        public Task ShowMessageBox(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
            return Task.CompletedTask;
        }

        #endregion
    }
}