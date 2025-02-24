using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class SpellResetter
{
     static SpellResetter()
     {
          EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
     }
     
     static void OnPlayModeStateChanged(PlayModeStateChange state)
     {
          if (state == PlayModeStateChange.ExitingPlayMode)
          {
               ResetSpells();
          }
     }

     static void ResetSpells()
     {
          string[] ids = AssetDatabase.FindAssets("t:PassiveSpell");
          for (int i = 0; i < ids.Length; i++)
          {
               PassiveSpell passiveSpell = AssetDatabase.LoadAssetAtPath<PassiveSpell>(AssetDatabase.GUIDToAssetPath(ids[i]));
               if(passiveSpell == null) continue;
               
               passiveSpell.Reset();
               EditorUtility.SetDirty(passiveSpell);
               Debug.Log($"{passiveSpell.name} has been reset");
          }
          AssetDatabase.SaveAssets();
     }
}
