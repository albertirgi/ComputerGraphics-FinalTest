using System;
using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;

namespace LearnOpenTK
{
    // In this tutorial we focus on how to set up a scene with multiple lights, both of different types but also
    // with several point lights
    public class Window : GameWindow
    {
        private readonly float[] _vertices =
        {
            // Positions          Normals              Texture coords
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f
        };

        private readonly Vector3[] _waterPosition =
        {
            new Vector3(10.0f, -1.2f, 10.0f),
            new Vector3(10.0f, -1.2f, 9.0f),
            new Vector3(10.0f, -1.2f, 8.0f),
            new Vector3(10.0f, -1.2f, 7.0f),
            new Vector3(10.0f, -1.2f, 6.0f),
            new Vector3(10.0f, -1.2f, 5.0f),
            new Vector3(10.0f, -1.2f, 4.0f),

            new Vector3(9.0f, -1.2f, 10.0f),
            new Vector3(9.0f, -1.2f, 9.0f),
            new Vector3(9.0f, -1.2f, 8.0f),
            new Vector3(9.0f, -1.2f, 7.0f),
            new Vector3(9.0f, -1.2f, 6.0f),
            new Vector3(9.0f, -1.2f, 5.0f),
            new Vector3(9.0f, -1.2f, 4.0f),
            new Vector3(9.0f, -1.2f, 3.0f),
            new Vector3(9.0f, -1.2f, 2.0f),

            new Vector3(8.0f, -1.2f, 10.0f),
            new Vector3(8.0f, -1.2f, 9.0f),
            new Vector3(8.0f, -1.2f, 8.0f),
            new Vector3(8.0f, -1.2f, 7.0f),
            new Vector3(8.0f, -1.2f, 6.0f),
            new Vector3(8.0f, -1.2f, 5.0f),
            new Vector3(8.0f, -1.2f, 4.0f),
            new Vector3(8.0f, -1.2f, 3.0f),

            new Vector3(7.0f, -1.2f, 10.0f),
            new Vector3(7.0f, -1.2f, 9.0f),
            new Vector3(7.0f, -1.2f, 8.0f),
            new Vector3(7.0f, -1.2f, 7.0f),
            new Vector3(7.0f, -1.2f, 6.0f),
            new Vector3(7.0f, -1.2f, 5.0f),
            new Vector3(7.0f, -1.2f, 4.0f),
            new Vector3(7.0f, -1.2f, 3.0f),
            new Vector3(7.0f, -1.2f, 2.0f),

            new Vector3(6.0f, -1.2f, 10.0f),
            new Vector3(6.0f, -1.2f, 9.0f),
            new Vector3(6.0f, -1.2f, 8.0f),
            new Vector3(6.0f, -1.2f, 7.0f),
            new Vector3(6.0f, -1.2f, 6.0f),
            new Vector3(6.0f, -1.2f, 5.0f),
            new Vector3(6.0f, -1.2f, 4.0f),
            new Vector3(6.0f, -1.2f, 3.0f),
            new Vector3(6.0f, -1.2f, 2.0f),
            new Vector3(6.0f, -1.2f, 1.0f),

            new Vector3(5.0f, -1.2f, 10.0f),
            new Vector3(5.0f, -1.2f, 9.0f),
            new Vector3(5.0f, -1.2f, 8.0f),
            new Vector3(5.0f, -1.2f, 7.0f),
            new Vector3(5.0f, -1.2f, 6.0f),
            new Vector3(5.0f, -1.2f, 5.0f),
            new Vector3(5.0f, -1.2f, 4.0f),
            new Vector3(5.0f, -1.2f, 3.0f),
            new Vector3(5.0f, -1.2f, 2.0f),

            new Vector3(4.0f, -1.2f, 10.0f),
            new Vector3(4.0f, -1.2f, 9.0f),
            new Vector3(4.0f, -1.2f, 8.0f),
            new Vector3(4.0f, -1.2f, 7.0f),
            new Vector3(4.0f, -1.2f, 6.0f),
            new Vector3(4.0f, -1.2f, 5.0f),
            new Vector3(4.0f, -1.2f, 4.0f),
            new Vector3(4.0f, -1.2f, 3.0f),
            new Vector3(4.0f, -1.2f, 2.0f),
            new Vector3(4.0f, -1.2f, 1.0f),

            new Vector3(3.0f, -1.2f, 10.0f),
            new Vector3(3.0f, -1.2f, 9.0f),
            new Vector3(3.0f, -1.2f, 8.0f),
            new Vector3(3.0f, -1.2f, 7.0f),
            new Vector3(3.0f, -1.2f, 6.0f),
            new Vector3(3.0f, -1.2f, 5.0f),
            new Vector3(3.0f, -1.2f, 4.0f),
            new Vector3(3.0f, -1.2f, 3.0f),
            new Vector3(3.0f, -1.2f, 2.0f),
            new Vector3(3.0f, -1.2f, 1.0f),
            new Vector3(3.0f, -1.2f, 0.0f),

            new Vector3(2.0f, -1.2f, 10.0f),
            new Vector3(2.0f, -1.2f, 9.0f),
            new Vector3(2.0f, -1.2f, 8.0f),
            new Vector3(2.0f, -1.2f, 7.0f),
            new Vector3(2.0f, -1.2f, 6.0f),
            new Vector3(2.0f, -1.2f, 5.0f),
            new Vector3(2.0f, -1.2f, 4.0f),
            new Vector3(2.0f, -1.2f, 3.0f),
            new Vector3(2.0f, -1.2f, 2.0f),

            new Vector3(1.0f, -1.2f, 10.0f),
            new Vector3(1.0f, -1.2f, 9.0f),
            new Vector3(1.0f, -1.2f, 8.0f),
            new Vector3(1.0f, -1.2f, 7.0f),
            new Vector3(1.0f, -1.2f, 6.0f),
            new Vector3(1.0f, -1.2f, 5.0f),
            new Vector3(1.0f, -1.2f, 4.0f),
            new Vector3(1.0f, -1.2f, 3.0f),

            new Vector3(0.0f, -1.2f, 10.0f),
            new Vector3(0.0f, -1.2f, 9.0f),
            new Vector3(0.0f, -1.2f, 8.0f),
            new Vector3(0.0f, -1.2f, 7.0f),
            new Vector3(0.0f, -1.2f, 6.0f),
            new Vector3(0.0f, -1.2f, 5.0f),
            new Vector3(0.0f, -1.2f, 4.0f),

            new Vector3(-10.0f, -1.2f, 10.0f),
            new Vector3(-10.0f, -1.2f, 9.0f),
            new Vector3(-10.0f, -1.2f, 8.0f),
            new Vector3(-10.0f, -1.2f, 7.0f),
            new Vector3(-10.0f, -1.2f, 6.0f),

            new Vector3(-9.0f, -1.2f, 10.0f),
            new Vector3(-9.0f, -1.2f, 9.0f),
            new Vector3(-9.0f, -1.2f, 8.0f),

            new Vector3(-1.0f, -1.2f, 10.0f),
            new Vector3(-1.0f, -1.2f, 9.0f),
            new Vector3(-1.0f, -1.2f, 8.0f),
            new Vector3(-1.0f, -1.2f, 7.0f),
            new Vector3(-1.0f, -1.2f, 6.0f),
            new Vector3(-1.0f, -1.2f, 5.0f),
            new Vector3(-1.0f, -1.2f, 4.0f),
            new Vector3(-1.0f, -1.2f, 3.0f),

            new Vector3(-2.0f, -1.2f, 10.0f),
            new Vector3(-2.0f, -1.2f, 9.0f),
            new Vector3(-2.0f, -1.2f, 8.0f),
            new Vector3(-2.0f, -1.2f, 7.0f),
            new Vector3(-2.0f, -1.2f, 6.0f),
            new Vector3(-2.0f, -1.2f, 5.0f),
            new Vector3(-2.0f, -1.2f, 4.0f),

            new Vector3(-3.0f, -1.2f, 10.0f),
            new Vector3(-3.0f, -1.2f, 9.0f),
            new Vector3(-3.0f, -1.2f, 8.0f),
            new Vector3(-3.0f, -1.2f, 7.0f),
            new Vector3(-3.0f, -1.2f, 6.0f),
            new Vector3(-3.0f, -1.2f, 5.0f),
            new Vector3(-3.0f, -1.2f, 4.0f),
            new Vector3(-3.0f, -1.2f, 3.0f),

            new Vector3(-4.0f, -1.2f, 10.0f),
            new Vector3(-4.0f, -1.2f, 9.0f),
            new Vector3(-4.0f, -1.2f, 8.0f),
            new Vector3(-4.0f, -1.2f, 7.0f),
            new Vector3(-4.0f, -1.2f, 6.0f),
            new Vector3(-4.0f, -1.2f, 5.0f),

            new Vector3(-5.0f, -1.2f, 10.0f),
            new Vector3(-5.0f, -1.2f, 9.0f),
            new Vector3(-5.0f, -1.2f, 8.0f),
            new Vector3(-5.0f, -1.2f, 7.0f),

            new Vector3(-6.0f, -1.2f, 10.0f),
            new Vector3(-6.0f, -1.2f, 9.0f),
            new Vector3(-6.0f, -1.2f, 8.0f),

        };

        private readonly Vector3[] _groundPositions =
        {

            new Vector3(10.0f, -1.0f, 3.0f),
            new Vector3(10.0f, -1.0f, 2.0f),
            new Vector3(10.0f, -1.0f, 1.0f),
            new Vector3(10.0f, -1.0f, 0.0f),
            new Vector3(10.0f, -1.0f, -1.0f),
            new Vector3(10.0f, -1.0f, -2.0f),
            new Vector3(10.0f, -1.0f, -3.0f),
            new Vector3(10.0f, -1.0f, -4.0f),
            new Vector3(10.0f, -1.0f, -5.0f),
            new Vector3(10.0f, -1.0f, -6.0f),
            new Vector3(10.0f, -1.0f, -7.0f),
            new Vector3(10.0f, -1.0f, -8.0f),
            new Vector3(10.0f, -1.0f, -9.0f),
            new Vector3(10.0f, -1.0f, -10.0f),

            new Vector3(9.0f, -1.0f, 1.0f),
            new Vector3(9.0f, -1.0f, 0.0f),
            new Vector3(9.0f, -1.0f, -1.0f),
            new Vector3(9.0f, -1.0f, -2.0f),
            new Vector3(9.0f, -1.0f, -3.0f),
            new Vector3(9.0f, -1.0f, -4.0f),
            new Vector3(9.0f, -1.0f, -5.0f),
            new Vector3(9.0f, -1.0f, -6.0f),
            new Vector3(9.0f, -1.0f, -7.0f),
            new Vector3(9.0f, -1.0f, -8.0f),
            new Vector3(9.0f, -1.0f, -9.0f),
            new Vector3(9.0f, -1.0f, -10.0f),

            new Vector3(8.0f, -1.0f, 2.0f),
            new Vector3(8.0f, -1.0f, 1.0f),
            new Vector3(8.0f, -1.0f, 0.0f),
            new Vector3(8.0f, -1.0f, -1.0f),
            new Vector3(8.0f, -1.0f, -2.0f),
            new Vector3(8.0f, -1.0f, -3.0f),
            new Vector3(8.0f, -1.0f, -4.0f),
            new Vector3(8.0f, -1.0f, -5.0f),
            new Vector3(8.0f, -1.0f, -6.0f),
            new Vector3(8.0f, -1.0f, -7.0f),
            new Vector3(8.0f, -1.0f, -8.0f),
            new Vector3(8.0f, -1.0f, -9.0f),
            new Vector3(8.0f, -1.0f, -10.0f),

            new Vector3(7.0f, -1.0f, 1.0f),
            new Vector3(7.0f, -1.0f, 0.0f),
            new Vector3(7.0f, -1.0f, -1.0f),
            new Vector3(7.0f, -1.0f, -2.0f),
            new Vector3(7.0f, -1.0f, -3.0f),
            new Vector3(7.0f, -1.0f, -4.0f),
            new Vector3(7.0f, -1.0f, -5.0f),
            new Vector3(7.0f, -1.0f, -6.0f),
            new Vector3(7.0f, -1.0f, -7.0f),
            new Vector3(7.0f, -1.0f, -8.0f),
            new Vector3(7.0f, -1.0f, -9.0f),
            new Vector3(7.0f, -1.0f, -10.0f),

            new Vector3(6.0f, -1.0f, 0.0f),
            new Vector3(6.0f, -1.0f, -1.0f),
            new Vector3(6.0f, -1.0f, -2.0f),
            new Vector3(6.0f, -1.0f, -3.0f),
            new Vector3(6.0f, -1.0f, -4.0f),
            new Vector3(6.0f, -1.0f, -5.0f),
            new Vector3(6.0f, -1.0f, -6.0f),
            new Vector3(6.0f, -1.0f, -7.0f),
            new Vector3(6.0f, -1.0f, -8.0f),
            new Vector3(6.0f, -1.0f, -9.0f),
            new Vector3(6.0f, -1.0f, -10.0f),

            new Vector3(5.0f, -1.0f, 1.0f),
            new Vector3(5.0f, -1.0f, 0.0f),
            new Vector3(5.0f, -1.0f, -1.0f),
            new Vector3(5.0f, -1.0f, -2.0f),
            new Vector3(5.0f, -1.0f, -3.0f),
            new Vector3(5.0f, -1.0f, -4.0f),
            new Vector3(5.0f, -1.0f, -5.0f),
            new Vector3(5.0f, -1.0f, -6.0f),
            new Vector3(5.0f, -1.0f, -7.0f),
            new Vector3(5.0f, -1.0f, -8.0f),
            new Vector3(5.0f, -1.0f, -9.0f),
            new Vector3(5.0f, -1.0f, -10.0f),

            new Vector3(4.0f, -1.0f, 0.0f),
            new Vector3(4.0f, -1.0f, -1.0f),
            new Vector3(4.0f, -1.0f, -2.0f),
            new Vector3(4.0f, -1.0f, -3.0f),
            new Vector3(4.0f, -1.0f, -4.0f),
            new Vector3(4.0f, -1.0f, -5.0f),
            new Vector3(4.0f, -1.0f, -6.0f),
            new Vector3(4.0f, -1.0f, -7.0f),
            new Vector3(4.0f, -1.0f, -8.0f),
            new Vector3(4.0f, -1.0f, -9.0f),
            new Vector3(4.0f, -1.0f, -10.0f),

            new Vector3(3.0f, -1.0f, -1.0f),
            new Vector3(3.0f, -1.0f, -2.0f),
            new Vector3(3.0f, -1.0f, -3.0f),
            new Vector3(3.0f, -1.0f, -4.0f),
            new Vector3(3.0f, -1.0f, -5.0f),
            new Vector3(3.0f, -1.0f, -6.0f),
            new Vector3(3.0f, -1.0f, -7.0f),
            new Vector3(3.0f, -1.0f, -8.0f),
            new Vector3(3.0f, -1.0f, -9.0f),
            new Vector3(3.0f, -1.0f, -10.0f),

            new Vector3(2.0f, -1.0f, 1.0f),
            new Vector3(2.0f, -1.0f, 0.0f),
            new Vector3(2.0f, -1.0f, -1.0f),
            new Vector3(2.0f, -1.0f, -2.0f),
            new Vector3(2.0f, -1.0f, -3.0f),
            new Vector3(2.0f, -1.0f, -4.0f),
            new Vector3(2.0f, -1.0f, -5.0f),
            new Vector3(2.0f, -1.0f, -6.0f),
            new Vector3(2.0f, -1.0f, -7.0f),
            new Vector3(2.0f, -1.0f, -8.0f),
            new Vector3(2.0f, -1.0f, -9.0f),
            new Vector3(2.0f, -1.0f, -10.0f),

            new Vector3(1.0f, -1.0f, 1.0f),
            new Vector3(1.0f, -1.0f, 0.0f),
            new Vector3(1.0f, -1.0f, -1.0f),
            new Vector3(1.0f, -1.0f, -2.0f),
            new Vector3(1.0f, -1.0f, -3.0f),
            new Vector3(1.0f, -1.0f, -4.0f),
            new Vector3(1.0f, -1.0f, -5.0f),
            new Vector3(1.0f, -1.0f, -6.0f),
            new Vector3(1.0f, -1.0f, -7.0f),
            new Vector3(1.0f, -1.0f, -8.0f),
            new Vector3(1.0f, -1.0f, -9.0f),
            new Vector3(1.0f, -1.0f, -10.0f),

            new Vector3(0.0f, -1.0f, 3.0f),
            new Vector3(0.0f, -1.0f, 2.0f),
            new Vector3(0.0f, -1.0f, 1.0f),
            new Vector3(0.0f, -1.0f, 0.0f),
            new Vector3(0.0f, -1.0f, -1.0f),
            new Vector3(0.0f, -1.0f, -2.0f),
            new Vector3(0.0f, -1.0f, -3.0f),
            new Vector3(0.0f, -1.0f, -4.0f),
            new Vector3(0.0f, -1.0f, -5.0f),
            new Vector3(0.0f, -1.0f, -6.0f),
            new Vector3(0.0f, -1.0f, -7.0f),
            new Vector3(0.0f, -1.0f, -8.0f),
            new Vector3(0.0f, -1.0f, -9.0f),
            new Vector3(0.0f, -1.0f, -10.0f),

            new Vector3(-10.0f, -1.0f, 5.0f),
            new Vector3(-10.0f, -1.0f, 4.0f),
            new Vector3(-10.0f, -1.0f, 3.0f),
            new Vector3(-10.0f, -1.0f, 2.0f),
            new Vector3(-10.0f, -1.0f, 1.0f),
            new Vector3(-10.0f, -1.0f, 0.0f),
            new Vector3(-10.0f, -1.0f, -1.0f),
            new Vector3(-10.0f, -1.0f, -2.0f),
            new Vector3(-10.0f, -1.0f, -3.0f),
            new Vector3(-10.0f, -1.0f, -4.0f),
            new Vector3(-10.0f, -1.0f, -5.0f),
            new Vector3(-10.0f, -1.0f, -6.0f),
            new Vector3(-10.0f, -1.0f, -7.0f),
            new Vector3(-10.0f, -1.0f, -8.0f),
            new Vector3(-10.0f, -1.0f, -9.0f),
            new Vector3(-10.0f, -1.0f, -10.0f),

            new Vector3(-9.0f, -1.0f, 7.0f),
            new Vector3(-9.0f, -1.0f, 6.0f),
            new Vector3(-9.0f, -1.0f, 5.0f),
            new Vector3(-9.0f, -1.0f, 4.0f),
            new Vector3(-9.0f, -1.0f, 3.0f),
            new Vector3(-9.0f, -1.0f, 2.0f),
            new Vector3(-9.0f, -1.0f, 1.0f),
            new Vector3(-9.0f, -1.0f, 0.0f),
            new Vector3(-9.0f, -1.0f, -1.0f),
            new Vector3(-9.0f, -1.0f, -2.0f),
            new Vector3(-9.0f, -1.0f, -3.0f),
            new Vector3(-9.0f, -1.0f, -4.0f),
            new Vector3(-9.0f, -1.0f, -5.0f),
            new Vector3(-9.0f, -1.0f, -6.0f),
            new Vector3(-9.0f, -1.0f, -7.0f),
            new Vector3(-9.0f, -1.0f, -8.0f),
            new Vector3(-9.0f, -1.0f, -9.0f),
            new Vector3(-9.0f, -1.0f, -10.0f),

            new Vector3(-8.0f, -1.0f, 10.0f),
            new Vector3(-8.0f, -1.0f, 9.0f),
            new Vector3(-8.0f, -1.0f, 8.0f),
            new Vector3(-8.0f, -1.0f, 7.0f),
            new Vector3(-8.0f, -1.0f, 6.0f),
            new Vector3(-8.0f, -1.0f, 5.0f),
            new Vector3(-8.0f, -1.0f, 4.0f),
            new Vector3(-8.0f, -1.0f, 3.0f),
            new Vector3(-8.0f, -1.0f, 2.0f),
            new Vector3(-8.0f, -1.0f, 1.0f),
            new Vector3(-8.0f, -1.0f, 0.0f),
            new Vector3(-8.0f, -1.0f, -1.0f),
            new Vector3(-8.0f, -1.0f, -2.0f),
            new Vector3(-8.0f, -1.0f, -3.0f),
            new Vector3(-8.0f, -1.0f, -4.0f),
            new Vector3(-8.0f, -1.0f, -5.0f),
            new Vector3(-8.0f, -1.0f, -6.0f),
            new Vector3(-8.0f, -1.0f, -7.0f),
            new Vector3(-8.0f, -1.0f, -8.0f),
            new Vector3(-8.0f, -1.0f, -9.0f),
            new Vector3(-8.0f, -1.0f, -10.0f),

            new Vector3(-7.0f, -1.0f, 10.0f),
            new Vector3(-7.0f, -1.0f, 9.0f),
            new Vector3(-7.0f, -1.0f, 8.0f),
            new Vector3(-7.0f, -1.0f, 7.0f),
            new Vector3(-7.0f, -1.0f, 6.0f),
            new Vector3(-7.0f, -1.0f, 5.0f),
            new Vector3(-7.0f, -1.0f, 4.0f),
            new Vector3(-7.0f, -1.0f, 3.0f),
            new Vector3(-7.0f, -1.0f, 2.0f),
            new Vector3(-7.0f, -1.0f, 1.0f),
            new Vector3(-7.0f, -1.0f, 0.0f),
            new Vector3(-7.0f, -1.0f, -1.0f),
            new Vector3(-7.0f, -1.0f, -2.0f),
            new Vector3(-7.0f, -1.0f, -3.0f),
            new Vector3(-7.0f, -1.0f, -4.0f),
            new Vector3(-7.0f, -1.0f, -5.0f),
            new Vector3(-7.0f, -1.0f, -6.0f),
            new Vector3(-7.0f, -1.0f, -7.0f),
            new Vector3(-7.0f, -1.0f, -8.0f),
            new Vector3(-7.0f, -1.0f, -9.0f),
            new Vector3(-7.0f, -1.0f, -10.0f),

            new Vector3(-6.0f, -1.0f, 7.0f),
            new Vector3(-6.0f, -1.0f, 6.0f),
            new Vector3(-6.0f, -1.0f, 5.0f),
            new Vector3(-6.0f, -1.0f, 4.0f),
            new Vector3(-6.0f, -1.0f, 3.0f),
            new Vector3(-6.0f, -1.0f, 2.0f),
            new Vector3(-6.0f, -1.0f, 1.0f),
            new Vector3(-6.0f, -1.0f, 0.0f),
            new Vector3(-6.0f, -1.0f, -1.0f),
            new Vector3(-6.0f, -1.0f, -2.0f),
            new Vector3(-6.0f, -1.0f, -3.0f),
            new Vector3(-6.0f, -1.0f, -4.0f),
            new Vector3(-6.0f, -1.0f, -5.0f),
            new Vector3(-6.0f, -1.0f, -6.0f),
            new Vector3(-6.0f, -1.0f, -7.0f),
            new Vector3(-6.0f, -1.0f, -8.0f),
            new Vector3(-6.0f, -1.0f, -9.0f),
            new Vector3(-6.0f, -1.0f, -10.0f),

            new Vector3(-5.0f, -1.0f, 6.0f),
            new Vector3(-5.0f, -1.0f, 5.0f),
            new Vector3(-5.0f, -1.0f, 4.0f),
            new Vector3(-5.0f, -1.0f, 3.0f),
            new Vector3(-5.0f, -1.0f, 2.0f),
            new Vector3(-5.0f, -1.0f, 1.0f),
            new Vector3(-5.0f, -1.0f, 0.0f),
            new Vector3(-5.0f, -1.0f, -1.0f),
            new Vector3(-5.0f, -1.0f, -2.0f),
            new Vector3(-5.0f, -1.0f, -3.0f),
            new Vector3(-5.0f, -1.0f, -4.0f),
            new Vector3(-5.0f, -1.0f, -5.0f),
            new Vector3(-5.0f, -1.0f, -6.0f),
            new Vector3(-5.0f, -1.0f, -7.0f),
            new Vector3(-5.0f, -1.0f, -8.0f),
            new Vector3(-5.0f, -1.0f, -9.0f),
            new Vector3(-5.0f, -1.0f, -10.0f),

            new Vector3(-4.0f, -1.0f, 4.0f),
            new Vector3(-4.0f, -1.0f, 3.0f),
            new Vector3(-4.0f, -1.0f, 2.0f),
            new Vector3(-4.0f, -1.0f, 1.0f),
            new Vector3(-4.0f, -1.0f, 0.0f),
            new Vector3(-4.0f, -1.0f, -1.0f),
            new Vector3(-4.0f, -1.0f, -2.0f),
            new Vector3(-4.0f, -1.0f, -3.0f),
            new Vector3(-4.0f, -1.0f, -4.0f),
            new Vector3(-4.0f, -1.0f, -5.0f),
            new Vector3(-4.0f, -1.0f, -6.0f),
            new Vector3(-4.0f, -1.0f, -7.0f),
            new Vector3(-4.0f, -1.0f, -8.0f),
            new Vector3(-4.0f, -1.0f, -9.0f),
            new Vector3(-4.0f, -1.0f, -10.0f),

            new Vector3(-3.0f, -1.0f, 2.0f),
            new Vector3(-3.0f, -1.0f, 1.0f),
            new Vector3(-3.0f, -1.0f, 0.0f),
            new Vector3(-3.0f, -1.0f, -1.0f),
            new Vector3(-3.0f, -1.0f, -2.0f),
            new Vector3(-3.0f, -1.0f, -3.0f),
            new Vector3(-3.0f, -1.0f, -4.0f),
            new Vector3(-3.0f, -1.0f, -5.0f),
            new Vector3(-3.0f, -1.0f, -6.0f),
            new Vector3(-3.0f, -1.0f, -7.0f),
            new Vector3(-3.0f, -1.0f, -8.0f),
            new Vector3(-3.0f, -1.0f, -9.0f),
            new Vector3(-3.0f, -1.0f, -10.0f),

            new Vector3(-2.0f, -1.0f, 3.0f),
            new Vector3(-2.0f, -1.0f, 2.0f),
            new Vector3(-2.0f, -1.0f, 1.0f),
            new Vector3(-2.0f, -1.0f, 0.0f),
            new Vector3(-2.0f, -1.0f, -1.0f),
            new Vector3(-2.0f, -1.0f, -2.0f),
            new Vector3(-2.0f, -1.0f, -3.0f),
            new Vector3(-2.0f, -1.0f, -4.0f),
            new Vector3(-2.0f, -1.0f, -5.0f),
            new Vector3(-2.0f, -1.0f, -6.0f),
            new Vector3(-2.0f, -1.0f, -7.0f),
            new Vector3(-2.0f, -1.0f, -8.0f),
            new Vector3(-2.0f, -1.0f, -9.0f),
            new Vector3(-2.0f, -1.0f, -10.0f),

            new Vector3(-1.0f, -1.0f, 1.0f),
            new Vector3(-1.0f, -1.0f, 0.0f),
            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(-1.0f, -1.0f, -2.0f),
            new Vector3(-1.0f, -1.0f, -3.0f),
            new Vector3(-1.0f, -1.0f, -4.0f),
            new Vector3(-1.0f, -1.0f, -5.0f),
            new Vector3(-1.0f, -1.0f, -6.0f),
            new Vector3(-1.0f, -1.0f, -7.0f),
            new Vector3(-1.0f, -1.0f, -8.0f),
            new Vector3(-1.0f, -1.0f, -9.0f),
            new Vector3(-1.0f, -1.0f, -10.0f),

        };

        private readonly Vector3[] _stonePositions =
        {
            new Vector3(-1.0f, -0.5f, 1.0f),
            new Vector3(0.0f, -0.5f, 1.0f),
            new Vector3(1.0f, -0.5f, 1.0f),
            new Vector3(-1.0f, 0.0f, 2.0f),
            new Vector3(0.0f, 0.0f, 2.0f),
            new Vector3(1.0f, 0.0f, 2.0f),

            //rumah
            new Vector3(-9.0f, 0.0f, -3.0f),
            new Vector3(-8.0f, 0.0f, -3.0f),
            new Vector3(-7.0f, 0.0f, -3.0f),
            new Vector3(-6.0f, 0.0f, -3.0f),
            new Vector3(-5.0f, 0.0f, -3.0f),
            new Vector3(-4.0f, 0.0f, -3.0f),
            new Vector3(-9.0f, 1.0f, -3.0f),
            new Vector3(-8.0f, 1.0f, -3.0f),
            new Vector3(-7.0f, 1.0f, -3.0f),
            new Vector3(-6.0f, 1.0f, -3.0f),
            new Vector3(-5.0f, 1.0f, -3.0f),
            new Vector3(-4.0f, 1.0f, -3.0f),
            new Vector3(-9.0f, 2.0f, -3.0f),
            new Vector3(-8.0f, 2.0f, -3.0f),
            new Vector3(-7.0f, 2.0f, -3.0f),
            new Vector3(-6.0f, 2.0f, -3.0f),
            new Vector3(-5.0f, 2.0f, -3.0f),
            new Vector3(-4.0f, 2.0f, -3.0f),
            new Vector3(-9.0f, 3.0f, -3.0f),
            new Vector3(-8.0f, 3.0f, -3.0f),
            new Vector3(-7.0f, 3.0f, -3.0f),
            new Vector3(-6.0f, 3.0f, -3.0f),
            new Vector3(-5.0f, 3.0f, -3.0f),
            new Vector3(-4.0f, 3.0f, -3.0f),


            new Vector3(-4.0f, 0.0f, -2.0f),
            new Vector3(-4.0f, 0.0f, -1.0f),
            new Vector3(-4.0f, 0.0f, 1.0f),
            new Vector3(-4.0f, 0.0f, 2.0f),
            new Vector3(-4.0f, 1.0f, -2.0f),
            new Vector3(-4.0f, 1.0f, -1.0f),
            new Vector3(-4.0f, 1.0f, 1.0f),
            new Vector3(-4.0f, 1.0f, 2.0f),
            new Vector3(-4.0f, 2.0f, -2.0f),
            new Vector3(-4.0f, 2.0f, -1.0f),
            new Vector3(-4.0f, 2.0f, 0.0f),
            new Vector3(-4.0f, 2.0f, 1.0f),
            new Vector3(-4.0f, 2.0f, 2.0f),
            new Vector3(-4.0f, 3.0f, -2.0f),
            new Vector3(-4.0f, 3.0f, -1.0f),
            new Vector3(-4.0f, 3.0f, 0.0f),
            new Vector3(-4.0f, 3.0f, 1.0f),
            new Vector3(-4.0f, 3.0f, 2.0f),
            new Vector3(-4.0f, 4.0f, -2.0f),
            new Vector3(-5.0f, 4.0f, -2.0f),
            new Vector3(-6.0f, 4.0f, -2.0f),
            new Vector3(-7.0f, 4.0f, -2.0f),
            new Vector3(-8.0f, 4.0f, -2.0f),
            new Vector3(-4.0f, 4.0f, -1.0f),
            new Vector3(-4.0f, 5.0f, -1.0f),
            new Vector3(-5.0f, 5.0f, -1.0f),
            new Vector3(-6.0f, 5.0f, -1.0f),
            new Vector3(-7.0f, 5.0f, -1.0f),
            new Vector3(-8.0f, 5.0f, -1.0f),
            new Vector3(-9.0f, 5.0f, -1.0f),
            new Vector3(-4.0f, 4.0f, 0.0f),
            new Vector3(-4.0f, 6.0f, 0.0f),
            new Vector3(-5.0f, 6.0f, 0.0f),
            new Vector3(-6.0f, 6.0f, 0.0f),
            new Vector3(-7.0f, 6.0f, 0.0f),
            new Vector3(-8.0f, 6.0f, 0.0f),
            new Vector3(-9.0f, 6.0f, 0.0f),
            new Vector3(-4.0f, 4.0f, 1.0f),
            new Vector3(-4.0f, 5.0f, 1.0f),
            new Vector3(-5.0f, 5.0f, 1.0f),
            new Vector3(-6.0f, 5.0f, 1.0f),
            new Vector3(-7.0f, 5.0f, 1.0f),
            new Vector3(-8.0f, 5.0f, 1.0f),
            new Vector3(-9.0f, 5.0f, 1.0f),
            new Vector3(-4.0f, 4.0f, 2.0f),
            new Vector3(-5.0f, 4.0f, 2.0f),
            new Vector3(-6.0f, 4.0f, 2.0f),
            new Vector3(-7.0f, 4.0f, 2.0f),
            new Vector3(-8.0f, 4.0f, 2.0f),
            new Vector3(-9.0f, 4.0f, 2.0f),

            new Vector3(-9.0f, 0.0f, -2.0f),
            new Vector3(-9.0f, 0.0f, -1.0f),
            new Vector3(-9.0f, 0.0f, 0.0f),
            new Vector3(-9.0f, 0.0f, 1.0f),
            new Vector3(-9.0f, 0.0f, 2.0f),
            new Vector3(-9.0f, 1.0f, -2.0f),
            new Vector3(-9.0f, 1.0f, -1.0f),
            new Vector3(-9.0f, 1.0f, 0.0f),
            new Vector3(-9.0f, 1.0f, 1.0f),
            new Vector3(-9.0f, 1.0f, 2.0f),
            new Vector3(-9.0f, 2.0f, -2.0f),
            new Vector3(-9.0f, 2.0f, -1.0f),
            new Vector3(-9.0f, 2.0f, 0.0f),
            new Vector3(-9.0f, 2.0f, 1.0f),
            new Vector3(-9.0f, 2.0f, 2.0f),
            new Vector3(-9.0f, 3.0f, -2.0f),
            new Vector3(-9.0f, 3.0f, -1.0f),
            new Vector3(-9.0f, 3.0f, 0.0f),
            new Vector3(-9.0f, 3.0f, 1.0f),
            new Vector3(-9.0f, 3.0f, 2.0f),
            new Vector3(-9.0f, 4.0f, -2.0f),
            new Vector3(-9.0f, 4.0f, -1.0f),
            new Vector3(-9.0f, 4.0f, 0.0f),
            new Vector3(-9.0f, 4.0f, 1.0f),
            new Vector3(-9.0f, 4.0f, 2.0f),

            new Vector3(-9.0f, 0.0f, 3.0f),
            new Vector3(-8.0f, 0.0f, 3.0f),
            new Vector3(-7.0f, 0.0f, 3.0f),
            new Vector3(-6.0f, 0.0f, 3.0f),
            new Vector3(-5.0f, 0.0f, 3.0f),
            new Vector3(-4.0f, 0.0f, 3.0f),
            new Vector3(-9.0f, 1.0f, 3.0f),
            new Vector3(-8.0f, 1.0f, 3.0f),
            new Vector3(-7.0f, 1.0f, 3.0f),
            new Vector3(-6.0f, 1.0f, 3.0f),
            new Vector3(-5.0f, 1.0f, 3.0f),
            new Vector3(-4.0f, 1.0f, 3.0f),
            new Vector3(-9.0f, 2.0f, 3.0f),
            new Vector3(-8.0f, 2.0f, 3.0f),
            new Vector3(-7.0f, 2.0f, 3.0f),
            new Vector3(-6.0f, 2.0f, 3.0f),
            new Vector3(-5.0f, 2.0f, 3.0f),
            new Vector3(-4.0f, 2.0f, 3.0f),
            new Vector3(-9.0f, 3.0f, 3.0f),
            new Vector3(-8.0f, 3.0f, 3.0f),
            new Vector3(-7.0f, 3.0f, 3.0f),
            new Vector3(-6.0f, 3.0f, 3.0f),
            new Vector3(-5.0f, 3.0f, 3.0f),
            new Vector3(-4.0f, 3.0f, 3.0f),

            //atap
            new Vector3(-9.0f, 2.0f, -4.0f),
            new Vector3(-8.0f, 2.0f, -4.0f),
            new Vector3(-7.0f, 2.0f, -4.0f),
            new Vector3(-6.0f, 2.0f, -4.0f),
            new Vector3(-5.0f, 2.0f, -4.0f),
            new Vector3(-4.0f, 2.0f, -4.0f),
            new Vector3(-9.0f, 2.0f, 4.0f),
            new Vector3(-8.0f, 2.0f, 4.0f),
            new Vector3(-7.0f, 2.0f, 4.0f),
            new Vector3(-6.0f, 2.0f, 4.0f),
            new Vector3(-5.0f, 2.0f, 4.0f),
            new Vector3(-4.0f, 2.0f, 4.0f),

        };

        private readonly Vector3[] _woodPositions =
        {
            //batang pohon
            new Vector3(-7.0f, 0.0f, -8.0f),
            new Vector3(-6.0f, 0.0f, -8.0f),
            new Vector3(-8.0f, 0.0f, -7.0f),
            new Vector3(-7.0f, 0.0f, -7.0f),
            new Vector3(-7.0f, 1.0f, -7.0f),
            new Vector3(-7.0f, 2.0f, -7.0f),
            new Vector3(-7.0f, 3.0f, -6.0f),
            new Vector3(-7.0f, 3.0f, -8.0f),
            new Vector3(-7.0f, 4.0f, -6.0f),
            new Vector3(-6.0f, 5.0f, -6.0f),
            new Vector3(-7.0f, 5.0f, -7.0f),
            new Vector3(-7.0f, 0.0f, -6.0f),
            new Vector3(-6.0f, 0.0f, -7.0f),
            new Vector3(-6.0f, 1.0f, -7.0f),
            new Vector3(-5.0f, 0.0f, -7.0f),

            new Vector3(7.0f, 0.0f, -8.0f),
            new Vector3(6.0f, 0.0f, -8.0f),
            new Vector3(8.0f, 0.0f, -7.0f),
            new Vector3(7.0f, 0.0f, -7.0f),
            new Vector3(7.0f, 1.0f, -7.0f),
            new Vector3(7.0f, 2.0f, -7.0f),
            new Vector3(7.0f, 3.0f, -6.0f),
            new Vector3(7.0f, 3.0f, -8.0f),
            new Vector3(7.0f, 4.0f, -6.0f),
            new Vector3(6.0f, 5.0f, -6.0f),
            new Vector3(7.0f, 5.0f, -7.0f),
            new Vector3(7.0f, 0.0f, -6.0f),
            new Vector3(6.0f, 0.0f, -7.0f),
            new Vector3(6.0f, 1.0f, -7.0f),
            new Vector3(5.0f, 0.0f, -7.0f),

            //dock
            new Vector3(-1.0f, 0.0f, 3.0f),
            new Vector3(0.0f, 0.0f, 3.0f),
            new Vector3(1.0f, 0.0f, 3.0f),
            new Vector3(-1.0f, 0.0f, 4.0f),
            new Vector3(0.0f, 0.0f, 4.0f),
            new Vector3(1.0f, 0.0f, 4.0f),
            new Vector3(-1.0f, 0.0f, 5.0f),
            new Vector3(0.0f, 0.0f, 5.0f),
            new Vector3(1.0f, 0.0f, 5.0f),
            new Vector3(-1.0f, -0.5f, 6.0f),
            new Vector3(0.0f, -0.5f, 6.0f),
            new Vector3(1.0f, -0.5f, 6.0f),
            new Vector3(-1.0f, -0.5f, 7.0f),
            new Vector3(0.0f, -0.5f, 7.0f),
            new Vector3(1.0f, -0.5f, 7.0f),
            new Vector3(-1.0f, -0.5f, 8.0f),
            new Vector3(0.0f, -0.5f, 8.0f),
            new Vector3(1.0f, -0.5f, 8.0f),

            new Vector3(2.0f, -0.5f, 8.0f),
            new Vector3(2.0f, -0.5f, 7.0f),
            new Vector3(2.0f, -0.5f, 6.0f),
            new Vector3(3.0f, -0.5f, 8.0f),
            new Vector3(3.0f, -0.5f, 7.0f),
            new Vector3(3.0f, -0.5f, 6.0f),
            new Vector3(4.0f, -0.5f, 8.0f),
            new Vector3(4.0f, -0.5f, 7.0f),
            new Vector3(4.0f, -0.5f, 6.0f),

            //rumah
            new Vector3(-3.0f, 2.0f, 0.0f),
            new Vector3(-3.0f, 0.0f, 1.0f),
            new Vector3(-3.0f, 0.0f, -1.0f),
            new Vector3(-3.0f, 1.0f, 1.0f),
            new Vector3(-3.0f, 1.0f, -1.0f),
            new Vector3(-3.0f, 2.0f, 1.0f),
            new Vector3(-3.0f, 2.0f, -1.0f),
            new Vector3(-3.0f, 3.0f, 1.0f),
            new Vector3(-3.0f, 3.0f, -1.0f),
            new Vector3(-3.0f, 4.0f, 1.0f),
            new Vector3(-3.0f, 4.0f, -1.0f),

            new Vector3(-3.0f, 2.0f, 4.0f),
            new Vector3(-3.0f, 2.0f, -4.0f),
            new Vector3(-3.0f, 3.0f, 3.0f),
            new Vector3(-3.0f, 3.0f, -3.0f),
            new Vector3(-3.0f, 4.0f, 2.0f),
            new Vector3(-3.0f, 4.0f, -2.0f),
            new Vector3(-3.0f, 5.0f, 1.0f),
            new Vector3(-3.0f, 5.0f, -1.0f),
            new Vector3(-3.0f, 5.0f, 0.0f),
            new Vector3(-3.0f, 6.0f, 0.0f),

            new Vector3(-10.0f, 2.0f, 4.0f),
            new Vector3(-10.0f, 2.0f, -4.0f),
            new Vector3(-10.0f, 3.0f, 3.0f),
            new Vector3(-10.0f, 3.0f, -3.0f),
            new Vector3(-10.0f, 4.0f, 2.0f),
            new Vector3(-10.0f, 4.0f, -2.0f),
            new Vector3(-10.0f, 5.0f, 1.0f),
            new Vector3(-10.0f, 5.0f, -1.0f),
            new Vector3(-10.0f, 5.0f, 0.0f),
            new Vector3(-10.0f, 6.0f, 0.0f),

        };

        private readonly Vector3[] _leavesPositions =
        {
            //layer3
            new Vector3(-7.0f, 8.0f, -6.0f),
            new Vector3(-6.0f, 8.0f, -6.0f),

            new Vector3(-7.0f, 8.0f, -7.0f),
            new Vector3(-6.0f, 8.0f, -7.0f),

            new Vector3(-7.0f, 8.0f, -8.0f),
            new Vector3(-6.0f, 8.0f, -8.0f),

            //layer2
            new Vector3(-7.0f, 7.0f, -5.0f),
            new Vector3(-6.0f, 7.0f, -5.0f),

            new Vector3(-8.0f, 7.0f, -6.0f),
            new Vector3(-7.0f, 7.0f, -6.0f),
            new Vector3(-6.0f, 7.0f, -6.0f),
            new Vector3(-5.0f, 7.0f, -6.0f),

            new Vector3(-8.0f, 7.0f, -7.0f),
            new Vector3(-7.0f, 7.0f, -7.0f),
            new Vector3(-6.0f, 7.0f, -7.0f),
            new Vector3(-5.0f, 7.0f, -7.0f),

            new Vector3(-8.0f, 7.0f, -8.0f),
            new Vector3(-7.0f, 7.0f, -8.0f),
            new Vector3(-6.0f, 7.0f, -8.0f),
            new Vector3(-5.0f, 7.0f, -8.0f),

            new Vector3(-7.0f, 7.0f, -9.0f),
            new Vector3(-6.0f, 7.0f, -9.0f),

            //layer1
            new Vector3(-7.0f, 6.0f, -4.0f),
            new Vector3(-6.0f, 6.0f, -4.0f),

            new Vector3(-8.0f, 6.0f, -5.0f),
            new Vector3(-7.0f, 6.0f, -5.0f),
            new Vector3(-6.0f, 6.0f, -5.0f),
            new Vector3(-5.0f, 6.0f, -5.0f),

            new Vector3(-9.0f, 6.0f, -6.0f),
            new Vector3(-8.0f, 6.0f, -6.0f),
            new Vector3(-7.0f, 6.0f, -6.0f),
            new Vector3(-6.0f, 6.0f, -6.0f),
            new Vector3(-5.0f, 6.0f, -6.0f),
            new Vector3(-4.0f, 6.0f, -6.0f),

            new Vector3(-9.0f, 6.0f, -7.0f),
            new Vector3(-8.0f, 6.0f, -7.0f),
            new Vector3(-7.0f, 6.0f, -7.0f),
            new Vector3(-6.0f, 6.0f, -7.0f),
            new Vector3(-5.0f, 6.0f, -7.0f),
            new Vector3(-4.0f, 6.0f, -7.0f),

            new Vector3(-9.0f, 6.0f, -8.0f),
            new Vector3(-8.0f, 6.0f, -8.0f),
            new Vector3(-7.0f, 6.0f, -8.0f),
            new Vector3(-6.0f, 6.0f, -8.0f),
            new Vector3(-5.0f, 6.0f, -8.0f),
            new Vector3(-4.0f, 6.0f, -8.0f),

            new Vector3(-8.0f, 6.0f, -9.0f),
            new Vector3(-7.0f, 6.0f, -9.0f),
            new Vector3(-6.0f, 6.0f, -9.0f),
            new Vector3(-5.0f, 6.0f, -9.0f),

            new Vector3(-7.0f, 6.0f, -10.0f),
            new Vector3(-6.0f, 6.0f, -10.0f),

            //layer0
            new Vector3(-8.0f, 5.0f, -5.0f),
            new Vector3(-7.0f, 5.0f, -5.0f),
            new Vector3(-6.0f, 5.0f, -5.0f),
            new Vector3(-5.0f, 5.0f, -5.0f),

            new Vector3(-9.0f, 5.0f, -6.0f),
            new Vector3(-8.0f, 5.0f, -6.0f),
            new Vector3(-7.0f, 5.0f, -6.0f),
            new Vector3(-6.0f, 5.0f, -6.0f),
            new Vector3(-5.0f, 5.0f, -6.0f),
            new Vector3(-4.0f, 5.0f, -6.0f),

            new Vector3(-9.0f, 5.0f, -7.0f),
            new Vector3(-8.0f, 5.0f, -7.0f),
            new Vector3(-7.0f, 5.0f, -7.0f),
            new Vector3(-6.0f, 5.0f, -7.0f),
            new Vector3(-5.0f, 5.0f, -7.0f),
            new Vector3(-4.0f, 5.0f, -7.0f),

            new Vector3(-9.0f, 5.0f, -8.0f),
            new Vector3(-8.0f, 5.0f, -8.0f),
            new Vector3(-7.0f, 5.0f, -8.0f),
            new Vector3(-6.0f, 5.0f, -8.0f),
            new Vector3(-5.0f, 5.0f, -8.0f),
            new Vector3(-4.0f, 5.0f, -8.0f),

            new Vector3(-8.0f, 5.0f, -9.0f),
            new Vector3(-7.0f, 5.0f, -9.0f),
            new Vector3(-6.0f, 5.0f, -9.0f),
            new Vector3(-5.0f, 5.0f, -9.0f),

            //layer-1
            new Vector3(-7.0f, 4.0f, -5.0f),
            new Vector3(-6.0f, 4.0f, -5.0f),

            new Vector3(-8.0f, 4.0f, -6.0f),
            new Vector3(-7.0f, 4.0f, -6.0f),
            new Vector3(-6.0f, 4.0f, -6.0f),
            new Vector3(-5.0f, 4.0f, -6.0f),

            new Vector3(-8.0f, 4.0f, -7.0f),
            new Vector3(-7.0f, 4.0f, -7.0f),
            new Vector3(-6.0f, 4.0f, -7.0f),
            new Vector3(-5.0f, 4.0f, -7.0f),

            new Vector3(-8.0f, 4.0f, -8.0f),
            new Vector3(-7.0f, 4.0f, -8.0f),
            new Vector3(-6.0f, 4.0f, -8.0f),
            new Vector3(-5.0f, 4.0f, -8.0f),

            new Vector3(-7.0f, 4.0f, -9.0f),
            new Vector3(-6.0f, 4.0f, -9.0f),

        };

        private readonly Vector3[] _charPositions =
        {
            //box char thirdperson
            new Vector3(7f, 0f, 0f)
        };

        // We need the point lights' positions to draw the lamps and to get light the materials properly
        private readonly Vector3[] _pointLightPositions =
        {
            new Vector3(-4.0f, 4.8f, 0.0f),
            new Vector3(-9.0f, 4.8f, 0.0f),
            new Vector3(7.0f, 1.2f, -6.0f),
            new Vector3(4.0f, 0.2f, 6.0f),
        };

        private int _vertexBufferObject;
        private int _vaoModel;
        private int _vaoLamp;

        private Shader _lampShader;
        private Shader _waterShader;
        private Shader _groundShader;
        private Shader _stoneShader;
        private Shader _woodShader;
        private Shader _leavesShader;

        private Texture _groundMap;
        private Texture _diffuseMap;
        private Texture _waterMap;
        private Texture _stoneMap;
        private Texture _woodMap;
        private Texture _leavesMap;

        private Shader _charShader;
        private Texture _charMap;

        private Camera _camera;

        private bool _firstMove = true;

        private Vector2 _lastPos;
        float totalTime = 0;
        bool demo = false;
        int scene = 1;
        bool thirdPerson = false;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.0f, 0.0f, 0.1f, 0.0f);

            GL.Enable(EnableCap.DepthTest);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _waterShader = new Shader("Shaders/shader.vert", "Shaders/lighting.frag");
            _stoneShader = new Shader("Shaders/shader.vert", "Shaders/lighting.frag");
            _groundShader = new Shader("Shaders/shader.vert", "Shaders/lighting.frag");
            _woodShader = new Shader("Shaders/shader.vert", "Shaders/lighting.frag");
            _leavesShader = new Shader("Shaders/shader.vert", "Shaders/lighting.frag");
            _charShader = new Shader("Shaders/shader.vert", "Shaders/lighting.frag");

            _lampShader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

            {
                _vaoModel = GL.GenVertexArray();
                GL.BindVertexArray(_vaoModel);

                //box
                var positionLocation = _waterShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);
                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

                var normalLocation = _waterShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalLocation);
                GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

                var texCoordLocation = _waterShader.GetAttribLocation("aTexCoords");
                GL.EnableVertexAttribArray(texCoordLocation);
                GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));

                //ground
                var positionLocation2 = _groundShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation2);
                GL.VertexAttribPointer(positionLocation2, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

                var normalLocation2 = _groundShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalLocation2);
                GL.VertexAttribPointer(normalLocation2, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

                var texCoordLocation2 = _groundShader.GetAttribLocation("aTexCoords");
                GL.EnableVertexAttribArray(texCoordLocation2);
                GL.VertexAttribPointer(texCoordLocation2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));

                //stone
                var positionLocation3 = _stoneShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation3);
                GL.VertexAttribPointer(positionLocation3, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

                var normalLocation3 = _stoneShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalLocation3);
                GL.VertexAttribPointer(normalLocation3, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

                var texCoordLocation3 = _stoneShader.GetAttribLocation("aTexCoords");
                GL.EnableVertexAttribArray(texCoordLocation3);
                GL.VertexAttribPointer(texCoordLocation3, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));

                //wood
                var positionLocation4 = _woodShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation4);
                GL.VertexAttribPointer(positionLocation4, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

                var normalLocation4 = _woodShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalLocation4);
                GL.VertexAttribPointer(normalLocation4, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

                var texCoordLocation4 = _woodShader.GetAttribLocation("aTexCoords");
                GL.EnableVertexAttribArray(texCoordLocation4);
                GL.VertexAttribPointer(texCoordLocation4, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));

                //leaves
                var positionLocation5 = _leavesShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation5);
                GL.VertexAttribPointer(positionLocation5, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

                var normalLocation5 = _leavesShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalLocation5);
                GL.VertexAttribPointer(normalLocation5, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

                var texCoordLocation5 = _leavesShader.GetAttribLocation("aTexCoords");
                GL.EnableVertexAttribArray(texCoordLocation5);
                GL.VertexAttribPointer(texCoordLocation5, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));

                //char
                var positionLocation6 = _waterShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation6);
                GL.VertexAttribPointer(positionLocation6, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

                var normalLocation6 = _waterShader.GetAttribLocation("aNormal");
                GL.EnableVertexAttribArray(normalLocation6);
                GL.VertexAttribPointer(normalLocation6, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

                var texCoordLocation6 = _waterShader.GetAttribLocation("aTexCoords");
                GL.EnableVertexAttribArray(texCoordLocation6);
                GL.VertexAttribPointer(texCoordLocation6, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));

            }

            {
                _vaoLamp = GL.GenVertexArray();
                GL.BindVertexArray(_vaoLamp);

                var positionLocation = _lampShader.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(positionLocation);
                GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            }

            _leavesMap = Texture.LoadFromFile("Resources/pink_leaves.png");
            _woodMap = Texture.LoadFromFile("Resources/accacia.png");
            _stoneMap = Texture.LoadFromFile("Resources/stone.png");
            _groundMap = Texture.LoadFromFile("Resources/grass.jpg");
            _diffuseMap = Texture.LoadFromFile("Resources/water.jpg");
            _waterMap = Texture.LoadFromFile("Resources/water.jpg");
            _charMap = Texture.LoadFromFile("Resources/diamond_block.png");

            _camera = new Camera(new Vector3(-8f, 0.75f, 0f), Size.X / (float)Size.Y);

            CursorGrabbed = true;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            #region animasi
            float time = (float)e.Time; //Deltatime ==> waktu antara frame sebelumnya ke frame berikutnya, gunakan untuk animasi
            totalTime = totalTime + time;
            float cameraSpeed = 2f;
            float rotateSpeed = 100;
            
            if (demo)
            {
                if (scene == 1)
                {
                    _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
                    if(_camera.Position.X >= -1f)
                    {
                        scene++;
                    }
                }
                if (scene == 2) //Lihat Kiri
                {
                    _camera.Yaw -= rotateSpeed * (float)e.Time;
                    if (_camera.Yaw <= -90)
                    {
                        scene++;
                    }
                }
                if (scene == 3)
                {
                    _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
                    if (_camera.Position.Z <= -5f)  //Kiri = -, Kanan = +
                    {
                        scene++;
                    }
                }
                if (scene == 4) //Lihat Kiri
                {
                    _camera.Yaw -= rotateSpeed * (float)e.Time;
                    if (_camera.Yaw <= -180)
                    {
                        scene++;
                    }
                }
                if (scene == 5) //Lihat Kanan sampe belakang
                {
                    _camera.Yaw += rotateSpeed * (float)e.Time;
                    if (_camera.Yaw >= 0)
                    {
                        scene++;
                    }
                }
                if (scene == 6)
                {
                    _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
                    if (_camera.Position.X >= 5f)
                    {
                        scene++;
                    }
                }
                if (scene == 7) //Lihat Kanan sampe belakang
                {
                    _camera.Yaw += rotateSpeed * (float)e.Time;
                    if (_camera.Yaw >= 90)
                    {
                        scene++;
                    }
                }
                if (scene == 8)
                {
                    _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
                    if (_camera.Position.Z >= -1f)  //Kiri = -, Kanan = +
                    {
                        scene++;
                    }
                }
                if (scene == 9) //Lihat Kanan sampe belakang
                {
                    _camera.Yaw += rotateSpeed * (float)e.Time;
                    if (_camera.Yaw >= 180)
                    {
                        scene++;
                    }
                }
                if (scene == 10)
                {
                    _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
                    if (_camera.Position.X <= 0f)
                    {
                        scene++;
                    }
                }
                if (scene == 11) //Lihat Kiri
                {
                    _camera.Yaw -= rotateSpeed * (float)e.Time;
                    if (_camera.Yaw <= 90)
                    {
                        scene++;
                    }
                }
                if (scene == 12)
                {
                    _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
                    if (_camera.Position.Z >= 0f)  //Kiri = -, Kanan = +
                    {
                        scene++;
                    }
                }
                if (scene == 13)
                {
                    _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
                    _camera.Position += _camera.Up * cameraSpeed * (float)e.Time * 0.75f; // Forward
                    if (_camera.Position.Z >= 2f)  //Kiri = -, Kanan = +
                    {
                        scene++;
                    }
                }
                if (scene == 14)
                {
                    _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
                    if (_camera.Position.Z >= 5f)  //Kiri = -, Kanan = +
                    {
                        scene++;
                    }
                }
                if (scene == 15)
                {
                    _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
                    _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time * 0.75f; // Forward
                    if (_camera.Position.Z >= 6f)  //Kiri = -, Kanan = +
                    {
                        scene++;
                    }
                }
                if (scene == 16)
                {
                    _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
                    if (_camera.Position.Z >= 7f)  //Kiri = -, Kanan = +
                    {
                        scene++;
                    }
                }
                if (scene == 17) //Lihat Kiri
                {
                    _camera.Yaw -= rotateSpeed * (float)e.Time;
                    if (_camera.Yaw <= 0)
                    {
                        scene++;
                    }
                }
                if (scene == 18)
                {
                    _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
                    if (_camera.Position.X >= 4f)
                    {
                        scene++;
                    }
                }
                if (scene == 19) //Lihat Kiri
                {
                    _camera.Yaw -= rotateSpeed * (float)e.Time;
                    if (_camera.Yaw <= -115)
                    {
                        scene++;
                    }
                }

            }
            #endregion

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.BindVertexArray(_vaoModel);

            #region texture and box
            //ground texture
            _groundMap.Use(TextureUnit.Texture2);
            _groundShader.Use();
            _groundShader.SetMatrix4("view", _camera.GetViewMatrix());
            _groundShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
            _groundShader.SetVector3("viewPos", _camera.Position);
            _groundShader.SetInt("material.diffuse", 2);
            _groundShader.SetInt("material.specular", 2); //membuat objek memantulkan cahaya
            _groundShader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            _groundShader.SetFloat("material.shininess", 32.0f);

            _groundShader.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            _groundShader.SetVector3("dirLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
            _groundShader.SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
            _groundShader.SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));

            //stone texture
            _stoneMap.Use(TextureUnit.Texture3);
            _stoneShader.Use();
            _stoneShader.SetMatrix4("view", _camera.GetViewMatrix());
            _stoneShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
            _stoneShader.SetVector3("viewPos", _camera.Position);
            _stoneShader.SetInt("material.diffuse", 3);
            _stoneShader.SetInt("material.specular", 3);
            _stoneShader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            _stoneShader.SetFloat("material.shininess", 32.0f);

            _stoneShader.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            _stoneShader.SetVector3("dirLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
            _stoneShader.SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
            _stoneShader.SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));

            //wood texture
            _woodMap.Use(TextureUnit.Texture4);
            _woodShader.Use();
            _woodShader.SetMatrix4("view", _camera.GetViewMatrix());
            _woodShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
            _woodShader.SetVector3("viewPos", _camera.Position);
            _woodShader.SetInt("material.diffuse", 4);
            _woodShader.SetInt("material.specular", 4); //membuat objek memantulkan cahaya
            _woodShader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            _woodShader.SetFloat("material.shininess", 32.0f);

            _woodShader.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            _woodShader.SetVector3("dirLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
            _woodShader.SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
            _woodShader.SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));

            //leaves texture
            _leavesMap.Use(TextureUnit.Texture5);
            _leavesShader.Use();
            _leavesShader.SetMatrix4("view", _camera.GetViewMatrix());
            _leavesShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
            _leavesShader.SetVector3("viewPos", _camera.Position);
            _leavesShader.SetInt("material.diffuse", 5);
            _leavesShader.SetInt("material.specular", 5); //membuat objek memantulkan cahaya
            _leavesShader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            _leavesShader.SetFloat("material.shininess", 32.0f);

            _leavesShader.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            _leavesShader.SetVector3("dirLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
            _leavesShader.SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
            _leavesShader.SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));

            //box
            _diffuseMap.Use(TextureUnit.Texture0);
            _waterMap.Use(TextureUnit.Texture1);
            _waterShader.Use();

            _waterShader.SetMatrix4("view", _camera.GetViewMatrix());
            _waterShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            _waterShader.SetVector3("viewPos", _camera.Position);

            _waterShader.SetInt("material.diffuse", 0);
            _waterShader.SetInt("material.specular", 1);
            _waterShader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            _waterShader.SetFloat("material.shininess", 32.0f);

            //char
            _charMap.Use(TextureUnit.Texture6);
            _charShader.SetMatrix4("view", _camera.GetViewMatrix());
            _charShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
            _charShader.SetVector3("viewPos", _camera.Position);
            _charShader.SetInt("material.diffuse", 6);
            _charShader.SetInt("material.specular", 6); //membuat objek memantulkan cahaya
            _charShader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            _charShader.SetFloat("material.shininess", 32.0f);

            _charShader.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            _charShader.SetVector3("dirLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
            _charShader.SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
            _charShader.SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));
            
            // Directional light
            _waterShader.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            _waterShader.SetVector3("dirLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
            _waterShader.SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
            _waterShader.SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));
            #endregion

            #region lighting
            // Point lights
            for (int i = 0; i < _pointLightPositions.Length; i++)
            {
                _groundShader.SetVector3($"pointLights[{i}].position", _pointLightPositions[i]);
                _groundShader.SetVector3($"pointLights[{i}].ambient", new Vector3(0.05f, 0.05f, 0.05f));
                _groundShader.SetVector3($"pointLights[{i}].diffuse", new Vector3(0.8f, 0.8f, 0.8f));
                _groundShader.SetVector3($"pointLights[{i}].specular", new Vector3(1.0f, 1.0f, 1.0f));
                _groundShader.SetFloat($"pointLights[{i}].constant", 1.0f);
                _groundShader.SetFloat($"pointLights[{i}].linear", 0.09f);
                _groundShader.SetFloat($"pointLights[{i}].quadratic", 0.032f);

                _stoneShader.SetVector3($"pointLights[{i}].position", _pointLightPositions[i]);
                _stoneShader.SetVector3($"pointLights[{i}].ambient", new Vector3(0.05f, 0.05f, 0.05f));
                _stoneShader.SetVector3($"pointLights[{i}].diffuse", new Vector3(0.8f, 0.8f, 0.8f));
                _stoneShader.SetVector3($"pointLights[{i}].specular", new Vector3(1.0f, 1.0f, 1.0f));
                _stoneShader.SetFloat($"pointLights[{i}].constant", 1.0f);
                _stoneShader.SetFloat($"pointLights[{i}].linear", 0.09f);
                _stoneShader.SetFloat($"pointLights[{i}].quadratic", 0.032f);

                _woodShader.SetVector3($"pointLights[{i}].position", _pointLightPositions[i]);
                _woodShader.SetVector3($"pointLights[{i}].ambient", new Vector3(0.05f, 0.05f, 0.05f));
                _woodShader.SetVector3($"pointLights[{i}].diffuse", new Vector3(0.8f, 0.8f, 0.8f));
                _woodShader.SetVector3($"pointLights[{i}].specular", new Vector3(1.0f, 1.0f, 1.0f));
                _woodShader.SetFloat($"pointLights[{i}].constant", 1.0f);
                _woodShader.SetFloat($"pointLights[{i}].linear", 0.09f);
                _woodShader.SetFloat($"pointLights[{i}].quadratic", 0.032f);

                _leavesShader.SetVector3($"pointLights[{i}].position", _pointLightPositions[i]);
                _leavesShader.SetVector3($"pointLights[{i}].ambient", new Vector3(0.05f, 0.05f, 0.05f));
                _leavesShader.SetVector3($"pointLights[{i}].diffuse", new Vector3(0.8f, 0.8f, 0.8f));
                _leavesShader.SetVector3($"pointLights[{i}].specular", new Vector3(1.0f, 1.0f, 1.0f));
                _leavesShader.SetFloat($"pointLights[{i}].constant", 1.0f);
                _leavesShader.SetFloat($"pointLights[{i}].linear", 0.09f);
                _leavesShader.SetFloat($"pointLights[{i}].quadratic", 0.032f);

                _waterShader.SetVector3($"pointLights[{i}].position", _pointLightPositions[i]);
                _waterShader.SetVector3($"pointLights[{i}].ambient", new Vector3(0.05f, 0.05f, 0.05f));
                _waterShader.SetVector3($"pointLights[{i}].diffuse", new Vector3(0.8f, 0.8f, 0.8f));
                _waterShader.SetVector3($"pointLights[{i}].specular", new Vector3(1.0f, 1.0f, 1.0f));
                _waterShader.SetFloat($"pointLights[{i}].constant", 1.0f);
                _waterShader.SetFloat($"pointLights[{i}].linear", 0.09f);
                _waterShader.SetFloat($"pointLights[{i}].quadratic", 0.032f);

                _charShader.SetVector3($"pointLights[{i}].position", _pointLightPositions[i]);
                _charShader.SetVector3($"pointLights[{i}].ambient", new Vector3(0.05f, 0.05f, 0.05f));
                _charShader.SetVector3($"pointLights[{i}].diffuse", new Vector3(0.8f, 0.8f, 0.8f));
                _charShader.SetVector3($"pointLights[{i}].specular", new Vector3(1.0f, 1.0f, 1.0f));
                _charShader.SetFloat($"pointLights[{i}].constant", 1.0f);
                _charShader.SetFloat($"pointLights[{i}].linear", 0.09f);
                _charShader.SetFloat($"pointLights[{i}].quadratic", 0.032f);
            }

            // Spot light
            _groundShader.SetVector3("spotLight.ambient", new Vector3(0.0f, 0.0f, 0.0f));
            _groundShader.SetVector3("spotLight.diffuse", new Vector3(1.0f, 1.0f, 1.0f));
            _groundShader.SetVector3("spotLight.specular", new Vector3(1.0f, 1.0f, 1.0f));
            _groundShader.SetFloat("spotLight.constant", 1.0f);
            _groundShader.SetFloat("spotLight.linear", 0.09f);
            _groundShader.SetFloat("spotLight.quadratic", 0.032f);
            _groundShader.SetFloat("spotLight.cutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            _groundShader.SetFloat("spotLight.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));

            _stoneShader.SetVector3("spotLight.ambient", new Vector3(0.0f, 0.0f, 0.0f));
            _stoneShader.SetVector3("spotLight.diffuse", new Vector3(1.0f, 1.0f, 1.0f));
            _stoneShader.SetVector3("spotLight.specular", new Vector3(1.0f, 1.0f, 1.0f));
            _stoneShader.SetFloat("spotLight.constant", 1.0f);
            _stoneShader.SetFloat("spotLight.linear", 0.09f);
            _stoneShader.SetFloat("spotLight.quadratic", 0.032f);
            _stoneShader.SetFloat("spotLight.cutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            _stoneShader.SetFloat("spotLight.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));

            _woodShader.SetVector3("spotLight.ambient", new Vector3(0.0f, 0.0f, 0.0f));
            _woodShader.SetVector3("spotLight.diffuse", new Vector3(1.0f, 1.0f, 1.0f));
            _woodShader.SetVector3("spotLight.specular", new Vector3(1.0f, 1.0f, 1.0f));
            _woodShader.SetFloat("spotLight.constant", 1.0f);
            _woodShader.SetFloat("spotLight.linear", 0.09f);
            _woodShader.SetFloat("spotLight.quadratic", 0.032f);
            _woodShader.SetFloat("spotLight.cutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            _woodShader.SetFloat("spotLight.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));

            _leavesShader.SetVector3("spotLight.ambient", new Vector3(0.0f, 0.0f, 0.0f));
            _leavesShader.SetVector3("spotLight.diffuse", new Vector3(1.0f, 1.0f, 1.0f));
            _leavesShader.SetVector3("spotLight.specular", new Vector3(1.0f, 1.0f, 1.0f));
            _leavesShader.SetFloat("spotLight.constant", 1.0f);
            _leavesShader.SetFloat("spotLight.linear", 0.09f);
            _leavesShader.SetFloat("spotLight.quadratic", 0.032f);
            _leavesShader.SetFloat("spotLight.cutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            _leavesShader.SetFloat("spotLight.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));

            _waterShader.SetVector3("spotLight.ambient", new Vector3(0.0f, 0.0f, 0.0f));
            _waterShader.SetVector3("spotLight.diffuse", new Vector3(1.0f, 1.0f, 1.0f));
            _waterShader.SetVector3("spotLight.specular", new Vector3(1.0f, 1.0f, 1.0f));
            _waterShader.SetFloat("spotLight.constant", 1.0f);
            _waterShader.SetFloat("spotLight.linear", 0.09f);
            _waterShader.SetFloat("spotLight.quadratic", 0.032f);
            _waterShader.SetFloat("spotLight.cutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            _waterShader.SetFloat("spotLight.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));

            _charShader.SetVector3("spotLight.ambient", new Vector3(0.0f, 0.0f, 0.0f));
            _charShader.SetVector3("spotLight.diffuse", new Vector3(1.0f, 1.0f, 1.0f));
            _charShader.SetVector3("spotLight.specular", new Vector3(1.0f, 1.0f, 1.0f));
            _charShader.SetFloat("spotLight.constant", 1.0f);
            _charShader.SetFloat("spotLight.linear", 0.09f);
            _charShader.SetFloat("spotLight.quadratic", 0.032f);
            _charShader.SetFloat("spotLight.cutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            _charShader.SetFloat("spotLight.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));

            for (int i = 0; i < _waterPosition.Length; i++)
            {
                Matrix4 model = Matrix4.CreateScale(1f); //besar kecil ukuran kotak
                model = model * Matrix4.CreateTranslation(_waterPosition[i]);
                float angle = 1.0f * i;
                _waterShader.SetMatrix4("model", model);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }

            for (int i = 0; i < _groundPositions.Length; i++)
            {
                Matrix4 model = Matrix4.CreateScale(1f); //besar kecil ukuran kotak
                model = model * Matrix4.CreateTranslation(_groundPositions[i]);
                float angle = 1.0f * i;
                _groundShader.SetMatrix4("model", model);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }

            for (int i = 0; i < _stonePositions.Length; i++)
            {
                Matrix4 model = Matrix4.CreateScale(1f); //besar kecil ukuran kotak
                model = model * Matrix4.CreateTranslation(_stonePositions[i]);
                float angle = 1.0f * i;
                _stoneShader.SetMatrix4("model", model);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }

            for (int i = 0; i < _woodPositions.Length; i++)
            {
                Matrix4 model = Matrix4.CreateScale(1f); //besar kecil ukuran kotak
                model = model * Matrix4.CreateTranslation(_woodPositions[i]);
                float angle = 1.0f * i;
                _woodShader.SetMatrix4("model", model);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }

            for (int i = 0; i < _leavesPositions.Length; i++)
            {
                Matrix4 model = Matrix4.CreateScale(1f); //besar kecil ukuran kotak
                model = model * Matrix4.CreateTranslation(_leavesPositions[i]);
                float angle = 1.0f * i;
                _leavesShader.SetMatrix4("model", model);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }

            for (int i = 0; i < _charPositions.Length; i++)
            {
                Matrix4 model = Matrix4.CreateScale(1f); //besar kecil ukuran kotak
                model = model * Matrix4.CreateTranslation(_charPositions[i]);
                float angle = 1.0f * i;
                _charShader.SetMatrix4("model", model);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }

            GL.BindVertexArray(_vaoLamp);

            _lampShader.Use();

            _lampShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());
            // We use a loop to draw all the lights at the proper position
            for (int i = 0; i < _pointLightPositions.Length; i++)
            {
                Matrix4 lampMatrix = Matrix4.CreateScale(0.2f);
                lampMatrix = lampMatrix * Matrix4.CreateTranslation(_pointLightPositions[i]);

                _lampShader.SetMatrix4("model", lampMatrix);

                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }

#endregion

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (!IsFocused)
            {
                return;
            }

            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            const float cameraSpeed = 2f;
            const float sensitivity = 0.4f;

            if (input.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward

                thirdPerson = false;
            }
            if (input.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards

                thirdPerson = false;
            }
            if (input.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left

                thirdPerson = false;
            }
            if (input.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right

                thirdPerson = false;
            }
            if (input.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up

                thirdPerson = false;
            }
            if (input.IsKeyDown(Keys.LeftControl))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down

                thirdPerson = false;
            }
            if (input.IsKeyDown(Keys.D1))
            {
                demo = true;
            }

            # region third person control
            //toggle third person
            if (input.IsKeyDown(Keys.P))
            {
                _camera.Position = _charPositions[0] + new Vector3(2f, 1.5f, 0f);
                _camera.Yaw = 180;
                _camera.Pitch = 0;

                thirdPerson = true;
            }
            if (input.IsKeyDown(Keys.Up) && !checkCollision(_charPositions[0]) && thirdPerson)
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
                _charPositions[0] += _camera.Front * cameraSpeed * (float)e.Time;
            } else if (input.IsKeyDown(Keys.Up) && thirdPerson){
                _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time*2; // Backwards
                _charPositions[0] -= _camera.Front * cameraSpeed * (float)e.Time*2;
            }

            if (input.IsKeyDown(Keys.Down) && !checkCollision(_charPositions[0]) && thirdPerson)
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
                _charPositions[0] -= _camera.Front * cameraSpeed * (float)e.Time;
            } else if (input.IsKeyDown(Keys.Down) && thirdPerson){
                _camera.Position += _camera.Front * cameraSpeed * (float)e.Time*2; // Backwards
                _charPositions[0] += _camera.Front * cameraSpeed * (float)e.Time*2;
            }

            if (input.IsKeyDown(Keys.Left) && !checkCollision(_charPositions[0]) && thirdPerson)
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
                _charPositions[0] -= _camera.Right * cameraSpeed * (float)e.Time;
            }else if(input.IsKeyDown(Keys.Left) && thirdPerson){
                _camera.Position += _camera.Right * cameraSpeed * (float)e.Time*2; // Right
                _charPositions[0] += _camera.Right * cameraSpeed * (float)e.Time*2;
            }

            if (input.IsKeyDown(Keys.Right) && !checkCollision(_charPositions[0]) && thirdPerson)
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
                _charPositions[0] += _camera.Right * cameraSpeed * (float)e.Time;
            } else if (input.IsKeyDown(Keys.Right) && thirdPerson){
                _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time*2; // Left
                _charPositions[0] -= _camera.Right * cameraSpeed * (float)e.Time*2;
            }
            #endregion

            var mouse = MouseState;

            if (_firstMove)
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);

                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity;
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            _camera.Fov -= e.OffsetY;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
            _camera.AspectRatio = Size.X / (float)Size.Y;
        }

        //check collision tidak perlu ngecek dengan sumbu Y, water, dan ground karena camera thirdperson tidak bisa naik turun (melayang)
        private bool checkCollision(Vector3 position){
            bool collision = false;
            float rangeLimit = 1f;
            for (int i = 0; i < _groundPositions.Length; i++)
            {
                Vector3 ground = _groundPositions[i];
                if (Math.Abs(ground.Y - position.Y) < rangeLimit)
                {
                    collision = true;
                }
            }
            for (int i = 0; i<_stonePositions.Length; i++){
                Vector3 stone = _stonePositions[i];
                if(Math.Abs(stone.Z - position.Z) < rangeLimit && Math.Abs(stone.X - position.X) < rangeLimit && Math.Abs(stone.Y - position.Y) < rangeLimit){
                    collision = true;
                }
            }
            for (int i = 0; i<_woodPositions.Length; i++){
                Vector3 wood = _woodPositions[i];
                if(Math.Abs(wood.Z - position.Z) < rangeLimit && Math.Abs(wood.X - position.X) < rangeLimit && Math.Abs(wood.Y - position.Y) < rangeLimit){
                    collision = true;
                }
            }
            for (int i = 0; i<_leavesPositions.Length; i++){
                Vector3 leaves = _leavesPositions[i];
                if(Math.Abs(leaves.Z - position.Z) < rangeLimit && Math.Abs(leaves.X - position.X) < rangeLimit && Math.Abs(leaves.Y - position.Y) < rangeLimit ){
                    collision = true;
                }
            }
            return collision;
        }
    }
}
