﻿namespace LocalCrm.View.Services
{
  public interface IMessageDialogService
  {
    MessageDialogResult ShowYesNoDialog(string title, string text,
        MessageDialogResult defaultResult = MessageDialogResult.Yes);

        MessageDialogResult ShowMessageDialog(string title, string text,
       MessageDialogResult defaultResult = MessageDialogResult.Ok);
    }
}