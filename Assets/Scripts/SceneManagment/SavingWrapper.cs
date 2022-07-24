using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagment
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        [SerializeField] SavingSystem savesystem;

        private void Awake()
        {
            savesystem = GetComponent<SavingSystem>();
        }

        // Called in Unity UI Buttons
        public void Load()
        {
            savesystem.Load(defaultSaveFile);
        }
        public void Save()
        {
            savesystem.Save(defaultSaveFile);
        }
    }

}