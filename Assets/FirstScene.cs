using System.Collections;
using System.Collections.Generic;
using TapsellPlusSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour {

  private readonly string TAPSELL_PLUS_KEY = "chdbtgmphseatipmriionjqhkatlrcajkbsdcllcotehooeqabqkafepsthraplfssihth";

  void Start () {
    TapsellPlus.initialize (TAPSELL_PLUS_KEY);
  }

  public void changeScenes (string name) {
    SceneManager.LoadScene (name);
  }
}