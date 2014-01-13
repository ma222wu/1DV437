using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LevelEditor
{
    /// <summary>
    /// Class used to ferry data between game and .xml-files
    /// </summary>
    public class LevelData
    {
            public List<Rectangle> m_clips;
            public List<TileData> m_tiles;
            public List<TriggerData> m_triggers;
            public List<EnemyData> m_enemies;


            public LevelData()
            {
                //m_enemySpawnPoints = new List<Vector2>();
            }
    }

    public struct TileData
    {
        public string textureName;
        public float depth;
        public Vector2 position;
        public Vector2 dimensions;
    }
    public struct TriggerData
    {
        public List<string> lines;
        public Rectangle rectangle;
    }

    public struct EnemyData
    {
        public string textureName;
        public Vector2 position;
    }
}
