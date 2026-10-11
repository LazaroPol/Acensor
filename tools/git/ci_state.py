"""Imprime el estado agregado de los checks de CI de un commit: pending | success | failure."""
import json
import subprocess
import sys

OK = {"success", "skipped", "neutral"}


def aggregate(check_runs: list) -> str:
    if not check_runs or any(r["status"] != "completed" for r in check_runs):
        return "pending"
    return "success" if all(r["conclusion"] in OK for r in check_runs) else "failure"


def fetch(repo: str, sha: str) -> list:
    out = subprocess.run(["gh", "api", f"repos/{repo}/commits/{sha}/check-runs"],
                         capture_output=True, text=True, check=True).stdout
    return json.loads(out)["check_runs"]


if __name__ == "__main__":
    print(aggregate(fetch(sys.argv[1], sys.argv[2])))
