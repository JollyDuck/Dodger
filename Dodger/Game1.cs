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
        TimeSpan Wave, resetTime;
        byte buttonRepeatLimit;
        int buttonReleaseTimer, wavesSurvived;
        Vector2 thumb1, thumb2;
        MinionManager minionManager;
        SpriteFont font;
        Random rand;
        Texture2D redBox;
        bool waitForReset;

        //Gamepad states to deturmine if buttons have been pushed.
        GamePadState currentGamePadState;


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
            minionManager = new MinionManager(15);
            rand = new Random();
            waitForReset = false;
            buttonRepeatLimit = 8;
            wavesSurvived = 0;
            font = Content.Load<SpriteFont>("gameFont");
            redBox = Content.Load<Texture2D>("redBox");
            Wave = TimeSpan.Zero;
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
            handleInput();
            waveAttack(gameTime);

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
            drawBoxes(new Rectangle(64,64,64,64), new Rectangle(138,64,64,64), new Rectangle(212,64,64,64));
            spriteBatch.DrawString(font, minionManager.minionsIn(0).ToString(), new Vector2(66, 66),Color.Black);
            spriteBatch.DrawString(font, minionManager.minionsIn(1).ToString(), new Vector2(140, 66), Color.Black);
            spriteBatch.DrawString(font, minionManager.minionsIn(2).ToString(), new Vector2(214, 66), Color.Black);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        void handleInput()
        {
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            thumb1 = currentGamePadState.ThumbSticks.Left;
            thumb2 = currentGamePadState.ThumbSticks.Right;

            if (currentGamePadState.Buttons.LeftShoulder == ButtonState.Pressed)
            {
                if (buttonReleaseTimer > buttonRepeatLimit)
                {
                    minionManager.moveMinion(0);
                    buttonReleaseTimer = 0;
                }
            }
            else if (currentGamePadState.Buttons.RightShoulder == ButtonState.Pressed)
            {
                if (buttonReleaseTimer > buttonRepeatLimit)
                {
                    minionManager.moveMinion(2);
                    buttonReleaseTimer = 0;
                }
            }

            if (buttonReleaseTimer < buttonRepeatLimit + 1) buttonReleaseTimer++;

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
        }
        void drawBoxes(Rectangle a, Rectangle b, Rectangle c)
        {
            spriteBatch.Draw(redBox, a, Color.White);
            spriteBatch.Draw(redBox, b, Color.White);
            spriteBatch.Draw(redBox, c, Color.White);
        }

        void waveAttack(GameTime gameTime)
        {
            Wave += gameTime.ElapsedGameTime;

            if (waitForReset)
            {
                resetTime += gameTime.ElapsedGameTime;
                if (resetTime > TimeSpan.FromSeconds(1))
                {

                    minionManager.reset();
                    waitForReset = false;
                }
            }
            else if (Wave > TimeSpan.FromSeconds(15))
            {
                Wave = TimeSpan.Zero;
                minionManager.killMinionsIn(rand.Next(0,3));
                waitForReset = true;
            }
        }
    }
}
