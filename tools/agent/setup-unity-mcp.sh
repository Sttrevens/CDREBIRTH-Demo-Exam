#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/../.." && pwd)"

echo "=== CDREBIRTH Demo Exam — Unity MCP Setup ==="
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
  echo "Open this project in Unity once (with Unity MCP package installed), then run this script again." >&2
  exit 1
}

SERVER_PATH="$(detect_server)"
echo "Found MCP server binary: $SERVER_PATH"
echo ""

# Write Codex .mcp.json (port 22399 to avoid conflicts with main CDREBIRTH project on 22398)
cat > "$REPO_ROOT/.mcp.json" << 'MCPEOF'
{
  "mcpServers": {
    "ai-game-developer": {
      "command": "node",
      "args": [
        "tools/agent/run-unity-mcp.mjs",
        "port=22399",
        "plugin-timeout=10000",
        "client-transport=stdio",
        "authorization=none"
      ]
    }
  }
}
MCPEOF

echo "✓ Wrote .mcp.json (port 22399)"
echo ""
echo "=== Next Steps ==="
echo "1. Start the MCP server in a terminal:"
echo "   $SERVER_PATH port=22399 plugin-timeout=10000 client-transport=streamableHttp authorization=none"
echo ""
echo "2. In a separate terminal, link the CLI:"
echo "   unity-mcp-cli bootstrap-local --url http://localhost:22399 --token \"\" $REPO_ROOT"
echo ""
echo "3. Verify:"
echo "   unity-mcp-cli run-tool ping --url http://localhost:22399 --token \"\" --input '{\"message\":\"hello\"}'"
echo ""
echo "4. Open this folder in Codex Desktop and start the exam task!"
