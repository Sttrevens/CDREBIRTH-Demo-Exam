# Demo Project — Agent Instructions

This is a minimal Unity demo project for the CDREBIRTH intern technical exam.

## For AI Agents (Codex / Claude / etc.)

- Read the project context before making any changes.
- Use `update_plan` for multi-step tasks — break work into concrete, verifiable steps.
- Write C# code following Unity naming conventions: `PascalCase` for public, `camelCase` for private.
- Read relevant existing scripts before writing new ones — follow the patterns already in the project.
- When you encounter a bug or error, use systematic debugging:
  1. Reproduce the symptom
  2. Read the exact error message (use `console-get-logs` in Unity MCP)
  3. Identify the last-known-good state
  4. Form a minimal hypothesis
  5. Fix and verify
- After completing any task, verify it works: check Unity console for errors, run the scene, confirm the behavior matches the requirements.
- All code must compile and run in Unity 2022.3.34f1 (URP).

## For Candidates

This is a simplified project designed to test your ability to:
1. Set up the AI-assisted Unity development toolchain (Codex + Unity MCP)
2. Complete a Unity feature using AI as your pair programmer
3. Demonstrate systematic thinking and debugging skills

The project contains:
- A URP scene with a first-person controller (WASD move, mouse look, Shift to run, Space to jump)
- A Canvas HUD with a hint text element
- An `InfoTerminal` MonoBehaviour stub with trigger-based proximity detection
- Unity MCP pre-configured (`.mcp.json`, `tools/agent/`)

See `README.md` for setup instructions.
