using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CUL
{
/// <summary>
/// Special formatted Debugging class to make it easier to access different debug-logs in the Console.
/// </summary>
public static class Debugging 
{

    /// <summary>
    /// Takes an input string and the class that sent it and assigns a fixed random color to the class and prints out the Debug in bold sothat it stands out from other debugs.
    /// Must be called by providing the class name calling it. You can get that by using this.GetType().Name.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="sender"></param>
    public static void Log(string message, string sender)
    {
        var random = new System.Random(sender.GetHashCode());
        string color = String.Format("#{0:X6}", random.Next(0x1000000));

        string res = "<color=" + color +">" + sender + ": </color> <b>" + message + "</b>";

        Debug.Log(res);
    }

    /// <summary>
    /// A more generalized version for easier calling.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="sender"></param>
    public static void Log(string message, object sender)
    {
        Log(message, sender.GetType().Name);
    }

        /// <summary>
    /// Takes an input string and the class that sent it and assigns a fixed random color to the class and prints out the Debug in bold sothat it stands out from other debugs.
    /// Must be called by providing the class name calling it. You can get that by using this.GetType().Name.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="sender"></param>
    public static void LogError(string message, string sender)
    {
        string color = String.Format("#FF0000");

        string res = "<color=" + color +">" + sender + ": </color> <b>" + message + "</b>";

        Debug.LogError(res);
    }
   
}

}

