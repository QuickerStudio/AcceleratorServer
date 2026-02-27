using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

/// <summary>
/// 从官方服务器抓取加速器配置的小工具
/// </summary>
class ConfigFetcher
{
    private const string OfficialApiUrl = "https://api.steampp.net/api/Accelerate/All";
    private const string OutputDir = "../../config";
    private const string OutputFile = "accelerate.json";

    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Watt Toolkit 配置抓取工具 ===");
        Console.WriteLine($"正在从官方服务器获取配置: {OfficialApiUrl}");
        Console.WriteLine();

        try
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");

            // 获取配置
            var response = await httpClient.GetAsync(OfficialApiUrl);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("✓ 成功获取配置数据");

            // 解析 JSON 以验证格式
            using var jsonDoc = JsonDocument.Parse(jsonString);
            var root = jsonDoc.RootElement;

            // 检查是否是 API 响应格式
            if (root.TryGetProperty("Content", out var content))
            {
                // 是 API 响应格式，提取 Content 部分
                jsonString = content.GetRawText();
                Console.WriteLine("✓ 检测到 API 响应格式，已提取 Content 部分");
            }

            // 美化 JSON 格式
            var jsonElement = JsonSerializer.Deserialize<JsonElement>(jsonString);
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var formattedJson = JsonSerializer.Serialize(jsonElement, options);

            // 创建输出目录
            if (!Directory.Exists(OutputDir))
            {
                Directory.CreateDirectory(OutputDir);
                Console.WriteLine($"✓ 创建目录: {OutputDir}");
            }

            // 保存到文件
            var outputPath = Path.Combine(OutputDir, OutputFile);
            await File.WriteAllTextAsync(outputPath, formattedJson);

            Console.WriteLine($"✓ 配置已保存到: {outputPath}");
            Console.WriteLine();

            // 显示统计信息
            var configArray = JsonSerializer.Deserialize<JsonElement>(formattedJson);
            if (configArray.ValueKind == JsonValueKind.Array)
            {
                Console.WriteLine($"配置统计:");
                Console.WriteLine($"  - 平台组数量: {configArray.GetArrayLength()}");
                Console.WriteLine();
                Console.WriteLine("平台列表:");

                int index = 1;
                foreach (var group in configArray.EnumerateArray())
                {
                    if (group.TryGetProperty("Name", out var name))
                    {
                        var itemCount = 0;
                        if (group.TryGetProperty("Items", out var items) && items.ValueKind == JsonValueKind.Array)
                        {
                            itemCount = items.GetArrayLength();
                        }
                        Console.WriteLine($"  {index}. {name.GetString()} ({itemCount} 个加速项目)");
                        index++;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("=== 完成 ===");
            Console.WriteLine();
            Console.WriteLine("下一步:");
            Console.WriteLine("1. 将 config 文件夹上传到你的 GitHub 仓库");
            Console.WriteLine("2. 创建 .github/workflows/deploy.yml 文件");
            Console.WriteLine("3. 在仓库设置中启用 GitHub Pages");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"✗ 网络请求失败: {ex.Message}");
            Console.WriteLine("请检查网络连接或官方服务器是否可访问");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"✗ JSON 解析失败: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ 发生错误: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }

        Console.WriteLine();
        Console.WriteLine("按任意键退出...");
        Console.ReadKey();
    }
}
