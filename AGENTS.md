# Demo Project Agent Instructions

This is the CDREBIRTH Demo Exam project — a lightweight Unity + Codex workflow sandbox.

## Codex Workflow Rules
- Use plan tool for task decomposition before writing code
- Read `AGENTS.md` and `CLAUDE.md` (if present) at conversation start
- Read related source files before modifying them
- When you encounter a bug, use systematic debugging — don't guess
- C# code follows Unity conventions: PascalCase for public, camelCase for private
- Verify your work is complete before claiming done
- If using Superpowers: `using-superpowers` → `writing-plans` → `executing-plans` → `verification-before-completion`

## Tools Available
- **Unity MCP**: `script-*`, `assets-*`, `gameobject-*`, `scene-*`, `console-*`, `screenshot-*`, `tests-run`
- **Codex Desktop/CLI**: file read/write, git, `apply_patch`

## Project Context
- Unity 2022.3.34f1 (URP)
- No networking, no Photon Fusion — pure local gameplay
- DemoScene.unity is the starting scene
- FirstPersonController provides WASD movement + mouse look
- InfoTerminal.cs is the exam task stub
