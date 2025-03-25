using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common.Shared
{
    public class JsonUtils
    {
        public static string ConvertObjectToJSONString(object obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
            }
            catch (Exception ex)
            {
                return $"Error converting object to JSON: {ex.Message}";
            }
        }

        /// <summary>
        /// Ghi log vào file với timestamp.
        /// </summary>
        public static void WriteLoggerFile(string logMessage)
        {
            try
            {
                string logFilePath = "Logs/log.txt"; // Đường dẫn file log
                string logDirectory = Path.GetDirectoryName(logFilePath);

                // Tạo thư mục logs nếu chưa tồn tại
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                // Ghi log vào file với timestamp
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {logMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing log: {ex.Message}");
            }
        }
    }
}
