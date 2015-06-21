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

A GUI that illustrates the concept of "awaiting events". The basic idea is to use the state machinery given by the Async runtime, instead of creating our own state machine. You can go ahead, start the application and see a very little game that moves yellow ellipses around the screen. The ellipse which is currently drawn red is dropped from the game once it is selected by clicking with the mouse on it or touching it. The game is basically over once there is no ellipse left.

## Remarks

Some remarks for problems that are not provided in code form.

### Copying Memory Fast

Copying managed arrays should by done via `Array.Copy` or similar methods. Copying memory for instances, which provide a kind of native access handle, such as images or movie frames, should be done by using a 1-dim array access, i.e. using just one loop. Using two loops with computation of the index will most likely result in bad performance. There is no chance for the compiler to optimize this and use SIMD, loop unrolling, full cacheline loading or other techniques to improve performance.

### Abuse Default

C++ programmers transformed their code quite fast to use `auto` (type deduction) as often as possible. In general the tendency is to move the placement of types in declarations from left to the right side of the assignment. In C# we should do the same. Additionally using 

	var foo = default(MyType);

has some other benefits as compared to

	MyType foo = null;

The latter won't compile if `MyType` is a `struct`. However, it will work with whatever kind of type `MyType` is. That makes the code easier to refactor, even in regions, where Visual Studio can't help us.

### Optional Invocation

The famous Elvis operator simplifies a lot of things, but index operations and delegate method invocation are not part of it. Therefore we simply cannot write the following statement:

	Action a = GetSomeAction();
	a?();

Nevertheless, we still don't have to write a full conditional statement as shown below:

	Action a = GetSomeAction();

	if (a != null)
		a();

The easiest thing we can do is to use the `Invoke` method, which is also the one being called with the overloaded invocation operator `()`.

	Action a = GetSomeAction();
	a?.Invoke();

This can be also applied to event handlers and provides us an easy way to express our intentation of a conditional call quite nicely.

### Remove LINQ

LINQ is the source of many performance issues. The lazy evaluation scheme of LINQ queries does not solve all performance problems. The origin of poor performance with LINQ expressions can be found in the many unnecessary allocations for delegates, captures and evaluation scheme related intermediate objects.
s
Quite often we can express the same LINQ query in a very simple `foreach` (or even `for` loop, depending on the type of enumeration) loop, which is even more readable and a lot better performing. Here we should actually think about dropping LINQ in favor of a simpler solution.

### Add LINQ

Alright, the same argument can also be made to increase the usage of LINQ. Most of the times we do not care about very small performance losses. Sometimes `foreach` loops can be made a lot easier to read by using LINQ statements. In these cases we really should think about readers of our code first. After all code is meant to be read more often than written.

### Use the TPL wisely

The Task Parallel Library provides a lot of interesting methods and abilities to use multi-core architectures. However, before really using such resources we have to think if this makes really sense. It is not uncommon that parallelized code performs worse than its sequential counter-part. The reason is simple: Quite often the algorithm has to be adjusted with parallelization in mind. Synchronization has to be minimized and possible race-conditions have to be eliminated. This is a lot work.

What is more important that parallelization is concurrency or asynchronocy in general. Nowadays we can solve such problems quite nicely by using `async` / `await`. In intense computation may be placed in a separate thread, such that it won't block the UI. Finally we may adapt our code for multi-cores, but we should be sure about the scaling.

The ability to scale is mainly dependent on the work-load. We have two options: Fixed work-load and adpted work-load. While the former occurs more often the latter gives us much better scaling options. Here we can basically provide the right work-load to accomodate for the user's resources. This is similar to streaming videos according to the user's bandwidth. The user will always have a fluent experience, however, it is possible to give him a better visual experience by going beyond the user's resources, which is bandwidth in this example.