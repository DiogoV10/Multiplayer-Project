using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace V10
{
    public class Loadout : MonoBehaviour
    {

        [System.Serializable]
        public class LoadoutBlock
        {
            public string loadoutName;
            public List<GameObject> guns = new List<GameObject>();
            public bool isComplete => guns.Count == 3;
        }

        public List<LoadoutBlock> loadouts = new List<LoadoutBlock>();
        private LoadoutBlock selectedLoadout;

        private List<LoadoutButton> loadoutButtons;

        private void Start()
        {
            loadoutButtons = new List<LoadoutButton>(FindObjectsOfType<LoadoutButton>());
            LoadLoadouts();
        }

        public void LoadLoadouts()
        {
            for (int i = 0; i < 3; i++)
            {
                LoadLoadout(i);
            }
        }

        public void OnClickLoadout(string loadoutName)
        {
            selectedLoadout = new LoadoutBlock { loadoutName = loadoutName };
            Debug.Log("Selected loadout: " + selectedLoadout);
            loadouts.Add(selectedLoadout);
        }

        public void OnClickWeapon(GameObject gun)
        {
            if(loadouts.Count > 0)
            {

                if (!selectedLoadout.guns.Contains(gun) && !selectedLoadout.isComplete)
                {
                    selectedLoadout.guns.Add(gun);

                    if(selectedLoadout.isComplete)
                    {
                        UpdateLoadoutButton();
                    }
                }
                else
                {
                    Debug.Log("Gun Currently added: " + gun.name);
                }
            }
        }

        public List<LoadoutBlock> GetLoadouts()
        {
            return loadouts;
        }

        private void LoadLoadout(int loadoutIndex)
        {
            string filePath = Path.Combine(Application.persistentDataPath, "Loadout_" + loadoutIndex + ".json");

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                LoadoutBlock loadedLoadout = JsonUtility.FromJson<LoadoutBlock>(json);

                if (loadoutIndex < loadouts.Count)
                {
                    loadouts[loadoutIndex] = loadedLoadout;
                }
                else
                {
                    loadouts.Add(loadedLoadout);
                }

                Debug.Log("Loaded Loadout: " + loadedLoadout.loadoutName);

                UpdateLoadoutButtons();
            }
        }

        public void SaveLoadouts()
        {
            for (int i = 0; i < loadouts.Count; i++)
            {
                SaveLoadout(i);
            }
        }

        private void SaveLoadout(int loadoutIndex)
        {
            if (loadoutIndex >= 0 && loadoutIndex < loadouts.Count)
            {
                LoadoutBlock loadoutToSave = loadouts[loadoutIndex];

                string json = JsonUtility.ToJson(loadoutToSave);

                string filePath = Path.Combine(Application.persistentDataPath, "Loadout_" + loadoutIndex + ".json");

                File.WriteAllText(filePath, json);

                Debug.Log("Loadout salvo: " + loadoutToSave.loadoutName);
            }
        }


        public List<GameObject> GetLoadoutWeapons(int indiceLoadout)
        {
            if (indiceLoadout >= 0 && indiceLoadout < loadouts.Count)
            {
                return loadouts[indiceLoadout].guns;
            }
            else
            {
                Debug.Log("Loadout Index Invalid");
                return new List<GameObject>();
            }
        }

        private void UpdateLoadoutButton()
        {
            LoadoutButton loadoutButton = FindLoadoutButton(selectedLoadout.loadoutName);

            if (loadoutButton != null)
            {
                loadoutButton.UpdateWeaponsText();
            }
        }

        private void UpdateLoadoutButtons()
        {
            foreach (var button in loadoutButtons)
            {
                button.UpdateWeaponsText();
            }
        }

        private LoadoutButton FindLoadoutButton(string loadoutName)
        {
            LoadoutButton[] loadoutButtons = GameObject.FindObjectsOfType<LoadoutButton>();
            foreach (var button in loadoutButtons)
            {
                if (button.loadoutName.text == loadoutName)
                {
                    return button;
                }
            }

            return null;
        }

        public List<string> GetLoadoutNames()
        {
            List<string> nomesLoadouts = new List<string>();
            foreach (var loadout in loadouts)
            {
                nomesLoadouts.Add(loadout.loadoutName);
            }
            return nomesLoadouts;
        }

        public LoadoutBlock GetLoadoutByName(string loadoutName)
        {
            return loadouts.Find(loadout => loadout.loadoutName == loadoutName);
        }

    }
}
