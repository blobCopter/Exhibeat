﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Humble;
using Humble.Screens;
using Humble.Components;
using Humble.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Exhibeat.Components;
using Exhibeat.Settings;
using Exhibeat.Gameplay;
using Exhibeat.Shaders;
using Exhibeat.Rhythm;

namespace Exhibeat.Screens
{
    /// <summary>
    /// Le jeu en lui meme
    /// Ce screen sera cree au dessus du SongSelectionScreen
    /// TODO : prendre la chanson a jouer en parametre lors de la creation
    /// </summary>
    class GameScreen : Screen, ITimeEventReciever
    {
        private HexPad pad;
        private Visualizer visualizer;
        private LifeBar lifebar;
        private NoteGradeDisplay grades;
        private BlurEffect blurEffect = null;
        private ScrollingBackground scrollingbackground;
        private ScoreLogger scoreLogger;

        private Texture2D background;
        private Rectangle background_dest;
        //private AnimatedSprite runner;

        private MapReader mapReader;
        private MapPreview _mapPreview;

        public GameScreen(HumbleGame game)
            : base(game)
        {

        }

        public GameScreen(HumbleGame game, MapPreview map)
            : base(game)
        {
            _mapPreview = map;
        }

        public override void Initialize()
        {
            pad = new HexPad(Content, ExhibeatSettings.WindowWidth / 2, ExhibeatSettings.WindowHeight / 2);
            pad.Initialize();
            pad.CenteredOrigin = true;
            pad.Scale = 1;

            grades = new NoteGradeDisplay(Content);

            scrollingbackground = new ScrollingBackground(Content);

            mapReader = new MapReader();
            mapReader.Initialize(Content);
            mapReader.RegisterNewReciever(pad);

            mapReader.Read(_mapPreview.FilePath);
            mapReader.Play();

            mapReader.RegisterNewReciever(this);

            scoreLogger = new ScoreLogger();

            visualizer = new Visualizer(Content, 0, 0, 0, ExhibeatSettings.WindowHeight / 3, 50);
            mapReader.RegisterNewReciever(visualizer);
            mapReader.RegisterNewReciever(scoreLogger);

            lifebar = new LifeBar(Content, scoreLogger, 30, 30);

            //runner = new AnimatedSprite(Content.Load<Texture2D>("running-test"), Content.Load<SpriteSheet>("running-test-sheet"), new Vector2(100, 100), false);
            //runner.Position = new Vector2(0, 0/* ExhibeatSettings.WindowHeight - Content.Load<Texture2D>("running-test").Height / 2*/);

            background = Content.Load<Texture2D>("wp_back");
            background_dest = new Rectangle(0, 0, ExhibeatSettings.WindowWidth, ExhibeatSettings.WindowHeight);

            base.Initialize();
        }

        int osef = 0;
        public override void Update(GameTime gameTime)
        {
            mapReader.Update(gameTime);

            if (mapReader.getMapCompletion() >= 30)
            {
                mapReader.Stop();
                ScreenManager.Singleton.popScreen();
                ScreenManager.Singleton.pushScreen(new ScoreScreen(this.Game, scoreLogger));
            }

            scrollingbackground.Update(gameTime);
            visualizer.Update(gameTime);
            pad.Update(gameTime);
            lifebar.Update(gameTime);
            grades.Update(gameTime);
            scoreLogger.Update(gameTime);
            //runner.Update(gameTime);

            base.Update(gameTime);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void Draw()
        {

           /* RenderTarget2D tmp_buf = new RenderTarget2D(SpriteBatch.GraphicsDevice, SpriteBatch.GraphicsDevice.Viewport.Width, SpriteBatch.GraphicsDevice.Viewport.Height);
            SpriteBatch.GraphicsDevice.SetRenderTarget(tmp_buf);

            SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            SpriteBatch.Draw(background, background_dest, Color.White);
            SpriteBatch.End();*/


            ////SHADERS START
            if (blurEffect == null)
                 blurEffect = new BlurEffect(SpriteBatch.GraphicsDevice, Content);
            
            blurEffect.start();
            SpriteBatch.Begin();

            SpriteBatch.Draw(background, background_dest, Color.White);
            scrollingbackground.Draw(SpriteBatch);
            visualizer.Draw(SpriteBatch);

            //pad.Draw(SpriteBatch);
            //lifebar.Draw(SpriteBatch);

            // SHADERS END
            SpriteBatch.End();
            blurEffect.applyEffect(SpriteBatch);

        
            SpriteBatch.Begin();
            
            pad.Draw(SpriteBatch);
            lifebar.Draw(SpriteBatch);
            grades.Draw(SpriteBatch);

            //visualizer.Draw(SpriteBatch);
            //runner.Draw(SpriteBatch);

            SpriteBatch.End();
            base.Draw();
        }

        public void NewSongEvent(songEvent ev, object param)
        {
        }

        public void NewUserEvent(userEvent ev, object param)
        {
            switch (ev)
            {
                case userEvent.NOTEFAIL:
                    grades.DisplayFail();
                    break;
                case userEvent.NOTEBAD:
                    grades.DisplayBad();
                    break;
                case userEvent.NOTEGOOD:
                    grades.DisplayGood();
                    break;
                case userEvent.NOTENORMAL:
                    grades.DisplayNormal();
                    break;
                case userEvent.NOTEVERYGOOD:
                    grades.DisplayVeryGood();
                    break;
            }
        }
    }
}
