using Microsoft.Xna.Framework;
using SynapseGaming.LightingSystem.Editor;

namespace SynapseGaming.LightingSystem.Core;

[EditorObject(true)]
public interface ISceneEnvironment : IEditorObject, INamedObject
{
	[EditorProperty(true, Description = "Viewable Distance", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 3, ToolTipText = "")]
	[EditorNumberPadOptions(2, 0.0, 2147483647.0, 2.0)]
	float VisibleDistance { get; set; }

	[EditorProperty(true, Description = "Fog Enabled", HorizontalAlignment = true, MajorGrouping = 1, MinorGrouping = 1, ToolTipText = "")]
	bool FogEnabled { get; set; }

	[EditorNumberPadOptions(2, 0.0, 2147483647.0, 2.0)]
	[EditorProperty(true, Description = "Fog Start Distance", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 1, ToolTipText = "")]
	float FogStartDistance { get; set; }

	[EditorNumberPadOptions(2, 0.0, 2147483647.0, 2.0)]
	[EditorProperty(true, Description = "Fog End Distance", HorizontalAlignment = true, MajorGrouping = 4, MinorGrouping = 2, ToolTipText = "")]
	float FogEndDistance { get; set; }

	[EditorProperty(true, Description = "Fog Color", HorizontalAlignment = true, MajorGrouping = 1, MinorGrouping = 3, ControlType = ControlType.ColorSelection, ToolTipText = "")]
	Vector3 FogColor { get; set; }

	[EditorNumberPadOptions(2, 0.0, 2147483647.0, 2.0)]
	[EditorProperty(true, Description = "Shadow Fade Start", HorizontalAlignment = true, MajorGrouping = 5, MinorGrouping = 1, ToolTipText = "")]
	float ShadowFadeStartDistance { get; set; }

	[EditorNumberPadOptions(2, 0.0, 2147483647.0, 2.0)]
	[EditorProperty(true, Description = "Shadow Fade End", HorizontalAlignment = true, MajorGrouping = 5, MinorGrouping = 2, ToolTipText = "")]
	float ShadowFadeEndDistance { get; set; }

	[EditorNumberPadOptions(2, 0.0, 2147483647.0, 2.0)]
	[EditorProperty(true, Description = "Max Caster Distance", HorizontalAlignment = true, MajorGrouping = 5, MinorGrouping = 3, ToolTipText = "")]
	float ShadowCasterDistance { get; set; }

	[EditorNumberPadOptions(2, 0.0, 10000.0, 0.01)]
	[EditorProperty(true, Description = "Bloom Amount", HorizontalAlignment = true, MajorGrouping = 6, MinorGrouping = 1, ToolTipText = "")]
	float BloomAmount { get; set; }

	[EditorNumberPadOptions(2, 0.0, 100.0, 0.01)]
	[EditorProperty(true, Description = "Bloom Threshold", HorizontalAlignment = true, MajorGrouping = 6, MinorGrouping = 2, ToolTipText = "")]
	float BloomThreshold { get; set; }

	[EditorProperty(true, Description = "HDR Enabled", HorizontalAlignment = true, MajorGrouping = 1, MinorGrouping = 2, ToolTipText = "")]
	bool DynamicRangeEnabled { get; set; }

	[EditorNumberPadOptions(2, 0.0, 100.0, 0.1)]
	[EditorProperty(true, Description = "Exposure Amount", HorizontalAlignment = true, MajorGrouping = 6, MinorGrouping = 3, ToolTipText = "")]
	float ExposureAmount { get; set; }

	[EditorNumberPadOptions(2, 0.0, 1.0, 0.05)]
	[EditorProperty(true, Description = "Saturation Amount", HorizontalAlignment = true, MajorGrouping = 8, MinorGrouping = 1, ToolTipText = "")]
	float DynamicRangeSaturationAmount { get; set; }

	[EditorNumberPadOptions(2, 0.0, 0.5, 0.01)]
	[EditorProperty(true, Description = "Darken Amount", HorizontalAlignment = true, MajorGrouping = 8, MinorGrouping = 2, ToolTipText = "")]
	float DynamicRangeDarkenAmount { get; set; }

	[EditorNumberPadOptions(2, 0.0, 1.0, 0.01)]
	[EditorProperty(true, Description = "Cinematic Amount", HorizontalAlignment = true, MajorGrouping = 8, MinorGrouping = 3, ToolTipText = "")]
	float DynamicRangeCinematicAmount { get; set; }

	[EditorNumberPadOptions(2, 0.0, 10000.0, 0.1)]
	[EditorProperty(true, Description = "Transition Time", HorizontalAlignment = true, MajorGrouping = 7, MinorGrouping = 3, ToolTipText = "")]
	float DynamicRangeTransitionTime { get; set; }

	[EditorNumberPadOptions(2, 0.0, 10000.0, 0.5)]
	[EditorProperty(true, Description = "HDR Transition Max", HorizontalAlignment = true, MajorGrouping = 7, MinorGrouping = 1, ToolTipText = "")]
	float DynamicRangeTransitionMaxScale { get; set; }

	[EditorNumberPadOptions(2, 0.0, 10000.0, 0.5)]
	[EditorProperty(true, Description = "HDR Transition Min", HorizontalAlignment = true, MajorGrouping = 7, MinorGrouping = 2, ToolTipText = "")]
	float DynamicRangeTransitionMinScale { get; set; }

	[EditorProperty(true, Description = "Gravity", HorizontalAlignment = true, MajorGrouping = 2, MinorGrouping = 3, ToolTipText = "")]
	[EditorNumberPadOptions(4, 0.0, 1000000.0, 0.1)]
	float Gravity { get; set; }
}
