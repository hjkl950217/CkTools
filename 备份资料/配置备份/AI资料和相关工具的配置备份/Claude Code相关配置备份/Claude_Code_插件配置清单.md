# Claude Code 插件配置清单

**用途**：记录 Claude Code 各插件的详细配置，便于在新环境中复现

---

## 使用准则

1. 本文件仅作为检查清单使用，用于核对当前环境的插件配置是否完整
2. 检查完成后，如果发现配置项有遗漏，可以制定修复方案并询问用户是否执行
3. 如果发现工具、环境依赖有遗漏，仅提示用户需要安装或配置，**不要擅自执行安装操作**
4. 所有修改操作必须经过用户确认后方可执行

---

## 一、ccstatusline-zh（状态栏插件）

**项目地址**：https://github.com/huangguang1999/ccstatusline-zh
**上游项目**：https://github.com/sirmalloc/ccstatusline
**版本**：→ 参见 [版本清单](Claude_Code_版本清单.md)
**安装方式**：`npm install -g ccstatusline-zh`
**配置界面**：`ccstatusline-zh setup`（交互式 TUI）

### settings.json 中的 statusLine 配置

**配置位置**：`C:\Users\<用户名>\.claude\settings.json` → `statusLine` 字段

| 配置项 | 值 | 作用 |
|--------|-----|------|
| type | `command` | 状态栏由外部命令驱动 |
| command | `npx -y ccstatusline-zh@latest` | 通过 npx 拉取最新版执行 |
| padding | `0` | 状态栏无额外内边距 |

> 备选 command 值：
> - `ccstatusline-zh`（全局安装后直接调用，启动更快）
> - `bunx -y ccstatusline-zh@latest`

### 插件配置文件

**配置位置**：`C:\Users\<用户名>\.config\ccstatusline\settings.json`
**配置版本**：version 3

#### 状态栏布局（lines）

当前配置为 **2 行布局 + 1 行空行**：

**第 1 行**：模型 + 上下文 + Git 信息

| 顺序 | 组件 ID | 类型 | 颜色 | 作用 |
|------|---------|------|------|------|
| 1 | 1 | `model` | cyan | 显示当前 Claude 模型名称 |
| 2 | 2 | `separator` | — | 分隔符 |
| 3 | 3 | `context-length` | brightBlack | 显示当前上下文 Token 数 |
| 4 | 4 | `separator` | — | 分隔符 |
| 5 | 5 | `git-branch` | magenta | 显示当前 Git 分支名 |
| 6 | 6 | `separator` | — | 分隔符 |
| 7 | 7 | `git-changes` | yellow | 显示未提交的文件变更统计 |

**第 2 行**：Token 用量 + 速度

| 顺序 | 组件 ID | 类型 | 自定义文本 | 作用 |
|------|---------|------|------------|------|
| 1 | 7e10a4fa... | `tokens-input` | — | 显示输入 Token 数量 |
| 2 | efd54c9a... | `custom-text` | ` \| ` | 分隔符文本 |
| 3 | a9b7cd5f... | `tokens-cached` | — | 显示缓存 Token 数量 |
| 4 | 28863e34... | `custom-text` | ` \| ` | 分隔符文本 |
| 5 | b0371079... | `tokens-output` | — | 显示输出 Token 数量 |
| 6 | a97d1915... | `custom-text` | ` \| ` | 分隔符文本 |
| 7 | 1d962898... | `tokens-total` | — | 显示 Token 合计 |
| 8 | 86066bc3... | `custom-text` | ` \| ` | 分隔符文本 |
| 9 | 8584f767... | `total-speed` | — | 显示总 Token 速度 (tok/s) |

**第 3 行**：空行（预留）

#### 全局样式配置

| 配置项 | 值 | 作用 |
|--------|-----|------|
| flexMode | `full-minus-40` | 弹性分隔符模式（总宽度减 40） |
| compactThreshold | `60` | 紧凑模式阈值（终端宽度 < 60 时启用） |
| colorLevel | `2` | 颜色级别（2 = 256 色） |
| inheritSeparatorColors | `false` | 分隔符不继承前后组件颜色 |
| globalBold | `false` | 全局不加粗 |
| gitCacheTtlSeconds | `5` | Git 信息缓存 TTL（5 秒） |
| minimalistMode | `false` | 极简模式关闭（显示标签） |

#### Powerline 配置

| 配置项 | 值 | 作用 |
|--------|-----|------|
| enabled | `false` | Powerline 模式关闭 |
| separators | `[""]` | 分隔符字符 |
| separatorInvertBackground | `[false]` | 分隔符不反转背景色 |
| startCaps | `[]` | 无起始端帽 |
| endCaps | `[]` | 无结束端帽 |
| autoAlign | `false` | 不自动对齐 |
| continueThemeAcrossLines | `false` | 跨行不延续主题色 |

### 可用组件参考

<details>
<summary>点击展开全部 72 个可用组件</summary>

#### 核心组件

| 组件 | type ID | 说明 |
|------|---------|------|
| 模型 | `model` | 显示当前 Claude 模型名称 |
| 风格 | `style` | 显示当前输出风格 |
| 版本 | `version` | 显示 ccstatusline-zh 版本号 |
| 思考力度 | `thinking-level` | 显示当前思考力度等级 |
| Vim 模式 | `vim-mode` | 显示当前 Vim 模式 |
| 语音状态 | `voice-status` | 显示语音输入是否启用 |

#### Git 组件

| 组件 | type ID | 说明 |
|------|---------|------|
| Git 分支 | `git-branch` | 当前 Git 分支名，支持 GitHub 链接 |
| Git PR | `git-pr` | 当前分支的 PR 信息 |
| Git 变更 | `git-changes` | 未提交的文件变更统计 |
| Git 新增 | `git-additions` | 未提交的新增行数 |
| Git 删除 | `git-deletions` | 未提交的删除行数 |
| Git 状态 | `git-status` | 汇总状态指示 |
| Git 已暂存 | `git-staged` | 存在已暂存变更时显示 + |
| Git 未暂存 | `git-unstaged` | 存在未暂存变更时显示 * |
| Git 未跟踪 | `git-untracked` | 存在未跟踪文件时显示 ? |
| Git 冲突 | `git-conflicts` | 显示合并冲突数量 |
| Git 超前/滞后 | `git-ahead-behind` | 相对 upstream 的提交领先/落后数 |
| Git SHA | `git-sha` | 简短提交哈希 |
| Git Origin 所有者 | `git-origin-owner` | origin 远程的 owner |
| Git Origin 仓库 | `git-origin-repo` | origin 远程的 repo |
| Git Upstream 所有者 | `git-upstream-owner` | upstream 远程的 owner |
| Git Upstream 仓库 | `git-upstream-repo` | upstream 远程的 repo |
| Git 是否 Fork | `git-is-fork` | 仓库是 fork 时显示标识 |
| Git 根目录 | `git-root` | Git 仓库根目录名 |
| Git 工作树 | `git-worktree` | Git 工作树信息 |
| Git 工作树模式 | `git-worktree-mode` | 工作树模式指示 |
| Git 工作树名称 | `git-worktree-name` | 工作树名称 |
| Git 工作树分支 | `git-worktree-branch` | 工作树分支 |

#### Token 组件

| 组件 | type ID | 说明 |
|------|---------|------|
| 输入 Token | `tokens-input` | 输入 Token 数量 |
| 输出 Token | `tokens-output` | 输出 Token 数量 |
| 缓存 Token | `tokens-cached` | 缓存 Token 数量 |
| 总 Token | `tokens-total` | Token 合计 |

#### Token 速度组件

| 组件 | type ID | 说明 |
|------|---------|------|
| 输入速度 | `input-speed` | 输入 Token 速度 (tok/s) |
| 输出速度 | `output-speed` | 输出 Token 速度 (tok/s) |
| 总速度 | `total-speed` | 总 Token 速度 (tok/s) |

#### 上下文组件

| 组件 | type ID | 说明 |
|------|---------|------|
| 上下文长度 | `context-length` | 当前上下文 Token 数 |
| 上下文 % | `context-percent` | 上下文使用百分比 |
| 上下文 %（可用） | `context-percent-available` | 可用上下文百分比 |
| 上下文进度条 | `context-progress-bar` | 进度条形式显示上下文用量 |

#### 会话组件

| 组件 | type ID | 说明 |
|------|---------|------|
| 会话时钟 | `session-clock` | 当前会话持续时间 |
| 会话费用 | `session-cost` | 当前会话预估费用 |
| 会话名称 | `session-name` | Claude Code 会话名称 |
| 会话用量 | `session-usage` | 会话 API 用量 |
| 周用量 | `weekly-usage` | 本周 API 用量 |
| 周 Sonnet 用量 | `weekly-sonnet-usage` | 本周 Sonnet 模型 API 用量 |
| 周 Opus 用量 | `weekly-opus-usage` | 本周 Opus 模型 API 用量 |
| 超额用量占比 | `overage-usage-percent` | 超额用量占比 |
| 超额用量剩余 | `overage-usage-remaining` | 每月超额额度剩余金额 |
| 时段计时器 | `period-timer` | 当前 5 小时时段已用时间 |
| 时段重置计时 | `period-reset-timer` | 时段重置窗口剩余时间 |
| 周重置计时 | `weekly-reset-timer` | 周重置剩余时间 |
| Claude 会话 ID | `session-id` | 当前 Claude 会话 ID |
| Claude 账户邮箱 | `account-email` | 当前登录的 Claude 账户邮箱 |
| 技能 | `skill` | Claude Code 技能调用信息 |

#### 环境组件

| 组件 | type ID | 说明 |
|------|---------|------|
| 当前目录 | `cwd` | 当前工作目录 |
| 终端宽度 | `terminal-width` | 终端列数 |
| 内存用量 | `memory-usage` | 系统内存使用情况 |

#### 自定义组件

| 组件 | type ID | 说明 |
|------|---------|------|
| 自定义文本 | `custom-text` | 用户自定义文本 |
| 自定义命令 | `custom-command` | 执行 Shell 命令并显示输出 |
| 自定义符号 | `custom-symbol` | 自定义单字符符号或 Emoji |
| 链接 | `link` | 可点击的终端超链接 |

#### 布局组件

| 组件 | type ID | 说明 |
|------|---------|------|
| 分隔符 | `separator` | 组件之间的固定分隔符 |
| 弹性分隔符 | `flex-separator` | 自动填充剩余空间的弹性分隔符 |

</details>

### TUI 快捷键

| 按键 | 功能 |
|------|------|
| `↑` `↓` | 导航 |
| `Enter` | 选择/确认 |
| `a` | 添加组件 |
| `d` | 删除组件 |
| `e` | 编辑组件 |
| `w` | 组件选项 |
| `/` | 搜索 |
| `q` | 退出 |

---

## 二、chrome-devtools-mcp（Chrome 开发工具插件）

**安装方式**：通过 Claude Code 插件市场安装
**插件标识**：`chrome-devtools-mcp@claude-plugins-official`
**版本**：→ 参见 [版本清单](Claude_Code_版本清单.md)

### MCP 配置

**配置位置**：`C:\Users\<用户名>\.claude\plugins\marketplaces\chrome-devtools-plugins\.mcp.json`

```json
{
  "mcpServers": {
    "chrome-devtools": {
      "command": "npx",
      "args": ["chrome-devtools-mcp@latest", "--browserUrl", "http://127.0.0.1:9222"]
    }
  }
}
```

| 配置项 | 值 | 作用 |
|--------|-----|------|
| command | `npx` | 使用 npx 执行 |
| args[0] | `chrome-devtools-mcp@latest` | 插件包名 |
| args[1] | `--browserUrl` | 浏览器调试 URL 参数 |
| args[2] | `http://127.0.0.1:9222` | Chrome DevTools 调试端口 |

### 前置条件

- Chrome 浏览器需以 `--remote-debugging-port=9222` 启动
- 或使用 Chrome DevTools Protocol 连接

---

## 三、RTK - Rust Token Killer（Token 优化钩子）

**发布地址**：https://github.com/rtk-ai/rtk/releases
**安装位置**：`C:\Users\<用户名>\.claude\rtk.exe`
**版本**：→ 参见 [版本清单](Claude_Code_版本清单.md)
**配置方式**：无独立配置文件，通过 Claude Code 的 hooks 机制调用

### hooks 配置

**配置位置**：`C:\Users\<用户名>\.claude\settings.json` → `hooks.PreToolUse`

| 配置项 | 值 | 作用 |
|--------|-----|------|
| 钩子1 匹配器 | `Bash` | 匹配所有 Bash 命令 |
| 钩子1 命令 | `C:\Users\<用户名>\.claude\rtk.exe hook claude` | RTK 命令重写（硬编码路径） |
| 钩子2 匹配器 | `Bash` | 匹配所有 Bash 命令 |
| 钩子2 命令 | `rtk hook claude` | RTK 命令重写（使用命令名） |

### RTK 元命令

| 命令 | 作用 |
|------|------|
| `rtk gain` | 显示 token 节省统计 |
| `rtk gain --history` | 显示命令使用历史 |
| `rtk discover` | 分析 Claude Code 历史发现优化机会 |
| `rtk proxy <cmd>` | 执行原始命令（调试用） |
| `rtk --version` | 显示版本号 |

---

## 四、MCP 服务配置

### github

**配置方式**：通过 Claude Code 内置配置
**作用**：GitHub 仓库操作、PR 管理、Issue 管理等

### zhipu-search

**配置方式**：通过 Claude Code 内置配置
**作用**：智谱搜索服务，提供网络搜索能力

---

## 五、配置项统计

| 插件/工具 | 配置项数量 | 备注 |
|-----------|------------|------|
| ccstatusline-zh | 33 项 | 含布局 9 组件 + 样式 7 项 + Powerline 7 项 + statusLine 3 项 + TUI 配置 |
| chrome-devtools-mcp | 4 项 | MCP 配置 |
| RTK | 6 项 | hooks 配置 + 元命令 + 版本信息 |
| MCP 服务 | 2 项 | github + zhipu-search |
| **总计** | **45 项** | |

---

*备份时间：2026/05/21*
*环境检查更新：2026/05/21*
