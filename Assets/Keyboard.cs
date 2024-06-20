using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityRawInput;
using System;

public class Keyboard : MonoBehaviour
{
  public List<RawKey> keys = new List<RawKey>();

  public List<BigKey> bigKeys = new List<BigKey>();
  [Serializable]
  public class BigKey
  {
    public string name;
    public Vector2 pos, vel;

    public BigKey(string name, Vector2 pos, Vector2 vel)
    {
      this.name = name;
      this.pos = pos;
      this.vel = vel;
    }
  }

  // insert class and a modifier class
  // insert = abc 123 [] etc.
  // modifier = space enter esc etc.

  public Camera cam;
  public int keySize = 20;
  public Texture2D texture;

  // public ParticleSystem shatter;
  // public List<Vector3> shatterQueue;

  GUIStyle style = new GUIStyle();
  void OnEnable()
  {
    style.fontStyle = FontStyle.Bold;
    style.alignment = TextAnchor.MiddleCenter;

    RawKeyInput.Start(true);
    RawKeyInput.OnKeyDown += KeyDown;
  }

  void OnDisable()
  {
    RawKeyInput.OnKeyDown -= KeyDown;
    RawKeyInput.Stop();
  }

  float lastTime;
  void Update()
  {
    for (int i = bigKeys.Count - 1; i >= 0; i--)
    {
      // bigKeys[i].pos += bigKeys[i].vel * Time.deltaTime;

      if (Time.time - lastTime > 3f)
      {
        bigKeys.RemoveAt(i);
      }
    }
  }

  Vector2 lastPos = new Vector2(0,0);
  void KeyDown(RawKey rawKey)
  {
    foreach (RawKey key in keys)
    {
      if (key == rawKey)
      {
        Vector2 pos = lastPos + new Vector2(keySize, 0);

        if (pos.x > Screen.width / 2)
        {
          pos.x = keySize - Screen.width / 2;
        }

        BigKey newKey = new BigKey(
          key.ToString().TrimStart(new char[] { 'n', 'u', 'm' }), 
          pos,
          new Vector2(0, 10)
        );

        bigKeys.Add(newKey);
        lastPos = newKey.pos;
      }
    }

    for (int i = 0; i < bigKeys.Count; i++)
    {
      if (rawKey == RawKey.Space)
      {
        bigKeys[i].pos += new Vector2(-keySize, 0);
      }

      if (rawKey == RawKey.Return)
      {
        bigKeys[i].pos += new Vector2(0, keySize);
      }
    }

    lastTime = Time.time;
  }

  void OnGUI()
  {
    GUI.color = Color.black;
    GUI.backgroundColor = Color.white;

    Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

    for (int i = 0; i < bigKeys.Count; i++)
    {
      Rect rect = new Rect(
        screenCenter.x + bigKeys[i].pos.x, 
        screenCenter.y + bigKeys[i].pos.y, 
        keySize, keySize
      );
      GUI.DrawTexture(rect, texture, ScaleMode.StretchToFill, false, 1, Color.white, 0, 0);
      GUI.Box(rect, bigKeys[i].name, style);
    }
  }
}
