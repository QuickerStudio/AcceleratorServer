# Tools - 工具集

配置管理和图标处理工具。

## 工具列表

### 1. ConfigFetcher - 配置抓取工具

从官方服务器抓取最新的加速器配置。

**使用方法:**
```bash
cd ConfigFetcher
dotnet run
```

**功能:**
- 从官方 API 获取最新配置
- 自动格式化 JSON
- 保存到 `../config/accelerate.json`
- 显示配置统计信息

---

### 2. IconDownloader - 图标下载工具

自动下载各平台的图标。

**使用方法:**
```bash
cd IconDownloader
dotnet run
```

**功能:**
- 从官方网站下载图标
- 自动保存到 `../icons/` 目录
- 支持多种图片格式
- 显示下载统计

---

### 3. IconUpdater - 图标 URL 更新工具

更新配置文件中的图标 URL。

**使用方法:**
```bash
cd IconUpdater
dotnet run
```

**功能:**
- 读取配置文件
- 更新图标 URL 为 GitHub Pages 地址
- 自动备份原文件
- 显示更新结果

---

## 完整工作流程

### 初次设置

```bash
# 1. 抓取配置
cd ConfigFetcher && dotnet run && cd ..

# 2. 下载图标
cd IconDownloader && dotnet run && cd ..

# 3. 更新图标 URL
cd IconUpdater && dotnet run && cd ..

# 4. 提交到 Git
cd ..
git add config/ icons/
git commit -m "Initial setup with icons"
git push
```

### 更新配置

```bash
# 1. 抓取最新配置
cd ConfigFetcher && dotnet run && cd ..

# 2. 更新图标 URL（如果需要）
cd IconUpdater && dotnet run && cd ..

# 3. 提交更新
cd ..
git add config/accelerate.json
git commit -m "Update config from official server"
git push
```

## 注意事项

- 运行工具前确保在 `tools` 目录下
- 工具会自动创建必要的目录
- 配置文件会自动备份
- 图标下载可能需要一些时间
