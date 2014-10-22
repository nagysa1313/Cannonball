#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Diagnostics;
using System.Linq;
using Cannonball.Engine.Utils.Diagnostics;
using Cannonball.Engine.GameObjects;
using Cannonball.Engine.Graphics;
using Cannonball.GameObjects;
#endregion

namespace Cannonball
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class WorldTestGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        RenderTarget2D sceneTarget;
        Renderer Renderer;
        World World;

        public WorldTestGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            sceneTarget = new RenderTarget2D(GraphicsDevice
                , GraphicsDevice.PresentationParameters.BackBufferWidth
                , GraphicsDevice.PresentationParameters.BackBufferHeight
                , false
                , GraphicsDevice.PresentationParameters.BackBufferFormat
                , GraphicsDevice.PresentationParameters.DepthStencilFormat);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), spriteBatch);
            DiagnosticsManager.Initialize(this);
            SpriteBatchHelpers.Initialize(GraphicsDevice);
            Renderer = new Engine.Graphics.Renderer();

            // TODO: use this.Content to load your game content here
            World = new World(Renderer);



            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            DiagnosticsManager.Instance.TimeRuler.StartFrame();
            DiagnosticsManager.Instance.TimeRuler.BeginMark("Update", Color.Blue);

            World.Update(gameTime);

            base.Update(gameTime);

            DiagnosticsManager.Instance.TimeRuler.EndMark("Update");
        }

        private void DrawScene(GameTime gameTime)
        {
            DiagnosticsManager.Instance.TimeRuler.BeginMark("DrawScene", Color.Red);
            GraphicsDevice.SetRenderTarget(sceneTarget);

            GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };

            GraphicsDevice.Clear(Color.Black);

            // TODO: Renderer.Draw(GraphicsDevice, gameTime);

            GraphicsDevice.SetRenderTarget(null);
            DiagnosticsManager.Instance.TimeRuler.EndMark("DrawScene");
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            DiagnosticsManager.Instance.TimeRuler.BeginMark("Draw", Color.Green);

            DrawScene(gameTime);

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                        SamplerState.LinearClamp, DepthStencilState.Default,
                        RasterizerState.CullNone);

            spriteBatch.Draw(sceneTarget, new Rectangle(0, 0
                , GraphicsDevice.PresentationParameters.BackBufferWidth
                , GraphicsDevice.PresentationParameters.BackBufferHeight), Color.White);

            base.Draw(gameTime);

            spriteBatch.End();

            DiagnosticsManager.Instance.TimeRuler.EndMark("Draw");
        }
    }
}