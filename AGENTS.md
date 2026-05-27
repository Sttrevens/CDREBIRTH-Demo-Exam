# Demo Project Agent Instructions

CDREBIRTH Demo Exam — lightweight Unity + Codex workflow sandbox.

## Codex Workflow Rules
- Use plan tool for task decomposition before coding
- Read AGENTS.md and relevant scripts at conversation start
- Read source files before modifying them
- When encountering bugs, use systematic debugging — don't guess
- C# code: PascalCase for public, camelCase for private
- Verify work is complete before claiming done
- If using Superpowers: `using-superpowers` → `writing-plans` → `executing-plans` → `verification-before-completion`

## Tools Available
- **Unity MCP**: `script-*`, `assets-*`, `gameobject-*`, `scene-*`, `console-*`, `screenshot-*`, `tests-run`
- **Codex Desktop/CLI**: file read/write, git, apply_patch

## Project Context
- Unity 2022.3.34f1 (URP, no networking)
- DemoScene.unity is the starting scene
- Player uses SPPlayerMovement (CharacterController-based 3C) + character model with Animator
- InfoTerminal.cs is the exam task stub (trigger zone + HUD hint)
- Character model: Assets/Art/Characters/LegacyCharacterRoot/
- Animations: Root.controller → RootIdle, RootAttacking
