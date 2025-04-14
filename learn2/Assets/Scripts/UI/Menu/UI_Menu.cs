using UnityEngine;

public class UI_Menu : MonoBehaviour
{
    public UI_ItemToolTip itemToolTip;
    public UI_SkillToolTip skillToolTip;

    [SerializeField] private GameObject menuBackground;
    [SerializeField] private GameObject Character;
    [SerializeField] private GameObject CharacterStats;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (menuBackground.activeSelf)
                CloseUI();
            else
            {
                SwitchTo(Character);
                CharacterStats.SetActive(true);
            }
        }
    }

    public void SwitchTo(GameObject menu)
    {
        CloseUI();

        menuBackground.SetActive(true);
        menu.SetActive(true);
    }

    public void CloseUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
