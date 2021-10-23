using Bot.Utilities.Processed.Packet;
using RLBotDotNet;
using System;
using System.Drawing;
using System.Numerics;

namespace EncryptoBot.Moves
{
	public abstract class IMoves
	{
		public CarController carController;
		public String Name { get; private set; }
		public bool Done { get; set; }
		public int Index {get; private set;}
		public float Priority { get; set; } // should move to State
		public bool Available { get; set; }
		public void Initialize(string _name, int _index)
		{
			Name = _name;
			Index = _index;
			Done = true;
			carController = new CarController();
		}
		internal abstract void Run(EncryptoAgent bot);
		internal abstract void Update(EncryptoAgent bot);
		internal abstract Controller GetController(EncryptoAgent bot);
	}
	public class StandStil : IMoves
	{

		internal override void Run(EncryptoAgent bot)
		{
			Done = false;
		}

		internal override void Update( EncryptoAgent bot)
		{
			Done = true;
			Available = true;
		}
		internal override Controller GetController(EncryptoAgent bots)
		{
			return new CarController().GetController();
		}
	}
	public class DriveTo : IMoves
	{
		private const int CLOSE_DISTANCE = 1000;
		internal override void Run(EncryptoAgent bot)
		{
			Done = false;
		}
		internal override void Update(EncryptoAgent bot)
		{
			Available = true;
			Priority = 50;
		}
		internal override Controller GetController(EncryptoAgent bot)
		{
			Player carObject = bot.carObject;
			Vector3 targetRelLocation = Orientation.RelativeLocation(carObject.Location, bot.targetLoc, carObject.Rotation);
			Vector3 RelVelocity = Orientation.RelativeLocation(carObject.Location, carObject.Location + carObject.Velocity, carObject.Rotation);
			targetRelLocation.X += targetRelLocation.X > CLOSE_DISTANCE ? 0 : Math.Abs(targetRelLocation.X) * 2;
			bot.Renderer.DrawLine3D(Color.Red, carObject.Location, carObject.Location + carObject.Velocity);
			carController.GroundCtrl = targetRelLocation - RelVelocity / 4;
			carController.GroundCtrl.Z = targetRelLocation.X < CLOSE_DISTANCE ? 0 : 1;
			return carController.GetController();
		}
	}
	public class getBoost : IMoves
	{
		internal override void Run(EncryptoAgent bot)
		{
			Done = false;
		}

		internal override void Update(EncryptoAgent bot)
		{
		}
		internal override Controller GetController(EncryptoAgent bot)
		{
			carController.GroundCtrl.Z = 1;
			return carController.GetController();
		}
	}
	/*
	public class Jump : IMoves
	{
		internal override void Run(EncryptoAgent bot)
		{
			Done = false;
		}

		internal override void Update(EncryptoAgent bot)
		{
		}
		internal override Controller GetController(EncryptoAgent bot)
		{
			carController.GroundCtrl.Z = 1;
			return carController.GetController();
		}
	}
	public class FlyTo : IMoves
	{

		internal override void Run(EncryptoAgent bot)
		{
		}
		internal override void Update(EncryptoAgent bot)
		{
			Available = bot.BoostAmount > 0;
			Priority = 0;
		}
		internal override Controller GetController(EncryptoAgent bot)
		{
			return carController.GetController();
		}
	}
	*/
}