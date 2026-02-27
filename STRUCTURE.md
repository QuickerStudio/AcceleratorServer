# AcceleratorServer 完整目录结构

```
AcceleratorServer/
├── .github/
│   └── workflows/
│       └── deploy.yml                      # GitHub Actions 自动部署
│
├── config/
│   ├── accelerate.json                     # 加速器配置文件
│   └── accelerate.json.backup              # 配置备份（自动生成）
│
├── icons/
│   ├── README.md                           # 图标说明
│   ├── steam_服务.png                      # Steam 图标
│   ├── github.png                          # GitHub 图标
│   ├── twitch_直播.png                     # Twitch 图标
│   └── ...                                 # 其他平台图标
│
├── tools/
│   ├── README.md                           # 工具说明
│   │
│   ├── ConfigFetcher/                      # 配置抓取工具
│   │   ├── Program.cs
│   │   └── ConfigFetcher.csproj
│   │
│   ├── IconDownloader/                     # 图标下载工具
│   │   ├── Program.cs
│   │   └── IconDownloader.csproj
│   │
│   └── IconUpdater/                        # 图标 URL 更新工具
│       ├── Program.cs
│       └── IconUpdater.csproj
│
├── .gitignore                               # Git 忽略文件
└── README.md                                # 主说明文档
```

## 文件说明

### 配置文件
- `config/accelerate.json` - 主配置文件，包含所有加速平台的规则
- `config/accelerate.json.backup` - 自动备份文件（更新时生成）

### 图标文件
- `icons/*.png` - 各平台的图标文件
- 图标会通过 GitHub Pages 提供访问

### 工具程序
- `ConfigFetcher` - 从官方服务器抓取最新配置
- `IconDownloader` - 自动下载各平台图标
- `IconUpdater` - 更新配置中的图标 URL

### 部署配置
- `.github/workflows/deploy.yml` - GitHub Actions 工作流
- 自动将 config 和 icons 部署到 GitHub Pages

## 访问地址

部署后可通过以下地址访问：

- **配置文件**: `https://quickerstudio.github.io/AcceleratorServer/accelerate.json`
- **图标文件**: `https://quickerstudio.github.io/AcceleratorServer/icons/[图标名].png`

## 工作流程

### 初次设置
1. 运行 ConfigFetcher 抓取配置
2. 运行 IconDownloader 下载图标
3. 运行 IconUpdater 更新图标 URL
4. 提交到 GitHub
5. GitHub Actions 自动部署

### 日常更新
1. 运行 ConfigFetcher 抓取最新配置
2. （可选）运行 IconUpdater 更新图标 URL
3. 提交更新
4. 自动部署
