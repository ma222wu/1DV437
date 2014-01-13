using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Potholy
{
    /// <summary>
    /// Serializable class that tracks player items, money and completed levels
    /// </summary>
    public class PlayerStats
    {
        public int m_money;
        public List<Item> m_items = new List<Item>();
        public int m_levelsCleared;

        public PlayerStats()
        {

        }
    }
}
