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
    public bool debug;

    [SerializeField]
    private string textkey;

    [Tooltip("Determines whether or not to fill the adjacent TextMeshPro or Text Component with the MLS text.")]
    public bool fillText = true;

    private string mlsText = "";

    private TextMeshProUGUI textMeshPro;
    private bool tmProExists;
    private bool simpleTextExists;
    private UnityEngine.UI.Text text;
    private MultiLanguageController multiLanguageController;

    private bool ready;

#region Monobehaviour Callbacks

    // Start is called before the first frame update
    void Start()
    {
        if(debug)
            Debugging.Log("Running MLS Text Initialization.", this.GetType().Name);

        MultilanguageSingleton.LanguageSelection += LanguageSelectionHandler;

        tmProExists = TryGetComponent<TextMeshProUGUI>(out textMeshPro);
        simpleTextExists = TryGetComponent<UnityEngine.UI.Text>(out text);

        LoadLanguageFiles();
    }

    #endregion
    
    #region private Methods

    private void LanguageSelectionHandler(string langkey)
    {

        LoadLanguageFiles();

        if(debug) Debugging.Log("Received Language Selection Event. Switching Texts", this.GetType().Name);
    }

    private void LoadLanguageFiles(string key = "")
    {
        if(debug)
        {
            Debugging.Log("MLS found, loading language files", this);
        }

        if(key == "")
        {
            mlsText = MultilanguageSingleton.Instance.ReadXML(textkey);
        }
        else
        {
            mlsText = MultilanguageSingleton.Instance.ReadXML(key);
        }
        

        if(fillText)
        {
            FillTextComponents(mlsText);
        }

        ready = true;
    }

    private void FillTextComponents(string mls)
    {
        if (tmProExists)
        {
            textMeshPro.text = mlsText;
        }
        if (simpleTextExists)
        {
            text.text = mlsText;
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
        return mlsText;
    }

    public string GetMLSText(string textKey)
    {
        LoadLanguageFiles(textKey);
        return mlsText;
    }

    #endregion
    
}


}

