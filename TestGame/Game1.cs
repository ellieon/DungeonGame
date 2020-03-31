using TestGame.Entity.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Tiled;
using System.Linq;
using TestGame.Entity.Systems;

namespace TestGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private World _world;
        private BitmapFont _font;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _font = Content.Load<BitmapFont>("fonts/Sensation");
            _world = new WorldBuilder()
                .AddSystem(new MapSystem(GraphicsDevice))
                .AddSystem(new PlayerSystem())
                .AddSystem(new HudSystem(GraphicsDevice, _font))
                .Build();

            var map = _world.CreateEntity();

            map.Attach(new MapComponent(Content.Load<TiledMap>("maps/TestMap")));
           
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _world.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _world.Draw(gameTime);

            base.Draw(gameTime);

        }
    }
}
