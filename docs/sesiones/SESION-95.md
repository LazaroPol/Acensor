# Sesión 95 — AR-01 Pipeline de importación de assets de terceros

- **Fecha:** 2026-10-12 · **PR:** [#80](https://github.com/MenaDeFerro/Project-Especimen/pull/80) · **Estado:** ✅ cerrada

## Qué se hizo
- Packs de Lazaro copiados a su copia local (`Art/ThirdParty`, `ThirdPartyPackages`), fuera de git.
- Reglas de importación, materiales URP desde las texturas y menú de importación de Kevin Iglesias.
## Decisiones
- Repo público: los assets no se versionan (QAL de Quaternius y Asset Store lo prohíben; ~450 MB).
- El código de Runtime/Editor se compila contra las DLL de Unity del equipo de Lazaro antes de subir.
## Calidad
- 22 tests NUnit nuevos (935 en total) en verde.
## Siguiente sesión
**AR-02** Personajes del kit de clases.
