using rlbot.flat;
using RLBotDotNet;
using System;

namespace EncryptoBot.Moves
{
	public abstract class IMoves
	{
		public String Name{ get; private set;} 
		public bool Done { get; set; }
		public float priority {get;private set; }
		public bool Availale { get; set; }
		public void Initialize(string _name)
		{
			Name = _name;
		}
		public abstract Controller Update(rlbot.flat.GameTickPacket gameTickPacket);
	}
	public class DriveTo : IMoves
	{
		public override Controller Update(GameTickPacket gameTickPacket)
		{
			return new CarController().getController();
		}
	}
}