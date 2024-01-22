using UnityEngine;
public interface IBuffable
{
    public void onChangeAttackPower(float senderAttackPower, bool isBuff);
    public void onChangeAttackSpeed(float senderAttackSpeed, bool isBuff);
    public void onChangeHealth(float senderHealthvalue, bool isBuff);
    public void onChangeMovementSpeed(float senderMovementvalue, bool isBuff);
}
