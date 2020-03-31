using System.Collections;
using TestGame.Entity.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace TestGame.Entity.Systems
{
    class MapSystem : EntityDrawSystem
    {
        private ComponentMapper<MapComponent> _mapMapper;
        private Hashtable _renderers;
        private GraphicsDevice _graphics;

        public MapSystem(GraphicsDevice graphics) : base(Aspect.All(typeof(MapComponent)))
        {
            _graphics = graphics;
            _renderers = new Hashtable();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (int entity in ActiveEntities)
            {
                MapComponent mapComponent = _mapMapper.Get(entity);
                TiledMapRenderer renderer = _renderers.ContainsKey(entity) ? (TiledMapRenderer)_renderers[entity] : createRenderer(entity, mapComponent);
                if (mapComponent.MapChanged)
                {
                    renderer.LoadMap(mapComponent.Map);
                }

                renderer.Draw();
            }
        }

        private TiledMapRenderer createRenderer(int entity, MapComponent mapComponent)
        {
            TiledMap map = mapComponent.Map;
            TiledMapRenderer renderer = new TiledMapRenderer(_graphics, map);
            _renderers.Add(entity, renderer);
            return renderer;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _mapMapper = mapperService.GetMapper<MapComponent>();
        }
    }
}
