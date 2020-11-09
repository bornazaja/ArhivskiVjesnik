using Caliburn.Micro;
using System;

namespace ArhivskiVjesnikWPF.ViewModels
{
    public class LoadingViewModel : Screen
    {
        public string LoadingMessage { get; set; }
        public bool IsClosable { get; set; }

        public override void CanClose(Action<bool> callback)
        {
            if (!IsClosable)
            {
                callback(false);
            }
            else
            {
                base.CanClose(callback);
            }
        }
    }
}
