# DWX 2015 Demos

This repository contains the demo code as shown in the talk regarding *Another look at the C# (6.0) trickbox*. The code is annotated with comments. The readme also contains instructions on how to use / experiment with certain tips or tricks as illustrated during the presentation.

## Presentation

You can find the presentation on [talks.florian-rappl.de/Cs-Tricks](http://talks.florian-rappl.de/Cs-Tricks). Additionally the talk is also listed directly on [my homepage](http://www.florian-rappl.de). Feel free to contact me if anything could be improved.

## Glance at C# 6

The class `AllNewFeatures` contains (almost) all new features. Some features are missing - they are all missing intentionally. For instance the parameterless constructor of `struct` types is in my opinion more problematic than useful.

Again comments have been added to explain the various points.

## List of tricks

The following provides an overview over all tricks and their sample class(es), if any.

1. **switch to dictionary** (see `DictionaryVsSwitch`)
2. **readonly vs. constants** (see `ConstVsReadonly` and the instructions below)
3. **weak references** (see `WeakRef`)
4. **partial methods** (see `PartialClass`, two files)
5. **params indexer** (see `VariadicIndexers`)
6. **debugger attributes** (see `DebAttr`)
7. **copying memory fast** (see information below)
8. **unions** (see `Color`)
9. **expose local state to friends** (see `Friends`)
10. **abuse default** (see information below)
11. **create a stringbuilder pool** (see `StringBuilderPool`)
12. **init time branching** (see `InitTimeBranching`)
13. **configure await (probably)** (see `UseConfigureAwait`)
14. **prevent boxing** (see `Boxing`)
15. **remove LINQ** (see information below)
16. **add LINQ** (see information below)
17. **consider aliasing** (see `Aliasing`)
18. **operational enums** (see `OperationalEnums`)
19. **variadic arguments** (see `VarArgs`)
20. **optional invocation** (see information below)
21. **string interpolation** (see `Interpolate`)
22. **use the tpl (wisely)** (see information below)
23. **don't use plinq (probably)** (see `PLinq`)
24. **await events** (see `AwaitEvents` and the instructions below)
25. **use nameof** (see `UsingNameof`)

