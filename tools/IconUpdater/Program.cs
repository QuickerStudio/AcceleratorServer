using System;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 图标 URL 更新工具 - 更新配置文件中的图标 URL
/// </summary>
class IconUpdater
{
    private const string ConfigFile = "../../config/accelerate.json";
    private const string IconsBaseUrl = "https://quickerstudio.github.io/AcceleratorServer/icons/";

    // 平台名称到图标文件名的映射
    private static readonly Dictionary<string, string> PlatformIconFiles = new()
    {
        { "Steam 服务", "steam_服务.png" },
        { "Twitch 直播", "twitch_直播.png" },
        { "Origin", "origin.png" },
        { "Uplay", "uplay.png" },
        { "公共 CDN", "公共_cdn.png" },
        { "国外验证码平台", "国外验证码平台.png" },
        { "Github", "github.png" },
        { "Nexus Mods", "nexus_mods.png" },
        { "网盘服务", "网盘服务.png" },
        { "其它网站", "其它网站.png" },
    };

    static void Main(string[] args)
    {
        Console.WriteLine("=== 图标 URL 更新工具 ===");
        Console.WriteLine();

        if (!File.Exists(ConfigFile))
        {
            Console.WriteLine($"✗ 配置文件不存在: {ConfigFile}");
            return;
        }

        try
        {
            // 读取配置文件
            var jsonString = File.ReadAllText(ConfigFile);
            Console.WriteLine("✓ 读取配置文件");

            // 解析 JSON
            using var jsonDoc = JsonDocument.Parse(jsonString);
            var root = jsonDoc.RootElement;

            // 获取平台数组（字段 🦓）
            if (!root.TryGetProperty("\uD83E\uDD93", out var platformsArray))
            {
                Console.WriteLine("✗ 无法找到平台数组");
                return;
            }

            Console.WriteLine($"✓ 找到 {platformsArray.GetArrayLength()} 个平台组");
            Console.WriteLine();

            // 手动构建更新后的 JSON
            var updatedJson = UpdateIconUrls(jsonString);

            // 保存备份
            var backupFile = ConfigFile + ".backup";
            File.Copy(ConfigFile, backupFile, true);
            Console.WriteLine($"✓ 备份原文件到: {backupFile}");

            // 保存更新后的文件
            File.WriteAllText(ConfigFile, updatedJson);
            Console.WriteLine($"✓ 更新配置文件");

            Console.WriteLine();
            Console.WriteLine("=== 完成 ===");
            Console.WriteLine();
            Console.WriteLine("已更新的平台:");
            foreach (var (platform, iconFile) in PlatformIconFiles)
            {
                Console.WriteLine($"  - {platform} → {IconsBaseUrl}{iconFile}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ 发生错误: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }

    static string UpdateIconUrls(string jsonString)
    {
        // 使用正则表达式更新图标 URL
        // 查找模式: "0":"平台名称"...后面的 "2":"xxx"

        foreach (var (platformName, iconFile) in PlatformIconFiles)
        {
            // 匹配平台组的模式
            // "0":"Steam 服务","1":[...],"2":"xxx"
            var pattern = $@"(""0"":""{Regex.Escape(platformName)}"".*?""2"":\s*"")[^""]*("")";
            var replacement = $"$1{IconsBaseUrl}{iconFile}$2";

            jsonString = Regex.Replace(jsonString, pattern, replacement, RegexOptions.Singleline);
        }

        return jsonString;
    }
}
