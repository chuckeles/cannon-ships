using FlowCanvas.Nodes;
using ParadoxNotion.Design;
using UnityEngine;

[Category("Sound")]
public class PlaySoundAt : CallableActionNode<AudioSource, Vector3, AudioClip> {

    public override void Invoke(AudioSource original, Vector3 position, AudioClip sound) {
        var soundGameObject = new GameObject("TemporaryAudio");
        var soundAudioSource = soundGameObject.AddComponent<AudioSource>();

        soundGameObject.transform.position = position;

        soundAudioSource.clip = sound;
        soundAudioSource.outputAudioMixerGroup = original.outputAudioMixerGroup;
        soundAudioSource.mute = original.mute;
        soundAudioSource.bypassEffects = original.bypassEffects;
        soundAudioSource.bypassListenerEffects = original.bypassListenerEffects;
        soundAudioSource.bypassReverbZones = original.bypassReverbZones;
        soundAudioSource.playOnAwake = original.playOnAwake;
        soundAudioSource.loop = original.loop;
        soundAudioSource.priority = original.priority;
        soundAudioSource.volume = original.volume;
        soundAudioSource.pitch = original.pitch + UnityEngine.Random.Range(-0.12f, 0.18f);
        soundAudioSource.panStereo = original.panStereo;
        soundAudioSource.spatialBlend = original.spatialBlend;
        soundAudioSource.reverbZoneMix = original.reverbZoneMix;
        soundAudioSource.dopplerLevel = original.dopplerLevel;
        soundAudioSource.rolloffMode = original.rolloffMode;
        soundAudioSource.minDistance = original.minDistance;
        soundAudioSource.spread = original.spread;
        soundAudioSource.maxDistance = original.maxDistance;

        soundAudioSource.Play();
        Object.Destroy(soundGameObject, sound.length);
    }

}
