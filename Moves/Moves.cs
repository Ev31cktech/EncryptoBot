using Bot.Utilities.Processed.Packet;
using RLBotDotNet;
using System;
using System.Drawing;
using System.Numerics;

namespace EncryptoBot.Moves
{
	public abstract class IMove
	{
		public CarController carController;
		public String Name { get; private set; }
		public bool Done { get; set; }
		public int Index {get; private set;} //what use?
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
	public class StandStil : IMove
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
	public class DriveTo : IMove
	{
		private const int CLOSE_DISTANCE = 1000;
		internal override void Run(EncryptoAgent bot)
		{
			Done = false;
		}
		internal override void Update(EncryptoAgent bot)
		{
			Available = true;
			float dist = Vector3.Distance(bot.targetLoc, bot.Location); 
			Done = dist < 100;
			bot.Renderer.DrawString2D(dist.ToString(), Color.Black, new Vector2(0,0),1,1);
		}
		internal override Controller GetController(EncryptoAgent bot)
		{
			Player carObject = bot.carObject;
			bot.targetLoc.Z = 0;
			Vector3 targetRelLocation = Orientation.RelativeLocation(bot.Location, bot.targetLoc , carObject.Rotation);
			carController.Steer = Vector3.Normalize(targetRelLocation).Y * 5;
			carController.Throttle = targetRelLocation.X < CLOSE_DISTANCE ? targetRelLocation.X : Math.Abs(targetRelLocation.X);
			carController.GroundCtrl.Z = 0;
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