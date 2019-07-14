using System.Collections;
using System.Collections.Generic;
using TapsellPlusSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour {

  private const string TapsellPlusKey = "alsoatsrtrotpqacegkehkaiieckldhrgsbspqtgqnbrrfccrtbdomgjtahflchkqtqosa";

  void Start () {
    TapsellPlus.initialize (TapsellPlusKey);
  }

  public void changeScenes (string name) {
    SceneManager.LoadScene (name);
  }
}