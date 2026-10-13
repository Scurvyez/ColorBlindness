using System.Runtime.InteropServices;
using Waylith.Math;

namespace ColorBlindness.Rendering
{
  // float4 - 16 bytes, vec4 - 16 bytes
  // use Marshal.SizeOf instead of a hardcoded 48 when using
  [StructLayout(LayoutKind.Sequential)]
  public struct ColorBlindnessPushConstants
  {
    public float4 R;
    public float4 G;
    public float4 B;
  }
}