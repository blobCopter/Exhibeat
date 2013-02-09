﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Exhibeat.Settings
{
    public class ExhibeatSettings
    {
        public static bool ResolutionIndependent = true;
        public static bool Fullscreen = false;
        public static int WindowHeight = 720;
        public static int WindowWidth = 1280;

        public static Vector2[] TilePositions = {
            new Vector2(0.5f, 0.0f),
            new Vector2(1.5f, 0.0f),
            new Vector2(0, 0.75f),
            new Vector2(1, 0.75f),
            new Vector2(2, 0.75f),
            new Vector2(0.5f, 1.5f),
            new Vector2(1.5f, 1.5f)
        };

        public static bool TileGrowth = true;
        public static float TileGrowthStartScale = 1;
        public static int TileGrowthDuration = 1000;

#if ANIMATED_TILE
        public static String GrowthAnimationName = "tile_animation_test";
#endif
    }
}