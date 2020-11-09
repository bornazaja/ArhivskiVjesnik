using ArhivskiVjesnikLibrary.Common.Constants;
using ArhivskiVjesnikWPF.Managers.Interfaces;
using Caliburn.Micro;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class PaginationViewModel : Screen
    {
        private bool _hasPrevious;
        private bool _hasNext;
        private int _totalPages;
        private System.Action _action;
        private IExtendedWindowManager _extendedWindowManager;

        public PaginationViewModel(IExtendedWindowManager extendedWindowManager)
        {
            _extendedWindowManager = extendedWindowManager;
            CurrentPage = PaginationConstants.FirstPage;
            RefreshCurrentAndMaxNumberOfPages();
        }

        public void SetActionForNavigations(System.Action action)
        {
            _action = action;
        }

        public void SetActionForNavigations(System.Action action, string loadingMessage, string errorMessage)
        {
            _action = async () => await _extendedWindowManager.ShowLoadingDialogAsync(action, loadingMessage, errorMessage);
        }

        public int CurrentPage { get; set; }

        public void Refresh(bool hasPrevious, bool hasNext, int totalPages)
        {
            _hasPrevious = hasPrevious;
            _hasNext = hasNext;
            _totalPages = totalPages;

            if (_totalPages == PaginationConstants.ZeroPage)
            {
                CurrentPage = PaginationConstants.ZeroPage;
            }

            RefreshCurrentAndMaxNumberOfPages();
        }

        private string _currentAndMaxNumberOfPages;

        public string CurrentAndMaxNumberOfPages
        {
            get
            {
                return _currentAndMaxNumberOfPages;
            }
            set
            {
                _currentAndMaxNumberOfPages = value;
                NotifyOfPropertyChange(() => CurrentAndMaxNumberOfPages);
                NotifyOfPropertyChange(() => CanFirst);
                NotifyOfPropertyChange(() => CanPrevious);
                NotifyOfPropertyChange(() => CanNext);
                NotifyOfPropertyChange(() => CanLast);
            }
        }

        public void First()
        {
            CurrentPage = PaginationConstants.FirstPage;
            _action.Invoke();
            RefreshCurrentAndMaxNumberOfPages();
        }

        public bool CanFirst
        {
            get
            {
                if (CurrentPage > PaginationConstants.FirstPage)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Previous()
        {
            CurrentPage = CurrentPage - 1;
            _action.Invoke();
            RefreshCurrentAndMaxNumberOfPages();
        }

        public bool CanPrevious
        {
            get
            {
                if (_hasPrevious)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Next()
        {
            CurrentPage = CurrentPage + 1;
            _action.Invoke();
            RefreshCurrentAndMaxNumberOfPages();
        }

        public bool CanNext
        {
            get
            {
                if (_hasNext)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Last()
        {
            CurrentPage = _totalPages;
            _action.Invoke();
            RefreshCurrentAndMaxNumberOfPages();
        }

        public bool CanLast
        {
            get
            {
                if (CurrentPage < _totalPages)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void RefreshCurrentAndMaxNumberOfPages()
        {
            CurrentAndMaxNumberOfPages = $"{CurrentPage}{PaginationConstants.SeparatorBetweenCurrentAndLastPage}{_totalPages}";
        }
    }
}
