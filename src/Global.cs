using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace SpaceArcade;

public enum GameState{
    notStarted,inBattle,finished
}
public struct Global{
    public static ContentManager content;
    public static SpriteBatch spriteBatch;
    public static GraphicsDeviceManager graphics;
    public static GameTime gameTime;
    public static TouchCollection touchState;
    public static GameState gameState;
}