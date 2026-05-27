#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/../.." && pwd)"

echo "=== CDREBIRTH Demo Project — Unity MCP Setup ==="
echo "Make sure you've opened this project in Unity at least once before running this."
echo ""

detect_server() {
  local candidates=()
  case "$(uname -s)" in
    Darwin)
      candidates=(
        "$REPO_ROOT/Library/mcp-server/osx-arm64/unity-mcp-server"
        "$REPO_ROOT/Library/mcp-server/osx-x64/unity-mcp-server"
      )
      ;;
    Linux)
      candidates=("$REPO_ROOT/Library/mcp-server/linux-x64/unity-mcp-server")
      ;;
    MINGW*|MSYS*|CYGWIN*)
      candidates=(
        "$REPO_ROOT/Library/mcp-server/win-x64/unity-mcp-server.exe"
        "$REPO_ROOT/Library/mcp-server/win-x64/unity-mcp-server"
      )
      ;;
  esac

  for candidate in "${candidates[@]}"; do
    if [[ -f "$candidate" ]]; then
      printf '%s\n' "$candidate"
      return 0
    fi
  done

  echo "ERROR: Unity MCP server binary not found under Library/mcp-server/." >&2
  echo "Open this project in Unity once, then run this script again." >&2
  exit 1
}

SERVER_PATH="$(detect_server)"
echo "Found MCP server binary: $SERVER_PATH"
echo ""

# Write Codex .mcp.json
cat > "$REPO_ROOT/.mcp.json" << 'MCPEOF'
{
  "mcpServers": {
    "ai-game-developer": {
      "command": "node",
      "args": [
        "tools/agent/run-unity-mcp.mjs",
        "port=22398",
        "plugin-timeout=10000",
        "client-transport=stdio",
        "authorization=none"
      ]
    }
  }
}
MCPEOF

echo "✓ Wrote .mcp.json"
echo ""
echo "Setup complete!"
echo "To verify: ask Codex to run 'ping' or 'console-get-logs' on this project."
