using System.Numerics;

namespace Bot.Utilities.Processed.FieldInfo
{
    public struct BoostPad
    {
        public Vector3 Location;
        public bool IsFullBoost;
        
        public float Timer;
        public bool IsActive;

        public BoostPad(rlbot.flat.BoostPad boostPad)
        {
            Location = boostPad.Location.Value.ToVector3();
            IsFullBoost = boostPad.IsFullBoost;
            Timer = 0;
            IsActive = false;
        }
        public void BoostPad_Update(rlbot.flat.BoostPadState boostPadState)
		{
            Timer = boostPadState.Timer;
            IsActive = boostPadState.IsActive;
		}
    }
}