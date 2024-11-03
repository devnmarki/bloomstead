using Bloomstead.Bloomstead.Game_Objects;
using LumiEngine;
using LumiEngine.Input;
using LumiEngine.LevelEditor;
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
        
        TilemapManager.AddGameObjectToLoad("Farmer", () => new Farmer());
        
        SceneManager.ChangeScene("overworld");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        KeyboardHandler.GetState();
        
        Config.Time = gameTime;
        
        if (KeyboardHandler.IsPressed(Keys.R))
            SceneManager.RefreshCurrentScene();
        
        if (KeyboardHandler.IsPressed(Keys.Tab))
            Config.DebugMode = !Config.DebugMode;
        
        SceneManager.UpdateCurrentScene();
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        
        Config.Batch.Begin(sortMode: SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp, transformMatrix: Config.Camera.GetTransformation());
        SceneManager.RenderCurrentScene();
        Config.Batch.End();
        
        base.Draw(gameTime);
    }
}