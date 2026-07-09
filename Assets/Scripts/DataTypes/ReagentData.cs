using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Reagent", menuName = "MyData/Reagent")]
[Serializable]

public class ReagentData : ItemData
{
    public float adictivness;
    public float energizing;
    public float focused;
    public float athletics;
    public float calming;
    public float brightEyed;
    public float disorienting;
    public float foggy;
    public float glowing;
    public float longFaced;
    public float sedating;
    public float seizure;
    public float slippery;
    public float sneaky;
    public float extraMultiplierEffect;

}

public struct Reagent
{
    public Color color;
    public float multiplier;
}

public class Effects
{
    public static Reagent adictivness = new() {color=Color.red,multiplier=1.54f};
    public static Reagent energizing =  new() {color=new Color(0,0.9f,0.1f,1),multiplier=1.22f};
    public static Reagent focused = new() {color=new Color(0,0.45f,0.5f,1),multiplier=1.16f};
    public static Reagent athletics = new() {color=new Color(0,0.6f,0.8f,1),multiplier=1.32f};
    public static Reagent calming = new() {color=new Color(0.4f,0.3f,0.1f,1),multiplier=1.1f};
    public static Reagent brightEyed = new() {color=new Color(0.6f,0.9f,0.9f,1),multiplier=1.14f};
    public static Reagent disorienting = new() {color=new Color(0.8f,0.4f,0.3f,1),multiplier=1};
    public static Reagent foggy = new() {color=new Color(0.4f,0.4f,0.4f,1),multiplier=1.22f};
    public static Reagent glowing = new() {color=new Color(0.45f,0.75f,0.25f,1),multiplier=1.48f};
    public static Reagent longFaced = new() {color=new Color(0.95f,0.8f,0.15f,1),multiplier=1.52f};
    public static Reagent sedating = new() {color=new Color(0.7f,0.3f,0.85f,1),multiplier=1.26f};
    public static Reagent seizure = new() {color=new Color(0.9f,0.9f,0.1f,1),multiplier=1};
    public static Reagent slippery = new() {color=new Color(0.5f,0.8f,0.8f,1),multiplier=1.34f};
    public static Reagent sneaky = new() {color=new Color(0.65f,0.65f,0.65f,1),multiplier=1.24f};
}