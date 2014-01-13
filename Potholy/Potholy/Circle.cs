using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Potholy
{
    public class Circle
    {
        public Vector2 m_center;
        public float m_radius;

        public Circle()
        {

        }

        public Circle(Vector2 center, float radius)
        {
            m_center = center;
            m_radius = radius;

        }

        public bool Intersects(Rectangle r)
        {
            Vector2 circleDistance;

            Vector2 woolength = new Vector2(r.Width * 0.5f, r.Height * 0.5f + m_radius);
            Vector2 realDist = new Vector2(m_center.X - r.Center.X, m_center.Y - r.Center.Y);

            circleDistance.X = Math.Abs(m_center.X - r.X - r.Width / 2);
            circleDistance.Y = Math.Abs(m_center.Y - r.Y - r.Height / 2);

            if (circleDistance.X > (r.Width / 2 + m_radius)) { return false; }
            if (circleDistance.Y > (r.Height / 2 + m_radius)) { return false; }

                if (circleDistance.X <= (r.Width / 2)) { return true; }
                if (circleDistance.Y <= (r.Height / 2)) { return true; }

                double cornerDistance_sq = Math.Pow((circleDistance.X - r.Width / 2), 2) +
                                     Math.Pow((circleDistance.Y - r.Height / 2),2);

                double powX = Math.Pow((circleDistance.X - r.Width / 2), 2);
                double powY = Math.Pow((circleDistance.Y - r.Height / 2), 2);

                double sqrX = Math.Sqrt((circleDistance.X - r.Width / 2));
                double sqrY = Math.Sqrt((circleDistance.Y - r.Height / 2));
                
                double woo = (Math.Pow(m_radius, 2));
               
                return (cornerDistance_sq <= (Math.Pow(m_radius,2)));
        }

        public bool Intersects(Rectangle r, out bool corner)
        {
            corner = false;

            Vector2 circleDistance;


            Vector2 woolength = new Vector2(r.Width * 0.5f, r.Height * 0.5f + m_radius);
            Vector2 realDist = new Vector2(m_center.X - r.Center.X, m_center.Y - r.Center.Y);

            circleDistance.X = Math.Abs(m_center.X - r.X - r.Width / 2);
            circleDistance.Y = Math.Abs(m_center.Y - r.Y - r.Height / 2);

            if (circleDistance.X > (r.Width / 2 + m_radius)) { return false; }
            if (circleDistance.Y > (r.Height / 2 + m_radius)) { return false; }

            if (circleDistance.X <= (r.Width / 2)) { return true; }
            if (circleDistance.Y <= (r.Height / 2)) { return true; }

            double cornerDistance_sq = Math.Pow((circleDistance.X - r.Width / 2), 2) +
                                 Math.Pow((circleDistance.Y - r.Height / 2), 2);

            double powX = Math.Pow((circleDistance.X - r.Width / 2), 2);
            double powY = Math.Pow((circleDistance.Y - r.Height / 2), 2);

            double sqrX = Math.Sqrt((circleDistance.X - r.Width / 2));
            double sqrY = Math.Sqrt((circleDistance.Y - r.Height / 2));

            double woo = (Math.Pow(m_radius, 2));

            corner = true;
            return (cornerDistance_sq <= (Math.Pow(m_radius, 2)));
        }

        public bool Intersects(Circle c)
        {
            if (Vector2.Distance(m_center, c.m_center) < m_radius + c.m_radius)
                return true;

            return false;
        }

    }

    
}
