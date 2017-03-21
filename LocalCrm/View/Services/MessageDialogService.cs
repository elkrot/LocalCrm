using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace LocalCrm.View.Services
{
  public class MessageDialogService : IMessageDialogService
  {
        public MessageDialogResult ShowMessageDialog(string title, string text, MessageDialogResult defaultResult = MessageDialogResult.Ok)
        {
            var dlg = new MessageDialog(title, text, defaultResult, MessageDialogResult.Ok);
            dlg.Owner = Application.Current.MainWindow;
            return dlg.ShowDialog();
        }

        public MessageDialogResult ShowYesNoDialog(string title, string text,
      MessageDialogResult defaultResult = MessageDialogResult.Yes)
    {
      var dlg = new MessageDialog(title, text, defaultResult, MessageDialogResult.Yes, MessageDialogResult.No);
      dlg.Owner = Application.Current.MainWindow;
      return dlg.ShowDialog();
    }
  }
}
