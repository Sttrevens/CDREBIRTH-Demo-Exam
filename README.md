# CDREBIRTH Demo Exam Project

Lightweight Unity project for the intern technical exam. Includes a 3D character with animations, FPS controller, and Unity MCP tooling.

## Prerequisites

- **Unity Hub** + **Unity 2022.3.34f1** with URP template
- **Node.js 20.19+ or 22.12+**
- **Codex Desktop** or **Codex CLI**
- **unity-mcp-cli**: `npm install -g unity-mcp-cli`

## Tool Installation

Install the tools below before starting the exam.

### Unity Editor / Unity CLI

- Install Unity Hub: <https://unity.com/unity-hub>
- Install the required editor version: **Unity 2022.3.34f1**
- Unity Hub documentation: <https://docs.unity.com/hub>
- Unity Hub CLI documentation: <https://docs.unity.com/hub/unity-cli>

Recommended CLI path after installing `unity-mcp-cli`:

```bash
unity-mcp-cli install-unity 2022.3.34f1
```

Manual fallback:

1. Open Unity Hub.
2. Go to **Installs**.
3. Install **Unity 2022.3.34f1** with the required modules for your platform.

Launch the project from the command line after Unity is installed:

```bash
/Applications/Unity/Hub/Editor/2022.3.34f1/Unity.app/Contents/MacOS/Unity \
  -projectPath /path/to/CDREBIRTH-Demo-Exam
```

### Unity MCP CLI and MCP Plugin

- Unity MCP repository: <https://github.com/IvanMurzak/Unity-MCP>
- Unity MCP installation guide: <https://github.com/IvanMurzak/Unity-MCP/wiki/Installation-Guide>
- CLI package: <https://www.npmjs.com/package/unity-mcp-cli>
- Node.js download: <https://nodejs.org/>

Install the CLI:

```bash
npm install -g unity-mcp-cli
unity-mcp-cli --version
```

This exam project already includes the Unity MCP package in `Packages/manifest.json`. If you are setting up another Unity project from scratch, install the plugin from the project folder:

```bash
unity-mcp-cli install-plugin .
```

## Quick Start

1. Clone and open in Unity (2022.3.34f1):
   ```bash
   git clone https://github.com/Sttrevens/CDREBIRTH-Demo-Exam.git
   ```

2. Wait for Unity to finish importing packages (no red errors in Console).

3. Start MCP server:
   ```bash
   ./Library/mcp-server/osx-arm64/unity-mcp-server \
     port=22399 plugin-timeout=10000 client-transport=streamableHttp authorization=none
   ```

4. Connect CLI:
   ```bash
   unity-mcp-cli bootstrap-local --url http://localhost:22399 --token "" .
   ```

5. Verify:
   ```bash
   unity-mcp-cli run-tool ping --url http://localhost:22399 --token "" --input '{"message":"hello"}'
   ```

6. Open this folder in Codex and ask it to verify the setup with `console-get-logs`.

## Project Structure

```
CDREBIRTH-Demo-Exam/
├── Assets/
│   ├── Art/             # Character models, animations, materials
│   ├── Input/           # (reserved for custom input assets)
│   ├── Scenes/          # DemoScene.unity
│   └── Scripts/         # Player 3C, InfoTerminal stub
├── Packages/            # URP + Unity MCP + ProBuilder
├── tools/agent/         # MCP bridge scripts
├── .mcp.json
├── AGENTS.md
└── README.md
```

## Scene Contents
- **Directional Light** — URP directional light with soft shadows
- **Ground** — Scaled plane (20×20) with URP Lit material
- **Player** — CharacterController + SP3C + Character model with Animator
  - WASD movement, Shift to run, Space to jump, mouse look
  - Reuses CDREBIRTH's character model and animations
- **HUD Canvas** — HintText for proximity interaction
- **TerminalStand + TerminalScreen** — Info terminal with trigger zone (exam task)
- **EventSystem** — Required for input

## Exam Tasks (choose one)

### Task A — Enemy AI
Design and implement an enemy character. Requirements:
- At least two behavioral states (patrol / chase / attack / idle) that switch based on game logic
- Must NOT be simple linear chase — use state machine, NavMesh, or custom logic
- Bonus: drive animations through the Animator state machine

### Task B — Rules-Driven Mini-Game
Design a playable mini-game within the demo scene. Requirements:
- Clear rules, win/lose conditions, and player feedback (UI, sound, visual)
- Examples: collection/score system, dodge/survive, timed challenge, simple puzzle
- Minimum viable — it must be playable start to finish

### Task C — Visual Showcase
Create a striking visual effect. Options:
- Particle system (VFX Graph or Shuriken) — fire, light beams, rain, etc.
- Custom shader via Shader Graph — dissolve, hologram, glow, distortion
- Post-processing stack setup with intentional artistic direction
- Must demonstrate deliberate visual intent, not just dragging defaults

## Workflow Requirements (all tasks)
- Use Codex to drive Unity MCP for the entire task
- Use plan tool for task decomposition
- Use systematic-debugging when encountering errors
- Code follows Unity C# conventions
- Verify your work before submission (screenshots, scene test)

## Submission
- Working scene screenshot(s)
- Full Codex conversation log
- Brief write-up explaining your approach
