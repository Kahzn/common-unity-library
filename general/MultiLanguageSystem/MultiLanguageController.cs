using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;


namespace CUL.MLS
{

/// <summary>
/// Data Container for the Multilanguage-Singleton. Can be accessed by the Instance Controller further down.
/// <para> Created by Katharina Ziolkowski, 31_10_2020</para>
/// </summary>
public class MultilanguageSingleton
{
    private string defaultLanguage = "GER";
    public string currentLanguage { get; private set; } 

    private string systemPath = Application.streamingAssetsPath;
    private string path;
    private string configPath;
    private string config = "language_config";

    public delegate void MultilanguageEvent(string langkey);
    public static MultilanguageEvent LanguageSelection;
    private Dictionary <string, string> XML_Strings;


    //constructor, used for class initialization
    private MultilanguageSingleton()
    {
        Initialize();
    }


    private static MultilanguageSingleton _instance;

    public static MultilanguageSingleton Instance
    {
        get{
            if(_instance == null)
            {
                _instance = new MultilanguageSingleton();
            }
            return _instance;
        }
    }

    private void Initialize()
    {
        path = string.Concat(systemPath, Path.DirectorySeparatorChar, "LocalizationFiles", Path.DirectorySeparatorChar);
        configPath = string.Concat(path, config, ".ini");

        if (!File.Exists(configPath))
        {
            CreateFile(configPath);
            WriteLanguageSettings(defaultLanguage);
        }
        else
        {
            currentLanguage = ReadLanguageSettings();
        }

        FillXmlStrings();
    }

    #region Read / Write Config
    public void WriteLanguageSettings(string langkey)
    {
        currentLanguage = langkey;

        string[] content = { langkey };
        if (File.Exists(configPath))
        {
            File.WriteAllLines(configPath, content);
        }
        else
        {
            CreateFile(configPath);
            WriteLanguageSettings(langkey);
            Debugging.Log("Missing language config file, unable to write!", this.GetType().Name);
        }
        FillXmlStrings();
        LanguageSelection?.Invoke(langkey);
    }

    public string ReadLanguageSettings()
    {
        string[] result = {""};
        if (File.Exists(configPath))
        {
          result  = File.ReadAllLines(configPath);
            if (result != null && result.Length  > 0){
                LanguageSelection?.Invoke(result[0]);
                Debugging.Log("Reading: " + result[0], this.GetType().Name);
            }
            else
            {
                WriteLanguageSettings(defaultLanguage);
                LanguageSelection?.Invoke(defaultLanguage);
                Debugging.Log("Nothing to read!", this.GetType().Name);
                return defaultLanguage;
            }
        }
        else
        {
            WriteLanguageSettings(defaultLanguage);
            LanguageSelection?.Invoke(defaultLanguage);
            Debugging.Log("Missing language config file, unable to read!", this.GetType().Name);
            return defaultLanguage;
        }
        
        return result[0];
    }

    private void CreateFile(string filepath)
    {
        File.Create(filepath);
        Debugging.Log("Creating new File: " + configPath, this.GetType().Name);
    }
    #endregion

    #region XML Handling

    public string ReadXML(string textkey)
    {
        if (!XML_Strings.ContainsKey(textkey))
        {
            Debug.LogWarning("This string is not present in the XML file where you're reading: " + textkey);
            return "";
        }
        return (string)XML_Strings[textkey];
    }

    ///Read a XML stored on the computer
    private void FillXmlStrings()
    {
        XmlDocument xml = new XmlDocument();
        string path = string.Concat(systemPath, Path.DirectorySeparatorChar, "LocalizationFiles", Path.DirectorySeparatorChar, currentLanguage, ".xml");
        Debug.Log("Reading XML from this path: " + path);
        xml.Load(path);

        XML_Strings = new Dictionary<string, string>();
        XmlElement element = xml.DocumentElement[currentLanguage];

        if (element != null)
        {

            var elemEnum = element.GetEnumerator();

            while (elemEnum.MoveNext())
            {
                if (!string.IsNullOrEmpty((elemEnum.Current as XmlElement)?.InnerText))
                {
                    XML_Strings.Add((elemEnum.Current as XmlElement).GetAttribute("name"), (elemEnum.Current as XmlElement).InnerText.Replace(@"\n", Environment.NewLine));
                }
              
            }
        }
        else
        {
            Debug.LogError("The specified language does not exist: " + currentLanguage);
        }
    }

    #endregion
}

public class MultiLanguageController : MonoBehaviour
{
    public MultilanguageSingleton multiLanguageInstance;


    private void Awake()
    {
        multiLanguageInstance = MultilanguageSingleton.Instance;
    }


    public void SwitchLanguage(string langkey)
    {
        multiLanguageInstance.WriteLanguageSettings(langkey);
    }

    public string ReadFromLanguageFile(string textkey)
    {
        if (multiLanguageInstance == null)
        {
            multiLanguageInstance = MultilanguageSingleton.Instance;
        }
        return multiLanguageInstance.ReadXML(textkey);
    }
}



}
