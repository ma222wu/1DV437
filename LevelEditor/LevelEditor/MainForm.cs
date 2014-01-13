using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LevelEditor
{
    public partial class MainForm : Form
    {
        Game1 m_game;
        public Game1 Game
        {
            get { return m_game; }
            set { m_game = value; }
        }
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Game.SaveLevel(saveFileDialog1.FileName);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Game.m_currentTool = Tool.Select;
        }

        private void rbToolClip_CheckedChanged(object sender, EventArgs e)
        {
            Game.m_currentTool = Tool.Clip;
        }

        private void rbToolEnemy_CheckedChanged(object sender, EventArgs e)
        {
            Game.m_currentTool = Tool.Enemy;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Game.LoadLevel(openFileDialog1.FileName);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void rbToolTrigger_CheckedChanged(object sender, EventArgs e)
        {
            Game.m_currentTool = Tool.Trigger;

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tbTriggerData_TextChanged(object sender, EventArgs e)
        {
            if (Game.m_selectedObject != null && Game.m_selectedObjectsList == Game.m_triggers)
            {
                (Game.m_selectedObject as Trigger).m_scriptLines[0] = tbTriggerData.Text;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            pictureBox1.Image = Image.FromFile("../../../../../Potholy/PotholyContent/" + (string)lbCurrentTexture.SelectedItem + ".png");
        }

        private void rbToolTile_CheckedChanged(object sender, EventArgs e)
        {
            Game.m_currentTool = Tool.Tile;

        }

        private void btnObjXPlus_Click(object sender, EventArgs e)
        {
            if (Game.m_selectedObjectsList == Game.m_tiles)
            {
                (Game.m_selectedObject as Sprite).m_transform.m_position.X += (float)nudMasterValue.Value;
            }
            else if (Game.m_selectedObjectsList == Game.m_clips)
            {
                (Game.m_selectedObject as Block).m_sourceRectangle.X += (int)nudMasterValue.Value;
                (Game.m_selectedObject as Block).m_transform.m_position.X += (int)nudMasterValue.Value;

            }
             else if (Game.m_selectedObjectsList == Game.m_triggers)
            {
                (Game.m_selectedObject as Trigger).m_sourceRectangle.X += (int)nudMasterValue.Value;
                (Game.m_selectedObject as Trigger).m_transform.m_position.X += (int)nudMasterValue.Value;

            }
        }

        private void btnObjYPlus_Click(object sender, EventArgs e)
        {
            if (Game.m_selectedObjectsList == Game.m_tiles)
            {
                (Game.m_selectedObject as Sprite).m_transform.m_position.Y += (float)nudMasterValue.Value;
            }
            else if (Game.m_selectedObjectsList == Game.m_clips)
            {
                (Game.m_selectedObject as Block).m_sourceRectangle.Y += (int)nudMasterValue.Value;
                (Game.m_selectedObject as Block).m_transform.m_position.Y += (int)nudMasterValue.Value;

            }
            else if (Game.m_selectedObjectsList == Game.m_triggers)
            {
                (Game.m_selectedObject as Trigger).m_sourceRectangle.Y += (int)nudMasterValue.Value;
                (Game.m_selectedObject as Trigger).m_transform.m_position.Y += (int)nudMasterValue.Value;

            }
        }

        private void btnObjWidthPlus_Click(object sender, EventArgs e)
        {
            if (Game.m_selectedObjectsList == Game.m_tiles)
            {
                (Game.m_selectedObject as Sprite).m_sourceRectangle.Width += (int)nudMasterValue.Value;
            }
            else if (Game.m_selectedObjectsList == Game.m_clips)
            {
                (Game.m_selectedObject as Block).m_sourceRectangle.Width += (int)nudMasterValue.Value;
            }
            else if (Game.m_selectedObjectsList == Game.m_triggers)
            {
                (Game.m_selectedObject as Trigger).m_sourceRectangle.Width += (int)nudMasterValue.Value;
            }
        }

        private void btnObjHeightPlus_Click(object sender, EventArgs e)
        {
            if (Game.m_selectedObjectsList == Game.m_tiles)
            {
                (Game.m_selectedObject as Sprite).m_sourceRectangle.Height += (int)nudMasterValue.Value;
            }
            else if (Game.m_selectedObjectsList == Game.m_clips)
            {
                (Game.m_selectedObject as Block).m_sourceRectangle.Height += (int)nudMasterValue.Value;
            }
            else if (Game.m_selectedObjectsList == Game.m_triggers)
            {
                (Game.m_selectedObject as Trigger).m_sourceRectangle.Height += (int)nudMasterValue.Value;
            }
        }

        private void btnObjXMinus_Click(object sender, EventArgs e)
        {
            if (Game.m_selectedObjectsList == Game.m_tiles)
            {
                (Game.m_selectedObject as Sprite).m_transform.m_position.X -= (float)nudMasterValue.Value;
            }
            else if (Game.m_selectedObjectsList == Game.m_clips)
            {
                (Game.m_selectedObject as Block).m_sourceRectangle.X -= (int)nudMasterValue.Value;
                (Game.m_selectedObject as Block).m_transform.m_position.X -= (int)nudMasterValue.Value;

            }
            else if (Game.m_selectedObjectsList == Game.m_triggers)
            {
                (Game.m_selectedObject as Trigger).m_sourceRectangle.X -= (int)nudMasterValue.Value;
                (Game.m_selectedObject as Trigger).m_transform.m_position.X -= (int)nudMasterValue.Value;

            }
        }

        private void btnObjYMinus_Click(object sender, EventArgs e)
        {
            if (Game.m_selectedObjectsList == Game.m_tiles)
            {
                (Game.m_selectedObject as Sprite).m_transform.m_position.Y -= (float)nudMasterValue.Value;
            }
            else if (Game.m_selectedObjectsList == Game.m_clips)
            {
                (Game.m_selectedObject as Block).m_sourceRectangle.Y -= (int)nudMasterValue.Value;
                (Game.m_selectedObject as Block).m_transform.m_position.Y -= (int)nudMasterValue.Value;

            }
            else if (Game.m_selectedObjectsList == Game.m_triggers)
            {
                (Game.m_selectedObject as Trigger).m_sourceRectangle.Y -= (int)nudMasterValue.Value;
                (Game.m_selectedObject as Trigger).m_transform.m_position.Y -= (int)nudMasterValue.Value;

            }
        }

        private void btnObjWidthMinus_Click(object sender, EventArgs e)
        {
            if (Game.m_selectedObjectsList == Game.m_tiles)
            {
                (Game.m_selectedObject as Sprite).m_sourceRectangle.Width -= (int)nudMasterValue.Value;
            }
            else if (Game.m_selectedObjectsList == Game.m_clips)
            {
                (Game.m_selectedObject as Block).m_sourceRectangle.Width -= (int)nudMasterValue.Value;
            }
            else if (Game.m_selectedObjectsList == Game.m_triggers)
            {
                (Game.m_selectedObject as Trigger).m_sourceRectangle.Width -= (int)nudMasterValue.Value;
            }
        }

        private void btnObjHeightMinus_Click(object sender, EventArgs e)
        {
            if (Game.m_selectedObjectsList == Game.m_tiles)
            {
                (Game.m_selectedObject as Sprite).m_sourceRectangle.Height -= (int)nudMasterValue.Value;
            }
            else if (Game.m_selectedObjectsList == Game.m_clips)
            {
                (Game.m_selectedObject as Block).m_sourceRectangle.Height -= (int)nudMasterValue.Value;
            }
            else if (Game.m_selectedObjectsList == Game.m_triggers)
            {
                (Game.m_selectedObject as Trigger).m_sourceRectangle.Height -= (int)nudMasterValue.Value;
            }
        }

        private void btnClipsToBrickTiles_Click(object sender, EventArgs e)
        {
            Game.AddTilesToClips();
        }
    }
}
