using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 图标下载工具 - 为各个平台下载图标
/// </summary>
class IconDownloader
{
    private const string IconsDir = "../../icons";

    // 平台图标 URL 映射
    private static readonly Dictionary<string, string> PlatformIcons = new()
    {
        // Steam
        { "Steam 服务", "https://cdn.cloudflare.steamstatic.com/store/home/store_icon.png" },
        { "Steam", "https://cdn.cloudflare.steamstatic.com/store/home/store_icon.png" },

        // Epic Games
        { "Epic Games", "https://cdn2.unrealengine.com/Epic+Games+Node%2Fxlarge_whitetext_blackback_epiclogo_504x512_1529964470588-503x512-ac795e81c54b27aaa2e196456dd307bfe4ca3ca4.jpg" },

        // Origin
        { "Origin", "https://www.ea.com/assets/images/ea-app-icon.png" },

        // Uplay
        { "Uplay", "https://ubistatic-a.akamaihd.net/orbit/uplay_launcher_3_0/assets/images/logo.png" },

        // Battle.net
        { "Battle.net", "https://www.blizzard.com/static/_images/logos/battlenet-logo.png" },

        // Twitch
        { "Twitch 直播", "https://static.twitchcdn.net/assets/favicon-32-e29e246c157142c94346.png" },
        { "Twitch", "https://static.twitchcdn.net/assets/favicon-32-e29e246c157142c94346.png" },

        // Discord
        { "Discord", "https://discord.com/assets/847541504914fd33810e70a0ea73177e.ico" },

        // GitHub
        { "Github", "https://github.githubassets.com/favicons/favicon.png" },

        // Spotify
        { "Spotify", "https://www.scdn.co/i/_global/favicon.png" },

        // mod.io
        { "mod.io", "https://mod.io/favicon.ico" },

        // Nexus Mods
        { "Nexus Mods", "https://www.nexusmods.com/favicon.ico" },

        // Roblox
        { "Roblox", "https://www.roblox.com/favicon.ico" },

        // 公共 CDN
        { "公共 CDN", "https://www.cloudflare.com/favicon.ico" },

        // 国外验证码平台
        { "国外验证码平台", "https://www.google.com/favicon.ico" },

        // 网盘服务
        { "网盘服务", "https://onedrive.live.com/favicon.ico" },

        // 其它网站
        { "其它网站", "https://www.google.com/favicon.ico" },
    };

    static async Task Main(string[] args)
    {
        Console.WriteLine("=== 图标下载工具 ===");
        Console.WriteLine();

        // 创建图标目录
        if (!Directory.Exists(IconsDir))
        {
            Directory.CreateDirectory(IconsDir);
            Console.WriteLine($"✓ 创建目录: {IconsDir}");
        }

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");

        int successCount = 0;
        int failCount = 0;

        foreach (var (platformName, iconUrl) in PlatformIcons)
        {
            try
            {
                Console.Write($"下载 {platformName} 图标... ");

                var response = await httpClient.GetAsync(iconUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();

                    // 根据 Content-Type 确定文件扩展名
                    var contentType = response.Content.Headers.ContentType?.MediaType ?? "image/png";
                    var extension = contentType switch
                    {
                        "image/png" => ".png",
                        "image/jpeg" => ".jpg",
                        "image/x-icon" => ".ico",
                        "image/svg+xml" => ".svg",
                        _ => ".png"
                    };

                    // 生成文件名（移除特殊字符）
                    var fileName = platformName
                        .Replace(" ", "_")
                        .Replace(".", "")
                        .Replace("/", "_")
                        .ToLower();

                    var filePath = Path.Combine(IconsDir, fileName + extension);
                    await File.WriteAllBytesAsync(filePath, content);

                    Console.WriteLine($"✓ ({content.Length} bytes)");
                    successCount++;
                }
                else
                {
                    Console.WriteLine($"✗ HTTP {response.StatusCode}");
                    failCount++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ {ex.Message}");
                failCount++;
            }

            // 避免请求过快
            await Task.Delay(500);
        }

        Console.WriteLine();
        Console.WriteLine($"=== 完成 ===");
        Console.WriteLine($"成功: {successCount}, 失败: {failCount}");
        Console.WriteLine();
        Console.WriteLine("下一步:");
        Console.WriteLine("1. 检查 icons 目录中的图标");
        Console.WriteLine("2. 运行 IconUpdater 更新配置文件中的图标 URL");
        Console.WriteLine("3. 提交到 Git 仓库");
    }
}
