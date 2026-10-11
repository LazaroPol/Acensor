"""Genera las páginas Markdown a partir del resultado de `scan.scan_all`."""
import pathlib

from .scan import ROOT, SCRIPTS

HEADER = "<!-- GENERADO por `python3 -m tools.arch`; no editar a mano. -->\n"


def short(ns: str) -> str:
    return ns.replace("Specimen.", "", 1)


def page_name(ns: str) -> str:
    return short(ns) + ".md"


def rel(path: pathlib.Path) -> str:
    try:
        return path.relative_to(ROOT).as_posix()
    except ValueError:
        return path.as_posix()


def dependents(modules: dict, ns: str) -> list:
    return sorted(o for o, m in modules.items() if ns in m["deps"])


def link(ns: str, prefix: str = "") -> str:
    return f"[{short(ns)}]({prefix}{page_name(ns)})"


def render_module(modules: dict, ns: str) -> str:
    mod = modules[ns]
    folder = pathlib.Path(mod["files"][0]["path"]).parent
    tests = ROOT / "Assets/_Project/Tests/EditMode" / (
        folder.relative_to(SCRIPTS) if SCRIPTS in folder.parents else folder.name)
    out = [HEADER, f"# `{ns}`\n", f"Ensamblado: `{mod['assembly']}` · Carpeta: `{rel(folder)}`",
           f"Tests: `{rel(tests)}`\n" if tests.exists() else "Tests: (sin carpeta propia)\n",
           "**Depende de:** " + (", ".join(link(d) for d in sorted(mod["deps"])) or "nada"),
           "**Lo usan:** " + (", ".join(link(d) for d in dependents(modules, ns)) or "nadie") + "\n",
           "| Fichero | Tipo | Resumen |", "|---|---|---|"]
    for f in mod["files"]:
        for t in f["types"]:
            out.append(f"| `{f['path'].name}` | {t['kind']} `{t['name']}` | {t['summary']} |")
    return "\n".join(out) + "\n"


def mermaid(modules: dict) -> str:
    ids = {ns: f"n{i}" for i, ns in enumerate(sorted(modules))}
    lines = ["```mermaid", "flowchart LR"]
    lines += [f'  {ids[ns]}["{short(ns)}"]' for ns in sorted(modules)]
    lines += [f"  {ids[ns]} --> " + " & ".join(ids[d] for d in sorted(modules[ns]["deps"]))
              for ns in sorted(modules) if modules[ns]["deps"]]
    return "\n".join(lines + ["```"])


def render_index(modules: dict) -> str:
    out = [HEADER, "# Índice de módulos (C4 nivel 3: componentes)\n",
           "Un módulo = un namespace C#. Abre solo la página del módulo que vayas a tocar.\n",
           "| Módulo | Ficheros | Tipos principales | Depende de |", "|---|---|---|---|"]
    for ns in sorted(modules):
        mod = modules[ns]
        names = list(dict.fromkeys(t["name"] for f in mod["files"] for t in f["types"]))
        shown = ", ".join(f"`{n}`" for n in names[:5]) + (" …" if len(names) > 5 else "")
        deps = ", ".join(short(d) for d in sorted(mod["deps"])) or "—"
        out.append(f"| {link(ns, 'modulos/')} | {len(mod['files'])} | {shown} | {deps} |")
    out += ["", "## Grafo de dependencias (flecha = «usa a»)\n", mermaid(modules)]
    return "\n".join(out) + "\n"


def outputs(modules: dict) -> dict:
    files = {"MODULOS.md": render_index(modules)}
    for ns in modules:
        files["modulos/" + page_name(ns)] = render_module(modules, ns)
    return files
