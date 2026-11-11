using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using b;

namespace SynapseGaming.LightingSystem.Effects;

public class EffectBatchHelper
{
	private Dictionary<string, Effect> l8a = new Dictionary<string, Effect>(16);

	public Effect CollapseEffect(Effect effect, bool disposeunused)
	{
		if (effect == null)
		{
			return effect;
		}
		string text = "";
		if (effect is b.a)
		{
			text = (effect as b.a).MaterialFile;
		}
		if (string.IsNullOrEmpty(text))
		{
			return effect;
		}
		if (l8a.ContainsKey(text))
		{
			Effect effect2 = l8a[text];
			if (disposeunused && effect != effect2 && !effect.IsDisposed)
			{
				effect.Dispose();
			}
			return effect2;
		}
		l8a.Add(text, effect);
		return effect;
	}

	public void CollapseEffects(Model model, bool disposeunused)
	{
		for (int i = 0; i < model.Meshes.Count; i++)
		{
			ModelMesh modelMesh = model.Meshes[i];
			for (int j = 0; j < modelMesh.MeshParts.Count; j++)
			{
				ModelMeshPart modelMeshPart = modelMesh.MeshParts[j];
				modelMeshPart.Effect = CollapseEffect(modelMeshPart.Effect, disposeunused);
			}
		}
	}

	public void Clear()
	{
		l8a.Clear();
	}
}
