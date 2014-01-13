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


namespace Potholy
{
    /// <summary>
    /// Simple audio managed that doesn't use XACT
    /// </summary>
    public class SimpleAudioManager : Microsoft.Xna.Framework.GameComponent
    {
        public Dictionary<string, SoundEffect> m_soundEffects = new Dictionary<string,SoundEffect>();
        public Dictionary<string, Song> m_songs = new Dictionary<string,Song>();
        string m_currentTrack;

        float m_soundVolume = 0.1f;
        float m_musicVolume = 0.1f;


        public SimpleAudioManager(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = m_musicVolume;

            m_soundEffects.Add("buy", Game.Content.Load<SoundEffect>("audio/buy"));
            m_soundEffects.Add("coin", Game.Content.Load<SoundEffect>("audio/coin"));
            m_soundEffects.Add("dash", Game.Content.Load<SoundEffect>("audio/dash"));
            m_soundEffects.Add("death", Game.Content.Load<SoundEffect>("audio/death"));
            m_soundEffects.Add("jump", Game.Content.Load<SoundEffect>("audio/jump"));
            m_soundEffects.Add("laser", Game.Content.Load<SoundEffect>("audio/laser"));
            m_soundEffects.Add("pain", Game.Content.Load<SoundEffect>("audio/pain"));
            m_soundEffects.Add("victory", Game.Content.Load<SoundEffect>("audio/victory"));
            m_soundEffects.Add("enemydeath", Game.Content.Load<SoundEffect>("audio/enemydeath"));

            m_songs.Add("adia", Game.Content.Load<Song>("audio/adia"));
            m_songs.Add("under", Game.Content.Load<Song>("audio/under"));
            m_songs.Add("power", Game.Content.Load<Song>("audio/power"));

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public void PlayMusic(string name)
        {
            if (name != m_currentTrack)
            {
                m_currentTrack = name;
                MediaPlayer.Stop();
                MediaPlayer.Play(m_songs[name]);
            }
        }

        public void PlaySoundFX(string name)
        {
            m_soundEffects[name].Play(m_soundVolume, 0, 0);

        }

        public void Stop(string name)
        {
        }
    }
}