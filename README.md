# CDREBIRTH 实习生笔试题 Demo 项目

> Unity 2022.3.34f1 • URP • 轻量 Demo 项目，包含 FPS Controller、HUD Canvas、InfoTerminal 桩代码，预配 Unity MCP。

---

## 快速开始

### 第一步：环境准备

1. **安装 Codex Desktop**（如果你还没有）
   - 下载地址：[Codex](https://codex.openai.com)
   - 注册并登录

2. **安装 Unity CLI**（推荐，可选）
   ```bash
   curl -fsSL https://public-cdn.cloud.unity3d.com/hub/prod/cli/install.sh | UNITY_CLI_CHANNEL=beta bash
   source ~/.zshrc   # 或 source ~/.bashrc
   unity --version   # 确认安装成功
   ```

3. **确保你已安装 Unity 2022.3.34f1**
   - 通过 Unity Hub 安装，或使用 Unity CLI:
     ```bash
     unity install 2022.3.34f1
     ```

4. **了解 AI 工作流基础知识**（花 15 分钟读一遍）
   - [Codex 使用指南](https://platform.openai.com/docs/guides/codex)
   - [Superpowers 工作流](https://github.com/obra/superpowers) — 这是我们的 AI 调度层
   - Unity MCP 是什么：一个 Unity Editor 插件，让 AI 能直接操作场景、创建 GameObjects、写脚本、跑测试
   - [Unity CLI 文档](https://docs.unity.com/en-us/hub/unity-cli)

### 第二步：打开项目

```bash
# Clone 或下载本项目到本地
git clone <这个项目的地址> CDREBIRTH-Demo-Exam
# 或者直接解压

# 用 Unity Hub 打开项目文件夹
# 或者用 Unity CLI:
unity open ./CDREBIRTH-Demo-Exam
```

**重要：第一次打开后，等待 Unity 完成导入（Package Manager 会自动下载 Unity MCP 等依赖）。** 看到 Unity 编辑器主界面，没有 Package 错误弹窗后再继续。

### 第三步：配置 Unity MCP

```bash
cd CDREBIRTH-Demo-Exam
bash tools/agent/setup-unity-mcp.sh
```

脚本会自动检测 MCP server 二进制并配置 `.mcp.json`。

### 第四步：验证连通性

1. 确保 Unity Editor 正在运行，且 Demo 项目已打开
2. 打开 Codex Desktop，切换到本项目的目录
3. 在 Codex 中输入以下验证命令:
   - `ping` — 确认 MCP 连接正常
   - `console-get-logs` — 确认能读取 Unity 控制台
   - `editor-application-get-state` — 确认能读取 Editor 状态

如果以上命令都返回正常结果，工具链就搭建完成了！

---

## 项目结构

```
CDREBIRTH-Demo-Exam/
├── .mcp.json                    # Codex MCP 配置
├── AGENTS.md                    # AI Agent 行为规范
├── README.md                    # 本文件
├── Packages/
│   └── manifest.json            # 依赖: URP, Input System, Unity MCP, ProBuilder MCP
├── ProjectSettings/             # Unity 项目设置
├── tools/agent/
│   ├── setup-unity-mcp.sh       # MCP 配置脚本
│   └── run-unity-mcp.mjs        # MCP 启动桥接
└── Assets/
    ├── Input/
    │   └── PlayerControls.inputactions   # WASD/鼠标/手柄 输入绑定
    ├── Scenes/
    │   └── DemoScene.unity               # 主场景
    ├── Scripts/
    │   ├── PlayerInputHandler.cs         # 输入处理
    │   ├── FirstPersonController.cs      # 第一人称控制器
    │   └── InfoTerminal.cs               # 信息终端桩代码（笔试题目标）
    └── Settings/
        └── URP 管线资产
```

### 场景说明

打开 `Assets/Scenes/DemoScene.unity` 可以看到：

- **地面**: 20×20 的平面，带 Box Collider
- **方向光**: 暖色调主光源，投射阴影
- **玩家**: 挂载了 CharacterController + FirstPersonController + PlayerInputHandler
  - W/A/S/D 移动，Shift 加速跑，空格跳跃，鼠标转视角
  - Tag 为 "Player"
- **终端**: 由两个 Cube 组成（底座 + 倾斜屏幕），挂载了 `InfoTerminal` 脚本
  - 3 米范围内自动触发 HUD 提示显示
- **HUD Canvas**: 包含一个 TextMeshPro 提示文本（默认隐藏）
- **EventSystem**: 挂载 InputSystemUIInputModule，处理键盘/鼠标输入

---

## 核心组件说明

### FirstPersonController
- 使用 `CharacterController` 做移动和碰撞
- 读取 `PlayerInputHandler` 获取输入
- Shift = Run（7m/s），默认 Walk（4m/s）
- 鼠标灵敏度 2，垂直视角限制 ±85°

### PlayerInputHandler
- 封装 `PlayerControls.inputactions`
- 暴露 `Move`（Vector2）、`Look`（Vector2）、`JumpPressed`（bool）、`InteractPressed`（bool）

### InfoTerminal（笔试题目标）
- 继承 MonoBehaviour，自带 SphereCollider（Trigger，3 米半径）
- `OnPlayerEnterRange()` / `OnPlayerExitRange()` 虚方法供扩展
- Inspector 中 `hintText` 字段已连到 HUD 的 HintText GameObject
- 候选人需要扩展此类来实现完整的信息终端交互

---

## 笔试题流程

笔试题的完整说明见面试官提供的 `InternExam_CandidateBrief_2026-05-27.md`，核心流程：

1. **题一（不计分）**: 完成上述环境搭建步骤，验证 MCP 连通
2. **题二（80%）**: 用 Codex agent 驱动 Unity MCP 完成 Info Terminal 功能
3. **题三（20%，选做）**: 设计一个角色概念 或 分析一个系统代码

### 题二详细需求（示例）

> 扩展 `InfoTerminal.cs`，使玩家走近终端时：
> 1. 屏幕材质发光（变成亮蓝色自发光）
> 2. HUD 显示提示文字 "[E] 查看终端"
> 3. 玩家按 E 键后，HUD 切换显示一段信息文本（如 "欢迎来到 CDREBIRTH 项目..."）
> 4. 再次按 E 或走远后，信息文本消失，回到待机状态

**要求**：
- 全程使用 Codex agent 驱动 Unity MCP 完成任务
- 使用 `update_plan` 做任务拆解
- 遇到 bug 使用 systematic debugging（参考 AGENTS.md）
- 代码能在 Unity 中运行，没有编译错误

**提交内容**：
- 可运行的场景截图
- Codex 对话记录（菜单 → Export Chat）
- 简短的复现说明（你是怎么做的、遇到了什么问题、怎么解决的）

---

## 常见问题

**Q: 打开项目后 Package Manager 报错 "Failed to resolve packages"？**
A: 正常的首次导入过程。等待 Unity 自动下载完成（可能需要几分钟）。如果持续报错，检查网络连接，或在 Unity 菜单 `Window > Package Manager` 中手动刷新。

**Q: `setup-unity-mcp.sh` 报 "MCP server binary not found"？**
A: 先打开 Unity 编辑器，在菜单 `Tools > AI Game Developer > Setup/Repair` 中修复，再运行脚本。

**Q: Codex 说 "MCP server not connected"？**
A: 确保 Unity 编辑器正在运行且项目已打开，然后重启 Codex。

**Q: 进入场景后鼠标不能转视角？**
A: 点击 Game 窗口内部，确保焦点在 Unity 内。按 Enter 可以切换光标锁定。

**Q: HintText 显示乱码/方块？**
A: 项目使用 TextMeshPro，首次打开时需要导入 TMP Essential Resources: `Window > TextMeshPro > Import TMP Essential Resources`。

**Q: Unity CLI 安装后 `unity` 命令找不到？**
A: 运行 `source ~/.zshrc`（macOS/Linux）或重新打开终端。
