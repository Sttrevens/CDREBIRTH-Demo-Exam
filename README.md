# CDREBIRTH Demo Exam Project

Lightweight Unity project for the intern technical exam. No Photon Fusion, no full gameplay — just a simple FPS controller, a demo scene, and Unity MCP tooling.

## Prerequisites

- **Unity Hub** + **Unity 2022.3.34f1** with URP template
- **Node.js 18+** (for running the MCP bridge script)
- **Codex Desktop** or **Codex CLI** installed
- **unity-mcp-cli**: `brew install unity-mcp-cli` or `npm install -g unity-mcp-cli`

## Quick Start

1. Clone and open in Unity:
   ```bash
   git clone <this-repo-url>
   cd CDREBIRTH-Demo-Exam
   ```

2. Open the project in Unity Hub (Editor 2022.3.34f1).
   - Unity will import packages automatically.
   - Wait for the import to finish (no red errors in Console).

3. Start the Unity MCP server:
   ```bash
   /path/to/project/Library/mcp-server/osx-arm64/unity-mcp-server \
     port=22399 plugin-timeout=10000 client-transport=streamableHttp authorization=none
   ```

4. Connect via unity-mcp-cli:
   ```bash
   unity-mcp-cli bootstrap-local --url http://localhost:22399 --token "" /path/to/project
   ```

5. Verify:
   ```bash
   unity-mcp-cli run-tool ping --url http://localhost:22399 --token "" --input '{"message":"hello"}'
   ```

## Working with Codex

Open this project folder in Codex Desktop or use Codex CLI. Codex will discover `.mcp.json` and connect to the Unity MCP server automatically.

See [AGENTS.md](AGENTS.md) for the Codex workflow conventions expected during the exam.

## Project Structure

```
CDREBIRTH-Demo-Exam/
├── Assets/
│   ├── Input/           # PlayerControls.inputactions
│   ├── Scenes/          # DemoScene.unity (start scene)
│   └── Scripts/         # FirstPersonController, PlayerInputHandler, InfoTerminal
├── Packages/            # manifest.json with URP + Unity MCP
├── tools/agent/         # setup-unity-mcp.sh, run-unity-mcp.mjs
├── .mcp.json            # Codex MCP server config
├── AGENTS.md            # Agent workflow rules
└── README.md            # This file
```

## Scene Contents

- **Directional Light** — URP directional light with soft shadows
- **Ground** — Scaled plane (20×20) with URP Lit material
- **Player** — FPS character controller (WASD + mouse look, jump, run)
- **HUD Canvas** — HintText that appears near the Info Terminal
- **TerminalStand + TerminalScreen** — Info terminal with sphere trigger (exam task)
- **EventSystem** — Required for Input System

## Exam Task

See the candidate brief for the Info Terminal implementation task. The `InfoTerminal.cs` stub provides the trigger zone and hook methods — candidates extend it using Codex + Unity MCP.
