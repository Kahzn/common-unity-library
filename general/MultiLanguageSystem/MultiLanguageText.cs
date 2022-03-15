using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


namespace CUL.MLS
{

/// <summary>
/// Switches the UI Texts according to a specified text key. These keys are used to parse an XML file and add the according text within them.
/// <para> Created by Katharina Ziolkowski, 31_10_2020</para>
/// </summary>
public class MultiLanguageText : MonoBehaviour
{
    [SerializeField]
    private string textkey = "textkey";

    [Tooltip("Determines whether or not to fill the adjacent TextMeshPro or Text Component with the MLS text.")]
    public bool fillText = true;

    private string translatedText = "";
    private TextMeshProUGUI textMeshPro;
    private UnityEngine.UI.Text text;
    private MultiLanguageController multiLanguageController;
    private bool tmProExists;
    private bool simpleTextExists;
    private bool ready;

#region Initialization

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        DebugHandler.Log("Running MLS Text Initialization.", this);

        BindLanguageSelecionHandler();
        CheckForTextComponents();
        LoadLanguageFiles();
    }

    private void BindLanguageSelecionHandler()
    {
        MultilanguageSingleton.LanguageSelection += LanguageSelectionHandler;
    }

    private void CheckForTextComponents()
    {
        tmProExists = TryGetComponent<TextMeshProUGUI>(out textMeshPro);
        simpleTextExists = TryGetComponent<UnityEngine.UI.Text>(out text);
    }

#endregion

#region private Methods

    private void LanguageSelectionHandler(string langkey)
    {
        DebugHandler.Log("Received Language Selection Event. Switching Texts", this);
        LoadLanguageFiles();
    }

    private void LoadLanguageFiles(string key = "")
    {
        DebugHandler.Log("MLS found, loading language files", this);

        if (key == "")
        {
            translatedText = MultilanguageSingleton.Instance.ReadXML(textkey);
        }
        else
        {
            translatedText = MultilanguageSingleton.Instance.ReadXML(key);
        }
        

        if(fillText)
        {
            FillTextComponents(translatedText);
        }

        ready = true;
    }

    private void FillTextComponents(string mls)
    {
        if (tmProExists)
        {
            textMeshPro.text = translatedText;
        }
        if (simpleTextExists)
        {
            text.text = translatedText;
        }
    }

    #endregion

    #region Public Methods

    public bool IsReady()
    {
        return ready;
    }

    public string GetMLSText()
    {
        return translatedText;
    }

    public string GetMLSText(string textKey)
    {
        LoadLanguageFiles(textKey);
        return translatedText;
    }

    #endregion
    
}


}

