using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;
using TMPro;

public class CardController : MonoBehaviour
{
    [SerializeField]
    ARTrackedImageManager m_TrackedImageManager;
    public Sprite[] cards = new Sprite[22];
    public string[] titles = new string[22], desc = new string[22], descinv = new string[22], credit = new string[22];

    public TMP_Text titleUI, descUI, descinvUI, creditUI;
    public Image cardUp, cardDown;
    public GameObject menu;
    public bool disabled = false, inverted = false, debug = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        DetectCard();
    }
    
    void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
{
        foreach (var newImage in eventArgs.added)
        {
            // Handle added event
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            // Handle updated event
            DetectCard();
        }

        foreach (var removed in eventArgs.removed)
        {
            // Handle removed event
        }
    }

    void DetectCard()
    {
        if (m_TrackedImageManager.trackables.count == 1 && !disabled)
        {
            openInfo(true);
            foreach (var trackedImage in m_TrackedImageManager.trackables)
            {
                int i = int.Parse(trackedImage.referenceImage.name);
                titleUI.SetText(titles[i]);
                descUI.SetText(desc[i]);
                descinvUI.SetText(descinv[i]);
                creditUI.SetText(credit[i]);
                cardUp.sprite = cards[i];
                cardDown.sprite = cards[i];
            }
        }
    }

    public void openInfo(bool b)
    {
        disabled = b;
        menu.SetActive(b);
        inverted = b;
        invertCard();
    }

    public void closeInfo()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void invertCard()
    {
        inverted = !inverted;
        cardDown.gameObject.SetActive(inverted);
        cardUp.gameObject.SetActive(!inverted);
        descinvUI.gameObject.SetActive(inverted);
        descUI.gameObject.SetActive(!inverted);
    }
}
