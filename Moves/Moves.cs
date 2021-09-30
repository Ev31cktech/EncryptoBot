using Bot.Utilities.Processed.Packet;
using RLBotDotNet;
using System;
using System.Numerics;

namespace EncryptoBot.Moves
{
	public abstract class IMoves
	{
		public String Name{ get; private set;} 
		public bool Done { get; set; }
		public float Priority {get; set; }
		public bool Available { get; set; }
		public void Initialize(string _name)
		{
			Name = _name;
			Done = true;
		}
		internal abstract void Run(Packet packet);
		internal abstract void Update(Packet packet);
		internal abstract Controller GetController(EncryptoAgent bot);
	}
	public class DriveTo : IMoves
	{
		CarController carCntrl;
		Packet packet;
		internal override void Run(Packet packet)
		{
			carCntrl = new CarController();
		}
		internal override void Update(Packet _packet)
		{
			Available = false;
			Done = true;
			Priority = 0;
		}
		internal override Controller GetController(EncryptoAgent bot)
		{
			return carCntrl.getController();
		}
	}
}
/*
const int CLOSE_TARGET = 1000;
Player carObject = packet.Players[Index];
float distance = Vector3.Distance(carObject.Location, targetLoc);
Vector3 RelVelocity = Orientation.RelativeLocation(carObject.Location, carObject.Location + carObject.Velocity, carObject.Rotation);
Vector3 targetRelLoc = Orientation.RelativeLocation(carObject.Location, targetLoc, carObject.Rotation);
CarController Controller = new CarController();
if (carObject.HasWheelContact)
{
	if (distance > CLOSE_TARGET)
	{
		targetRelLoc.X = Math.Abs(targetRelLoc.X);
	}
	Controller.GroundCtrl.X = targetRelLoc.X - RelVelocity.X / 4;// should take desel value in account.
	Controller.GroundCtrl.Y = targetRelLoc.Y - RelVelocity.Y / 4;
	//Controller.X = 1;
	Renderer.DrawLine3D(Color.Green, Vector3.Zero, targetRelLoc);
	Renderer.DrawLine3D(Color.Yellow, carObject.Location, targetLoc);
}
return Controller.getController();
*/