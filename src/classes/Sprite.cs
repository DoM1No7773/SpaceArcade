using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace SpaceArcade;

public struct Size{
    public int Width{get;set;}
    public int Height{get;set;}

    public Size(int value){
        this.Width = value;
        this.Height = value;
    }
    public Size(int Width, int Height){
        this.Width = Width;
        this.Height = Height;
    }

}

public class Sprite{
    protected readonly Texture2D texture;
    public Vector2 Position;

    public int AnimationFrame;
    public int AnimationRow;

    public float opacity = 1f;
    public Size AnimationSize;
    protected Rectangle sourceRect;
    public float scale = 1f;
    protected float depth;
    public Sprite(Texture2D texture, Vector2 Position, Size AnimationSize, float depth){
        this.texture = texture;
        this.Position = Position;
        this.AnimationSize = AnimationSize;

        this.AnimationFrame = 0;
        this.AnimationRow = 0;
        this.depth = depth;
    }

    public void Draw(){
        this.sourceRect = new Rectangle(AnimationFrame * AnimationSize.Width, AnimationRow * AnimationSize.Height, AnimationSize.Width, AnimationSize.Height);
        Global.spriteBatch.Draw(texture, Position, sourceRect, Color.White * opacity, 0f, Vector2.Zero, scale, SpriteEffects.None, depth);
    }
}