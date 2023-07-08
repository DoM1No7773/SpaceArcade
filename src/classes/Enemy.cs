using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Android.Util;

namespace SpaceArcade;

public class Enemy : Sprite{
    private int speed;
    private float shotMaxTimer;
    private float shotTimer;
    public float lifespan;
    public int hp = 1;
    public Enemy(Texture2D texture, Vector2 Position, Size AnimationSize, float depth) : base(texture, Position, AnimationSize, depth)
    {
        this.scale = (Global.graphics.GraphicsDevice.Viewport.Width / AnimationSize.Width) / 6;
        this.Position = Position;

        speed = 200;

        lifespan = 10f;

        shotMaxTimer = 2f;
        shotTimer = shotMaxTimer;
    }

    public void Fire(){
        shotTimer -= (float)Global.gameTime.ElapsedGameTime.TotalSeconds;

        if(shotTimer <= 0){
            var bulletPos = new Vector2(Position.X+((AnimationSize.Width * scale)/2),Position.Y);
            BulletManager.EnemyBullets.Add(new Bullet(bulletPos, Global.content.Load<Texture2D>("EnemyAmmo"),2f));
            shotTimer = shotMaxTimer;
        }
    }

    public void Moving(){
        var deltaTime = (float)Global.gameTime.ElapsedGameTime.TotalSeconds;
        lifespan -= deltaTime;
        var velocity = Vector2.Zero;

        velocity.Y += 1;

        if(velocity != Vector2.Zero)
            velocity.Normalize();

        this.Position += velocity * deltaTime * speed;
    }

    public void Update(Player player){
        Moving();
        Fire();
    }
}