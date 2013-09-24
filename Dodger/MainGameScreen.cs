using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Dodger
{
    class MainGameScreen : Screen
    {
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
        Texture2D mMainGameScreenBackground;
        public MainGameScreen(ContentManager Content)
        {
            mMainGameScreenBackground = Content.Load<Texture2D>("orangeBack");
            minionManager = new MinionManager(15);
            rand = new Random();
            waitForReset = false;
            buttonRepeatLimit = 8;
            wavesSurvived = 0;
            font = Content.Load<SpriteFont>("gameFont");
            redBox = Content.Load<Texture2D>("redBox");
            Wave = TimeSpan.Zero;
        }

        //Update all of the elements that need updating in the Title Screen        
        public override void Update(GameTime gameTime)
        {
            handleInput();
            waveAttack(gameTime);
            //Check to see if the Player one controller has pressed the "B" button, if so, then
            //call the screen event associated with this screen
            if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.B) == true)
            {
                ScreenEvent.Invoke(this, new EventArgs());
            }

            base.Update(gameTime);
        }

        //Draw all of the elements that make up the Title Screen
        public override void Draw(SpriteBatch spriteBatch)
        {

            drawBoxes(new Rectangle(64, 64, 64, 64), new Rectangle(138, 64, 64, 64), new Rectangle(212, 64, 64, 64), spriteBatch);
            spriteBatch.DrawString(font, minionManager.minionsIn(0).ToString(), new Vector2(66, 66), Color.Black);
            spriteBatch.DrawString(font, minionManager.minionsIn(1).ToString(), new Vector2(140, 66), Color.Black);
            spriteBatch.DrawString(font, minionManager.minionsIn(2).ToString(), new Vector2(214, 66), Color.Black);
            base.Draw(spriteBatch);
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
        }
        void drawBoxes(Rectangle a, Rectangle b, Rectangle c, SpriteBatch spriteBatch)
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
                minionManager.killMinionsIn(rand.Next(0, 3));
                waitForReset = true;
            }
        }
    }
}
