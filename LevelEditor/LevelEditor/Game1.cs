using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Xml.Serialization;
using System.IO;

namespace LevelEditor
{
    /// <summary>
    /// Currently selected tool
    /// </summary>
    public enum Tool
    {
        Select,
        Clip,
        Enemy,
        Trigger,
        Tile,
    }

    /// <summary>
    /// To be used when copying and selecting stuff
    /// </summary>
    public struct SelectedObject
    {
        object theList;
        object theObject;
    }

    /// <summary>
    /// XNA game of the level editor
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MainForm m_mainForm;
        public Camera m_camera;
        public List<Block> m_clips;//blocks
        public List<Trigger> m_triggers;//triggers with script code
        public List<Enemy> m_enemies;//enemies
        public List<Sprite> m_tiles;//visual tiles


        GamePadState gpsc; KeyboardState kbsc; MouseState msc; //current input states
        GamePadState gpsp; KeyboardState kbsp; MouseState msp;//previous input states

        Vector2 actualCurrentMouseCoordinates; //current coordinates of mouse cursor in world
        Vector2 actualPreviousMouseCoordinates; //previous coordinates of mouse cursor in world

        Vector2 m_mouseHoldStartPoint;//when dragging, this is the point the drag started

        public object m_selectedObject; //currently selected object
        public object m_selectedObjectsList; //list the selected object is in, so that it can be manipulated there

        Sprite m_dragArea; //visual dragging area

        Color m_gridColor; //color of grid
        bool colorPulsatingUp; //if grid opacity is rising or shrinking

        Random m_random;

        public Tool m_currentTool = Tool.Select;

        Texture2D m_debugBlock;//texture of a grid with black and purple squares

        public Game1(MainForm form)
        {
            m_mainForm = form;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(Graphics_PreparingDeviceSettings);

            System.Windows.Forms.Form xnaWindow = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(this.Window.Handle);
            xnaWindow.GotFocus += new EventHandler(XnaWindow_GotFocus);
            m_mainForm.Resize += new EventHandler(XnaWindow_Resize);

            m_random = new Random(DateTime.Now.Ticks.GetHashCode());

            m_clips = new List<Block>();
            m_enemies = new List<Enemy>();
            m_triggers = new List<Trigger>();
            m_tiles = new List<Sprite>();

        }

        #region methods relating to the manipulation of the form itself

        /// <summary>
        /// Called when the XnaWindow gets focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void XnaWindow_GotFocus(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Form)sender).Visible = false;
            m_mainForm.TopMost = false;
        }

        /// <summary>
        /// Resizes the XnaWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void XnaWindow_Resize(object sender, EventArgs e)
        {
            graphics.PreferredBackBufferWidth = m_mainForm.xnaRenderWindow.Width;
            graphics.PreferredBackBufferHeight = m_mainForm.xnaRenderWindow.Height;

            //TODO: calculate new aspect ratio?

            graphics.ApplyChanges();
        }

        /// <summary>
        /// ?
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = m_mainForm.xnaRenderWindow.Handle;
        }
        #endregion

        protected override void Initialize()
        {
            #region sets correct window size
            graphics.PreferredBackBufferWidth = m_mainForm.xnaRenderWindow.Width;
            graphics.PreferredBackBufferHeight = m_mainForm.xnaRenderWindow.Height;
            graphics.ApplyChanges();
            #endregion

            m_camera = new Camera(this);
            m_camera.Initialize(Vector2.Zero);

            m_debugBlock = Content.Load<Texture2D>("debugBlock");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            gpsc = GamePad.GetState(PlayerIndex.One);
            kbsc = Keyboard.GetState();
            msc = Mouse.GetState();

            Rectangle mouseIntersect;//1x1 rectangle of where the mouse is

            Mouse.WindowHandle = this.Window.Handle;
            Mouse.WindowHandle = this.m_mainForm.xnaRenderWindow.Handle;

            actualCurrentMouseCoordinates = new Vector2(msc.X + GraphicsDevice.Viewport.Bounds.Width * -0.5f,
                                                        msc.Y + GraphicsDevice.Viewport.Bounds.Height * -0.5f) / m_camera.m_transformation.m_scale + new Vector2(+m_camera.m_transformation.m_position.X, m_camera.m_transformation.m_position.Y) / m_camera.m_transformation.m_scale;
            mouseIntersect = new Rectangle((int)actualCurrentMouseCoordinates.X, (int)actualCurrentMouseCoordinates.Y, 1, 1); 

            m_mainForm.label2.Text = actualCurrentMouseCoordinates.ToString();

            if (msc.LeftButton == ButtonState.Pressed && msp.LeftButton == ButtonState.Released && MouseIsInsideWindow() /*&& !m_mainForm.subFormOpen*/)
            {
                m_mainForm.xnaRenderWindow.Focus();

                switch (m_currentTool)
                {
                    case Tool.Clip:
                        {
                            #region new region or block/starting to drag

                m_mouseHoldStartPoint = new Vector2(actualCurrentMouseCoordinates.X, actualCurrentMouseCoordinates.Y) 
                                      - new Vector2(actualCurrentMouseCoordinates.X % (float)m_mainForm.nudSnapGridX.Value
                                                  , actualCurrentMouseCoordinates.Y % (float)m_mainForm.nudSnapGridY.Value);
                        
                
                            #endregion

                            break;
                        }
                    case Tool.Trigger:
                        {
                            #region new region or block/starting to drag

                            m_mouseHoldStartPoint = new Vector2(actualCurrentMouseCoordinates.X, actualCurrentMouseCoordinates.Y)
                                                  - new Vector2(actualCurrentMouseCoordinates.X % (float)m_mainForm.nudSnapGridX.Value
                                                              , actualCurrentMouseCoordinates.Y % (float)m_mainForm.nudSnapGridY.Value);


                            #endregion

                            break;
                        }
                    case Tool.Select:
                        {
                            #region select object
                            Rectangle r = new Rectangle((int)actualCurrentMouseCoordinates.X, (int)actualCurrentMouseCoordinates.Y, 1, 1);

                            ResetAlphaOnEverything();
                            if(m_mainForm.chbClipsVisible.Checked)
                            for (int i = 0; i < m_clips.Count; i++)
                            {
                                if (m_clips[i].m_sourceRectangle.Intersects(r))
                                {
                                    m_selectedObject = m_clips[i];
                                    m_selectedObjectsList = m_clips;
                                    m_clips[i].Alpha = 0.4f;
                                }
                            }

                            if (m_mainForm.chbTriggersVisible.Checked)
                            for (int i = 0; i < m_triggers.Count; i++)
                            {
                                if (m_triggers[i].m_sourceRectangle.Intersects(r))
                                {
                                    m_selectedObject = m_triggers[i];
                                    m_selectedObjectsList = m_triggers;
                                    m_triggers[i].Alpha = 0.4f;
                                    m_mainForm.tbTriggerData.Text = m_triggers[i].m_scriptLines[0];
                                }
                            }

                            if (m_mainForm.chbTilesVisible.Checked)
                            for (int i = 0; i < m_tiles.Count; i++)
                            {
                                if (new Rectangle((int)m_tiles[i].m_transform.m_position.X, (int)m_tiles[i].m_transform.m_position.Y, m_tiles[i].m_sourceRectangle.Width, m_tiles[i].m_sourceRectangle.Height)
                                    .Intersects(r))
                                {
                                    m_selectedObject = m_tiles[i];
                                    m_selectedObjectsList = m_tiles;
                                    m_tiles[i].Alpha = 0.4f;
                                }
                            }
                            #endregion
                            break;
                            
                        }
                    case Tool.Enemy:
                        {
                            m_enemies.Add(new Enemy(this));
                            m_enemies[m_enemies.Count - 1].Initialize("enemy", ref spriteBatch);
                            m_enemies[m_enemies.Count - 1].m_transform.m_position = actualCurrentMouseCoordinates;

                            break;
                        }
                    case Tool.Tile:
                        {
                            if (m_mainForm.lbCurrentTexture.SelectedIndex == -1)
                                m_mainForm.lbCurrentTexture.SelectedIndex = 0;

                            m_tiles.Add(new Sprite(this));
                            m_tiles[m_tiles.Count - 1].Initialize((string)m_mainForm.lbCurrentTexture.SelectedItem, ref spriteBatch);
                            m_tiles[m_tiles.Count - 1].m_transform.m_position = actualCurrentMouseCoordinates - new Vector2(actualCurrentMouseCoordinates.X % (float)m_mainForm.nudSnapGridX.Value
                                                                            , actualCurrentMouseCoordinates.Y % (float)m_mainForm.nudSnapGridY.Value);
                            m_tiles[m_tiles.Count - 1].m_texture.Name = (string)m_mainForm.lbCurrentTexture.SelectedItem;
                            break;
                        }
                }
            }

            if (kbsc.IsKeyDown(Keys.Delete) && kbsp.IsKeyUp(Keys.Delete))
            {
                if (m_selectedObjectsList == m_clips)
                {
                    m_clips.Remove(m_selectedObject as Block);
                }
                if (m_selectedObjectsList == m_triggers)
                {
                    m_triggers.Remove(m_selectedObject as Trigger);
                }
                if (m_selectedObjectsList == m_tiles)
                {
                    m_tiles.Remove(m_selectedObject as Sprite);
                }
            }

            //mouse was released
            if (msc.LeftButton == ButtonState.Released && msp.LeftButton == ButtonState.Pressed && MouseIsInsideWindow())
            {
                switch (m_currentTool)
                {
                        //the following two cases deal with blocks and triggers being dragged out, with four different if cases depending on what direction the user dragged in
                    case Tool.Clip:
                        #region MouseBlock
                        if (actualCurrentMouseCoordinates.X > m_mouseHoldStartPoint.X && actualCurrentMouseCoordinates.Y > m_mouseHoldStartPoint.Y)
                        {//DOWN RIGHT
                            m_clips.Add(new Block(this));
                            m_clips[m_clips.Count - 1].Initialize(ref m_debugBlock, ref spriteBatch);
                            m_clips[m_clips.Count - 1].m_transform.m_position = m_mouseHoldStartPoint;
                            m_clips[m_clips.Count - 1].m_sourceRectangle = new Rectangle((int)m_mouseHoldStartPoint.X, (int)m_mouseHoldStartPoint.Y,
                                (int)(actualCurrentMouseCoordinates.X - m_mouseHoldStartPoint.X) - (int)((actualCurrentMouseCoordinates.X - m_mouseHoldStartPoint.X) % (float)m_mainForm.nudSnapGridX.Value)
                                , (int)((actualCurrentMouseCoordinates.Y - m_mouseHoldStartPoint.Y) - (actualCurrentMouseCoordinates.Y - m_mouseHoldStartPoint.Y) % (float)m_mainForm.nudSnapGridY.Value));
                            m_clips[m_clips.Count - 1].m_sourceRectangle.Height += (int)m_mainForm.nudSnapGridY.Value;
                            m_clips[m_clips.Count - 1].m_sourceRectangle.Width += (int)m_mainForm.nudSnapGridX.Value;

                            m_clips[m_clips.Count - 1].m_transform.m_depth = 0;
                            m_clips[m_clips.Count - 1].m_alpha = 100;

                            if (actualCurrentMouseCoordinates.X < 0)
                            {
                                m_clips[m_clips.Count - 1].m_sourceRectangle.X -= (int)m_mainForm.nudSnapGridX.Value;
                                m_clips[m_clips.Count - 1].m_transform.m_position.X -= (int)m_mainForm.nudSnapGridX.Value;
                                m_clips[m_clips.Count - 1].m_sourceRectangle.Width += (int)m_mainForm.nudSnapGridX.Value;
                            }
                            else
                            {
                                //m_clips[m_clips.Count - 1].m_sourceRectangle.Width += (int)m_mainForm.nudSnapGridX.Value;
                            }
                            if (actualCurrentMouseCoordinates.Y < 0)
                            {
                                m_clips[m_clips.Count - 1].m_sourceRectangle.Y -= (int)m_mainForm.nudSnapGridY.Value;
                                m_clips[m_clips.Count - 1].m_transform.m_position.Y -= (int)m_mainForm.nudSnapGridY.Value;
                                m_clips[m_clips.Count - 1].m_sourceRectangle.Height += (int)m_mainForm.nudSnapGridY.Value;
                            }
                            else
                            {
                                //m_clips[m_clips.Count - 1].m_sourceRectangle.Height += (int)m_mainForm.nudSnapGridY.Value;
                            }
                            break;
                        }
                        if (actualCurrentMouseCoordinates.X < m_mouseHoldStartPoint.X && actualCurrentMouseCoordinates.Y > m_mouseHoldStartPoint.Y)
                        { //DOWN LEFT
                            m_clips.Add(new Block(this));
                            m_clips[m_clips.Count - 1].Initialize(ref m_debugBlock, ref spriteBatch);
                            m_clips[m_clips.Count - 1].m_transform.m_position = new Vector2(actualCurrentMouseCoordinates.X - actualCurrentMouseCoordinates.X % (float)m_mainForm.nudSnapGridX.Value, m_mouseHoldStartPoint.Y);


                            m_clips[m_clips.Count - 1].m_sourceRectangle = new Rectangle((int)actualCurrentMouseCoordinates.X, (int)m_mouseHoldStartPoint.Y,
                                (int)(m_mouseHoldStartPoint.X - actualCurrentMouseCoordinates.X) + (int)((actualCurrentMouseCoordinates.X - m_mouseHoldStartPoint.X) % (float)m_mainForm.nudSnapGridX.Value)
                                , (int)((actualCurrentMouseCoordinates.Y - m_mouseHoldStartPoint.Y) - (actualCurrentMouseCoordinates.Y - m_mouseHoldStartPoint.Y) % (float)m_mainForm.nudSnapGridY.Value));
                            //m_clips[m_clips.Count - 1].m_sourceRectangle.X += (int)m_mainForm.nudSnapGrid.Value;
                            m_clips[m_clips.Count - 1].m_sourceRectangle.Height += (int)m_mainForm.nudSnapGridY.Value;
                            m_clips[m_clips.Count - 1].m_sourceRectangle.Width += (int)m_mainForm.nudSnapGridX.Value * 2;

                            m_clips[m_clips.Count - 1].m_transform.m_depth = 0;
                            m_clips[m_clips.Count - 1].m_alpha = 100;

                            if (actualCurrentMouseCoordinates.X < 0)
                            {
                                m_clips[m_clips.Count - 1].m_sourceRectangle.X -= (int)m_mainForm.nudSnapGridX.Value;
                                m_clips[m_clips.Count - 1].m_transform.m_position.X -= (int)m_mainForm.nudSnapGridX.Value;
                                m_clips[m_clips.Count - 1].m_sourceRectangle.Width -= (int)m_mainForm.nudSnapGridX.Value;
                            }
                            else
                            {
                                //m_clips[m_clips.Count - 1].m_sourceRectangle.Width += (int)m_mainForm.nudSnapGridX.Value;
                            }
                            if (actualCurrentMouseCoordinates.Y < 0)
                            {
                                m_clips[m_clips.Count - 1].m_sourceRectangle.Y -= (int)m_mainForm.nudSnapGridY.Value;
                                m_clips[m_clips.Count - 1].m_transform.m_position.Y -= (int)m_mainForm.nudSnapGridY.Value;
                                m_clips[m_clips.Count - 1].m_sourceRectangle.Height += (int)m_mainForm.nudSnapGridY.Value;
                            }
                            else
                            {
                                //m_clips[m_clips.Count - 1].m_sourceRectangle.Height += (int)m_mainForm.nudSnapGridY.Value;
                            }
                            break;
                        }
                        if (actualCurrentMouseCoordinates.X > m_mouseHoldStartPoint.X && actualCurrentMouseCoordinates.Y < m_mouseHoldStartPoint.Y)
                        {//UP RIGHT
                            m_clips.Add(new Block(this));

                            m_clips[m_clips.Count - 1].Initialize(ref m_debugBlock, ref spriteBatch);
                            m_clips[m_clips.Count - 1].m_transform.m_position = new Vector2(m_mouseHoldStartPoint.X, actualCurrentMouseCoordinates.Y)
                                      - new Vector2(m_mouseHoldStartPoint.X % (float)m_mainForm.nudSnapGridX.Value
                                                  , actualCurrentMouseCoordinates.Y % (float)m_mainForm.nudSnapGridY.Value);

                            m_clips[m_clips.Count - 1].m_sourceRectangle = new Rectangle((int)m_mouseHoldStartPoint.X, (int)actualCurrentMouseCoordinates.Y,
                                (int)(actualCurrentMouseCoordinates.X - m_mouseHoldStartPoint.X - (int)((actualCurrentMouseCoordinates.X - m_mouseHoldStartPoint.X) % (float)m_mainForm.nudSnapGridX.Value))
                                , (int)((m_mouseHoldStartPoint.Y - actualCurrentMouseCoordinates.Y) + (actualCurrentMouseCoordinates.Y - m_mouseHoldStartPoint.Y) % (float)m_mainForm.nudSnapGridX.Value));
                            m_clips[m_clips.Count - 1].m_sourceRectangle.Height = (int)((float)m_mainForm.nudSnapGridY.Value * ((int)((m_mouseHoldStartPoint.Y - actualCurrentMouseCoordinates.Y) / (float)m_mainForm.nudSnapGridY.Value) + 1));
                            m_clips[m_clips.Count - 1].m_sourceRectangle.Width = (int)((float)m_mainForm.nudSnapGridX.Value * ((int)((actualCurrentMouseCoordinates.X - m_mouseHoldStartPoint.X) / (float)m_mainForm.nudSnapGridX.Value) + 1));

                            m_clips[m_clips.Count - 1].m_transform.m_depth = 0;
                            m_clips[m_clips.Count - 1].m_alpha = 100;

                            if (actualCurrentMouseCoordinates.X < 0)
                            {
                                m_clips[m_clips.Count - 1].m_sourceRectangle.X -= (int)m_mainForm.nudSnapGridX.Value;
                                m_clips[m_clips.Count - 1].m_transform.m_position.X -= (int)m_mainForm.nudSnapGridX.Value;
                                m_clips[m_clips.Count - 1].m_sourceRectangle.Width += (int)m_mainForm.nudSnapGridX.Value;
                            }
                            else
                            {
                                //m_clips[m_clips.Count - 1].m_sourceRectangle.Width += (int)m_mainForm.nudSnapGridX.Value;
                            }
                            if (actualCurrentMouseCoordinates.Y < 0)
                            {
                                m_clips[m_clips.Count - 1].m_sourceRectangle.Y -= (int)m_mainForm.nudSnapGridY.Value;
                                m_clips[m_clips.Count - 1].m_transform.m_position.Y -= (int)m_mainForm.nudSnapGridY.Value;

                            }
                            else
                            {
                                m_clips[m_clips.Count - 1].m_sourceRectangle.Height += (int)m_mainForm.nudSnapGridY.Value;
                            }
                            break;
                        }
                        if (actualCurrentMouseCoordinates.X < m_mouseHoldStartPoint.X && actualCurrentMouseCoordinates.Y < m_mouseHoldStartPoint.Y)
                        {//UP LEFT
                            m_clips.Add(new Block(this));

                            m_clips[m_clips.Count - 1].Initialize(ref m_debugBlock, ref spriteBatch);
                            m_clips[m_clips.Count - 1].m_transform.m_position = new Vector2(actualCurrentMouseCoordinates.X, actualCurrentMouseCoordinates.Y)
                                      - new Vector2(actualCurrentMouseCoordinates.X % (float)m_mainForm.nudSnapGridX.Value
                                                  , actualCurrentMouseCoordinates.Y % (float)m_mainForm.nudSnapGridY.Value);

                            m_clips[m_clips.Count - 1].m_sourceRectangle = new Rectangle((int)actualCurrentMouseCoordinates.X, (int)actualCurrentMouseCoordinates.Y,
                                (int)(m_mouseHoldStartPoint.X - actualCurrentMouseCoordinates.X + (int)((actualCurrentMouseCoordinates.X - m_mouseHoldStartPoint.X) % (float)m_mainForm.nudSnapGridX.Value))
                                , (int)(m_mouseHoldStartPoint.Y - actualCurrentMouseCoordinates.Y + (actualCurrentMouseCoordinates.Y - m_mouseHoldStartPoint.Y) % (float)m_mainForm.nudSnapGridY.Value));
                            m_clips[m_clips.Count - 1].m_sourceRectangle.Height = (int)((float)m_mainForm.nudSnapGridY.Value * ((int)((m_mouseHoldStartPoint.Y - actualCurrentMouseCoordinates.Y) / (float)m_mainForm.nudSnapGridY.Value) + 1));
                            m_clips[m_clips.Count - 1].m_sourceRectangle.Width = (int)((float)m_mainForm.nudSnapGridX.Value * ((int)((m_mouseHoldStartPoint.X - actualCurrentMouseCoordinates.X) / (float)m_mainForm.nudSnapGridX.Value) + 1));
                            m_clips[m_clips.Count - 1].m_transform.m_depth = 0;
                            m_clips[m_clips.Count - 1].m_alpha = 100;

                            if (actualCurrentMouseCoordinates.X < 0)
                            {
                                m_clips[m_clips.Count - 1].m_sourceRectangle.X -= (int)m_mainForm.nudSnapGridX.Value;
                                m_clips[m_clips.Count - 1].m_transform.m_position.X -= (int)m_mainForm.nudSnapGridX.Value;
                            }
                            else
                            {
                                m_clips[m_clips.Count - 1].m_sourceRectangle.Width += (int)m_mainForm.nudSnapGridX.Value;
                            }
                            if (actualCurrentMouseCoordinates.Y < 0)
                            {
                                m_clips[m_clips.Count - 1].m_sourceRectangle.Y -= (int)m_mainForm.nudSnapGridY.Value;
                                m_clips[m_clips.Count - 1].m_transform.m_position.Y -= (int)m_mainForm.nudSnapGridY.Value;

                            }
                            else
                            {
                                m_clips[m_clips.Count - 1].m_sourceRectangle.Height += (int)m_mainForm.nudSnapGridY.Value;
                            }
                        }
                        break;
                        #endregion

                    case Tool.Trigger:
                        #region trigger
                        Texture2D tempTex = Content.Load<Texture2D>("trigger");
                        m_triggers.Add(new Trigger(this));
                        if (actualCurrentMouseCoordinates.X > m_mouseHoldStartPoint.X && actualCurrentMouseCoordinates.Y > m_mouseHoldStartPoint.Y)
                        {//DOWN RIGHT
                            m_triggers[m_triggers.Count - 1].Initialize(ref tempTex, ref spriteBatch , m_mouseHoldStartPoint);
                            m_triggers[m_triggers.Count - 1].m_sourceRectangle = new Rectangle((int)m_mouseHoldStartPoint.X, (int)m_mouseHoldStartPoint.Y,
                                (int)(actualCurrentMouseCoordinates.X - m_mouseHoldStartPoint.X) - (int)((actualCurrentMouseCoordinates.X - m_mouseHoldStartPoint.X) % (float)m_mainForm.nudSnapGridX.Value)
                                , (int)((actualCurrentMouseCoordinates.Y - m_mouseHoldStartPoint.Y) - (actualCurrentMouseCoordinates.Y - m_mouseHoldStartPoint.Y) % (float)m_mainForm.nudSnapGridY.Value));
                            m_triggers[m_triggers.Count - 1].m_sourceRectangle.Height += (int)m_mainForm.nudSnapGridY.Value;
                            m_triggers[m_triggers.Count - 1].m_sourceRectangle.Width += (int)m_mainForm.nudSnapGridX.Value;

                            if (actualCurrentMouseCoordinates.X < 0)
                            {
                                m_triggers[m_triggers.Count - 1].m_sourceRectangle.X -= (int)m_mainForm.nudSnapGridX.Value;
                                m_triggers[m_triggers.Count - 1].m_transform.m_position.X -= (int)m_mainForm.nudSnapGridX.Value;
                                m_triggers[m_triggers.Count - 1].m_sourceRectangle.Width += (int)m_mainForm.nudSnapGridX.Value;
                            }
                            if (actualCurrentMouseCoordinates.Y < 0)
                            {
                                m_triggers[m_triggers.Count - 1].m_sourceRectangle.Y -= (int)m_mainForm.nudSnapGridY.Value;
                                m_triggers[m_triggers.Count - 1].m_transform.m_position.Y -= (int)m_mainForm.nudSnapGridY.Value;
                                m_triggers[m_triggers.Count - 1].m_sourceRectangle.Height += (int)m_mainForm.nudSnapGridY.Value;
                            }
                        }
                        else if (actualCurrentMouseCoordinates.X < m_mouseHoldStartPoint.X && actualCurrentMouseCoordinates.Y > m_mouseHoldStartPoint.Y)
                        { //DOWN LEFT
                            m_triggers[m_triggers.Count - 1].Initialize(ref tempTex, ref spriteBatch ,new Vector2(actualCurrentMouseCoordinates.X - actualCurrentMouseCoordinates.X % (float)m_mainForm.nudSnapGridX.Value, m_mouseHoldStartPoint.Y));
                            m_triggers[m_triggers.Count - 1].m_sourceRectangle = new Rectangle((int)actualCurrentMouseCoordinates.X, (int)m_mouseHoldStartPoint.Y,
                                (int)(m_mouseHoldStartPoint.X - actualCurrentMouseCoordinates.X) + (int)((actualCurrentMouseCoordinates.X - m_mouseHoldStartPoint.X) % (float)m_mainForm.nudSnapGridX.Value)
                                , (int)((actualCurrentMouseCoordinates.Y - m_mouseHoldStartPoint.Y) - (actualCurrentMouseCoordinates.Y - m_mouseHoldStartPoint.Y) % (float)m_mainForm.nudSnapGridY.Value));
                            //m_triggers[m_triggers.Count - 1].m_sourceRectangle.X += (int)m_mainForm.nudSnapGrid.Value;
                            m_triggers[m_triggers.Count - 1].m_sourceRectangle.Height += (int)m_mainForm.nudSnapGridY.Value;
                            m_triggers[m_triggers.Count - 1].m_sourceRectangle.Width += (int)m_mainForm.nudSnapGridX.Value * 2;

                            if (actualCurrentMouseCoordinates.X < 0)
                            {
                                m_triggers[m_triggers.Count - 1].m_sourceRectangle.X -= (int)m_mainForm.nudSnapGridX.Value;
                                m_triggers[m_triggers.Count - 1].m_transform.m_position.X -= (int)m_mainForm.nudSnapGridX.Value;
                                m_triggers[m_triggers.Count - 1].m_sourceRectangle.Width -= (int)m_mainForm.nudSnapGridX.Value;
                            }
                            if (actualCurrentMouseCoordinates.Y < 0)
                            {
                                m_triggers[m_triggers.Count - 1].m_sourceRectangle.Y -= (int)m_mainForm.nudSnapGridY.Value;
                                m_triggers[m_triggers.Count - 1].m_transform.m_position.Y -= (int)m_mainForm.nudSnapGridY.Value;
                                m_triggers[m_triggers.Count - 1].m_sourceRectangle.Height += (int)m_mainForm.nudSnapGridY.Value;
                            }
                        }
                        else if (actualCurrentMouseCoordinates.X > m_mouseHoldStartPoint.X && actualCurrentMouseCoordinates.Y < m_mouseHoldStartPoint.Y)
                        {//UP RIGHT
                            m_triggers[m_triggers.Count - 1].Initialize(ref tempTex, ref spriteBatch, new Vector2(m_mouseHoldStartPoint.X, actualCurrentMouseCoordinates.Y)
                                      - new Vector2(m_mouseHoldStartPoint.X % (float)m_mainForm.nudSnapGridX.Value
                                                  , actualCurrentMouseCoordinates.Y % (float)m_mainForm.nudSnapGridY.Value));
                            m_triggers[m_triggers.Count - 1].m_sourceRectangle = new Rectangle((int)m_mouseHoldStartPoint.X, (int)actualCurrentMouseCoordinates.Y,
                                (int)(actualCurrentMouseCoordinates.X - m_mouseHoldStartPoint.X - (int)((actualCurrentMouseCoordinates.X - m_mouseHoldStartPoint.X) % (float)m_mainForm.nudSnapGridX.Value))
                                , (int)((m_mouseHoldStartPoint.Y - actualCurrentMouseCoordinates.Y) + (actualCurrentMouseCoordinates.Y - m_mouseHoldStartPoint.Y) % (float)m_mainForm.nudSnapGridX.Value));
                            m_triggers[m_triggers.Count - 1].m_sourceRectangle.Height = (int)((float)m_mainForm.nudSnapGridY.Value * ((int)((m_mouseHoldStartPoint.Y - actualCurrentMouseCoordinates.Y) / (float)m_mainForm.nudSnapGridY.Value) + 1));
                            m_triggers[m_triggers.Count - 1].m_sourceRectangle.Width = (int)((float)m_mainForm.nudSnapGridX.Value * ((int)((actualCurrentMouseCoordinates.X - m_mouseHoldStartPoint.X) / (float)m_mainForm.nudSnapGridX.Value) + 1));

                            if (actualCurrentMouseCoordinates.X < 0)
                            {
                                m_triggers[m_triggers.Count - 1].m_sourceRectangle.X -= (int)m_mainForm.nudSnapGridX.Value;
                                m_triggers[m_triggers.Count - 1].m_transform.m_position.X -= (int)m_mainForm.nudSnapGridX.Value;
                                m_triggers[m_triggers.Count - 1].m_sourceRectangle.Width += (int)m_mainForm.nudSnapGridX.Value;
                            }
                            if (actualCurrentMouseCoordinates.Y < 0)
                            {
                                m_triggers[m_triggers.Count - 1].m_sourceRectangle.Y -= (int)m_mainForm.nudSnapGridY.Value;
                                m_triggers[m_triggers.Count - 1].m_transform.m_position.Y -= (int)m_mainForm.nudSnapGridY.Value;

                            }
                            else
                            {
                                m_triggers[m_triggers.Count - 1].m_sourceRectangle.Height += (int)m_mainForm.nudSnapGridY.Value;
                            }
                        }
                        else if (actualCurrentMouseCoordinates.X < m_mouseHoldStartPoint.X && actualCurrentMouseCoordinates.Y < m_mouseHoldStartPoint.Y)
                        {//UP LEFT
                            m_triggers[m_triggers.Count - 1].Initialize(ref tempTex, ref spriteBatch, new Vector2(actualCurrentMouseCoordinates.X, actualCurrentMouseCoordinates.Y)
                                      - new Vector2(actualCurrentMouseCoordinates.X % (float)m_mainForm.nudSnapGridX.Value
                                                  , actualCurrentMouseCoordinates.Y % (float)m_mainForm.nudSnapGridY.Value));
                            m_triggers[m_triggers.Count - 1].m_sourceRectangle = new Rectangle((int)actualCurrentMouseCoordinates.X, (int)actualCurrentMouseCoordinates.Y,
                                (int)(m_mouseHoldStartPoint.X - actualCurrentMouseCoordinates.X + (int)((actualCurrentMouseCoordinates.X - m_mouseHoldStartPoint.X) % (float)m_mainForm.nudSnapGridX.Value))
                                , (int)(m_mouseHoldStartPoint.Y - actualCurrentMouseCoordinates.Y + (actualCurrentMouseCoordinates.Y - m_mouseHoldStartPoint.Y) % (float)m_mainForm.nudSnapGridY.Value));
                            m_triggers[m_triggers.Count - 1].m_sourceRectangle.Height = (int)((float)m_mainForm.nudSnapGridY.Value * ((int)((m_mouseHoldStartPoint.Y - actualCurrentMouseCoordinates.Y) / (float)m_mainForm.nudSnapGridY.Value) + 1));
                            m_triggers[m_triggers.Count - 1].m_sourceRectangle.Width = (int)((float)m_mainForm.nudSnapGridX.Value * ((int)((m_mouseHoldStartPoint.X - actualCurrentMouseCoordinates.X) / (float)m_mainForm.nudSnapGridX.Value) + 1));

                            if (actualCurrentMouseCoordinates.X < 0)
                            {
                                m_triggers[m_triggers.Count - 1].m_sourceRectangle.X -= (int)m_mainForm.nudSnapGridX.Value;
                                m_triggers[m_triggers.Count - 1].m_transform.m_position.X -= (int)m_mainForm.nudSnapGridX.Value;
                            }
                            else
                            {
                                m_triggers[m_triggers.Count - 1].m_sourceRectangle.Width += (int)m_mainForm.nudSnapGridX.Value;
                            }
                            if (actualCurrentMouseCoordinates.Y < 0)
                            {
                                m_triggers[m_triggers.Count - 1].m_sourceRectangle.Y -= (int)m_mainForm.nudSnapGridY.Value;
                                m_triggers[m_triggers.Count - 1].m_transform.m_position.Y -= (int)m_mainForm.nudSnapGridY.Value;

                            }
                            else
                            {
                                m_triggers[m_triggers.Count - 1].m_sourceRectangle.Height += (int)m_mainForm.nudSnapGridY.Value;
                            }
                        }
                        else
                        {
                            m_triggers.RemoveAt(m_triggers.Count - 1);
                            break;

                        }
                            m_triggers[m_triggers.Count - 1].m_transform.m_depth = 0;
                            //m_triggers[m_triggers.Count - 1].m_tintColor.A = 100;
                            //m_triggers[m_triggers.Count - 1].m_tintColor.R = 100;
                            //m_triggers[m_triggers.Count - 1].m_tintColor.G = 100;
                            //m_triggers[m_triggers.Count - 1].m_tintColor.B = 255;
                        break;
                    #endregion
                }
            }

            for (int i = 0; i < m_clips.Count; i++)
            {
                m_clips[i].Update(gameTime);
            }
            for (int i = 0; i < m_triggers.Count; i++)
            {
                m_triggers[i].Update(gameTime);
            }
            for (int i = 0; i < m_enemies.Count; i++)
            {
                m_enemies[i].Update(gameTime);
            }
            for (int i = 0; i < m_tiles.Count; i++)
            {
                m_tiles[i].Update(gameTime);
            }

            #region Camera manipulation
            if (msc.ScrollWheelValue > msp.ScrollWheelValue)
            {
                m_camera.m_transformation.m_scale += (Vector2.One * (float)gameTime.ElapsedGameTime.TotalSeconds) * m_camera.m_transformation.m_scale * 10;

            }
            if (msc.ScrollWheelValue < msp.ScrollWheelValue)
            {
                m_camera.m_transformation.m_scale -= (Vector2.One * (float)gameTime.ElapsedGameTime.TotalSeconds) * m_camera.m_transformation.m_scale * 10;
            }

            if (msc.MiddleButton == ButtonState.Pressed)
            {
                m_camera.m_transformation.m_position -= (new Vector2((float)msc.X, (float)msc.Y) - new Vector2((float)msp.X, (float)msp.Y));
            }
            #endregion

            m_camera.Update(gameTime);

            base.Update(gameTime);

            msp = msc;
            gpsp = gpsc;
            kbsp = kbsc;

            actualPreviousMouseCoordinates = actualCurrentMouseCoordinates;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(0, 0, 22));

            SamplerState ss = new SamplerState();//sampler states that mirror tiles horizontally, but not vertically
            ss.AddressU = TextureAddressMode.Wrap;
            ss.AddressV = TextureAddressMode.Wrap;
            ss.Filter = TextureFilter.Anisotropic;

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, ss, null, null, null, m_camera.m_transformationMatrix);

            float oldAlpha = 1;//a quick and very dirty way to lower the alphas of objects that are set to be invisible
            for (int i = 0; i < m_clips.Count; i++)
            {
                if (!m_mainForm.chbClipsVisible.Checked)
                {
                    oldAlpha = m_clips[i].Alpha;
                    m_clips[i].Alpha = 0.1f;
                }

                m_clips[i].Draw(gameTime);

                if (!m_mainForm.chbClipsVisible.Checked)
                    m_clips[i].Alpha = oldAlpha;
            }

            for (int i = 0; i < m_enemies.Count; i++)
            {
                m_enemies[i].Draw(gameTime);
            }

            for (int i = 0; i < m_triggers.Count; i++)
            {
                if (!m_mainForm.chbTriggersVisible.Checked)
                {
                    oldAlpha = m_triggers[i].Alpha;
                    m_triggers[i].Alpha = 0.1f;
                }

                m_triggers[i].Draw(gameTime);

                if (!m_mainForm.chbTriggersVisible.Checked)
                    m_triggers[i].Alpha = oldAlpha;
            }

            for (int i = 0; i < m_tiles.Count; i++)
            {
                if (!m_mainForm.chbTilesVisible.Checked)
                {
                    oldAlpha = m_tiles[i].Alpha;
                    m_tiles[i].Alpha = 0.1f;
                }

                m_tiles[i].Draw(gameTime);

                if (!m_mainForm.chbTilesVisible.Checked)
                    m_tiles[i].Alpha = oldAlpha;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Checks if mouse is inside window
        /// </summary>
        /// <returns></returns>
        bool MouseIsInsideWindow()
        {
            if (msc.X < m_mainForm.xnaRenderWindow.Size.Width //+ m_camera.m_transform.m_position.X*0.5f
                && msc.X >= 0 //+ m_camera.m_transform.m_position.X * 0.5f 
                &&
                msc.Y < m_mainForm.xnaRenderWindow.Size.Height //+ m_camera.m_transform.m_position.Y * 0.5f
                && msc.Y >= 0)//+ m_camera.m_transform.m_position.Y * 0.5f)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Resets alpha on all sprite-based objects
        /// </summary>
        void ResetAlphaOnEverything()
        {
            foreach (Block b in m_clips)
            {
                b.Alpha = 1;
            }
            foreach (Sprite s in m_tiles)
            {
                s.Alpha = 1;
            }
            foreach (Trigger t in m_triggers)
            {
                t.Alpha = 1;
            }
            foreach (Enemy e in m_enemies)
            {
                e.Alpha = 1;
            }
        }

        /// <summary>
        /// Saves level
        /// </summary>
        /// <param name="pathName"></param>
        public void SaveLevel(string pathName)
        {
            LevelData levelData = new LevelData();

                levelData.m_clips = new List<Rectangle>();
                levelData.m_tiles = new List<TileData>();
                levelData.m_triggers = new List<TriggerData>();
                levelData.m_enemies = new List<EnemyData>();
                

                for (int i = 0; i < m_clips.Count; i++)
                {
                    Rectangle r = new Rectangle((int)m_clips[i].m_transform.m_position.X,
                        (int)m_clips[i].m_transform.m_position.Y,
                        m_clips[i].m_sourceRectangle.Width,
                        m_clips[i].m_sourceRectangle.Height);

                    levelData.m_clips.Add(r);
                }

                TriggerData trd;
                trd = new TriggerData();
                for (int i = 0; i < m_triggers.Count; i++)
                {
                    trd.lines = m_triggers[i].m_scriptLines;

                    Rectangle r = new Rectangle((int)m_triggers[i].m_transform.m_position.X,
                        (int)m_triggers[i].m_transform.m_position.Y,
                        m_triggers[i].m_sourceRectangle.Width,
                        m_triggers[i].m_sourceRectangle.Height);

                    trd.rectangle = r;
                    levelData.m_triggers.Add(trd);
                }

                TileData td;
                td = new TileData();

                for (int i = 0; i < m_tiles.Count; i++)
                {
                    td.position = m_tiles[i].m_transform.m_position;
                    td.textureName = m_tiles[i].m_texture.Name;
                    td.depth = m_tiles[i].m_transform.m_depth;
                    td.dimensions = new Vector2(m_tiles[i].m_sourceRectangle.Width, m_tiles[i].m_sourceRectangle.Height);

                    levelData.m_tiles.Add(td);
                }

                EnemyData ed = new EnemyData();

                for (int i = 0; i < m_enemies.Count; i++)
                {
                    ed.position = m_enemies[i].m_transform.m_position;
                    ed.textureName = m_enemies[i].m_texture.Name;

                    levelData.m_enemies.Add(ed);
                }


                XmlSerializer seralizer = new XmlSerializer(levelData.GetType());
                StreamWriter stream = new StreamWriter(pathName);

                try
                {
                    seralizer.Serialize(stream, levelData);
                }

                catch
                {
                    throw new Exception();
                }
                finally
                {
                    stream.Close();
                }
        }

        /// <summary>
        /// Loads level
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadLevel(string fileName)
        {
            Texture2D tempTexture = Content.Load<Texture2D>("debugblock");

            m_tiles.Clear();
            m_clips.Clear();
            m_enemies.Clear();
            m_triggers.Clear();

            LevelData levelData = new LevelData();

            XmlSerializer seralizer = new XmlSerializer(levelData.GetType());
            StreamReader stream;

            stream = new StreamReader(fileName);
            levelData = (LevelData)seralizer.Deserialize(stream);


            stream.Close();

            foreach (Rectangle r in levelData.m_clips)
            {
                m_clips.Add(new Block(this));
                m_clips[m_clips.Count - 1].Initialize(ref tempTexture, ref spriteBatch);
                m_clips[m_clips.Count - 1].m_transform.m_position = new Vector2(r.X, r.Y);
                m_clips[m_clips.Count - 1].m_sourceRectangle = r;
                m_clips[m_clips.Count - 1].m_transform.m_depth = 0;
                m_clips[m_clips.Count - 1].Alpha = 0.3f;
            }
            tempTexture = Content.Load<Texture2D>("trigger");
            foreach (TriggerData trd in levelData.m_triggers)
            {
                m_triggers.Add(new Trigger(this));
                m_triggers[m_triggers.Count - 1].Initialize(ref tempTexture, ref spriteBatch, new Vector2(trd.rectangle.X, trd.rectangle.Y));
                m_triggers[m_triggers.Count - 1].m_sourceRectangle = trd.rectangle;
                m_triggers[m_triggers.Count - 1].m_transform.m_depth = 0;
                m_triggers[m_triggers.Count - 1].Alpha = 0.3f;
                m_triggers[m_triggers.Count - 1].m_scriptLines = trd.lines;
            }

            foreach (TileData td in levelData.m_tiles)
            {

                m_tiles.Add(new Sprite(this));
                m_tiles[m_tiles.Count - 1].Initialize(td.textureName, ref spriteBatch);
                m_tiles[m_tiles.Count - 1].m_transform.m_depth = td.depth;
                m_tiles[m_tiles.Count - 1].m_transform.m_position = td.position;
                m_tiles[m_tiles.Count - 1].m_sourceRectangle.Width = (int)td.dimensions.X;
                m_tiles[m_tiles.Count - 1].m_sourceRectangle.Height = (int)td.dimensions.Y;
                m_tiles[m_tiles.Count - 1].m_texture.Name = td.textureName;

            }

            foreach (EnemyData ee in levelData.m_enemies)
            {
                m_enemies.Add(new Enemy(this));
                if(ee.textureName != "")
                m_enemies[m_enemies.Count - 1].Initialize(ee.textureName, ref spriteBatch);
                else
                    m_enemies[m_enemies.Count - 1].Initialize("enemy", ref spriteBatch);

                m_enemies[m_enemies.Count - 1].m_transform.m_position = ee.position;
                m_enemies[m_enemies.Count - 1].m_transform.m_depth = 0;
            }

        }

        public void AddTilesToClips()
        {
            for (int i = 0; i < m_clips.Count; i++)
            {
                m_tiles.Add(new Sprite(this));
                m_tiles[m_tiles.Count - 1].Initialize("brickwall", ref spriteBatch);
                m_tiles[m_tiles.Count - 1].m_transform.m_position = m_clips[i].m_transform.m_position;
                m_tiles[m_tiles.Count - 1].m_sourceRectangle.Width = m_clips[i].m_sourceRectangle.Width;
                m_tiles[m_tiles.Count - 1].m_sourceRectangle.Height = m_clips[i].m_sourceRectangle.Height;
                m_tiles[m_tiles.Count - 1].m_texture.Name = "brickwall";
            }
        }
    }

            

       
}
