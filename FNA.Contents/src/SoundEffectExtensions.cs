using System;

namespace Microsoft.Xna.Framework.Audio
{
    /// <summary>Compatibility helpers for legacy XNA sound playback.</summary>
    public static class SoundEffectExtensions
    {
        /// <summary>
        /// Plays a sound, optionally looping until its owning SoundEffect is disposed.
        /// Use CreateInstance when the caller needs to stop or control playback.
        /// </summary>
        public static bool Play(this SoundEffect soundEffect, float volume, float pitch, float pan, bool loop)
        {
            ArgumentNullException.ThrowIfNull(soundEffect);

            if (!loop)
                return soundEffect.Play(volume, pitch, pan);

            var instance = soundEffect.CreateInstance();
            try
            {
                instance.Volume = volume;
                instance.Pitch = pitch;
                instance.Pan = pan;
                instance.IsLooped = true;
                instance.Play();
                if (instance.State == SoundState.Playing)
                {
                    // FNA keeps playing fire-and-forget instances alive and disposes
                    // them when their owning SoundEffect is disposed.
                    return true;
                }
            }
            catch
            {
                instance.Dispose();
                throw;
            }

            instance.Dispose();
            return false;
        }
    }
}
