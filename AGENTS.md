# Repository editing rules

- Keep every edited C# source file (`*.cs`) in Windows CRLF line endings. Patch tools may insert LF even when `.editorconfig` and `.gitattributes` specify CRLF, so normalize the entire edited C# file after the last patch in the turn.
- Before finishing a turn that edits C# files, verify each edited file has zero bare LF and zero bare CR line endings. Do not rely on `git diff --check` alone; mixed line endings can pass that check and still trigger Visual Studio's line-ending prompt.
- Preserve file bytes other than line endings when normalizing; do not use an encoding-changing text rewrite.
- Visual Studio may show the mixed-line-ending dialog while a patch is temporarily on disk, even if CRLF normalization happens afterward. For future `*.cs` edits, patch a same-directory staging copy, normalize and verify that copy to CRLF, then replace the real file in one operation. Never apply a patch that inserts LF directly to a C# file open in Visual Studio.

# Existing code and change scope

- Before editing, read the target code, its callers, and at least one comparable implementation in this repository. Check the complete flow: input source, calculation order, in-memory registration, DB writes, and report/UI readers. Do not infer behavior from function names alone.
- Follow existing patterns for naming, object ownership, dictionaries, equipment-number lists, key construction, and calculation/save sequencing. Prefer patterns used by Zones, Heatings, and DHWs. Compound string keys use the existing "+" delimiter and Split('+') convention.
- Keep changes focused on the requested problem. Avoid adding parameters, fields, helpers, or abstractions just to rearrange code. If one is necessary, explain the specific need and keep it consistent with existing implementations.
- Keep calculation, result registration in memory, and DB persistence as separate responsibilities. Make the call sequence explicit. Alternative/virtual calculations must not overwrite normal result tables, and saving must not recalculate results.
- Prefer straightforward for loops. Avoid Any, Where, ToList, foreach, and converting dictionary values to arrays when the existing number-list and dictionary lookup pattern works.
- Keep individual C# conditions, declarations, and calls on one line; do not wrap them across lines. Use normal multiline blocks for method and loop bodies.
- Unless the user asks otherwise, do not build; the user performs builds. Use relevant static checks and describe what was and was not verified.
- When reporting changes, explain the final call flow and why the change is necessary. Do not introduce a new architecture without first checking the existing one.
- Do not change formulas, allocation policies, or coefficient limits based only on plausibility or variable names. Verify the intended method first; keep unverified mathematical concerns separate from confirmed coding errors.
