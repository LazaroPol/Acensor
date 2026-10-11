"""Servidor HTTP local del tablero (solo biblioteca estándar).

GET  /api/board            estado completo          POST /api/tasks          crear tarjeta
PUT  /api/tasks/<ID>       editar campos            POST /api/tasks/<ID>/move  {status, before}
DELETE /api/tasks/<ID>     borrar (solo del tablero) GET /api/git · POST /api/git/pull · POST /api/git/publish
"""
import json
import mimetypes
import pathlib
from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer

from .store import BoardError

STATIC = pathlib.Path(__file__).resolve().parent / "static"


def make_handler(store, git):
    class Handler(BaseHTTPRequestHandler):
        def log_message(self, *args):  # silencioso
            pass

        def _send(self, code, body, ctype="application/json; charset=utf-8"):
            data = body if isinstance(body, bytes) else json.dumps(body, ensure_ascii=False).encode()
            self.send_response(code)
            self.send_header("Content-Type", ctype)
            self.send_header("Cache-Control", "no-store")
            self.end_headers()
            self.wfile.write(data)

        def _body(self):
            size = int(self.headers.get("Content-Length") or 0)
            return json.loads(self.rfile.read(size) or b"{}")

        def _route(self, method):
            parts = [p for p in self.path.split("?")[0].split("/") if p]
            try:
                if parts[:1] != ["api"]:
                    return self._static(parts)
                self._send(200, self._api(method, parts[1:]))
            except BoardError as e:
                self._send(400, {"error": str(e)})
            except RuntimeError as e:
                self._send(409, {"error": str(e)})
            except (KeyError, ValueError) as e:
                self._send(400, {"error": f"Petición inválida: {e}"})

        def _api(self, method, p):
            if method == "GET" and p == ["board"]:
                return store.snapshot()
            if method == "POST" and p == ["tasks"]:
                return store.create(self._body())
            if len(p) == 2 and p[0] == "tasks" and method == "PUT":
                return store.update(p[1], self._body())
            if len(p) == 2 and p[0] == "tasks" and method == "DELETE":
                store.delete(p[1])
                return {"ok": True}
            if len(p) == 3 and p[0] == "tasks" and p[2] == "move" and method == "POST":
                b = self._body()
                return store.move(p[1], b["status"], b.get("before"))
            if p[:1] == ["git"] and git is not None:
                if method == "GET":
                    return git.status()
                if p[1:] == ["pull"]:
                    return {"message": git.pull()}
                if p[1:] == ["publish"]:
                    return {"message": git.publish(self._body().get("message", ""))}
            if p[:1] == ["git"]:
                return {"disabled": True}
            raise BoardError(f"Ruta desconocida: {method} /api/{'/'.join(p)}")

        def _static(self, parts):
            path = (STATIC / "/".join(parts or ["index.html"])).resolve()
            if STATIC not in path.parents or not path.is_file():
                return self._send(404, {"error": "No encontrado"})
            ctype = mimetypes.guess_type(path.name)[0] or "application/octet-stream"
            self._send(200, path.read_bytes(), f"{ctype}; charset=utf-8")

        def do_GET(self):
            self._route("GET")

        def do_POST(self):
            self._route("POST")

        def do_PUT(self):
            self._route("PUT")

        def do_DELETE(self):
            self._route("DELETE")

    return Handler


def serve(store, git, host="127.0.0.1", port=8765):
    return ThreadingHTTPServer((host, port), make_handler(store, git))
