using LumiEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bloomstead;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = Config.WindowWidth;
        _graphics.PreferredBackBufferHeight = Config.WindowHeight;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Config.Graphics = GraphicsDevice;
        Config.Batch = _spriteBatch;
        Config.Content = Content;
        Config.LoadContent();
        
        SceneManager.AddScene("overworld", new OverworldScene());
        
        SceneManager.ChangeScene("overworld");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Config.Time = gameTime;
        
        SceneManager.CurrentScene.Update();
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        
        Config.Batch.Begin(SpriteSortMode.BackToFront, null, SamplerState.PointClamp);
        SceneManager.CurrentScene.Render();
        Config.Batch.End();
        
        base.Draw(gameTime);
    }
}