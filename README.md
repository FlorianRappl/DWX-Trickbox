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
7. **copying memory fast** (see remark below)
8. **unions** (see `Color`)
9. **expose local state to friends** (see `Friends`)
10. **abuse default** (see remark below)
11. **create a stringbuilder pool** (see `StringBuilderPool`)
12. **init time branching** (see `InitTimeBranching`)
13. **configure await (probably)** (see `UseConfigureAwait`)
14. **prevent boxing** (see `Boxing`)
15. **remove LINQ** (see remark below)
16. **add LINQ** (see remark below)
17. **consider aliasing** (see `Aliasing`)
18. **operational enums** (see `OperationalEnums`)
19. **variadic arguments** (see `VarArgs`)
20. **optional invocation** (see remark below)
21. **string interpolation** (see `Interpolate`)
22. **use the tpl (wisely)** (see remark below)
23. **don't use plinq (probably)** (see `PLinq`)
24. **await events** (see `AwaitEvents` and the instructions below)
25. **use nameof** (see `UsingNameof`)

## Instructions

Some instructions on how to use special parts of the code.

### Readonly vs Constants

The huge difference is a that a `const` is constant by definition and therefore completely resolved during compile-time. This is basically a match and replace mechanism. `static readonly` fields on the other site, are just referenced. The compiler will insert the load and store instructions, thus not copying the *value* behind the field. As a consequence constants cannot change without re-compilation, however, variables that reference fields, which are `static readonly`, can.

The code contains another library called `Trickbox.External`. This is an external library that only contains a single class, which has two `public` members: A constant and a `static readonly` field. One could try referencing the assembly and compiling code that uses both members.

### Await Events

A GUI that illustrates the concept of "awaiting events". The basic idea is to use the state machinery given by the Async runtime, instead of creating our own state machine.

## Remarks

Some remarks for problems that are not provided in code form.

### Copying Memory Fast

TBD

### Abuse Default

TBD

### Optional Invocation

TBD

### Remove LINQ

TBD

### Add LINQ

TBD

### Use the TPL wisely

TBD