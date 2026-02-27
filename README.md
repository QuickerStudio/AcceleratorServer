# AcceleratorServer

Watt Toolkit 自建加速器配置服务器

## 📁 文件说明

- `config/accelerate.json` - 加速器配置文件（从官方服务器抓取）
- `icons/` - 平台图标文件
- `.github/workflows/deploy.yml` - GitHub Actions 自动部署配置
- `tools/` - 配置管理和图标处理工具

## 🚀 使用方法

### 1. 启用 GitHub Pages

1. 进入仓库 **Settings** → **Pages**
2. **Source** 选择 **GitHub Actions**
3. 保存后会自动部署

### 2. 访问配置

部署完成后，配置文件将可通过以下地址访问：

```
https://quickerstudio.github.io/AcceleratorServer/config/accelerate.json
```

### 3. 修改客户端

修改 Watt Toolkit 客户端代码，将 API 地址指向你的服务器。

## 🔄 更新配置

### 完整流程（包含图标）

```bash
cd tools

# 1. 抓取最新配置
cd ConfigFetcher && dotnet run && cd ..

# 2. 下载图标（首次或需要更新时）
cd IconDownloader && dotnet run && cd ..

# 3. 更新配置中的图标 URL
cd IconUpdater && dotnet run && cd ..

# 4. 提交更新
cd ..
git add config/ icons/
git commit -m "Update config and icons"
git push
```

### 仅更新配置（不更新图标）

```bash
cd tools/ConfigFetcher
dotnet run
cd ../..
git add config/accelerate.json
git commit -m "Update config from official server"
git push
```

详细说明请查看 [tools/README.md](tools/README.md)

### 手动编辑

直接编辑 `config/accelerate.json` 文件，提交后会自动部署。

## 📊 配置统计

当前配置包含以下平台：
- Steam 服务
- Epic Games
- Origin
- Uplay
- Battle.net
- Twitch
- Discord
- GitHub
- 等等...

## 🛠️ 自定义配置

你可以编辑 `config/accelerate.json` 来：
- 删除不需要的平台
- 添加新的加速规则
- 修改域名转发规则

## 📄 许可证

本项目仅用于学习和研究目的。
