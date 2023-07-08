using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Android.Util;

namespace SpaceArcade;
public struct GameManager{

    private Player player;
    public GameManager(){
        BackgroundManager.Init();
        HighScoreManager.Init();
        var WindowSize = new Size(Global.graphics.GraphicsDevice.Viewport.Width, Global.graphics.GraphicsDevice.Viewport.Height);
        player = new(Global.content.Load<Texture2D>("Player"),new Vector2(WindowSize.Width,WindowSize.Height), new Size(32, 16), 0.8f);
        BulletManager.Init();
        EnemyManager.Init();
        StringManager.Init();
        Global.gameState = GameState.notStarted;
        StringManager.countingText.score = 3;
    }
    private float waitSecs = 2;
    private int counting=3;
    public void Update(){
        waitSecs -= (float) Global.gameTime.ElapsedGameTime.TotalSeconds;

        if(Global.gameState == GameState.notStarted){
            waitSecs -= (float) Global.gameTime.ElapsedGameTime.TotalSeconds;

            if(waitSecs <= 0){
                waitSecs = 2;
                counting-=1;
                StringManager.countingText.score = counting;
                if(counting<=0){
                    StringManager.countingTextOpacity = 0f;
                    Global.gameState = GameState.inBattle;
                }
            }
        }
    
        if(Global.gameState == GameState.inBattle){
            BackgroundManager.Update();
            BulletManager.Update(player, EnemyManager.enemies);
            EnemyManager.Update(player);
            player.Update();
        }

        if(Global.gameState == GameState.finished){
            HighScoreManager.Load();
            var score = StringManager.topLeftBar.score;
            var highScore = HighScoreManager.Score;

            if(score > highScore){
                StringManager.endInfoContent = "NEW HIGHSCORE:";
                StringManager.endInfoOpacity = 1f;
                HighScoreManager.Save(score);
            }else{
                StringManager.endInfoContent = "HIGHSCORE:";
                StringManager.endInfoOpacity = 1f;
            }
            StringManager.endInfo.score = highScore;
            Log.Info("CosTakiego",""+StringManager.endInfo.score);
            StringManager.endInfo.Position = new Vector2((Global.graphics.GraphicsDevice.Viewport.Width / 2) - ((StringManager.endInfo.spriteFont.MeasureString(StringManager.endInfoContent+StringManager.endInfo.score).X * StringManager.endInfo.scale)/2),500);
        }
    }
    public void Draw(){
        BackgroundManager.Draw();
        player.Draw();
        BulletManager.Draw();
        EnemyManager.Draw();
        StringManager.Draw();
    }
}