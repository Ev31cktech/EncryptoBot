using Bot.Utilities.Processed.Packet;
using RLBotDotNet;
using System;
using System.Drawing;
using System.Numerics;

namespace EncryptoBot.Moves
{
	public abstract class IMoves
	{
		public String Name { get; private set; }
		public bool Done { get; set; }
		public int Index {get; private set;}
		public float Priority { get; set; }
		public bool Available { get; set; }
		public void Initialize(string _name, int _index)
		{
			Name = _name;
			Index = _index;
			Done = true;
		}
		internal abstract void Run(Packet packet, EncryptoAgent bot);
		internal abstract void Update(Packet packet, EncryptoAgent bot);
		internal abstract Controller GetController(EncryptoAgent bot);
	}
	public class StandStil : IMoves
	{

		internal override void Run(Packet packet, EncryptoAgent bot)
		{ }

		internal override void Update(Packet packet, EncryptoAgent bot)
		{
			Done = true;
			Available = true;
		}
		internal override Controller GetController(EncryptoAgent bots)
		{
			return new CarController().getController();
		}
	}
	public class DriveTo : IMoves
	{
		private const int CLOSE_DISTANCE = 1000;
		CarController carController;
		Packet packet;
		internal override void Run(Packet packet, EncryptoAgent bot)
		{
			carController = new CarController();
		}
		internal override void Update(Packet _packet, EncryptoAgent bot)
		{
			packet = _packet;
			Available = true;
			Done = false;
			Priority = 100;
		}
		internal override Controller GetController(EncryptoAgent bot)
		{
			Player carObject = packet.Players[bot.Index];
			Vector3 targetRelLocation = Orientation.RelativeLocation(carObject.Location, bot.targetLoc, carObject.Rotation);
			Vector3 RelVelocity = Orientation.RelativeLocation(carObject.Location, carObject.Location + carObject.Velocity, carObject.Rotation);
			targetRelLocation.X += targetRelLocation.X > CLOSE_DISTANCE ? 0 : Math.Abs(targetRelLocation.X) * 2;
			bot.Renderer.DrawLine3D(Color.Red, carObject.Location, carObject.Location + carObject.Velocity);
			carController.GroundCtrl = targetRelLocation - RelVelocity / 4;
			carController.GroundCtrl.Z = 0;
			return carController.getController();
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