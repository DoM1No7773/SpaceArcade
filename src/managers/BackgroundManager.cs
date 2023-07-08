using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Android.Util;

namespace SpaceArcade;

public class BackgroundManager{
    public static Background background1;
    public static Background background2;

    public static void Init(){
        background1 = new(Global.content.Load<Texture2D>("background"),Vector2.Zero);
        background2 = new(Global.content.Load<Texture2D>("background"),Vector2.Zero);

        var bgPos1 = new Vector2(0,0 - (background1.size.Height - Global.graphics.GraphicsDevice.Viewport.Height));
        var bgPos2 = new Vector2(0,bgPos1.Y - background2.size.Height);

        background1.Position = bgPos1;
        background2.Position = bgPos2;
    }
    public static void Update(){
        if(background1.Position.Y >= Global.graphics.GraphicsDevice.Viewport.Height){
            var bgPos = new Vector2(0,background2.Position.Y - background1.size.Height);
            background1.Position = bgPos;
        }
        if(background2.Position.Y >= Global.graphics.GraphicsDevice.Viewport.Height){
            var bgPos = new Vector2(0,background1.Position.Y - background2.size.Height);
            background2.Position = bgPos;
        }
        background1.Update();
        background2.Update();
    }
    public static void Draw(){
        background1.Draw();
        background2.Draw();
    }
}