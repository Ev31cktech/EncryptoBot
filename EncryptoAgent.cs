using Bot.Utilities.Processed.BallPrediction;
using Bot.Utilities.Processed.FieldInfo;
using Bot.Utilities.Processed.Packet;
using RLBotDotNet;
using System;
using System.Drawing;
using System.Numerics;

namespace EncryptoBot
{
	public class EncryptoAgent : RLBotDotNet.Bot
	{
		/// Customization<summary>
		/// Body:	Breakout Type-S
		/// Livery:	Encryption
		/// Hat:	Top-Hat
		/// Wheels:	Stella inverted
		/// Trail:	Binary
		/// 
		/// </summary>
		int fieldLocInt = 0;
		int carHeigt = 0;
		int randomHeight = 0;
		//readonly LookAheadDist = 
		Packet packet;

		public EncryptoAgent(string botName, int botTeam, int botIndex) : base(botName, botTeam, botIndex)
		{
			EncryptoBotGui.AddBot(this);
		}
		public override Controller GetOutput(rlbot.flat.GameTickPacket gameTickPacket)
		{
			packet = new Packet(gameTickPacket, this);
			if (packet.GameInfo.IsMatchEnded)
			{
				return new Controller();
			}
			Vector3 tarLoc = new Vector3(Field.DiamondPath[fieldLocInt], randomHeight);
			Vector3 carLocation = packet.Players[Index].Location;

			float Distance = Vector3.Distance(carLocation, tarLoc);
			if (Distance < 200)
			{
				fieldLocInt = ++fieldLocInt % 4;
				randomHeight = new Random().Next(carHeigt, carHeigt + 1000);
			}
			//tarLoc = Vector3.Zero;
			return DriveTo(tarLoc);
		}
		public Controller DriveTo(Vector3 targetLoc)
		{
			const int CLOSE_TARGET = 1000;
			Player carObject = packet.Players[Index];
			if (carHeigt > carObject.Location.Z)
				carHeigt = (int)Math.Round(carObject.Location.Z);
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
				Controller.GroundCtrl.X = targetRelLoc.X - RelVelocity.X;// should take desel value in account.
				Controller.GroundCtrl.Y = targetRelLoc.Y - RelVelocity.Y;
			}
			else
			{
				Controller.GroundCtrl.Z = 1;
				Controller.AirCtrl = targetRelLoc - RelVelocity; //More Math involved. RelVelocity mirrored direction in air
			}
			float schuin = Vector3.Distance(carObject.Location,targetLoc);
			Vector3 flooredVect = targetLoc;
			flooredVect.Z = carObject.Location.Z;
			float aanlig = Vector3.Distance(carObject.Location, flooredVect);
			Renderer.DrawString2D(String.Format("{0}",Math.Cos(schuin / aanlig)), Color.White, new Vector2(20,20),1 ,1);
			Renderer.DrawString2D(String.Format("{0}",Math.Cos(schuin / aanlig) * 180 / Math.PI), Color.White, new Vector2(20,40),1 ,1);
			if ((Math.Cos(schuin / aanlig) * 180 / Math.PI) > 45 && carObject.HasWheelContact)
			{
				Controller.Jump = true;
			}
			Renderer.DrawLine3D(Color.Green, carObject.Location, carObject.Location + carObject.Velocity);
			Renderer.DrawLine3D(Color.Yellow, carObject.Location, targetLoc);
			return Controller.getController();
		}
		internal new FieldInfo GetFieldInfo() => new FieldInfo(base.GetFieldInfo());
		internal new BallPrediction GetBallPrediction() => new BallPrediction(base.GetBallPrediction());
	}
}
