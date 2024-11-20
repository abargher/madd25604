# Manim

Manim is a Python library for precise, programmatic animations, created by Grant
Sanderson, creator of the 3Blue1Brown YouTube channel. Manim (from "math
animation") is the custom technology at the core of all 3Blue1Brown videos. It
supports LaTeX text rendering, a variety of built-in animations, smooth
transitions between objects, and more.

## Two Versions

There are two (or three, depending on how you count) distinct versions of
Manim. Manim began as a personal project for Grant to make his videos, which he
open-sourced. This version, sometimes called ManimGL, is Grant's version, which
is less stable, introduces more breaking changes, and has less complete
documentation. Manim Community Edition, or ManimCE, is a community-maintained
fork of Manim that aims to solve these issues.

ManimCE is updated more frequently with stability and bugfix changes, has much
more complete documentation, and aims to maintain a more stable API, clearly
documenting breaking changes. However, this also means that ManimCE is slower to
adopt new features implemented in Grant's version of Manim. For those wishing to
start with Manim and use it for their own projects or videos, ManimCE is the
recommended version due to its better stability, testing and CI, and far more
complete documentation.

## How it works

To write an animation in Manim, you begin by writing a `Scene` (sub)class.
`Scene`s represent one whole unit of animation-- at the command line, you can
render the animations represented by one entire `Scene`.

Manim's main object is the `Mobject` ('Math Object'). This is the base class for
representing essentially any object that may appear on the screen within a
`Scene`.

<!-- `Mobject`s can -->

## Some creations with Manim

### 3b1b videos

- [Manim Demo](https://www.youtube.com/watch?v=rbu7Zu5X1zI)
- [Holograms](https://www.youtube.com/watch?v=EmKQsSDlaa4)
  - see 12:00
- [Newton's Fractal](https://www.youtube.com/watch?v=-RdOwhmqP5s)
- [How LLMs might store facts](https://www.youtube.com/watch?v=9-Jl0dxWQs8)
  - see 6:07

### Others

- [Vimjoyer's NixOS Guide](https://www.youtube.com/watch?v=a67Sv4Mbxmc)
  - See text morph at 7:34
