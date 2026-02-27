# 修复 GitHub Pages 部署路径

## 问题
之前的配置只部署了 `config` 目录，导致访问路径不正确。

## 修改内容

### 1. 更新 `.github/workflows/deploy.yml`
- 从 `path: './config'` 改为 `path: '.'`
- 现在会部署整个仓库（包括 config 和 icons）

### 2. 更新访问路径
正确的访问地址：
```
https://quickerstudio.github.io/AcceleratorServer/config/accelerate.json
```

图标地址：
```
https://quickerstudio.github.io/AcceleratorServer/icons/[图标名].png
```

## 提交更新

```bash
cd c:\Users\Quick\Desktop\AcceleratorServer

git add .github/workflows/deploy.yml README.md STRUCTURE.md
git commit -m "Fix: Update GitHub Pages deployment path"
git push
```

等待 GitHub Actions 重新部署后，配置文件就可以正常访问了！
