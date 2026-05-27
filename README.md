# CDREBIRTH Demo Exam Project

Lightweight Unity project for the intern technical exam. Includes a 3D character with animations, FPS controller, and Unity MCP tooling.

## Prerequisites

- **Unity Hub** + **Unity 2022.3.34f1** with URP template
- **Node.js 18+**
- **Codex Desktop** or **Codex CLI**
- **unity-mcp-cli**: `brew install unity-mcp-cli` or `npm install -g unity-mcp-cli`

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
