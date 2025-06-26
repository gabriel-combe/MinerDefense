using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSkinsMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject skinButtonPrefab;

    [SerializeField]
    private BackgroundSkinsTypeSO[] backgroundSkinList;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var skin in backgroundSkinList)
        {
            BackgroundSkinButton button = Instantiate(skinButtonPrefab, transform).GetComponent<BackgroundSkinButton>();

            button.SetBackgroundSkinType(skin);
        }
    }
}
