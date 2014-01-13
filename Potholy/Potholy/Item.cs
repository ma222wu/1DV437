using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Potholy
{
    /// <summary>
    /// Item applied to player's stats
    /// </summary>
    public class Item
    {
        public int m_cost;
        /// <summary>
        /// Player default is 15
        /// </summary>
        public float m_jumpPower;
        /// <summary>
        /// Player default is 10
        /// </summary>
        public float m_runSpeed;
        /// <summary>
        /// Player default is 10
        /// </summary>
        public float m_bulletDamage;
        /// <summary>
        /// Player default is 10
        /// </summary>
        public float m_dashDamage;
        /// <summary>
        /// Player default is 10
        /// </summary>
        public float m_dashLength;
        /// <summary>
        /// Player default is 100
        /// </summary>
        public float m_gunCooldown;
        public string m_name;

        public Item()
        {

        }

        public Item(int cost, float jumppower, float runspeed, float bulletdamage, float dashdamage, float dashlength, float guncooldown, string name)
        {
            m_cost = cost;
            m_jumpPower = jumppower;
            m_runSpeed = runspeed;
            m_bulletDamage = bulletdamage;
            m_dashDamage = dashdamage;
            m_dashLength = dashlength;
            m_gunCooldown = guncooldown;
            m_name = name;
        }
    }
}
