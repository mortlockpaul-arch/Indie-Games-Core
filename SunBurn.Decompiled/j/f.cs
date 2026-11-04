using System.Collections.Generic;

namespace j;

internal class f
{
	internal enum nNB
	{
		DeferredDepth,
		DeferredGBuffer,
		DeferredFinal,
		DeferredFinalFog,
		Lighting,
		Shadow,
		ShadowGen,
		Fog,
		Billboard
	}

	internal enum nNa
	{
		None,
		Diffuse,
		DiffuseBump,
		DiffuseBumpSpecular,
		DiffuseBumpSpecularColor,
		DiffuseBumpFresnel,
		DiffuseBumpFresnelColor,
		DiffuseParallax,
		DiffuseParallaxSpecular,
		DiffuseParallaxSpecularColor,
		DiffuseParallaxFresnel,
		DiffuseParallaxFresnelColor,
		DiffuseAmbient,
		DiffuseBumpAmbient,
		DiffuseParallaxAmbient,
		DiffuseAmbientEmissive,
		DiffuseBumpAmbientEmissive,
		DiffuseParallaxAmbientEmissive,
		DiffuseParallaxSpecularColorEmissive,
		DiffuseParallaxEmissive,
		DiffuseBumpSpecularColorEmissive,
		DiffuseBumpEmissive,
		Linear,
		Point,
		Directional,
		Point3,
		Directional3,
		Point4,
		Directional4,
		Count
	}

	private static Dictionary<int, string> fNB = new Dictionary<int, string>(32);

	private static int f_0016(nNB P_0, nNa P_1, int P_2, bool P_3, bool P_4, bool P_5, bool P_6)
	{
		int num = (int)(P_0 + ((int)P_1 << 8));
		num += P_2 << 16;
		if (P_5)
		{
			num += 16777216;
		}
		if (P_3)
		{
			num += 33554432;
		}
		if (P_4)
		{
			num += 67108864;
		}
		if (P_6)
		{
			num += 134217728;
		}
		return num;
	}

	internal static void fx()
	{
		Dictionary<int, char> dictionary = new Dictionary<int, char>(16);
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 29; j++)
			{
				for (int k = 0; k < 3; k++)
				{
					dictionary.Add(f_0016((nNB)i, (nNa)j, k, false, false, false, false), '0');
					dictionary.Add(f_0016((nNB)i, (nNa)j, k, true, false, false, false), '0');
					dictionary.Add(f_0016((nNB)i, (nNa)j, k, false, true, false, false), '0');
					dictionary.Add(f_0016((nNB)i, (nNa)j, k, true, true, false, false), '0');
					dictionary.Add(f_0016((nNB)i, (nNa)j, k, false, false, true, false), '0');
					dictionary.Add(f_0016((nNB)i, (nNa)j, k, true, false, true, false), '0');
					dictionary.Add(f_0016((nNB)i, (nNa)j, k, false, true, true, false), '0');
					dictionary.Add(f_0016((nNB)i, (nNa)j, k, true, true, true, false), '0');
					dictionary.Add(f_0016((nNB)i, (nNa)j, k, false, false, false, true), '0');
					dictionary.Add(f_0016((nNB)i, (nNa)j, k, true, false, false, true), '0');
					dictionary.Add(f_0016((nNB)i, (nNa)j, k, false, true, false, true), '0');
					dictionary.Add(f_0016((nNB)i, (nNa)j, k, true, true, false, true), '0');
					dictionary.Add(f_0016((nNB)i, (nNa)j, k, false, false, true, true), '0');
					dictionary.Add(f_0016((nNB)i, (nNa)j, k, true, false, true, true), '0');
					dictionary.Add(f_0016((nNB)i, (nNa)j, k, false, true, true, true), '0');
					dictionary.Add(f_0016((nNB)i, (nNa)j, k, true, true, true, true), '0');
				}
			}
		}
	}

	internal static string fF(nNB P_0, nNa P_1, int P_2, bool P_3, bool P_4, bool P_5, bool P_6)
	{
		int key = f_0016(P_0, P_1, P_2, P_3, P_4, P_5, P_6);
		if (fNB.ContainsKey(key))
		{
			return fNB[key];
		}
		string text = "";
		switch (P_0)
		{
		case nNB.DeferredDepth:
			text = "DeferredDepth_";
			break;
		case nNB.DeferredGBuffer:
			text = "DeferredGBuffer_";
			break;
		case nNB.DeferredFinal:
			text = "DeferredFinal_";
			break;
		case nNB.DeferredFinalFog:
			text = "DeferredFinalFog_";
			break;
		case nNB.Lighting:
			text = "Lighting_";
			break;
		case nNB.Shadow:
			text = "Shadow_";
			break;
		case nNB.ShadowGen:
			text = "ShadowGen_";
			break;
		case nNB.Fog:
			text = "Fog_";
			break;
		case nNB.Billboard:
			text = "Billboard_";
			break;
		}
		switch (P_1)
		{
		case nNa.Diffuse:
			text += "D_";
			break;
		case nNa.DiffuseBump:
			text += "DB_";
			break;
		case nNa.DiffuseBumpSpecular:
			text += "DBS_";
			break;
		case nNa.DiffuseBumpSpecularColor:
			text += "DBSC_";
			break;
		case nNa.DiffuseBumpFresnel:
			text += "DBF_";
			break;
		case nNa.DiffuseBumpFresnelColor:
			text += "DBFC_";
			break;
		case nNa.DiffuseParallax:
			text += "DP_";
			break;
		case nNa.DiffuseParallaxSpecular:
			text += "DPS_";
			break;
		case nNa.DiffuseParallaxSpecularColor:
			text += "DPSC_";
			break;
		case nNa.DiffuseParallaxFresnel:
			text += "DPF_";
			break;
		case nNa.DiffuseParallaxFresnelColor:
			text += "DPFC_";
			break;
		case nNa.DiffuseAmbient:
			text += "DA_";
			break;
		case nNa.DiffuseBumpAmbient:
			text += "DBA_";
			break;
		case nNa.DiffuseParallaxAmbient:
			text += "DPA_";
			break;
		case nNa.DiffuseAmbientEmissive:
			text += "DAG_";
			break;
		case nNa.DiffuseBumpAmbientEmissive:
			text += "DBAG_";
			break;
		case nNa.DiffuseParallaxAmbientEmissive:
			text += "DPAG_";
			break;
		case nNa.DiffuseParallaxSpecularColorEmissive:
			text += "DPSCE_";
			break;
		case nNa.DiffuseParallaxEmissive:
			text += "DPE_";
			break;
		case nNa.DiffuseBumpSpecularColorEmissive:
			text += "DBSCE_";
			break;
		case nNa.DiffuseBumpEmissive:
			text += "DBE_";
			break;
		case nNa.Linear:
			text += "Linear_";
			break;
		case nNa.Point:
			text += "Point_";
			break;
		case nNa.Point3:
			text += "Point3_";
			break;
		case nNa.Point4:
			text += "Point4_";
			break;
		case nNa.Directional:
			text += "Directional_";
			break;
		case nNa.Directional3:
			text += "Directional3_";
			break;
		case nNa.Directional4:
			text += "Directional4_";
			break;
		}
		if (P_0 == nNB.Lighting)
		{
			text = text + "L" + P_2 + "_";
		}
		if (P_3)
		{
			text += "Double_";
		}
		if (P_4)
		{
			text += "Transparent_";
		}
		if (P_5)
		{
			text += "Skinned_";
		}
		if (P_6)
		{
			text += "Terrain_";
		}
		text += "Technique";
		fNB.Add(key, text);
		return text;
	}
}
