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
    public class AudioManager : Microsoft.Xna.Framework.GameComponent
    {
        private AudioEngine m_audioEngine;
        private WaveBank m_waveBank;
        private SoundBank m_soundBank;

        private List<Cue> m_cues;

        public AudioManager(Game game)
            : base(game)
        {
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public virtual void Initialize(string audioEngineSettings, string waveBank, string soundBank)
        {
            m_cues = new List<Cue>();

            m_audioEngine = new AudioEngine(audioEngineSettings);
            m_waveBank = new WaveBank(m_audioEngine, waveBank);
            m_soundBank = new SoundBank(m_audioEngine, soundBank);

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            m_audioEngine.Update();

            base.Update(gameTime);
        }

        public void PlayLooped(string cue)
        {
            Cue tempCue = m_soundBank.GetCue(cue);
            tempCue.Play();
            m_cues.Add(tempCue);
        }

        public void PlaySoundFX(string cue)
        {
            m_soundBank.PlayCue(cue);
        }


        public void Stop(string cue)
        {
            for (int i = 0; i < m_cues.Count(); i++)
            {
                if (m_cues[i].Name == cue)
                {
                    m_cues[i].Stop(AudioStopOptions.Immediate);
                    //break;
                }
            }
        }

        public SoundBank SoundBank
        {
            get { return m_soundBank; }
            set { m_soundBank = value; }
        }
    }
}