//
// © Arthur Vasseur arthurvasseur.fr
//
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace GalaxieShootEmUp.Initialization
{
    public class OptionInitialization : MonoBehaviour
    {
        public List<AudioObject> audioObjects;
        public AudioMixer mixer;

        private void Awake()
        {
            foreach (AudioObject audioObject in audioObjects)
                audioObject.slider.value = 0.5f;
        }
    }
}