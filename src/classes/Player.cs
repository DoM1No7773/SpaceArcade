using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Android.Util;

namespace SpaceArcade;

public class Player : Sprite
{   
    private int speed;
    private float shotMaxTimer;
    private float shotTimer;
    public int hp = 3;
    public Rectangle collisionBox;
    public Player(Texture2D texture, Vector2 Position, Size AnimationSize, float depth) : base(texture, Position, AnimationSize, depth)
    {
        this.scale = (Global.graphics.GraphicsDevice.Viewport.Width / AnimationSize.Width) / 4;
        this.Position = new Vector2((Position.X / 2) - ((AnimationSize.Width * this.scale) / 2), Position.Y / 1.2f);

        collisionBox = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)(this.AnimationSize.Width * this.scale), (int)(this.AnimationSize.Height * this.scale));
        speed = 1000;

        shotMaxTimer = 0.5f;
        shotTimer = shotMaxTimer;
    }
    private float animTimer = 1f;
    public bool Hitted = false;
    public void DamageAnimation(bool hitted){
        animTimer -= (float)Global.gameTime.ElapsedGameTime.TotalSeconds;

        if(hitted)
            if(animTimer<=0){
                if(opacity < 0.99f){
                    opacity+=0.2f;
                }else{
                    this.Hitted = false;
                }
                Log.Info("CosTakiego",""+this.Hitted);
                animTimer=1f;
            }
    }
    public Point GetSpawnArea(){
        var point = new Point(0,(int)(Global.graphics.GraphicsDevice.Viewport.Width - ((scale*AnimationSize.Width))));
        return point;
    }
    public void Fire(){
        shotTimer -= (float)Global.gameTime.ElapsedGameTime.TotalSeconds;

        if(shotTimer <= 0){
            var bulletPos = new Vector2(Position.X+((AnimationSize.Width * scale)/2),Position.Y);
            BulletManager.PlayerBullets.Add(new Bullet(bulletPos, Global.content.Load<Texture2D>("Ammo"),2f));
            shotTimer = shotMaxTimer;
        }
    }
    public void Moving()
    {
        var touch = Global.touchState;
        collisionBox = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)(this.AnimationSize.Width * this.scale), (int)(this.AnimationSize.Height * this.scale));
        if (touch.Count > 0)
        {
            var velocity = Vector2.Zero;
            var gameTime = (float)Global.gameTime.ElapsedGameTime.TotalSeconds;

            var leftBox = new Rectangle(0, 0, Global.graphics.GraphicsDevice.Viewport.Width / 2, Global.graphics.GraphicsDevice.Viewport.Height);
            var rightBox = new Rectangle(Global.graphics.GraphicsDevice.Viewport.Width / 2, 0, Global.graphics.GraphicsDevice.Viewport.Width/2, Global.graphics.GraphicsDevice.Viewport.Height);

            if(leftBox.Contains(touch[0].Position)){
                velocity.X -= 1;
                collisionBox.X += (int)(6 * this.scale);
                collisionBox.Width = (int)((this.AnimationSize.Width  - 12)* this.scale);
                this.AnimationFrame = 2;
            }
            else if(rightBox.Contains(touch[0].Position)){
                velocity.X += 1;
                collisionBox.X += (int)(6 * this.scale);
                collisionBox.Width = (int)((this.AnimationSize.Width  - 12)* this.scale);
                this.AnimationFrame = 1;
            }
               

            if(this.Position.X <= 0)
                velocity.X += 1;
            if(this.Position.X + (this.scale * AnimationSize.Width) >= Global.graphics.GraphicsDevice.Viewport.Width)
                velocity.X -= 1;

            if(velocity != Vector2.Zero)
                velocity.Normalize();

            this.Position += velocity * gameTime * speed;
        }else this.AnimationFrame = 0;   
    }
    public void Update()
    {
        Moving();
        Fire();
        DamageAnimation(Hitted);
        
        if(hp<0) hp=0;

        if(hp==0) Global.gameState = GameState.finished;
        StringManager.topRightBar.score = hp;
    }
}