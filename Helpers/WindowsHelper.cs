namespace Sennit.RedisManager
{

    public class WindowsHelper
    {
        public static void OpenPath(string path, string fileName)
        {
            var fullPath = $"{path}\\{fileName}";
            var argument = $"/select, \"" + fullPath + "\"";
            System.Diagnostics.Process.Start("explorer.exe", argument);
        }

    }
}