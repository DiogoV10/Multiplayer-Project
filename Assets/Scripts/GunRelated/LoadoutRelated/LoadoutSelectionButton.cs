using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace V10
{
    public class LoadoutSelectionButton : MonoBehaviour
    {
        Button loadoutButton;
        public Loadout loadoutSystem;
        public Loadout.LoadoutBlock selectedLoadout;

        public TextMeshProUGUI loadoutName, loadoutGuns;

        bool loadedLoadouts;

        private void Start()
        {
            loadoutButton = GetComponent<Button>();
            loadoutButton.onClick.AddListener(OnLoadoutSelectClick);
        }

        private void Update()
        {
            if(!loadedLoadouts) UpdateLoadoutsTexts();
            
        }

        private void UpdateLoadoutsTexts()
        {
            int loadoutIndex = loadoutSystem.GetLoadoutNames().IndexOf(loadoutName.text);
            Debug.Log(loadoutIndex);
            if (loadoutIndex >= 0)
            {
                StringBuilder loadoutWeaponsText = new StringBuilder("Guns : ");

                foreach (var guns in loadoutSystem.GetLoadoutWeapons(loadoutIndex))
                {
                    loadoutWeaponsText.Append(guns.name).Append(", ");
                }
                loadoutGuns.text = loadoutWeaponsText.ToString().TrimEnd(',', ' ');
                Debug.Log("Loaded loadout on button");
            }
            else
            {
                Debug.Log("Could not load loadout");
                loadoutGuns.text = "Guns : ";
            }
            loadedLoadouts = true;
        }

        void OnLoadoutSelectClick()
        {
            Debug.Log("Selected: " + gameObject.name);
        }
    }
}
