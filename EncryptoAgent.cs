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
		//readonly LookAheadDist = 
		Packet packet;

		public EncryptoAgent(string botName, int botTeam, int botIndex) : base(botName, botTeam, botIndex)
		{
			EncryptoBotGui.AddBot(this);
		}
		public override Controller GetOutput(rlbot.flat.GameTickPacket gameTickPacket)
		{
			packet = new Packet(gameTickPacket);
			if (packet.GameInfo.IsMatchEnded)
			{
				return new Controller();
			}
			Vector3 tarLoc = new Vector3(Field.DiamondPath[fieldLocInt], carHeigt);
			Vector3 carLocation = packet.Players[Index].Location;

			float Distance = Vector3.Distance(carLocation, tarLoc);
			if (Distance < 200)
				fieldLocInt = ++fieldLocInt % 4;
				;
			//tarLoc = Vector3.Zero;
			return DriveTo(tarLoc);
		}
		public Controller DriveTo(Vector3 targetLoc) {
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
		}
		internal new FieldInfo GetFieldInfo() => new FieldInfo(base.GetFieldInfo());
		internal new BallPrediction GetBallPrediction() => new BallPrediction(base.GetBallPrediction());
	}
}
