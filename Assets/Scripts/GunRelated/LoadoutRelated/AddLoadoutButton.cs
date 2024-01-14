using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace V10
{
    public class AddLoadoutButton : MonoBehaviour
    {
        public GameObject loadoutItem;
        public Transform parent;

        public void OnPressBotao(int i)
        {
            InstantiateBotao();
            AjustarTamanhoContent();
        }

        void InstantiateBotao()
        {
            GameObject novoBotao = Instantiate(loadoutItem);
            novoBotao.transform.SetParent(parent);
        }

        void AjustarTamanhoContent()
        {
            // Obt�m o componente ContentSizeFitter do Content
            ContentSizeFitter contentSizeFitter = parent.GetComponent<ContentSizeFitter>();

            // Se o componente existir, force uma atualiza��o do tamanho
            if (contentSizeFitter != null)
            {
                contentSizeFitter.SetLayoutVertical();
            }
        }
    }
}
