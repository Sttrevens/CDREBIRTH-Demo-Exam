#!/usr/bin/env node
import { existsSync } from "node:fs";
import { dirname, join, resolve } from "node:path";
import { spawn } from "node:child_process";
import { fileURLToPath } from "node:url";

const scriptDir = dirname(fileURLToPath(import.meta.url));
const repoRoot = resolve(scriptDir, "../..");

const candidatesByPlatform = {
  darwin: [
    join(repoRoot, "Library/mcp-server/osx-arm64/unity-mcp-server"),
    join(repoRoot, "Library/mcp-server/osx-x64/unity-mcp-server"),
  ],
  win32: [
    join(repoRoot, "Library/mcp-server/win-x64/unity-mcp-server.exe"),
    join(repoRoot, "Library/mcp-server/win-x64/unity-mcp-server"),
  ],
  linux: [
    join(repoRoot, "Library/mcp-server/linux-x64/unity-mcp-server"),
  ],
};

const candidates = candidatesByPlatform[process.platform] ?? [];
const serverPath = candidates.find((candidate) => existsSync(candidate));

if (!serverPath) {
  console.error(`[Demo MCP] Unity MCP server was not found for ${process.platform}.`);
  console.error("[Demo MCP] Open the project in Unity once, then run tools/agent/setup-unity-mcp for your client.");
  console.error(`[Demo MCP] Checked: ${candidates.join(", ") || "(no candidates)"}`);
  process.exit(1);
}

const child = spawn(serverPath, process.argv.slice(2), {
  cwd: repoRoot,
  env: process.env,
  stdio: "inherit",
});

child.on("exit", (code, signal) => {
  if (signal) {
    process.kill(process.pid, signal);
    return;
  }

  process.exit(code ?? 0);
});
