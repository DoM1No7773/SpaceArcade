using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace SpaceArcade;

public class Game1 : Game
{
    private GameManager gameManager;
    public Game1()
    {
        Global.graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Global.content = Content;

        Global.graphics.IsFullScreen = true;
        Global.graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        Global.graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        Global.graphics.SupportedOrientations = DisplayOrientation.Portrait;
        Global.graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        Global.spriteBatch = new SpriteBatch(GraphicsDevice);
        gameManager = new();
    }

    protected override void Update(GameTime gameTime)
    {   
        Global.gameTime = gameTime;
        Global.touchState = TouchPanel.GetState();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        gameManager.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        Global.spriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack);
        gameManager.Draw();
        Global.spriteBatch.End();

        base.Draw(gameTime);
    }
}
