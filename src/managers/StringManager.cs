using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceArcade;

public class StringStruct{
    public SpriteFont spriteFont {get;set;}
    public int score {get;set;}
    public Vector2 Position {get;set;}
    public Color color = Color.White;
    public float rotation = 0f;
    public Vector2 origin = Vector2.Zero;
    public float scale {get;set;}
    public SpriteEffects spriteEffects = SpriteEffects.None;
    public float depth {get;set;}
}
public static class StringManager{

    public static StringStruct topLeftBar;
    public static StringStruct topRightBar;
    public static Texture2D hearts;
    public static StringStruct countingText;

    public static StringStruct endInfo;
    public static float countingTextOpacity;
    public static float endInfoOpacity = 0f;
    public static void Init(){
        var font = Global.content.Load<SpriteFont>("Font/PublicPixel");
        topLeftBar = new StringStruct(){
            spriteFont = font,
            score = 0,
            Position = new Vector2(0,0),
            scale = 2f,
            depth = 1f,
        };
        topRightBar = new StringStruct(){
            spriteFont = font,
            score = 0,
            Position = new Vector2(650,0),
            scale = 2f,
            depth = 1f,
        };
        hearts = Global.content.Load<Texture2D>("Heart");
        countingText = new StringStruct(){
            spriteFont = font,
            score = 3,
            scale = 4f,
            Position = new Vector2(0,0),
            depth = 1f,
        };

        countingText.Position = new Vector2((Global.graphics.GraphicsDevice.Viewport.Width / 2) - ((countingText.spriteFont.MeasureString(""+countingText.score).X * countingText.scale)/2),500);
        countingTextOpacity=1f;

        endInfo = new StringStruct(){
            spriteFont = font,
            score = 0,
            scale = 2f,
            Position = new Vector2(0,0),
            depth = 1f,
        };
    }

    public static string endInfoContent = "";
    public static void Draw(){
        Global.spriteBatch.DrawString(topLeftBar.spriteFont,"SCORE:"+topLeftBar.score,topLeftBar.Position,topLeftBar.color,topLeftBar.rotation,
        topLeftBar.origin,topLeftBar.scale,topLeftBar.spriteEffects,topLeftBar.depth);

        Global.spriteBatch.Draw(hearts,new Vector2(topRightBar.Position.X-50,0), null, Color.White*0.8f, 0f, Vector2.Zero, 5f, SpriteEffects.None, 1f);

        Global.spriteBatch.DrawString(topRightBar.spriteFont,"X"+topRightBar.score,topRightBar.Position,topRightBar.color,topRightBar.rotation,
        topRightBar.origin,topRightBar.scale,topRightBar.spriteEffects,topRightBar.depth);

        Global.spriteBatch.DrawString(countingText.spriteFont,""+countingText.score,countingText.Position,countingText.color * countingTextOpacity,countingText.rotation,
        countingText.origin,countingText.scale,countingText.spriteEffects,countingText.depth);

        Global.spriteBatch.DrawString(endInfo.spriteFont,endInfoContent+endInfo.score,endInfo.Position,endInfo.color * endInfoOpacity,endInfo.rotation,
        endInfo.origin,endInfo.scale,endInfo.spriteEffects,endInfo.depth);
    }
}