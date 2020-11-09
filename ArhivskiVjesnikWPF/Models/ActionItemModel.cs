using System;

namespace ArhivskiVjesnikWPF.Models
{
    public class ActionItemModel<T>
    {
        public string Name { get; set; }
        public Action<T> Action { get; set; }
    }
}
