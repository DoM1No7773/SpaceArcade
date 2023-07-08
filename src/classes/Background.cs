using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Android.Util;

namespace SpaceArcade;
public struct Background{

    private Texture2D texture;
    public Vector2 Position;
    public float scale;
    public Size size;
    public int speed = 1500;
    public Background(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        this.Position = position;
        this.scale = Global.graphics.GraphicsDevice.Viewport.Width / texture.Width+2;
        size = new((int)(texture.Width * scale),(int)(texture.Height * scale));
    }
    public void Update(){
        var velocity = Vector2.Zero;
        var gameTime = (float)Global.gameTime.ElapsedGameTime.TotalSeconds;

        velocity.Y += 1;

        if(velocity != Vector2.Zero)
            velocity.Normalize();

        this.Position += velocity * gameTime * speed;
    }

    public void Draw(){
        Global.spriteBatch.Draw(texture,Position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.01f);
    }
}