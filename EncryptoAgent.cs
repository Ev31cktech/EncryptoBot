using Bot.Utilities.Processed.BallPrediction;
using Bot.Utilities.Processed.FieldInfo;
using Bot.Utilities.Processed.Packet;
using RLBotDotNet;
using System;
using System.Drawing;
using System.Numerics;

namespace EncryptoBot
{
	class EncryptoAgent : RLBotDotNet.Bot
	{
		/// Customization<summary>
		/// Body:	Breakout Type-S
		/// Livery:	Encryption
		/// Hat:	Top-Hat
		/// Wheels:	Stella inverted
		/// Trail:	Binary
		/// 
		/// </summary>
		DateTime time;
		int fieldLocInt = 0;
		float carHeigt = 0;
		Packet packet;

		public EncryptoAgent(string botName, int botTeam, int botIndex) : base(botName, botTeam, botIndex)
		{
			EncryptoBotGui.AddBot(this);
		}
		public override Controller GetOutput(rlbot.flat.GameTickPacket gameTickPacket)
		{
			packet = new Packet(gameTickPacket);

			Vector3 tarLoc = new Vector3(Field.DiamondPath[fieldLocInt], carHeigt);
			Vector3 carLocation = packet.Players[Index].Physics.Location;

			float Distance = Vector3.Distance(carLocation, tarLoc);
			if (Distance < 200)
				fieldLocInt = ++fieldLocInt % 4;
			//tarLoc = Vector3.Zero;
			return 
				DriveTo(tarLoc);
			//return new Controller() { Steer = 1, Throttle = 1 };
		}
		public Controller DriveTo(Vector3 targetLoc) { return DriveTo(targetLoc, new Controller()); }
		public Controller DriveTo(Vector3 targetLoc, Controller ctrl)
		{
			const int CLOSE_TARGET = 1000;
			Physics carObject = packet.Players[Index].Physics;
			float distance = Vector3.Distance(carObject.Location, targetLoc);
			Vector3 RelVelocity = carObject.Location + (carObject.Velocity * new Vector3(-1, 1, -1));
			Vector3 targetRelLoc = Orientation.RelativeLocation(carObject.Location, targetLoc, carObject.Rotation);
			if (distance > CLOSE_TARGET)
			{
				targetRelLoc.X = Math.Abs(targetRelLoc.X);
			}
			return ctrl;
		}
		internal new FieldInfo GetFieldInfo() => new FieldInfo(base.GetFieldInfo());
		internal new BallPrediction GetBallPrediction() => new BallPrediction(base.GetBallPrediction());
	}
}
