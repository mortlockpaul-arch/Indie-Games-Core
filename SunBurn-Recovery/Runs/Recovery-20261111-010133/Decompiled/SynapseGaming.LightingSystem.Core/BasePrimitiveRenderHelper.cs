using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SynapseGaming.LightingSystem.Core;

public abstract class BasePrimitiveRenderHelper
{
	private List<VertexPositionColor> l8a = new List<VertexPositionColor>();

	private static VertexPositionColor[] l8_u0013_ = new VertexPositionColor[2040];

	protected abstract PrimitiveType PrimitiveType { get; }

	public BasePrimitiveRenderHelper()
	{
	}

	protected void SubmitVertex(ref Vector3 position, ref Color color)
	{
		l8a.Add(new VertexPositionColor(position, color));
	}

	public virtual void Clear()
	{
		l8a.Clear();
	}

	public void Render(Effect effect)
	{
		if (l8a.Count < 2)
		{
			return;
		}
		int num = 0;
		switch (PrimitiveType)
		{
		case PrimitiveType.LineList:
			num = 2;
			break;
		case PrimitiveType.TriangleList:
			num = 3;
			break;
		}
		if (num >= 1)
		{
			GraphicsDevice graphicsDevice = SunBurnCoreSystem.Instance.GraphicsDeviceManager.GraphicsDevice;
			for (int i = 0; i < effect.CurrentTechnique.Passes.Count; i++)
			{
				effect.CurrentTechnique.Passes[i].Apply();
				_u0013_6(graphicsDevice, num);
			}
		}
	}

	private void _u0013_6(GraphicsDevice param_0, int param_1)
	{
		int num = l8_u0013_.Length / param_1;
		int num2 = num * param_1;
		int num3 = 0;
		int count = l8a.Count;
		for (int i = 0; i < count; i++)
		{
			ref VertexPositionColor reference = ref l8_u0013_[num3];
			reference = l8a[i];
			num3++;
			if (num3 >= num2)
			{
				param_0.DrawUserPrimitives(PrimitiveType, l8_u0013_, 0, num3 / param_1);
				num3 = 0;
			}
		}
		if (num3 > 0)
		{
			param_0.DrawUserPrimitives(PrimitiveType, l8_u0013_, 0, num3 / param_1);
		}
	}
}
