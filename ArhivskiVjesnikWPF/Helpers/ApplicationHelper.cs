using System.Diagnostics;

namespace ArhivskiVjesnikWPF.Helpers
{
    public static class ApplicationHelper
    {
        public static string GetVersionNumber()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.FileVersion;
        }
    }
}
