﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Exhibeat.AudioPlayer;

namespace Exhibeat.Settings
{
    /// <summary>
    /// Contains all the settings and project configuration
    /// </summary>
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
        public static int TileGrowthDuration = 700;

        public static int ScoreScreenDuration = 10; // SECONDS

        public static int TimeElapsed { get; set; }


        /**
         *  PARAMETERS FOR SLIDING BACKGROUND
         */
        public static bool SlidingBackground = true;
        public static bool SlidingBackgroundColorChange = true;


        public static string ResourceFolder = ".\\test\\";

        private static IAudioManager audioManager = null;

        public static IAudioManager GetAudioManager()
        {
            if (audioManager == null)
            {
                audioManager = new AudioManager();
                audioManager.init();
            }
            return audioManager;
        }

        public static void CloseAudioManager()
        {
            if (audioManager != null)
                audioManager.destroy();
        }


#if ANIMATED_TILE
        public static String GrowthAnimationName = "tile_animation_test";
#endif

    
    
    }
}