from manim import *


class DefaultTemplate(Scene):
    def construct(self):

        kw = dict(
            font_size=72,
            tex_to_color_map={
                "A": RED,
                "B": GREEN,
                "C": BLUE,
            },
        )

        equations = VGroup(
            MathTex(
                "A^2",
                "+",
                "B^2",
                "=",
                "C^2",
                **kw,
            ),
            MathTex(
                "A^2",
                "=",
                "B^2",
                "-",
                "C^2",
                **kw,
            ),
            MathTex(
                "A^2",
                "=",
                "(C + B)(C - B)",
                **kw,
            ),
            MathTex(
                "A",
                "=",
                r"\sqrt{",
                "(C + B)(C - B)}",
                **kw,
            ),
        )

        equations.arrange(DOWN)
        self.add(equations[0])
        self.play(
            TransformMatchingTex(
                equations[0].copy(),
                equations[1],
                matched_keys=["A^2", "B^2", "C^2"],
                key_map={"+": "-"},
                path_arc=90 * DEGREES,
                run_time=2,
                transform_mismatches=True,
                # fade_transform_mismatches=True,
            )
        )
        self.wait()
        self.play(
            TransformMatchingTex(
                equations[1].copy(),
                equations[2],
                matched_keys=["A^2"],
                run_time=2,
                transform_mismatches=True,
                # fade_transform_mismatches=True,
            ),
        )
        self.wait()
        self.play(
            TransformMatchingTex(
                equations[2].copy(),
                equations[3],
                key_map={"2": r"\sqrt"},
                path_arc=-30 * DEGREES,
                run_time=2,
                transform_mismatches=True,
                # fade_transform_mismatches=True,
            )
        )
        self.wait(2)
        self.play(LaggedStartMap(FadeOut, equations, shift=2 * RIGHT))
        # self.play(Create(square))  # animate the creation of the square
        # self.play(Transform(square, circle))  # interpolate the square into the circle
        # self.play(FadeOut(square))  # fade out animation
