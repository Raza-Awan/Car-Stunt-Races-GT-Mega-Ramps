using UnityEngine;
[System.Serializable]

/* These stats are upgrading vehicle acceleration (speed pick duration to reach it max speed), increases vehicle suspension value 
 * from the vehicle's wheels controller, decreases the handling fill amount as with increase in acceleration and suspension will
 * makes vehicle a little bit difficult to handle & the acceleration fill amount value
 
 Acceleration will be increased by reducing a little bit value of inertia;
In PWR => Engine Settings: 
Under Common Prop. : Inertia => If this value is less(i.e; 0.2 : closer to 0 but should not be 0) means more acceleration and vice versa

And suspension will be increased as;
Under Spring:
Max Length => Greater this value mean more suspension. (recommended value as per NWH doucumentation is 0.2) but can go upto 0.333
 */

public class SuspensionUpgStats 
{
    public float[] price = new float[3];
}
