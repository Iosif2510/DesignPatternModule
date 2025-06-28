using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace _2510.DesignPatternModule.Utilities
{
    public static class SaveLoadUtility
    {
        public static void SaveJsonFile(string content, string filePath, string fileName)
        {
            try
            {
                var dir = $"{filePath}/{fileName}";
                using var file = File.CreateText(dir);
                file.Close();

                using var sw = new StreamWriter(dir);
                sw.Write(content);
                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public static string LoadJsonFile(string filePath, string fileName)
        {
            var dir = $"{filePath}/{fileName}";
            try
            {
                var saveData = File.ReadAllText(dir);
                return saveData;
            }
            catch (Exception e)
            {
                if (e is not (DirectoryNotFoundException or FileNotFoundException)) throw;
                using (var _ = File.CreateText(dir)) {} 
                return null;
            }
        }
        
        public static async Task SaveJsonAsync(string content, string filePath, string fileName)
        {
            try
            {
                var dir = $"{filePath}/{fileName}";
                await using var sw = new StreamWriter(dir);
                await sw.WriteAsync(content);
                await sw.FlushAsync();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }

        public static async Task<string> LoadJsonAsync(string filePath, string fileName)
        {
            var dir = $"{filePath}/{fileName}";

            if (File.Exists(dir))
            {
                return await File.ReadAllTextAsync(dir);
            }
            await using (var file = File.CreateText(dir));
            Debug.LogWarning("Save File Missing... New File is created");
            throw new FileNotFoundException();
        }

        public static void DeleteSaves(string filePath)
        {
            try
            {
                var files = Directory.GetFiles(filePath);
                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
    
    }
}