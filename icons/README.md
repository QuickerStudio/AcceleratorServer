# Icons 目录

存放各平台的图标文件。

## 图标列表

- `steam_服务.png` - Steam 平台
- `twitch_直播.png` - Twitch 直播
- `origin.png` - Origin (EA)
- `uplay.png` - Uplay (Ubisoft)
- `github.png` - GitHub
- `nexus_mods.png` - Nexus Mods
- `公共_cdn.png` - 公共 CDN
- `国外验证码平台.png` - 验证码平台
- `网盘服务.png` - 网盘服务
- `其它网站.png` - 其它网站

## 图标规格

- 推荐尺寸: 64x64 或 128x128 像素
- 格式: PNG (支持透明背景)
- 文件大小: 建议小于 50KB

## 使用方法

### 1. 下载图标

运行 IconDownloader 自动下载图标：

```bash
cd tools
dotnet run --project IconDownloader.csproj
```

### 2. 手动添加图标

将图标文件放入此目录，文件名格式：`平台名称.png`

### 3. 更新配置

运行 IconUpdater 更新配置文件中的图标 URL：

```bash
cd tools
dotnet run --project IconUpdater.csproj
```

## 图标来源

- 官方网站 favicon
- 品牌资源包
- 开源图标库

## 注意事项

- 确保图标使用权限
- 保持图标清晰度
- 使用透明背景
