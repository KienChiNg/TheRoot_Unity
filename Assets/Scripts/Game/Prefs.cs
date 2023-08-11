using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs
{
    public static float playerSpeed
    {
        set => PlayerPrefs.SetFloat("playerSpeed", value);
        get => PlayerPrefs.GetFloat("playerSpeed", 0.0f);
    }
    public static int amountRoot
    {
        set => PlayerPrefs.SetInt("amountRoot", value);

        get => PlayerPrefs.GetInt("amountRoot", 0);
    }
    public static float damageForce
    {
        set => PlayerPrefs.SetFloat("damageForce", value);

        get => PlayerPrefs.GetFloat("damageForce", 0.0f);
    }
    public static int maxCapacity
    {
        set => PlayerPrefs.SetInt("maxCapacity", value);

        get => PlayerPrefs.GetInt("maxCapacity", 0);
    }
    public static int curCapacity
    {
        set => PlayerPrefs.SetInt("curCapacity", value);

        get => PlayerPrefs.GetInt("curCapacity", 0);
    }
    public static int health
    {
        set => PlayerPrefs.SetInt("health", value);
        get => PlayerPrefs.GetInt("health", 0);
    }
    public static int heathLost
    {
        set => PlayerPrefs.SetInt("heathLost", value);
        get => PlayerPrefs.GetInt("heathLost", 0);
    }
    public static int radiusCircle
    {
        set => PlayerPrefs.SetInt("radiusCircle", value);
        get => PlayerPrefs.GetInt("radiusCircle", 1);
    }
    public static int exp
    {
        set => PlayerPrefs.SetInt("exp", value);
        get => PlayerPrefs.GetInt("exp", 0);
    }
    public static int level
    {
        set => PlayerPrefs.SetInt("level", value);
        get => PlayerPrefs.GetInt("level", 0);
    }
    // public static void SetScaleRadius(Vector3 scale){
    //     PlayerPrefs.SetFloat("scaleX", scale.x);
    //     PlayerPrefs.SetFloat("scaleY", scale.y);
    //     PlayerPrefs.SetFloat("scaleZ", scale.z);
    // }
    // public static Vector3 GetScaleRadius(){
    //     Vector3 scale;
    //     scale.x = PlayerPrefs.GetFloat("scaleX", 0);
    //     scale.y = PlayerPrefs.GetFloat("scaleY", 0);
    //     scale.z = PlayerPrefs.GetFloat("scaleZ", 0);
    //     return scale;
    // }
    public static void SetPosition(Vector3 pos){
        PlayerPrefs.SetFloat("posX", pos.x);
        PlayerPrefs.SetFloat("posY", pos.y);
        PlayerPrefs.SetFloat("posZ", pos.z);
    }
    public static Vector3 GetPosition(){
        Vector3 pos;
        pos.x = PlayerPrefs.GetFloat("posX", 0);
        pos.y = PlayerPrefs.GetFloat("posY", -1.12f);
        pos.z = PlayerPrefs.GetFloat("posZ", 0);
        return pos;
    }

    public static int coin
    {
        set => PlayerPrefs.SetInt("coin_TheRoot", value);
        get => PlayerPrefs.GetInt("coin_TheRoot", 0);
    }

    
}