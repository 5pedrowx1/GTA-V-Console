namespace Injector_UI.Core
{
    public static class FileUtils
    {
        public static string? SearchFileRecursive(string directory, string fileName, int maxDepth, int currentDepth = 0)
        {
            if (currentDepth > maxDepth) return null;

            try
            {
                var files = Directory.GetFiles(directory, fileName, SearchOption.TopDirectoryOnly);
                if (files.Length > 0) return files[0];

                foreach (var subDir in Directory.GetDirectories(directory))
                {
                    var result = SearchFileRecursive(subDir, fileName, maxDepth, currentDepth + 1);
                    if (!string.IsNullOrEmpty(result)) return result;
                }
            }
            catch
            {
            }

            return null;
        }

        public static List<string> FindAllFiles(string directory, string pattern, int maxDepth = 2)
        {
            var results = new List<string>();
            FindAllFilesRecursive(directory, pattern, maxDepth, 0, results);
            return results;
        }

        private static void FindAllFilesRecursive(string directory, string pattern, int maxDepth, int currentDepth, List<string> results)
        {
            if (currentDepth > maxDepth) return;

            try
            {
                results.AddRange(Directory.GetFiles(directory, pattern, SearchOption.TopDirectoryOnly));

                foreach (var subDir in Directory.GetDirectories(directory))
                {
                    FindAllFilesRecursive(subDir, pattern, maxDepth, currentDepth + 1, results);
                }
            }
            catch
            {
            }
        }

        public static bool TryCreateDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static long GetDirectorySize(string path)
        {
            try
            {
                var dirInfo = new DirectoryInfo(path);
                return dirInfo.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length);
            }
            catch
            {
                return 0;
            }
        }

        public static bool IsFileInUse(string filePath)
        {
            try
            {
                using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
                return false;
            }
            catch (IOException)
            {
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
