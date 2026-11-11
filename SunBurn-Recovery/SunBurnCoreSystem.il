// IL code: SynapseGaming-SunBurn-Pro
.class public auto ansi beforefieldinit SynapseGaming.LightingSystem.Core.SunBurnCoreSystem
	extends [mscorlib]System.Object
{
	// Nested Types
	.class nested private auto ansi beforefieldinit w8a
		extends [mscorlib]System.Object
	{
		// Fields
		.field private class [mscorlib]System.IServiceProvider l8a
		.field private class [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ResourceContentManager 'l8\u0013'

		// Methods
		.method public hidebysig specialname 
			instance class [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ResourceContentManager get_EmbeddedResourceManager () cil managed 
		{
			// Method begins at RVA 0xe7d4
			// Header size: 1
			// Code size: 37 (0x25)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldfld class [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ResourceContentManager SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a::'l8\u0013'
			IL_0006: brtrue.s IL_001e

			IL_0008: ldarg.0
			IL_0009: ldarg.0
			IL_000a: ldfld class [mscorlib]System.IServiceProvider SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a::l8a
			IL_000f: call class [mscorlib]System.Resources.ResourceManager SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'\u0013\u0015'()
			IL_0014: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ResourceContentManager::.ctor(class [mscorlib]System.IServiceProvider, class [mscorlib]System.Resources.ResourceManager)
			IL_0019: stfld class [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ResourceContentManager SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a::'l8\u0013'

			IL_001e: ldarg.0
			IL_001f: ldfld class [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ResourceContentManager SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a::'l8\u0013'
			IL_0024: ret
		} // end of method w8a::get_EmbeddedResourceManager

		.method public hidebysig specialname rtspecialname 
			instance void .ctor (
				class [mscorlib]System.IServiceProvider serviceprovider
			) cil managed 
		{
			// Method begins at RVA 0xe7fc
			// Header size: 1
			// Code size: 14 (0xe)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: call instance void [mscorlib]System.Object::.ctor()
			IL_0006: ldarg.0
			IL_0007: ldarg.1
			IL_0008: stfld class [mscorlib]System.IServiceProvider SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a::l8a
			IL_000d: ret
		} // end of method w8a::.ctor

		.method public hidebysig 
			instance void Unload () cil managed 
		{
			// Method begins at RVA 0xe80c
			// Header size: 1
			// Code size: 27 (0x1b)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldfld class [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ResourceContentManager SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a::'l8\u0013'
			IL_0006: brfalse.s IL_001a

			IL_0008: ldarg.0
			IL_0009: ldfld class [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ResourceContentManager SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a::'l8\u0013'
			IL_000e: callvirt instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ContentManager::Dispose()
			IL_0013: ldarg.0
			IL_0014: ldnull
			IL_0015: stfld class [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ResourceContentManager SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a::'l8\u0013'

			IL_001a: ret
		} // end of method w8a::Unload

		// Properties
		.property instance class [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ResourceContentManager EmbeddedResourceManager()
		{
			.get instance class [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ResourceContentManager SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a::get_EmbeddedResourceManager()
		}

	} // end of class w8a


	// Fields
	.field private static int32 l8a
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.field private static class SynapseGaming.LightingSystem.Core.SunBurnCoreSystem 'l8\u0013'
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.field private class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D l8N
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.field private class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D l8l
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.field private bool l8L
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.field private class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube l8w
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.field private class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube l8J
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.field private class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D l8B
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.field private class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteFont l8s
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.field private class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteBatch l8h
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.field private class SynapseGaming.LightingSystem.Core.GraphicsDeviceSupport 'l8\u0002'
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.field private class [mscorlib]System.IServiceProvider l8v
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.field private class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService l8Y
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.field private class SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a 'l8\u0005'
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.field private class SynapseGaming.LightingSystem.Core.ResourceManager l8b
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.field private class B.s l8I
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)

	// Methods
	.method public hidebysig specialname static 
		class SynapseGaming.LightingSystem.Core.SunBurnCoreSystem get_Instance () cil managed 
	{
		.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0xdbf8
		// Header size: 1
		// Code size: 29 (0x1d)
		.maxstack 8

		IL_0000: ldsfld class SynapseGaming.LightingSystem.Core.SunBurnCoreSystem SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'l8\u0013'
		IL_0005: brtrue.s IL_0012

		IL_0007: ldstr "SunBurnCoreSystem unavailable, please create an instance of the manager before using this object."
		IL_000c: newobj instance void [mscorlib]System.ArgumentException::.ctor(string)
		IL_0011: throw

		IL_0012: call void a.a::N()
		IL_0017: ldsfld class SynapseGaming.LightingSystem.Core.SunBurnCoreSystem SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'l8\u0013'
		IL_001c: ret
	} // end of method SunBurnCoreSystem::get_Instance

	.method public hidebysig specialname static 
		string get_Edition () cil managed 
	{
		.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0xdc18
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldstr "Pro"
		IL_0005: ret
	} // end of method SunBurnCoreSystem::get_Edition

	.method public hidebysig specialname static 
		string get_PublicKeyToken () cil managed 
	{
		.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0xdc24
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldstr "c23c60523565dbfd"
		IL_0005: ret
	} // end of method SunBurnCoreSystem::get_PublicKeyToken

	.method public hidebysig specialname static 
		string get_Version () cil managed 
	{
		// Method begins at RVA 0xdc2c
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldstr "2.0.18.7"
		IL_0005: ret
	} // end of method SunBurnCoreSystem::get_Version

	.method private hidebysig specialname static 
		class [mscorlib]System.Resources.ResourceManager '\u0013\u0015' () cil managed 
	{
		.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0xdc34
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: call class [mscorlib]System.Resources.ResourceManager b.s::'\u0013\u0015'()
		IL_0005: ret
	} // end of method SunBurnCoreSystem::'\u0013\u0015'

	.method assembly hidebysig specialname static 
		int32 '\u0013r' () cil managed 
	{
		// Method begins at RVA 0xdc40
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldsfld int32 SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8a
		IL_0005: ret
	} // end of method SunBurnCoreSystem::'\u0013r'

	.method assembly hidebysig specialname static 
		void '\u0013K' (
			int32 ''
		) cil managed 
	{
		// Method begins at RVA 0xdc4c
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: stsfld int32 SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8a
		IL_0006: ret
	} // end of method SunBurnCoreSystem::'\u0013K'

	.method public hidebysig specialname 
		instance bool get_DetectOverSizedFrameBuffers () cil managed 
	{
		.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0xdc58
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld bool SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8L
		IL_0006: ret
	} // end of method SunBurnCoreSystem::get_DetectOverSizedFrameBuffers

	.method public hidebysig specialname 
		instance void set_DetectOverSizedFrameBuffers (
			bool 'value'
		) cil managed 
	{
		.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0xdc64
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: stfld bool SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8L
		IL_0007: ret
	} // end of method SunBurnCoreSystem::set_DetectOverSizedFrameBuffers

	.method public hidebysig specialname 
		instance class SynapseGaming.LightingSystem.Core.ResourceManager get_DefaultResourceManager () cil managed 
	{
		// Method begins at RVA 0xdc70
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld class SynapseGaming.LightingSystem.Core.ResourceManager SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8b
		IL_0006: ret
	} // end of method SunBurnCoreSystem::get_DefaultResourceManager

	.method public hidebysig specialname 
		instance class [mscorlib]System.IServiceProvider get_Services () cil managed 
	{
		// Method begins at RVA 0xdc7c
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld class [mscorlib]System.IServiceProvider SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8v
		IL_0006: ret
	} // end of method SunBurnCoreSystem::get_Services

	.method public hidebysig specialname 
		instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService get_GraphicsDeviceManager () cil managed 
	{
		// Method begins at RVA 0xdc88
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8Y
		IL_0006: ret
	} // end of method SunBurnCoreSystem::get_GraphicsDeviceManager

	.method public hidebysig specialname rtspecialname 
		instance void .ctor (
			class [mscorlib]System.IServiceProvider service,
			class [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ContentManager managerwithactivationfile
		) cil managed 
	{
		.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0xdc94
		// Header size: 12
		// Code size: 116 (0x74)
		.maxstack 3

		IL_0000: ldarg.0
		IL_0001: ldc.i4.1
		IL_0002: stfld bool SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8L
		IL_0007: ldarg.0
		IL_0008: call instance void [mscorlib]System.Object::.ctor()
		IL_000d: ldarg.2
		IL_000e: callvirt instance string [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ContentManager::get_RootDirectory()
		IL_0013: call void a.N::B(string)
		IL_0018: ldarg.0
		IL_0019: stsfld class SynapseGaming.LightingSystem.Core.SunBurnCoreSystem SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'l8\u0013'
		IL_001e: ldarg.0
		IL_001f: ldarg.1
		IL_0020: stfld class [mscorlib]System.IServiceProvider SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8v
		IL_0025: ldarg.0
		IL_0026: ldarg.1
		IL_0027: ldtoken [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService
		IL_002c: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
		IL_0031: callvirt instance object [mscorlib]System.IServiceProvider::GetService(class [mscorlib]System.Type)
		IL_0036: isinst [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService
		IL_003b: stfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8Y
		IL_0040: ldarg.0
		IL_0041: ldarg.1
		IL_0042: newobj instance void SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a::.ctor(class [mscorlib]System.IServiceProvider)
		IL_0047: stfld class SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'l8\u0005'
		IL_004c: ldarg.0
		IL_004d: ldnull
		IL_004e: newobj instance void SynapseGaming.LightingSystem.Core.ResourceManager::.ctor(class SynapseGaming.LightingSystem.Core.IManagerServiceProvider)
		IL_0053: stfld class SynapseGaming.LightingSystem.Core.ResourceManager SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8b
		IL_0058: ldarg.0
		IL_0059: newobj instance void B.s::.ctor()
		IL_005e: stfld class B.s SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8I
		IL_0063: ldarg.0
		IL_0064: ldfld class B.s SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8I
		IL_0069: callvirt instance void B.s::'\u0013T'()
		IL_006e: call void a.a::N()
		IL_0073: ret
	} // end of method SunBurnCoreSystem::.ctor

	.method assembly hidebysig 
		instance void '\u0013M' (
			class SynapseGaming.LightingSystem.Core.FrameBuffers ''
		) cil managed 
	{
		// Method begins at RVA 0xdd14
		// Header size: 12
		// Code size: 69 (0x45)
		.maxstack 2
		.locals init (
			[0] class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice,
			[1] valuetype [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Viewport
		)

		IL_0000: ldarg.0
		IL_0001: ldfld bool SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8L
		IL_0006: brfalse.s IL_0044

		IL_0008: ldarg.0
		IL_0009: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8Y
		IL_000e: callvirt instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService::get_GraphicsDevice()
		IL_0013: stloc.0
		IL_0014: ldloc.0
		IL_0015: callvirt instance valuetype [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Viewport [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice::get_Viewport()
		IL_001a: stloc.1
		IL_001b: ldloca.s 1
		IL_001d: call instance int32 [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Viewport::get_Width()
		IL_0022: ldarg.1
		IL_0023: callvirt instance int32 SynapseGaming.LightingSystem.Core.FrameBuffers::get_Width()
		IL_0028: blt.s IL_0039

		IL_002a: ldloca.s 1
		IL_002c: call instance int32 [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Viewport::get_Height()
		IL_0031: ldarg.1
		IL_0032: callvirt instance int32 SynapseGaming.LightingSystem.Core.FrameBuffers::get_Height()
		IL_0037: bge.s IL_0044

		IL_0039: ldstr "Supplied frame buffers are too large for final target viewport, this will cause performance issues. Supply properly sized buffers or disable the SunBurnCoreSystem's DetectOverSizedFrameBuffers property to ignore."
		IL_003e: newobj instance void [mscorlib]System.Exception::.ctor(string)
		IL_0043: throw

		IL_0044: ret
	} // end of method SunBurnCoreSystem::'\u0013M'

	.method public hidebysig 
		instance void ManuallyLoadPlugin<(SynapseGaming.LightingSystem.Core.IPlugin) T> () cil managed 
	{
		// Method begins at RVA 0xdd68
		// Header size: 1
		// Code size: 13 (0xd)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld class B.s SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8I
		IL_0006: ldc.i4.0
		IL_0007: callvirt instance void B.s::'\u0013\u0014'<!!T>(bool)
		IL_000c: ret
	} // end of method SunBurnCoreSystem::ManuallyLoadPlugin

	.method public hidebysig 
		instance void InitializeSceneInterfaceWithLoadedPlugins (
			class SynapseGaming.LightingSystem.Core.IManagerServiceProvider sceneinterface,
			bool includeautoloaded
		) cil managed 
	{
		// Method begins at RVA 0xdd78
		// Header size: 1
		// Code size: 14 (0xe)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld class B.s SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8I
		IL_0006: ldarg.1
		IL_0007: ldarg.2
		IL_0008: callvirt instance void B.s::'\u0013E'(class SynapseGaming.LightingSystem.Core.IManagerServiceProvider, bool)
		IL_000d: ret
	} // end of method SunBurnCoreSystem::InitializeSceneInterfaceWithLoadedPlugins

	.method public hidebysig 
		instance valuetype [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.RenderTargetUsage GetBestRenderTargetUsage () cil managed 
	{
		// Method begins at RVA 0xdd8c
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: ldc.i4.2
		IL_0001: ret
	} // end of method SunBurnCoreSystem::GetBestRenderTargetUsage

	.method assembly hidebysig 
		instance class SynapseGaming.LightingSystem.Effects.EffectData '\u0013\u0001' (
			string ''
		) cil managed 
	{
		// Method begins at RVA 0xdd94
		// Header size: 12
		// Code size: 20 (0x14)
		.maxstack 2
		.locals init (
			[0] class SynapseGaming.LightingSystem.Effects.EffectData
		)

		IL_0000: ldarg.0
		IL_0001: ldfld class SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'l8\u0005'
		IL_0006: callvirt instance class [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ResourceContentManager SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a::get_EmbeddedResourceManager()
		IL_000b: ldarg.1
		IL_000c: callvirt instance !!0 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ContentManager::Load<class SynapseGaming.LightingSystem.Effects.EffectData>(string)
		IL_0011: stloc.0
		IL_0012: ldloc.0
		IL_0013: ret
	} // end of method SunBurnCoreSystem::'\u0013\u0001'

	.method assembly hidebysig 
		instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Model '\u0013\u0018' (
			string ''
		) cil managed 
	{
		// Method begins at RVA 0xddb4
		// Header size: 12
		// Code size: 20 (0x14)
		.maxstack 2
		.locals init (
			[0] class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Model
		)

		IL_0000: ldarg.0
		IL_0001: ldfld class SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'l8\u0005'
		IL_0006: callvirt instance class [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ResourceContentManager SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a::get_EmbeddedResourceManager()
		IL_000b: ldarg.1
		IL_000c: callvirt instance !!0 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ContentManager::Load<class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Model>(string)
		IL_0011: stloc.0
		IL_0012: ldloc.0
		IL_0013: ret
	} // end of method SunBurnCoreSystem::'\u0013\u0018'

	.method assembly hidebysig 
		instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D '\u0013S' (
			string ''
		) cil managed 
	{
		// Method begins at RVA 0xddd4
		// Header size: 12
		// Code size: 20 (0x14)
		.maxstack 2
		.locals init (
			[0] class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D
		)

		IL_0000: ldarg.0
		IL_0001: ldfld class SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'l8\u0005'
		IL_0006: callvirt instance class [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ResourceContentManager SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a::get_EmbeddedResourceManager()
		IL_000b: ldarg.1
		IL_000c: callvirt instance !!0 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ContentManager::Load<class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D>(string)
		IL_0011: stloc.0
		IL_0012: ldloc.0
		IL_0013: ret
	} // end of method SunBurnCoreSystem::'\u0013S'

	.method assembly hidebysig 
		instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D '\u00139' () cil managed 
	{
		// Method begins at RVA 0xddf4
		// Header size: 1
		// Code size: 44 (0x2c)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8l
		IL_0006: brtrue.s IL_0025

		IL_0008: ldarg.0
		IL_0009: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8Y
		IL_000e: callvirt instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService::get_GraphicsDevice()
		IL_0013: pop
		IL_0014: ldarg.0
		IL_0015: ldarg.0
		IL_0016: ldstr "SplashScreen"
		IL_001b: call instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'\u0013S'(string)
		IL_0020: stfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8l

		IL_0025: ldarg.0
		IL_0026: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8l
		IL_002b: ret
	} // end of method SunBurnCoreSystem::'\u00139'

	.method assembly hidebysig 
		instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube '\u0013G' () cil managed 
	{
		// Method begins at RVA 0xde24
		// Header size: 12
		// Code size: 295 (0x127)
		.maxstack 6
		.locals init (
			[0] class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice,
			[1] valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color[],
			[2] int32,
			[3] int32
		)

		IL_0000: ldarg.0
		IL_0001: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8w
		IL_0006: brtrue IL_0120

		IL_000b: ldarg.0
		IL_000c: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8Y
		IL_0011: callvirt instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService::get_GraphicsDevice()
		IL_0016: stloc.0
		IL_0017: ldarg.0
		IL_0018: ldloc.0
		IL_0019: ldc.i4.1
		IL_001a: ldc.i4.0
		IL_001b: ldc.i4.0
		IL_001c: newobj instance void [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube::.ctor(class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice, int32, bool, valuetype [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SurfaceFormat)
		IL_0021: stfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8w
		IL_0026: ldc.i4.1
		IL_0027: newarr [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
		IL_002c: stloc.1
		IL_002d: ldc.i4.0
		IL_002e: stloc.2
		IL_002f: br IL_0119
		// loop start (head: IL_0119)
			IL_0034: ldloc.2
			IL_0035: stloc.3
			IL_0036: ldloc.3
			IL_0037: switch (IL_0059, IL_007b, IL_0096, IL_00b5, IL_00d0, IL_00ef)

			IL_0054: br IL_0108

			IL_0059: ldloc.1
			IL_005a: ldc.i4.0
			IL_005b: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
			IL_0060: ldc.i4 255
			IL_0065: ldc.i4.0
			IL_0066: ldc.i4.0
			IL_0067: ldc.i4 255
			IL_006c: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color::.ctor(int32, int32, int32, int32)
			IL_0071: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
			IL_0076: br IL_0108

			IL_007b: ldloc.1
			IL_007c: ldc.i4.0
			IL_007d: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
			IL_0082: ldc.i4 255
			IL_0087: ldc.i4.0
			IL_0088: ldc.i4.0
			IL_0089: ldc.i4.0
			IL_008a: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color::.ctor(int32, int32, int32, int32)
			IL_008f: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
			IL_0094: br.s IL_0108

			IL_0096: ldloc.1
			IL_0097: ldc.i4.0
			IL_0098: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
			IL_009d: ldc.i4.0
			IL_009e: ldc.i4 255
			IL_00a3: ldc.i4.0
			IL_00a4: ldc.i4 255
			IL_00a9: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color::.ctor(int32, int32, int32, int32)
			IL_00ae: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
			IL_00b3: br.s IL_0108

			IL_00b5: ldloc.1
			IL_00b6: ldc.i4.0
			IL_00b7: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
			IL_00bc: ldc.i4.0
			IL_00bd: ldc.i4 255
			IL_00c2: ldc.i4.0
			IL_00c3: ldc.i4.0
			IL_00c4: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color::.ctor(int32, int32, int32, int32)
			IL_00c9: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
			IL_00ce: br.s IL_0108

			IL_00d0: ldloc.1
			IL_00d1: ldc.i4.0
			IL_00d2: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
			IL_00d7: ldc.i4.0
			IL_00d8: ldc.i4.0
			IL_00d9: ldc.i4 255
			IL_00de: ldc.i4 255
			IL_00e3: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color::.ctor(int32, int32, int32, int32)
			IL_00e8: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
			IL_00ed: br.s IL_0108

			IL_00ef: ldloc.1
			IL_00f0: ldc.i4.0
			IL_00f1: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
			IL_00f6: ldc.i4.0
			IL_00f7: ldc.i4.0
			IL_00f8: ldc.i4 255
			IL_00fd: ldc.i4.0
			IL_00fe: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color::.ctor(int32, int32, int32, int32)
			IL_0103: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color

			IL_0108: ldarg.0
			IL_0109: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8w
			IL_010e: ldloc.2
			IL_010f: ldloc.1
			IL_0110: callvirt instance void [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube::SetData<valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color>(valuetype [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.CubeMapFace, !!0[])
			IL_0115: ldloc.2
			IL_0116: ldc.i4.1
			IL_0117: add
			IL_0118: stloc.2

			IL_0119: ldloc.2
			IL_011a: ldc.i4.6
			IL_011b: blt IL_0034
		// end loop

		IL_0120: ldarg.0
		IL_0121: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8w
		IL_0126: ret
	} // end of method SunBurnCoreSystem::'\u0013G'

	.method assembly hidebysig 
		instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube '\u0013u' () cil managed 
	{
		// Method begins at RVA 0xdf58
		// Header size: 12
		// Code size: 1304 (0x518)
		.maxstack 6
		.locals init (
			[0] class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice,
			[1] valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color[],
			[2] int32,
			[3] float32,
			[4] valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3[],
			[5] valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3[],
			[6] valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3[],
			[7] int32,
			[8] int32,
			[9] int32,
			[10] valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3,
			[11] float32,
			[12] float32,
			[13] float32,
			[14] valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3[],
			[15] valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3[],
			[16] valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3[],
			[17] int32
		)

		IL_0000: ldarg.0
		IL_0001: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8J
		IL_0006: brtrue IL_0511

		IL_000b: ldarg.0
		IL_000c: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8Y
		IL_0011: callvirt instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService::get_GraphicsDevice()
		IL_0016: stloc.0
		IL_0017: ldarg.0
		IL_0018: ldloc.0
		IL_0019: ldc.i4 256
		IL_001e: ldc.i4.0
		IL_001f: ldc.i4.0
		IL_0020: newobj instance void [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube::.ctor(class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice, int32, bool, valuetype [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SurfaceFormat)
		IL_0025: stfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8J
		IL_002a: ldarg.0
		IL_002b: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8J
		IL_0030: callvirt instance int32 [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube::get_Size()
		IL_0035: ldarg.0
		IL_0036: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8J
		IL_003b: callvirt instance int32 [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube::get_Size()
		IL_0040: mul
		IL_0041: newarr [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
		IL_0046: stloc.1
		IL_0047: ldarg.0
		IL_0048: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8J
		IL_004d: callvirt instance int32 [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube::get_Size()
		IL_0052: stloc.2
		IL_0053: ldc.r4 1
		IL_0058: ldarg.0
		IL_0059: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8J
		IL_005e: callvirt instance int32 [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube::get_Size()
		IL_0063: ldc.i4.1
		IL_0064: sub
		IL_0065: conv.r4
		IL_0066: div
		IL_0067: stloc.3
		IL_0068: ldc.i4.6
		IL_0069: newarr [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_006e: stloc.s 14
		IL_0070: ldloc.s 14
		IL_0072: ldc.i4.0
		IL_0073: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_0078: ldc.r4 1
		IL_007d: ldc.r4 0.0
		IL_0082: ldc.r4 0.0
		IL_0087: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_008c: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_0091: ldloc.s 14
		IL_0093: ldc.i4.1
		IL_0094: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_0099: ldc.r4 -1
		IL_009e: ldc.r4 0.0
		IL_00a3: ldc.r4 0.0
		IL_00a8: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_00ad: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_00b2: ldloc.s 14
		IL_00b4: ldc.i4.2
		IL_00b5: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_00ba: ldc.r4 0.0
		IL_00bf: ldc.r4 1
		IL_00c4: ldc.r4 0.0
		IL_00c9: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_00ce: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_00d3: ldloc.s 14
		IL_00d5: ldc.i4.3
		IL_00d6: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_00db: ldc.r4 0.0
		IL_00e0: ldc.r4 -1
		IL_00e5: ldc.r4 0.0
		IL_00ea: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_00ef: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_00f4: ldloc.s 14
		IL_00f6: ldc.i4.4
		IL_00f7: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_00fc: ldc.r4 0.0
		IL_0101: ldc.r4 0.0
		IL_0106: ldc.r4 1
		IL_010b: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_0110: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_0115: ldloc.s 14
		IL_0117: ldc.i4.5
		IL_0118: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_011d: ldc.r4 0.0
		IL_0122: ldc.r4 0.0
		IL_0127: ldc.r4 -1
		IL_012c: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_0131: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_0136: ldloc.s 14
		IL_0138: stloc.s 4
		IL_013a: ldc.i4.6
		IL_013b: newarr [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_0140: stloc.s 15
		IL_0142: ldloc.s 15
		IL_0144: ldc.i4.0
		IL_0145: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_014a: ldc.r4 0.0
		IL_014f: ldc.r4 0.0
		IL_0154: ldc.r4 -1
		IL_0159: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_015e: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_0163: ldloc.s 15
		IL_0165: ldc.i4.1
		IL_0166: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_016b: ldc.r4 0.0
		IL_0170: ldc.r4 0.0
		IL_0175: ldc.r4 1
		IL_017a: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_017f: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_0184: ldloc.s 15
		IL_0186: ldc.i4.2
		IL_0187: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_018c: ldc.r4 1
		IL_0191: ldc.r4 0.0
		IL_0196: ldc.r4 0.0
		IL_019b: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_01a0: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_01a5: ldloc.s 15
		IL_01a7: ldc.i4.3
		IL_01a8: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_01ad: ldc.r4 1
		IL_01b2: ldc.r4 0.0
		IL_01b7: ldc.r4 0.0
		IL_01bc: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_01c1: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_01c6: ldloc.s 15
		IL_01c8: ldc.i4.4
		IL_01c9: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_01ce: ldc.r4 1
		IL_01d3: ldc.r4 0.0
		IL_01d8: ldc.r4 0.0
		IL_01dd: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_01e2: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_01e7: ldloc.s 15
		IL_01e9: ldc.i4.5
		IL_01ea: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_01ef: ldc.r4 -1
		IL_01f4: ldc.r4 0.0
		IL_01f9: ldc.r4 0.0
		IL_01fe: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_0203: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_0208: ldloc.s 15
		IL_020a: stloc.s 5
		IL_020c: ldc.i4.6
		IL_020d: newarr [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_0212: stloc.s 16
		IL_0214: ldloc.s 16
		IL_0216: ldc.i4.0
		IL_0217: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_021c: ldc.r4 0.0
		IL_0221: ldc.r4 -1
		IL_0226: ldc.r4 0.0
		IL_022b: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_0230: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_0235: ldloc.s 16
		IL_0237: ldc.i4.1
		IL_0238: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_023d: ldc.r4 0.0
		IL_0242: ldc.r4 -1
		IL_0247: ldc.r4 0.0
		IL_024c: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_0251: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_0256: ldloc.s 16
		IL_0258: ldc.i4.2
		IL_0259: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_025e: ldc.r4 0.0
		IL_0263: ldc.r4 0.0
		IL_0268: ldc.r4 1
		IL_026d: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_0272: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_0277: ldloc.s 16
		IL_0279: ldc.i4.3
		IL_027a: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_027f: ldc.r4 0.0
		IL_0284: ldc.r4 0.0
		IL_0289: ldc.r4 -1
		IL_028e: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_0293: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_0298: ldloc.s 16
		IL_029a: ldc.i4.4
		IL_029b: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_02a0: ldc.r4 0.0
		IL_02a5: ldc.r4 -1
		IL_02aa: ldc.r4 0.0
		IL_02af: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_02b4: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_02b9: ldloc.s 16
		IL_02bb: ldc.i4.5
		IL_02bc: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_02c1: ldc.r4 0.0
		IL_02c6: ldc.r4 -1
		IL_02cb: ldc.r4 0.0
		IL_02d0: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::.ctor(float32, float32, float32)
		IL_02d5: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
		IL_02da: ldloc.s 16
		IL_02dc: stloc.s 6
		IL_02de: ldc.i4.0
		IL_02df: stloc.s 7
		IL_02e1: br IL_0509
		// loop start (head: IL_0509)
			IL_02e6: ldc.i4.0
			IL_02e7: stloc.s 8
			IL_02e9: br IL_04ed
			// loop start (head: IL_04ed)
				IL_02ee: ldc.i4.0
				IL_02ef: stloc.s 9
				IL_02f1: br IL_04df
				// loop start (head: IL_04df)
					IL_02f6: ldloc.s 4
					IL_02f8: ldloc.s 7
					IL_02fa: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
					IL_02ff: ldobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
					IL_0304: ldloc.s 9
					IL_0306: conv.r4
					IL_0307: ldloc.3
					IL_0308: mul
					IL_0309: ldc.r4 2
					IL_030e: mul
					IL_030f: ldc.r4 1
					IL_0314: sub
					IL_0315: ldloc.s 5
					IL_0317: ldloc.s 7
					IL_0319: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
					IL_031e: ldobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
					IL_0323: call valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::op_Multiply(float32, valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3)
					IL_0328: call valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::op_Addition(valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3, valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3)
					IL_032d: ldloc.s 8
					IL_032f: conv.r4
					IL_0330: ldloc.3
					IL_0331: mul
					IL_0332: ldc.r4 2
					IL_0337: mul
					IL_0338: ldc.r4 1
					IL_033d: sub
					IL_033e: ldloc.s 6
					IL_0340: ldloc.s 7
					IL_0342: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
					IL_0347: ldobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3
					IL_034c: call valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::op_Multiply(float32, valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3)
					IL_0351: call valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::op_Addition(valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3, valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3)
					IL_0356: stloc.s 10
					IL_0358: ldloca.s 10
					IL_035a: call instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::Normalize()
					IL_035f: ldloc.s 7
					IL_0361: stloc.s 17
					IL_0363: ldloc.s 17
					IL_0365: switch (IL_0383, IL_03a4, IL_03c2, IL_03e0, IL_03fe)

					IL_037e: br IL_041c

					IL_0383: ldloca.s 10
					IL_0385: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::X
					IL_038a: neg
					IL_038b: stloc.s 11
					IL_038d: ldloca.s 10
					IL_038f: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::Z
					IL_0394: stloc.s 12
					IL_0396: ldloca.s 10
					IL_0398: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::Y
					IL_039d: stloc.s 13
					IL_039f: br IL_0437

					IL_03a4: ldloca.s 10
					IL_03a6: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::X
					IL_03ab: stloc.s 11
					IL_03ad: ldloca.s 10
					IL_03af: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::Z
					IL_03b4: neg
					IL_03b5: stloc.s 12
					IL_03b7: ldloca.s 10
					IL_03b9: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::Y
					IL_03be: stloc.s 13
					IL_03c0: br.s IL_0437

					IL_03c2: ldloca.s 10
					IL_03c4: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::Y
					IL_03c9: neg
					IL_03ca: stloc.s 11
					IL_03cc: ldloca.s 10
					IL_03ce: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::X
					IL_03d3: stloc.s 12
					IL_03d5: ldloca.s 10
					IL_03d7: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::Z
					IL_03dc: stloc.s 13
					IL_03de: br.s IL_0437

					IL_03e0: ldloca.s 10
					IL_03e2: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::Y
					IL_03e7: stloc.s 11
					IL_03e9: ldloca.s 10
					IL_03eb: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::X
					IL_03f0: neg
					IL_03f1: stloc.s 12
					IL_03f3: ldloca.s 10
					IL_03f5: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::Z
					IL_03fa: stloc.s 13
					IL_03fc: br.s IL_0437

					IL_03fe: ldloca.s 10
					IL_0400: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::Z
					IL_0405: stloc.s 11
					IL_0407: ldloca.s 10
					IL_0409: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::X
					IL_040e: stloc.s 12
					IL_0410: ldloca.s 10
					IL_0412: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::Y
					IL_0417: neg
					IL_0418: stloc.s 13
					IL_041a: br.s IL_0437

					IL_041c: ldloca.s 10
					IL_041e: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::Z
					IL_0423: stloc.s 11
					IL_0425: ldloca.s 10
					IL_0427: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::X
					IL_042c: stloc.s 12
					IL_042e: ldloca.s 10
					IL_0430: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::Y
					IL_0435: stloc.s 13

					IL_0437: ldloca.s 10
					IL_0439: ldloc.s 12
					IL_043b: ldloc.s 11
					IL_043d: div
					IL_043e: ldc.r4 0.5
					IL_0443: mul
					IL_0444: ldc.r4 0.5
					IL_0449: add
					IL_044a: ldc.r4 0.0
					IL_044f: ldc.r4 1
					IL_0454: call float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.MathHelper::Clamp(float32, float32, float32)
					IL_0459: stfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::X
					IL_045e: ldloca.s 10
					IL_0460: ldloc.s 13
					IL_0462: ldloc.s 11
					IL_0464: div
					IL_0465: ldc.r4 0.5
					IL_046a: mul
					IL_046b: ldc.r4 0.5
					IL_0470: add
					IL_0471: ldc.r4 0.0
					IL_0476: ldc.r4 1
					IL_047b: call float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.MathHelper::Clamp(float32, float32, float32)
					IL_0480: stfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::Y
					IL_0485: ldloc.1
					IL_0486: ldloc.s 8
					IL_0488: ldloc.2
					IL_0489: mul
					IL_048a: ldloc.s 9
					IL_048c: add
					IL_048d: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
					IL_0492: ldloca.s 10
					IL_0494: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::X
					IL_0499: ldc.r4 255
					IL_049e: mul
					IL_049f: ldc.r4 0.5
					IL_04a4: add
					IL_04a5: conv.u1
					IL_04a6: ldloca.s 10
					IL_04a8: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::Y
					IL_04ad: ldc.r4 255
					IL_04b2: mul
					IL_04b3: ldc.r4 0.5
					IL_04b8: add
					IL_04b9: conv.u1
					IL_04ba: ldloca.s 10
					IL_04bc: ldfld float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Vector3::Z
					IL_04c1: ldc.r4 255
					IL_04c6: mul
					IL_04c7: ldc.r4 0.5
					IL_04cc: add
					IL_04cd: conv.u1
					IL_04ce: ldc.i4.0
					IL_04cf: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color::.ctor(int32, int32, int32, int32)
					IL_04d4: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
					IL_04d9: ldloc.s 9
					IL_04db: ldc.i4.1
					IL_04dc: add
					IL_04dd: stloc.s 9

					IL_04df: ldloc.s 9
					IL_04e1: ldloc.2
					IL_04e2: blt IL_02f6
				// end loop

				IL_04e7: ldloc.s 8
				IL_04e9: ldc.i4.1
				IL_04ea: add
				IL_04eb: stloc.s 8

				IL_04ed: ldloc.s 8
				IL_04ef: ldloc.2
				IL_04f0: blt IL_02ee
			// end loop

			IL_04f5: ldarg.0
			IL_04f6: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8J
			IL_04fb: ldloc.s 7
			IL_04fd: ldloc.1
			IL_04fe: callvirt instance void [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube::SetData<valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color>(valuetype [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.CubeMapFace, !!0[])
			IL_0503: ldloc.s 7
			IL_0505: ldc.i4.1
			IL_0506: add
			IL_0507: stloc.s 7

			IL_0509: ldloc.s 7
			IL_050b: ldc.i4.6
			IL_050c: blt IL_02e6
		// end loop

		IL_0511: ldarg.0
		IL_0512: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8J
		IL_0517: ret
	} // end of method SunBurnCoreSystem::'\u0013u'

	.method assembly hidebysig 
		instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D '\u0013t' () cil managed 
	{
		// Method begins at RVA 0xe47c
		// Header size: 12
		// Code size: 87 (0x57)
		.maxstack 7
		.locals init (
			[0] class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice,
			[1] valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Graphics.PackedVector.HalfSingle[]
		)

		IL_0000: ldarg.0
		IL_0001: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8B
		IL_0006: brtrue.s IL_0050

		IL_0008: ldarg.0
		IL_0009: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8Y
		IL_000e: callvirt instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService::get_GraphicsDevice()
		IL_0013: stloc.0
		IL_0014: ldarg.0
		IL_0015: ldloc.0
		IL_0016: ldc.i4.s 16
		IL_0018: ldc.i4.s 16
		IL_001a: ldc.i4.0
		IL_001b: ldc.i4.s 16
		IL_001d: newobj instance void [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D::.ctor(class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice, int32, int32, bool, valuetype [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SurfaceFormat)
		IL_0022: stfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8B
		IL_0027: ldarg.0
		IL_0028: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8B
		IL_002d: callvirt instance int32 [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D::get_Width()
		IL_0032: ldarg.0
		IL_0033: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8B
		IL_0038: callvirt instance int32 [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D::get_Height()
		IL_003d: mul
		IL_003e: newarr [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Graphics.PackedVector.HalfSingle
		IL_0043: stloc.1
		IL_0044: ldarg.0
		IL_0045: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8B
		IL_004a: ldloc.1
		IL_004b: callvirt instance void [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D::SetData<valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Graphics.PackedVector.HalfSingle>(!!0[])

		IL_0050: ldarg.0
		IL_0051: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8B
		IL_0056: ret
	} // end of method SunBurnCoreSystem::'\u0013t'

	.method assembly hidebysig 
		instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D '\u00134' () cil managed 
	{
		// Method begins at RVA 0xe4e0
		// Header size: 12
		// Code size: 407 (0x197)
		.maxstack 7
		.locals init (
			[0] class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice,
			[1] int32,
			[2] int32,
			[3] int32,
			[4] valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color[],
			[5] float32,
			[6] float32,
			[7] int32,
			[8] int32,
			[9] int32,
			[10] float32,
			[11] float32,
			[12] float32,
			[13] float32,
			[14] float32,
			[15] float32,
			[16] uint8,
			[17] valuetype [mscorlib]System.Nullable`1<valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Rectangle>
		)

		IL_0000: ldarg.0
		IL_0001: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8N
		IL_0006: brtrue IL_0190

		IL_000b: ldarg.0
		IL_000c: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8Y
		IL_0011: callvirt instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService::get_GraphicsDevice()
		IL_0016: stloc.0
		IL_0017: ldarg.0
		IL_0018: ldloc.0
		IL_0019: ldc.i4.s 64
		IL_001b: ldc.i4 256
		IL_0020: ldc.i4.1
		IL_0021: ldc.i4.0
		IL_0022: newobj instance void [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D::.ctor(class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice, int32, int32, bool, valuetype [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SurfaceFormat)
		IL_0027: stfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8N
		IL_002c: ldarg.0
		IL_002d: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8N
		IL_0032: callvirt instance int32 [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D::get_Width()
		IL_0037: stloc.1
		IL_0038: ldarg.0
		IL_0039: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8N
		IL_003e: callvirt instance int32 [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D::get_Height()
		IL_0043: stloc.2
		IL_0044: ldc.i4.0
		IL_0045: stloc.3
		IL_0046: br IL_017f
		// loop start (head: IL_017f)
			IL_004b: ldloc.1
			IL_004c: ldloc.2
			IL_004d: mul
			IL_004e: newarr [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
			IL_0053: stloc.s 4
			IL_0055: ldc.r4 1
			IL_005a: ldloc.1
			IL_005b: ldc.i4.1
			IL_005c: sub
			IL_005d: conv.r4
			IL_005e: div
			IL_005f: stloc.s 5
			IL_0061: ldc.r4 1
			IL_0066: ldloc.2
			IL_0067: ldc.i4.1
			IL_0068: sub
			IL_0069: conv.r4
			IL_006a: div
			IL_006b: stloc.s 6
			IL_006d: ldc.i4.0
			IL_006e: stloc.s 7
			IL_0070: ldc.i4.0
			IL_0071: stloc.s 8
			IL_0073: br IL_0142
			// loop start (head: IL_0142)
				IL_0078: ldc.i4.0
				IL_0079: stloc.s 9
				IL_007b: br IL_0134
				// loop start (head: IL_0134)
					IL_0080: ldloc.s 9
					IL_0082: conv.r4
					IL_0083: ldloc.s 5
					IL_0085: mul
					IL_0086: stloc.s 10
					IL_0088: ldloc.s 8
					IL_008a: conv.r4
					IL_008b: ldloc.s 6
					IL_008d: mul
					IL_008e: stloc.s 11
					IL_0090: ldloc.s 10
					IL_0092: ldloc.s 10
					IL_0094: mul
					IL_0095: stloc.s 12
					IL_0097: ldloc.s 11
					IL_0099: ldloc.s 11
					IL_009b: mul
					IL_009c: stloc.s 13
					IL_009e: ldc.r4 12.566371
					IL_00a3: ldloc.s 12
					IL_00a5: mul
					IL_00a6: ldloc.s 13
					IL_00a8: mul
					IL_00a9: ldloc.s 13
					IL_00ab: mul
					IL_00ac: stloc.s 14
					IL_00ae: ldloc.s 14
					IL_00b0: ldc.r4 0.0
					IL_00b5: bne.un.s IL_00be

					IL_00b7: ldc.r4 1E-07
					IL_00bc: stloc.s 14

					IL_00be: ldc.r4 1
					IL_00c3: ldloc.s 14
					IL_00c5: div
					IL_00c6: stloc.s 14
					IL_00c8: ldloc.s 12
					IL_00ca: ldloc.s 13
					IL_00cc: mul
					IL_00cd: stloc.s 15
					IL_00cf: ldloc.s 15
					IL_00d1: ldc.r4 0.0
					IL_00d6: bne.un.s IL_00df

					IL_00d8: ldc.r4 1E-07
					IL_00dd: stloc.s 15

					IL_00df: ldloc.s 13
					IL_00e1: ldc.r4 1
					IL_00e6: sub
					IL_00e7: ldloc.s 15
					IL_00e9: div
					IL_00ea: stloc.s 15
					IL_00ec: ldloc.s 14
					IL_00ee: ldloc.s 15
					IL_00f0: conv.r8
					IL_00f1: call float64 [mscorlib]System.Math::Exp(float64)
					IL_00f6: conv.r4
					IL_00f7: mul
					IL_00f8: ldc.r4 0.0
					IL_00fd: ldc.r4 1
					IL_0102: call float32 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.MathHelper::Clamp(float32, float32, float32)
					IL_0107: ldc.r4 255
					IL_010c: mul
					IL_010d: conv.u1
					IL_010e: stloc.s 16
					IL_0110: ldloc.s 4
					IL_0112: ldloc.s 7
					IL_0114: dup
					IL_0115: ldc.i4.1
					IL_0116: add
					IL_0117: stloc.s 7
					IL_0119: ldelema [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
					IL_011e: ldloc.s 16
					IL_0120: ldloc.s 16
					IL_0122: ldloc.s 16
					IL_0124: newobj instance void [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color::.ctor(int32, int32, int32)
					IL_0129: stobj [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color
					IL_012e: ldloc.s 9
					IL_0130: ldc.i4.1
					IL_0131: add
					IL_0132: stloc.s 9

					IL_0134: ldloc.s 9
					IL_0136: ldloc.1
					IL_0137: blt IL_0080
				// end loop

				IL_013c: ldloc.s 8
				IL_013e: ldc.i4.1
				IL_013f: add
				IL_0140: stloc.s 8

				IL_0142: ldloc.s 8
				IL_0144: ldloc.2
				IL_0145: blt IL_0078
			// end loop

			IL_014a: ldarg.0
			IL_014b: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8N
			IL_0150: ldloc.3
			IL_0151: ldloca.s 17
			IL_0153: initobj valuetype [mscorlib]System.Nullable`1<valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Rectangle>
			IL_0159: ldloc.s 17
			IL_015b: ldloc.s 4
			IL_015d: ldc.i4.0
			IL_015e: ldloc.s 4
			IL_0160: ldlen
			IL_0161: conv.i4
			IL_0162: callvirt instance void [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D::SetData<valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Color>(int32, valuetype [mscorlib]System.Nullable`1<valuetype [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Rectangle>, !!0[], int32, int32)
			IL_0167: ldloc.1
			IL_0168: ldc.i4.2
			IL_0169: div
			IL_016a: ldc.i4.1
			IL_016b: call int32 [mscorlib]System.Math::Max(int32, int32)
			IL_0170: stloc.1
			IL_0171: ldloc.2
			IL_0172: ldc.i4.2
			IL_0173: div
			IL_0174: ldc.i4.1
			IL_0175: call int32 [mscorlib]System.Math::Max(int32, int32)
			IL_017a: stloc.2
			IL_017b: ldloc.3
			IL_017c: ldc.i4.1
			IL_017d: add
			IL_017e: stloc.3

			IL_017f: ldloc.3
			IL_0180: ldarg.0
			IL_0181: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8N
			IL_0186: callvirt instance int32 [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture::get_LevelCount()
			IL_018b: blt IL_004b
		// end loop

		IL_0190: ldarg.0
		IL_0191: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8N
		IL_0196: ret
	} // end of method SunBurnCoreSystem::'\u00134'

	.method assembly hidebysig 
		instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteFont '\u0013A' () cil managed 
	{
		// Method begins at RVA 0xe684
		// Header size: 12
		// Code size: 67 (0x43)
		.maxstack 3

		IL_0000: ldarg.0
		IL_0001: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteFont SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8s
		IL_0006: brfalse.s IL_000f

		IL_0008: ldarg.0
		IL_0009: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteFont SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8s
		IL_000e: ret

		IL_000f: ldarg.0
		IL_0010: ldarg.0
		IL_0011: ldfld class SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'l8\u0005'
		IL_0016: callvirt instance class [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ResourceContentManager SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a::get_EmbeddedResourceManager()
		IL_001b: ldstr "ConsoleFont"
		IL_0020: callvirt instance !!0 [Microsoft.Xna.Framework]Microsoft.Xna.Framework.Content.ContentManager::Load<class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteFont>(string)
		IL_0025: stfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteFont SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8s
		IL_002a: ldarg.0
		IL_002b: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteFont SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8s
		IL_0030: ldc.i4.s 95
		IL_0032: newobj instance void valuetype [mscorlib]System.Nullable`1<char>::.ctor(!0)
		IL_0037: callvirt instance void [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteFont::set_DefaultCharacter(valuetype [mscorlib]System.Nullable`1<char>)
		IL_003c: ldarg.0
		IL_003d: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteFont SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8s
		IL_0042: ret
	} // end of method SunBurnCoreSystem::'\u0013A'

	.method assembly hidebysig 
		instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteBatch '\u0013g' () cil managed 
	{
		// Method begins at RVA 0xe6d4
		// Header size: 12
		// Code size: 46 (0x2e)
		.maxstack 3
		.locals init (
			[0] class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice
		)

		IL_0000: ldarg.0
		IL_0001: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteBatch SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8h
		IL_0006: brfalse.s IL_000f

		IL_0008: ldarg.0
		IL_0009: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteBatch SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8h
		IL_000e: ret

		IL_000f: ldarg.0
		IL_0010: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8Y
		IL_0015: callvirt instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService::get_GraphicsDevice()
		IL_001a: stloc.0
		IL_001b: ldarg.0
		IL_001c: ldloc.0
		IL_001d: newobj instance void [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteBatch::.ctor(class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice)
		IL_0022: stfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteBatch SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8h
		IL_0027: ldarg.0
		IL_0028: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteBatch SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8h
		IL_002d: ret
	} // end of method SunBurnCoreSystem::'\u0013g'

	.method public hidebysig 
		instance class SynapseGaming.LightingSystem.Core.GraphicsDeviceSupport GetGraphicsDeviceSupport () cil managed 
	{
		// Method begins at RVA 0xe710
		// Header size: 12
		// Code size: 39 (0x27)
		.maxstack 3
		.locals init (
			[0] class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice
		)

		IL_0000: ldarg.0
		IL_0001: ldfld class SynapseGaming.LightingSystem.Core.GraphicsDeviceSupport SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'l8\u0002'
		IL_0006: brtrue.s IL_0020

		IL_0008: ldarg.0
		IL_0009: ldfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8Y
		IL_000e: callvirt instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService::get_GraphicsDevice()
		IL_0013: stloc.0
		IL_0014: ldarg.0
		IL_0015: ldloc.0
		IL_0016: newobj instance void SynapseGaming.LightingSystem.Core.GraphicsDeviceSupport::.ctor(class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.GraphicsDevice)
		IL_001b: stfld class SynapseGaming.LightingSystem.Core.GraphicsDeviceSupport SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'l8\u0002'

		IL_0020: ldarg.0
		IL_0021: ldfld class SynapseGaming.LightingSystem.Core.GraphicsDeviceSupport SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'l8\u0002'
		IL_0026: ret
	} // end of method SunBurnCoreSystem::GetGraphicsDeviceSupport

	.method public hidebysig 
		instance void Unload () cil managed 
	{
		// Method begins at RVA 0xe744
		// Header size: 12
		// Code size: 114 (0x72)
		.maxstack 2

		IL_0000: ldarg.0
		IL_0001: ldfld class SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'l8\u0005'
		IL_0006: callvirt instance void SynapseGaming.LightingSystem.Core.SunBurnCoreSystem/w8a::Unload()
		IL_000b: ldarg.0
		IL_000c: ldfld class SynapseGaming.LightingSystem.Core.ResourceManager SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8b
		IL_0011: callvirt instance void SynapseGaming.LightingSystem.Core.ResourceManager::Unload()
		IL_0016: ldarg.0
		IL_0017: ldfld class B.s SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8I
		IL_001c: callvirt instance void B.s::'6'()
		IL_0021: ldarg.0
		IL_0022: ldnull
		IL_0023: stfld class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteFont SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8s
		IL_0028: ldarg.0
		IL_0029: ldnull
		IL_002a: stfld class SynapseGaming.LightingSystem.Core.GraphicsDeviceSupport SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'l8\u0002'
		IL_002f: ldarg.0
		IL_0030: ldflda class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteBatch SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8h
		IL_0035: call void y.a::'w\u0005'<class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.SpriteBatch>(!!0&)
		IL_003a: ldarg.0
		IL_003b: ldflda class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8N
		IL_0040: call void y.a::'w\u0005'<class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D>(!!0&)
		IL_0045: ldarg.0
		IL_0046: ldflda class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8l
		IL_004b: call void y.a::'w\u0005'<class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D>(!!0&)
		IL_0050: ldarg.0
		IL_0051: ldflda class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8w
		IL_0056: call void y.a::'w\u0005'<class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube>(!!0&)
		IL_005b: ldarg.0
		IL_005c: ldflda class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8J
		IL_0061: call void y.a::'w\u0005'<class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.TextureCube>(!!0&)
		IL_0066: ldarg.0
		IL_0067: ldflda class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8B
		IL_006c: call void y.a::'w\u0005'<class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.Texture2D>(!!0&)
		IL_0071: ret
	} // end of method SunBurnCoreSystem::Unload

	.method private hidebysig specialname rtspecialname static 
		void .cctor () cil managed 
	{
		// Method begins at RVA 0xe7c4
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldc.i4 10000
		IL_0005: stsfld int32 SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::l8a
		IL_000a: ret
	} // end of method SunBurnCoreSystem::.cctor

	// Properties
	.property class SynapseGaming.LightingSystem.Core.SunBurnCoreSystem Instance()
	{
		.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
			01 00 00 00
		)
		.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
			01 00 00 00 00 00 00 00
		)
		.get class SynapseGaming.LightingSystem.Core.SunBurnCoreSystem SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::get_Instance()
	}
	.property string Edition()
	{
		.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
			01 00 00 00
		)
		.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
			01 00 00 00 00 00 00 00
		)
		.get string SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::get_Edition()
	}
	.property string PublicKeyToken()
	{
		.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
			01 00 00 00 00 00 00 00
		)
		.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
			01 00 00 00
		)
		.get string SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::get_PublicKeyToken()
	}
	.property string Version()
	{
		.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
			01 00 00 00 00 00 00 00
		)
		.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
			01 00 00 00
		)
		.get string SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::get_Version()
	}
	.property class [mscorlib]System.Resources.ResourceManager ResourceManager()
	{
		.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
			01 00 00 00 00 00 00 00
		)
		.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
			01 00 00 00
		)
		.get class [mscorlib]System.Resources.ResourceManager SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'\u0013\u0015'()
	}
	.property int32 MaxLightsPerGroup()
	{
		.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
			01 00 00 00 00 00 00 00
		)
		.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
			01 00 00 00
		)
		.get int32 SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'\u0013r'()
		.set void SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::'\u0013K'(int32)
	}
	.property instance bool DetectOverSizedFrameBuffers()
	{
		.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
			01 00 00 00
		)
		.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
			01 00 00 00 00 00 00 00
		)
		.get instance bool SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::get_DetectOverSizedFrameBuffers()
		.set instance void SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::set_DetectOverSizedFrameBuffers(bool)
	}
	.property instance class SynapseGaming.LightingSystem.Core.ResourceManager DefaultResourceManager()
	{
		.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
			01 00 00 00
		)
		.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
			01 00 00 00 00 00 00 00
		)
		.get instance class SynapseGaming.LightingSystem.Core.ResourceManager SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::get_DefaultResourceManager()
	}
	.property instance class [mscorlib]System.IServiceProvider Services()
	{
		.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
			01 00 00 00 00 00 00 00
		)
		.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
			01 00 00 00
		)
		.get instance class [mscorlib]System.IServiceProvider SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::get_Services()
	}
	.property instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService GraphicsDeviceManager()
	{
		.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
			01 00 00 00 00 00 00 00
		)
		.custom instance void [mscorlib]System.Diagnostics.DebuggerHiddenAttribute::.ctor() = (
			01 00 00 00
		)
		.get instance class [Microsoft.Xna.Framework.Graphics]Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService SynapseGaming.LightingSystem.Core.SunBurnCoreSystem::get_GraphicsDeviceManager()
	}

} // end of class SynapseGaming.LightingSystem.Core.SunBurnCoreSystem
