# Claude Code 配置清单

**用途**：指导另一个环境中的 AI 配置 Claude Code，列出所有配置细项的值和作用

## 使用准则

1. 本文件仅作为检查清单使用，用于核对当前环境的配置是否完整
2. 检查完成后，如果发现配置项有遗漏，可以制定修复方案并询问用户是否执行
3. 如果发现工具、环境依赖有遗漏，仅提示用户需要安装或配置，**不要擅自执行安装操作**
4. 所有修改操作必须经过用户确认后方可执行

---

## 一、Claude Code

### 全局配置文件（~/.claude/目录）

#### 文件：CLAUDE.md - Claude Code

**作用**：全局指令文件，定义 Claude Code 的行为规范
**文件参考地址**：`C:\Users\<用户名>\.claude\CLAUDE.md`

| 细项 | 值 | 作用 | 位置 |
|------|-----|------|------|
| 语言要求 | 所有输出使用中文，代码和变量名保持英文 | 确保 Claude 用中文回复 | 第一节"语言要求" |
| 回复内容 | 使用中文 | 回复用户时用中文 | 语言要求第1条 |
| 思考过程 | 使用中文 | 解释推理时用中文 | 语言要求第2条 |
| 工具调用说明 | 使用中文 | 调用工具前的说明用中文 | 语言要求第3条 |
| 确认提示 | 使用中文 | 询问用户时用中文 | 语言要求第4条 |
| 错误解释 | 使用中文 | 解释错误时用中文 | 语言要求第5条 |
| 进度更新 | 使用中文 | 汇报进度时用中文 | 语言要求第6条 |
| 代码保持英文 | 变量名、命令行指令保持英文 | 代码本身不用中文 | 语言要求第7条 |
| 注释使用中文 | 代码注释用中文 | 注释说明用中文 | 语言要求第8条 |
| 代码风格 | 简洁可读，不添加不必要的注释 | 保持代码质量 | 第二节"代码风格" |
| 变量命名 | 清晰，避免缩写 | 提高可读性 | 代码风格第3条 |
| 工作习惯 | 先阅读再修改，不确定时先确认 | 避免误操作 | 第三节"工作习惯" |
| 保持结构 | 不随意重构 | 维护代码稳定性 | 工作习惯第3条 |
| 工具引用 | @RTK.md | 引入 RTK 使用说明 | 第四节"工具使用" |

---

#### 文件：RTK.md - Claude Code

**作用**：RTK 工具使用说明，被 CLAUDE.md 引用
**文件参考地址**：`C:\Users\<用户名>\.claude\RTK.md`

| 细项 | 值 | 作用 | 位置 |
|------|-----|------|------|
| 工具名称 | RTK - Rust Token Killer | Token 优化代理 | 标题 |
| 用途 | 节省 60-90% 开销 | 优化 CLI 命令的 token 消耗 | 第一行 |
| rtk gain | 显示 token 节省统计 | 查看节省了多少 token | Meta Commands |
| rtk gain --history | 显示命令使用历史 | 查看历史命令的节省情况 | Meta Commands |
| rtk discover | 分析 Claude Code 历史 | 发现错过的优化机会 | Meta Commands |
| rtk proxy <cmd> | 执行原始命令 | 调试时使用 | Meta Commands |
| rtk --version | 显示版本号 | 验证安装 | Installation Verification |
| which rtk | 显示 rtk 路径 | 验证安装位置 | Installation Verification |
| 名称冲突警告 | reachingforthejack/rtk 是另一个工具 | 避免安装错误的 rtk | ⚠️ 注意事项 |
| Hook 使用 | 命令自动重写 | git status → rtk git status | Hook-Based Usage |

---

#### 文件：settings.json - Claude Code
**作用**：主配置文件，包含 API、权限、钩子、状态栏等所有设置
**文件参考地址**：`C:\Users\<用户名>\.claude\settings.json`
##### env 环境变量
**作用**：设置 API 连接和语言环境

| 配置项 | 值 | 作用 | 位置 |
|--------|-----|------|------|
| ANTHROPIC_BASE_URL | **需要填写**（每台电脑不同） | API 端点地址 | env 第1项 |
| ANTHROPIC_AUTH_TOKEN | **需要填写**（每台电脑不同） | API 认证 Token | env 第2项 |
| CLAUDE_CODE_ATTRIBUTION_HEADER | `0` | 归属头信息 | env 第3项 |
| ANTHROPIC_MODEL | **需要填写**（每台电脑不同） | 主模型名称 | env 第4项 |
| ANTHROPIC_DEFAULT_SONNET_MODEL | **需要填写**（每台电脑不同） | Sonnet 模型名称 | env 第5项 |
| ANTHROPIC_DEFAULT_OPUS_MODEL | **需要填写**（每台电脑不同） | Opus 模型名称 | env 第6项 |
| ANTHROPIC_DEFAULT_HAIKU_MODEL | **需要填写**（每台电脑不同） | Haiku 模型名称 | env 第7项 |
| PYTHONIOENCODING | `utf-8` | Python 输出编码 | env 第8项 |
| LC_ALL | `zh_CN.UTF-8` | 系统区域设置 | env 第9项 |
| LANG | `zh_CN.UTF-8` | 系统语言设置 | env 第10项 |

##### permissions 权限配置
**作用**：控制 Claude Code 的权限模式和允许的操作

| 配置项 | 值 | 作用 | 位置 |
|--------|-----|------|------|
| defaultMode | `auto` | 默认权限模式（自动批准） | permissions.defaultMode |
| allow | 3 条规则 | 允许的操作白名单 | permissions.allow |

###### permissions.allow 白名单详情

| 序号 | 规则 | 作用 |
|------|------|------|
| 1 | `Read` | 允许读取文件 |
| 2 | `Glob` | 允许文件模式匹配 |
| 3 | `Grep` | 允许内容搜索 |

##### hooks 钩子配置
**作用**：在工具使用前执行自定义命令

| 配置项 | 值 | 作用 | 位置 |
|--------|-----|------|------|
| 钩子1 匹配器 | `Bash` | 匹配所有 Bash 命令 | hooks.PreToolUse[0].matcher |
| 钩子1 命令 | `C:\Users\<用户名>\.claude\rtk.exe hook claude` | RTK 命令重写（硬编码路径） | hooks.PreToolUse[0].hooks[0].command |
| 钩子2 匹配器 | `Bash` | 匹配所有 Bash 命令 | hooks.PreToolUse[1].matcher |
| 钩子2 命令 | `rtk hook claude` | RTK 命令重写（使用命令名） | hooks.PreToolUse[1].hooks[0].command |

##### 其他配置

| 配置项 | 值 | 作用 | 位置 |
|--------|-----|------|------|
| skipDangerousModePermissionPrompt | `true` | 跳过危险模式提示 | 顶层 |
| statusLine.type | `command` | 状态栏类型 | statusLine |
| statusLine.command | `npx -y ccstatusline-zh@latest` | 状态栏命令 | statusLine |
| statusLine.padding | `0` | 状态栏内边距 | statusLine |

---

## 二、RTK（Rust Token Killer）

### 工具说明 - RTK
**作用**：Token 优化代理，通过重写 Bash 命令节省 60-90% 开销
**发布地址**：https://github.com/rtk-ai/rtk/releases
**安装位置**：`C:\Users\<用户名>\.claude\rtk.exe`（需根据新环境调整）
**配置方式**：无独立配置文件，通过 Claude Code 的 hooks 机制调用
**依赖关系**：
- 被 settings.json 的 hooks.PreToolUse 钩子调用
- 需要在 PATH 中或使用完整路径

---

## 三、外部依赖

### 必需依赖 - 外部依赖

| 依赖项 | 作用 | 是否必需 |
|--------|------|----------|
| RTK（rtk.exe） | Token 优化代理，钩子依赖此工具 | **是** |
| Node.js | 运行 npx 命令需要 | **是** |

### 可选依赖 - 外部依赖

| 依赖项 | 作用 | 是否必需 |
|--------|------|----------|
| ccstatusline-zh | 状态栏工具，通过 npx 自动安装 | **否** |
| chrome-devtools-mcp | Chrome 浏览器开发工具，版本 0.22.0 | **否** |
| docx MCP 服务 | Word 文档处理 | **否** |
| mcp-360chrome MCP 服务 | Chrome 浏览器交互 | **否** |

### MCP 服务配置 - chrome-devtools-mcp
**配置文件**：`C:\Users\<用户名>\.claude\plugins\marketplaces\chrome-devtools-plugins\.mcp.json`
**配置内容**：
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

### MCP 服务配置 - github
**服务名称**：github
**配置方式**：通过 Claude Code 内置配置
**作用**：GitHub 仓库操作、PR 管理、Issue 管理等

### MCP 服务配置 - zhipu-search
**服务名称**：zhipu-search
**配置方式**：通过 Claude Code 内置配置
**作用**：智谱搜索服务，提供网络搜索能力

### Skill 配置
**当前状态**：使用官方插件提供的 skill，无用户自定义 skill
**主要 skill 来源**：
- claude-plugins-official（官方插件）
- chrome-devtools-plugins（Chrome 开发工具插件）
- playwright-cli（浏览器自动化测试工具）

---

## 四、配置项统计

| 工具/文件 | 配置项数量 |
|-----------|------------|
| Claude Code - CLAUDE.md | 14 项 |
| Claude Code - RTK.md | 10 项 |
| Claude Code - settings.json | 33 项 |
| RTK | 0 项（无独立配置） |
| 外部依赖 | 6 项 |
| MCP 服务配置 | 7 项 |
| Skill 配置 | 3 项 |
| **总计** | **73 项** |

---

*备份时间：2026/05/18*
