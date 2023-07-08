using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace SpaceArcade;

public class Bullet{
    public Vector2 Position;
    public Texture2D texture;
    public float lifespan;
    private Vector2 velocity;
    public float scale;
    private float speed;
    public Bullet(Vector2 Position, Texture2D texture, float lifespan){
        this.texture = texture;
        this.lifespan = lifespan;
        this.velocity = Vector2.Zero;
        this.scale = (Global.graphics.GraphicsDevice.Viewport.Width / texture.Width) / 36;
        
        this.Position = new Vector2(Position.X - ((texture.Width*scale)/2),Position.Y);
        speed = 1000;
    }

    public void Update(){
        var deltaTime = (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
        lifespan -= deltaTime;

        if(texture.Name == "Ammo")
            velocity.Y -= 1;
        if(texture.Name == "EnemyAmmo")
            velocity.Y += 1;

        if(velocity != Vector2.Zero)
            velocity.Normalize();
        this.Position += velocity * deltaTime * speed;
    }

    public void Draw(){
        Global.spriteBatch.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.6f);
    }
}