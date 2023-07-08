using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace SpaceArcade;

public static class EnemyManager{

    public static List<Enemy> enemies;

    public static float enemyMaxColdown;
    public static float enemyColdown;
    private static Random random;
    public static void Init(){
        enemies = new List<Enemy>();
        enemyMaxColdown = 1f;
        enemyColdown = enemyMaxColdown;
        random = new();
    }

    public static Vector2 RandomSpawn(Point point){
        var spawn = Vector2.Zero;

        spawn.Y = -100;
        spawn.X = (float)(random.NextDouble() * (point.Y - point.X) + point.X);
        return spawn;
    }

    public static void AddEnemy(Player player){
        enemies.Add(new Enemy(Global.content.Load<Texture2D>("Enemy0"),RandomSpawn(player.GetSpawnArea()),new Size(16,8),0.8f));
    }

    public static void Update(Player player){
        enemyColdown -= (float)Global.gameTime.ElapsedGameTime.TotalSeconds;

        if(enemyColdown <= 0){
            AddEnemy(player);
            enemyColdown = enemyMaxColdown;
        }

        foreach(var item in enemies)
            item.Update(player);
        
        enemies.RemoveAll(e=>e.lifespan<=0||e.hp <= 0);
    }

    public static void Draw(){
        foreach(var item in enemies)
            item.Draw();
    }
}