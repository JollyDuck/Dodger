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
    class TitleScreen : Screen
    {
        Texture2D mTitleScreenBackground;
        public TitleScreen(ContentManager theContent, EventHandler theScreenEvent)
            : base(theScreenEvent)
        {
            mTitleScreenBackground = theContent.Load<Texture2D>("orangeBack");
        }

        //Update all of the elements that need updating in the Title Screen        
        public override void Update(GameTime theTime)
        {
            //Check to see if the Player one controller has pressed the "B" button, if so, then
            //call the screen event associated with this screen
            if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.B) == true)
            {
                ScreenEvent.Invoke(this, new EventArgs());
            }

            base.Update(theTime);
        }

        //Draw all of the elements that make up the Title Screen
        public override void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(mTitleScreenBackground, Vector2.Zero, Color.White);
            base.Draw(theBatch);
        }
    }
}
