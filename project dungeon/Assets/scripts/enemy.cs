using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class enemy : ScriptableObject
{
    public enum AItype { CHASER, ICESHOOTER, FARTNADO, KAMAKAZE, SLIMETRAIL, MUILTIPLY, GROWTOPARENT };
    public AItype type;
    public float health;
    public float baseDamage;
    public RuntimeAnimatorController animator;
    public Vector2 colliderOffset;
    public Vector2 colliderSize;
    public int cost;
    public Sprite sprite;
}

//#if UNITY_EDITOR
//[CustomEditor(typeof(enemy))]
//public class customInspector : Editor
//{

//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();
//        enemy enemyData = target as enemy;
//        if (enemyData.sprite != null)
//        {
//            GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
//            Vector2 truColliderSize = new Vector2(enemyData.sprite.texture.width * enemyData.colliderSize.x, enemyData.sprite.texture.height* enemyData.colliderSize.y);
//            Vector2 truColliderOffset = new Vector2(enemyData.sprite.texture.width * enemyData.colliderOffset.x, enemyData.sprite.texture.height * enemyData.colliderOffset.y);

//            Rect SebRect = new Rect(5, 5, enemyData.sprite.texture.width, enemyData.sprite.texture.height);

//            Rect SebRect2 = new Rect(
//                ((SebRect.width / 2) - truColliderSize.x / 2) + truColliderOffset.x + SebRect.x,
//               ((SebRect.height / 2) - 1-(truColliderSize.y / 2) + truColliderOffset.y + SebRect.y),
//                truColliderSize.x,
//                truColliderSize.y);


//            GUIContent SebGUIContent = new GUIContent("", enemyData.sprite.texture, "enemy");
//            GUIContent SebGUIContent2 = new GUIContent("");
//            GUI.Box(SebRect, SebGUIContent);
//            GUI.Box(SebRect2, SebGUIContent2);
//        }
       
//    }

//}
//#endif 

