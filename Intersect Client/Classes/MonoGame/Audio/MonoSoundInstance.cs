﻿/*
    The MIT License (MIT)

    Copyright (c) 2015 JC Snider, Joe Bridges
  
    Website: http://ascensiongamedev.com
    Contact Email: admin@ascensiongamedev.com

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntersectClientExtras.Audio;
using Intersect_Client.Classes.General;
using Microsoft.Xna.Framework.Audio;

namespace Intersect_MonoGameDx.Classes.SFML.Audio
{
    public class MonoSoundInstance : GameAudioInstance
    {
        private SoundEffectInstance _instance;
        private bool _disposed;
        private int _volume;
        public MonoSoundInstance(GameAudioSource music) : base(music)
        {
            _instance = ((MonoSoundSource)music).GetEffect().CreateInstance();
        }

        public override void Play()
        {
            _instance.Play();
        }

        public override void Pause()
        {
            _instance.Pause();
        }

        public override void Stop()
        {
            _instance.Stop();
        }

        public override void SetVolume(int volume, bool isMusic = false)
        {
            _volume = volume;
            _instance.Volume = (_volume * (float)(Globals.Database.SoundVolume / 100f) / 100f);
        }

        public override int GetVolume()
        {
            return _volume;
        }

        public override void SetLoop(bool val)
        {
            _instance.IsLooped = val;
        }

        public override AudioInstanceState GetState()
        {
            if (_disposed) return AudioInstanceState.Disposed;
            if (_instance.State == SoundState.Playing) return AudioInstanceState.Playing;
            if (_instance.State == SoundState.Stopped) return AudioInstanceState.Stopped;
            if (_instance.State == SoundState.Paused) return AudioInstanceState.Paused;
            return AudioInstanceState.Disposed;
        }

        public override void Dispose()
        {
            _disposed = true;
            _instance.Stop();
            _instance.Dispose();
        }
    }
}