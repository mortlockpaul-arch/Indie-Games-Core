namespace Microsoft.Xna.Framework;

/// <summary>Legacy color values provided by the FNA.Contents compatibility library.</summary>
public static class XnaColors
{
    /// <summary>White RGB channels with zero alpha, for legacy fade interpolation.</summary>
    public static Color TransparentWhite { get; } = new Color(255, 255, 255, 0);
}
