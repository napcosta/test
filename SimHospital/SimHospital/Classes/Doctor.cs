using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SimHospital
{
	class Doctor
	{
		Matrix rotation = Matrix.Identity;
		Matrix world = Matrix.Identity;
		public Model model
		{
			get;
			protected set;
		}

		public Doctor(Model m)
		{
			model = m;
		}

		public virtual void Update()
		{
			rotation *= Matrix.CreateRotationY(MathHelper.Pi / 180);
		}

		public void Draw(Camera camera)
		{
			Matrix[] transofrms = new Matrix[model.Bones.Count];
			model.CopyAbsoluteBoneTransformsTo(transofrms);

			foreach (ModelMesh mesh in model.Meshes) {
				foreach (BasicEffect be in mesh.Effects) {
					be.EnableDefaultLighting();
					be.Projection = camera.projection;
					be.View = camera.view;
					be.World = GetWorld() * mesh.ParentBone.Transform;
				}
				mesh.Draw();
			}
		}

		public virtual Matrix GetWorld()
		{
			return world * rotation;
			;
		}

	}
}
