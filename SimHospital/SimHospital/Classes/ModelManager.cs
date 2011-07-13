using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace SimHospital
{
	/// <summary>
	/// This is a game component that implements IUpdateable.
	/// </summary>
	public class ModelManager : DrawableGameComponent //Microsoft.Xna.Framework.GameComponent
	{
		List<Doctor> models = new List<Doctor>();

		public ModelManager(Game game)
			: base(game)
		{
			// TODO: Construct any child components here
		}

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run.  This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
			// TODO: Add your initialization code here

			base.Initialize();
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			for (int i = 0; i < models.Count; ++i) {
				models[i].Update();
			}
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			foreach (Doctor d in models) {
				d.Draw(((Game1)Game).camera);
			}
			base.Draw(gameTime);
		}

		protected override void LoadContent()
		{
			models.Add(new Doctor(
			    Game.Content.Load<Model>(@"models\spaceship")));
			base.LoadContent();
		}
	}
}
