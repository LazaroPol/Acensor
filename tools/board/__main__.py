"""Tablero kanban local de SPECIMEN.

  python3 -m tools.board                  abre http://127.0.0.1:8765
  python3 -m tools.board --port 9000 --no-browser --no-git
"""
import argparse
import webbrowser

from tools.progress import model

from .gitsync import GitSync
from .server import serve
from .store import BoardStore


def main(argv=None) -> None:
    ap = argparse.ArgumentParser(prog="board")
    ap.add_argument("--port", type=int, default=8765)
    ap.add_argument("--no-browser", action="store_true")
    ap.add_argument("--no-git", action="store_true", help="oculta los botones de sincronización")
    args = ap.parse_args(argv)

    git = None if args.no_git else GitSync(model.ROOT)
    httpd = serve(BoardStore(), git, port=args.port)
    url = f"http://127.0.0.1:{args.port}"
    print(f"🗂️  Tablero de SPECIMEN en {url}  (Ctrl+C para salir)")
    if not args.no_browser:
        webbrowser.open(url)
    try:
        httpd.serve_forever()
    except KeyboardInterrupt:
        print("\nTablero cerrado.")


if __name__ == "__main__":
    main()
