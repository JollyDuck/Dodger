using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Dodger
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int die1;
        int die2;
        int timeRolling;
        int rollTime;
        bool rolling;

        SpriteFont font;

        Random rand;

        //Gamepad states to deturmine if buttons have been pushed.
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;


        public Game1()
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
            die1 = 1;
            die2 = 1;
            timeRolling = 0;
            rollTime = 1000;            
            rolling = false;
            rand = new Random();
            font = Content.Load<SpriteFont>("gameFont");
            // TODO: Add your initialization logic here

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

            // TODO: use this.Content to load your game content here
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
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            if (!rolling)
            {
                if (currentGamePadState.Buttons.A == ButtonState.Pressed)
                {
                    rollDie();
                    rolling = true;
                }
            }
            else
            {
                timeRolling++;
                if (timeRolling >= rollTime)
                {
                    rolling = false;
                    timeRolling = 0;
                }
            }
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Text", new Vector2(50, 50), Color.Black);
            spriteBatch.DrawString(font, die1.ToString(), new Vector2(50, 250), Color.Black);
            spriteBatch.DrawString(font, die2.ToString(), new Vector2(50, 450), Color.Black);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        void rollDie()
        {
            die1 = rand.Next(1, 7);
            die2 = rand.Next(1, 7);
        }

    }
}
