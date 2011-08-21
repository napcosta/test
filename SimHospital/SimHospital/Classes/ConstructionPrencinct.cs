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
	public class ConstructionPrencinct : Microsoft.Xna.Framework.DrawableGameComponent
	{
		// Vertex data
		VertexPositionTexture[] verts;
		VertexBuffer vertexBuffer;
		SpriteBatch spriteBatch;
		BasicEffect effect;
		Camera camera;
		Texture2D texture;
//		GraphicsDeviceManager graphics;

		public ConstructionPrencinct(Game1 game)
			: base(game)
		{
			camera = game.camera;

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
		/* A área de um campo de futebol: 90x120 */
		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			verts = new VertexPositionTexture[4];
			verts[0] = new VertexPositionTexture(
			    new Vector3(0, 120, 0), new Vector2(0, 0));
			verts[1] = new VertexPositionTexture(
			    new Vector3(100, 120, 0), new Vector2(1, 0));
			verts[2] = new VertexPositionTexture(
			    new Vector3(0, 0, 0), new Vector2(0, 1));
			verts[3] = new VertexPositionTexture(
			    new Vector3(100, 0, 0), new Vector2(1, 1));
			// Set vertex data in VertexBuffer
			vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionTexture), verts.Length, BufferUsage.None);
			vertexBuffer.SetData(verts);

			texture = this.Game.Content.Load<Texture2D>(@"Textures\THTexture");
			effect = new BasicEffect(GraphicsDevice);
		}

		public override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			GraphicsDevice.SetVertexBuffer(vertexBuffer);

			effect.View = camera.view;
			effect.Projection = camera.projection;
			effect.TextureEnabled = true;

			effect.Texture = texture;
			effect.TextureEnabled = true;

			foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
				pass.Apply();
				GraphicsDevice.DrawUserPrimitives<VertexPositionTexture> 
					(PrimitiveType.TriangleStrip, verts, 0, 2);
			}
			base.Update(gameTime);
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			// TODO: Add your update code here
			base.Update(gameTime);
		}
	}
}
