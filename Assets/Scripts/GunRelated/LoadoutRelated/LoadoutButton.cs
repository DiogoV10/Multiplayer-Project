using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace V10
{
    public class LoadoutButton : MonoBehaviour
    {
        public Button saveLoadoutButton, deleteLoadoutButton;
        public Loadout loadoutSystem;

        public TextMeshProUGUI loadoutName, loadoutWeapons;

        private void Start()
        {
            Button ladoutButton = GetComponent<Button>();

            ladoutButton.onClick.AddListener(OnClickLoadout);
            saveLoadoutButton.onClick.AddListener(OnClickSaveLoadouts);
            deleteLoadoutButton.onClick.AddListener(OnClickDeleteLoadout);
        }

        private void OnClickLoadout()
        {
            loadoutSystem.OnClickLoadout(loadoutName.text);
        }

        private void OnClickDeleteLoadout()
        {
            loadoutSystem.DeleteLoadout(loadoutName.text);
        }

        private void OnClickSaveLoadouts()
        {
            loadoutSystem.SaveLoadouts();
        }

        public void UpdateWeaponsText()
        {
            int loadoutIndex = loadoutSystem.GetLoadoutNames().IndexOf(loadoutName.text);
            if(loadoutIndex >= 0)
            {
                StringBuilder loadoutWeaponsText = new StringBuilder("Guns : ");

                foreach(var guns in loadoutSystem.GetLoadoutWeapons(loadoutIndex))
                {
                    loadoutWeaponsText.Append(guns.name).Append(", ");
                }
                loadoutWeapons.text = loadoutWeaponsText.ToString().TrimEnd(',', ' ');
            }
            else
            {
                loadoutWeapons.text = "Guns : ";
            }
        }
    }
}
