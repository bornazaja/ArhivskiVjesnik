using ArhivskiVjesnikLibrary.Common.QueryCriterias;
using ArhivskiVjesnikWPF.Enums;
using ArhivskiVjesnikWPF.Managers.Interfaces;
using ArhivskiVjesnikWPF.ViewModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ArhivskiVjesnikWPF.Managers.Implenentations
{
    public class ExtendedWindowManager : WindowManager, IExtendedWindowManager
    {
        private NaprednaPretragaViewModel _naprednaPretragaViewModel;

        public ExtendedWindowManager(NaprednaPretragaViewModel naprednaPretragaViewModel)
        {
            _naprednaPretragaViewModel = naprednaPretragaViewModel;
        }

        public bool? ShowDialog<TBasicDtoDetailsViewModel, TDto>(TDto dto)
        {
            var dataViewModel = IoC.Get<TBasicDtoDetailsViewModel>();
            if (dataViewModel is IBasicDtoDetailsViewModel<TDto>)
            {
                var viewModel = dataViewModel as IBasicDtoDetailsViewModel<TDto>;
                viewModel.InitDto(dto);

                var contentControlInfoViewModel = IoC.Get<ContentControlInfoViewModel>();
                contentControlInfoViewModel.ViewModel = viewModel;

                dynamic settings = new ExpandoObject();
                settings.Title = "Info";
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;

                return ShowDialog(contentControlInfoViewModel, null, settings);
            }
            else
            {
                throw new Exception("ViewModel must implement \"IBasicDtoDetailsViewModel\" interface.");
            }
        }

        public async Task<bool?> ShowDialogAsync(object viewModel, object context = null, IDictionary<string, object> settings = null)
        {
            await Task.Yield();
            return ShowDialog(viewModel, context, settings);
        }

        public async Task<bool?> ShowLoadingDialogAsync(System.Action action, string loadingMessage, string errorMessage)
        {
            var loadingViewModel = IoC.Get<LoadingViewModel>();
            loadingViewModel.LoadingMessage = loadingMessage;
            loadingViewModel.IsClosable = false;

            dynamic settings = new ExpandoObject();
            settings.Title = string.Empty;
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;

            var cancellationTokenSource = new CancellationTokenSource();

            var actionTask = Task.Run(() =>
            {
                try
                {
                    cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    action.Invoke();
                    Thread.Sleep(500);
                }
                catch (Exception)
                {
                    cancellationTokenSource.Cancel();
                }
            });

            var loadingTask = ShowDialogAsync(loadingViewModel, null, settings);
            await actionTask;
            loadingViewModel.IsClosable = true;
            loadingViewModel.TryClose();
            await loadingTask;

            if (cancellationTokenSource.Token.IsCancellationRequested)
            {
                return await ShowStatusInfoDialogAsync(errorMessage);
            }

            return loadingTask.Result;
        }

        public bool? ShowNaprednaPretragaDialog(IEnumerable<KeyValuePair<string, string>> stupciZaSortiranje, Action<QueryCriteria> dialogResultOKAction)
        {
            _naprednaPretragaViewModel.InitStupciZaSortiranje(stupciZaSortiranje);
            bool? result = ShowDialog(_naprednaPretragaViewModel);

            if (_naprednaPretragaViewModel.DialogResult == DialogResult.OK)
            {
                dialogResultOKAction.Invoke(_naprednaPretragaViewModel.GetQueryCriteria());
            }

            return result;
        }

        public bool? ShowStatusInfoDialog(string message)
        {
            var keyValuePair = CreateKeyValuePairForStatusInfoViewModel(message);
            return ShowDialog(keyValuePair.Key, null, keyValuePair.Value);
        }

        public async Task<bool?> ShowStatusInfoDialogAsync(string message)
        {
            var keyValuePair = CreateKeyValuePairForStatusInfoViewModel(message);
            return await ShowDialogAsync(keyValuePair.Key, null, keyValuePair.Value);
        }

        private KeyValuePair<StatusInfoViewModel, dynamic> CreateKeyValuePairForStatusInfoViewModel(string message)
        {
            var statusInfoViewModel = IoC.Get<StatusInfoViewModel>();
            statusInfoViewModel.Message = message;

            dynamic settings = new ExpandoObject();
            settings.Title = "Poruka";
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;

            return new KeyValuePair<StatusInfoViewModel, dynamic>(statusInfoViewModel, settings);
        }
    }
}
