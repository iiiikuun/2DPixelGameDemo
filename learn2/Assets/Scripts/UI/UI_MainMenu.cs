using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;

    private void Start()
    {
        if (!SaveManager.instance.HasSaveData())
            continueButton.SetActive(false);
    }

}
