using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using Android.Util;

namespace SpaceArcade;

public static class BulletManager
{
    public static List<Bullet> PlayerBullets;
    public static List<Bullet> EnemyBullets;

    public static void Init()
    {
        PlayerBullets = new List<Bullet>();
        EnemyBullets = new List<Bullet>();
    }

    public static void CheckCollisions(Player player, List<Enemy> enemies)
    {
        var playerRect = player.collisionBox;
        foreach (var item in enemies)
        {
            var enemyRect = new Rectangle((int)item.Position.X, (int)item.Position.Y, (int)(item.AnimationSize.Width * item.scale), (int)(item.AnimationSize.Height * item.scale)-20);
            
            Log.Info("CosTakiego","collision: "+enemyRect.Contains(playerRect));
            if(enemyRect.Intersects(playerRect)){
                item.hp -= 1;
                player.hp -= 1;
                player.AnimationFrame = 3;

                if(player.opacity < 1f){
                    player.hp += 1;
                    player.Hitted = true;
                }
                else{
                    player.opacity = 0.2f;
                    player.Hitted = true;
                }
            }

            if(item.Position.Y >= Global.graphics.GraphicsDevice.Viewport.Height){
                player.hp -= 1;
                item.hp -= 1;
                player.AnimationFrame = 3;
                player.opacity = 0.2f;
                player.Hitted = true;
            }
            
            foreach (var bullet in PlayerBullets)
            {
                var bulletRect = new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, (int)(bullet.texture.Width * bullet.scale), (int)(bullet.texture.Height * bullet.scale));
                if (enemyRect.Intersects(bulletRect)){
                    item.hp -= 1;
                    bullet.lifespan = 0;
                    StringManager.topLeftBar.score++;
                }
            }
        }

        foreach (var bullet in EnemyBullets)
        {
            var bulletRect = new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, (int)(bullet.texture.Width * bullet.scale), (int)(bullet.texture.Height * bullet.scale));
            if (playerRect.Intersects(bulletRect)){
                player.hp -= 1;
                bullet.lifespan = 0;

                if(player.opacity < 1f){
                    player.hp += 1;
                    player.Hitted = true;
                }
                else{
                    player.opacity = 0.2f;
                    player.Hitted = true;
                }
            }
        }
    }

    public static void Update(Player player, List<Enemy> enemies)
    {
        CheckCollisions(player, enemies);

        foreach (var item in PlayerBullets)
            item.Update();

        PlayerBullets.RemoveAll(b => b.lifespan <= 0);

        foreach (var item in EnemyBullets)
            item.Update();

        EnemyBullets.RemoveAll(b => b.lifespan <= 0);
    }

    public static void Draw()
    {
        foreach (var item in PlayerBullets)
            item.Draw();

        foreach (var item in EnemyBullets)
            item.Draw();
    }
}