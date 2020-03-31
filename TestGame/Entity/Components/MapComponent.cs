using MonoGame.Extended.Tiled;

namespace TestGame.Entity.Components
{
    class MapComponent
    {
        private bool _mapChanged = false;
        private TiledMap _tiledMap;

        public MapComponent(TiledMap tiledMap)
        {
            Map = tiledMap;
        }

        public bool MapChanged { get; }

        public TiledMap Map
        {
            get
            {
                _mapChanged = false;
                return _tiledMap;
            }
            set
            {
                _mapChanged = true;
                _tiledMap = value;
            }
        }
    }
}
