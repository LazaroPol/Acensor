"""Escáner ligero de C#: namespace, tipos de primer nivel, resumen XML y dependencias por `using`."""
import pathlib
import re

ROOT = pathlib.Path(__file__).resolve().parents[2]
SCRIPTS = ROOT / "Assets/_Project/Scripts"
ASSEMBLIES = {"Core": "Specimen.Core", "Runtime": "Specimen.Runtime"}

NAMESPACE = re.compile(r"^namespace\s+([\w.]+)", re.M)
USING = re.compile(r"^using\s+(Specimen\.[\w.]+)\s*;", re.M)
TYPE = re.compile(
    r"^ {4}(?:public|internal)\s+(?:(?:sealed|static|abstract|partial|readonly)\s+)*"
    r"(class|interface|enum|struct|record)\s+(\w+)", re.M)


def summary_before(text: str, pos: int) -> str:
    """Primera frase del `/// <summary>` que precede a la declaración en `pos`."""
    block = []
    for line in reversed(text[:pos].splitlines()):
        stripped = line.strip()
        if stripped.startswith("///"):
            block.append(stripped[3:])
        elif stripped.startswith("[") or not stripped and not block:
            continue
        else:
            break
    body = re.sub(r"<[^>]+>", " ", " ".join(reversed(block)))
    body = " ".join(body.split())
    return (re.split(r"(?<=\.) ", body)[0] if body else "")[:140]


def scan_file(path: pathlib.Path) -> dict:
    text = path.read_text(encoding="utf-8-sig")
    ns = NAMESPACE.search(text)
    types = [{"kind": m.group(1), "name": m.group(2), "summary": summary_before(text, m.start())}
             for m in TYPE.finditer(text)]
    return {"path": path, "namespace": ns.group(1) if ns else "", "types": types,
            "uses": sorted(set(USING.findall(text)))}


def scan_all(scripts: pathlib.Path = SCRIPTS) -> dict:
    """namespace -> {'assembly', 'files': [scan_file...], 'deps': set(namespace)}."""
    modules = {}
    for folder, assembly in ASSEMBLIES.items():
        for path in sorted((scripts / folder).rglob("*.cs")):
            info = scan_file(path)
            if not info["namespace"]:
                continue
            mod = modules.setdefault(info["namespace"], {"assembly": assembly, "files": [], "deps": set()})
            mod["files"].append(info)
    for ns, mod in modules.items():
        for f in mod["files"]:
            mod["deps"].update(u for u in f["uses"] if u in modules and u != ns)
    return modules
