# Claude Code 版本清单

**用途**：记录各插件、MCP 服务、Skill 的版本号，用于环境对齐参考

---

## 核心工具

| 工具 | 版本 | 安装方式 | 备注 |
|------|------|----------|------|
| Node.js | v22.22.2 | 系统安装 | 运行环境 |
| npm | 10.9.7 | 随 Node.js | 包管理器 |
| RTK (Rust Token Killer) | 0.40.0 | `C:\Users\<用户名>\.claude\rtk.exe` | Token 优化代理 |

## 插件

| 插件名称 | 安装版本 | npm 包版本 | 标识 | 安装方式 |
|----------|----------|------------|------|----------|
| ccstatusline-zh | v2.2.19 | v2.2.19 | `ccstatusline-zh` | `npm install -g ccstatusline-zh` |
| chrome-devtools-mcp | 1.0.1 | 0.22.0 | `chrome-devtools-mcp@claude-plugins-official` | Claude Code 插件市场 |

## MCP 服务

| 服务名称 | 配置方式 | 状态 | 备注 |
|----------|----------|------|------|
| github | Claude Code 内置 | 已配置 | GitHub 仓库操作 |
| zhipu-search | Claude Code 内置 | 已配置 | 智谱搜索服务 |

> marketplace 目录中存在以下官方模板，但未实际启用：
> asana、context7、discord、fakechat、firebase、gitlab、greptile、imessage、laravel-boost、linear、playwright、serena、telegram、terraform

## Skill

| Skill 名称 | 来源 | 触发方式 | 备注 |
|------------|------|----------|------|
| graphify | 自定义（~/.claude/skills/graphify） | `/graphify` | 知识图谱工具 |
| docx | .cc-switch/skills（符号链接） | `/docx` | Word 文档处理 |
| html-ppt | .cc-switch/skills（符号链接） | `/html-ppt` | HTML 演示文稿 |
| pptx | .cc-switch/skills（符号链接） | `/pptx` | PowerPoint 处理 |
| xlsx | .cc-switch/skills（符号链接） | `/xlsx` | Excel 电子表格 |
| playwright-cli | .cc-switch/skills（符号链接） | `/playwright-cli` | 浏览器自动化测试 |
| frontend-design | .cc-switch/skills（符号链接） | `/frontend-design` | 前端界面设计 |
| simplify | 内置 | `/simplify` | 代码简化，审查 diff 并修复 |

## ccstatusline-zh 组件版本

| 功能 | 最低 Claude Code 版本 | 说明 |
|------|----------------------|------|
| refreshInterval | ≥ 2.1.97 | 状态栏刷新间隔配置 |
| 全部 72 个组件 | — | 含 Voice Status、超额用量、Jujutsu VCS 等 |

---

*记录时间：2026/05/21*
