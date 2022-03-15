using System.Collections;
using System.Collections.Generic;
using CUL.MLS;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MLS_Tests
{
    // A Test behaves as an ordinary method
    [Test]
    public void MLS_TestsSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator MLS_TestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    #region Setup

    private GameObject testObject;
    private MultiLanguageText mlsText;

    [SetUp]
    public void Setup()
    {
        testObject = new GameObject();
        mlsText = testObject.AddComponent<MultiLanguageText>();
    }

    #endregion

    #region MultiLanguageText Tests

    [UnityTest]
    public IEnumerator Test_IsReadyAfterInitialization()
    {
        yield return null;
        Assert.True(mlsText.IsReady());
        
    }

    #endregion
}
