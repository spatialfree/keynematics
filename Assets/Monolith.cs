using UnityEngine;
using UnityRawInput;
using UnityEngine.UI;

public class Monolith : MonoBehaviour
{
  public RawKey[] keys;

  public Transform keyboard;

  void OnEnable()
  {
    RawKeyInput.Start(true);
    RawKeyInput.OnKeyDown += KeyDown;
  }

  void OnDisable()
  {
    RawKeyInput.OnKeyDown -= KeyDown;
    RawKeyInput.Stop();
  }

  void KeyDown(RawKey key)
  {
    Debug.Log(key.ToString());

    for (int i = 0; i < keyboard.childCount; i++)
    {
      Transform k = keyboard.GetChild(i);
      if (k.name == key.ToString())
      {
        // set image color to white and alpha to 1
        k.GetComponent<Image>().color = Color.white;
      }
    }
  }

  void Update()
  {
    for (int i = 0; i < keyboard.childCount; i++)
    {
      Transform k = keyboard.GetChild(i);
      // k.localScale = Vector3.one * Random.value;
      Image image = k.GetComponent<Image>();
      // fade image alpha to 0
      Color c = image.color;
      c.a = Mathf.Lerp(c.a, 0, Time.deltaTime);
      image.color = c;
    }
  }
}
