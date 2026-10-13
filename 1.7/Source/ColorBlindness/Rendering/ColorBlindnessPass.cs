using System;
using System.Runtime.InteropServices;
using Brutal.VulkanApi;
using Waylith.Graphics;
using Waylith.Rendering;
using Waylith.Rendering.PostFx;

namespace ColorBlindness.Rendering
{
  public sealed class ColorBlindnessPass : PostEffectPass
  {
    private readonly VkSampler _sampler;
    
    public Func<int, RenderTexture> Source { get; set; }
    public Func<int, RenderTexture> OutputSource { get; set; }
    
    public ColorBlindnessPass(VkSampler sampler) : base(
      "ColorBlindnessPass", "CB_ScreenColorBlindness.frag.glsl",
      [0], default, Marshal.SizeOf<ColorBlindnessPushConstants>())
    {
      _sampler = sampler;
    }
    
    public override void Record(in FrameContext ctx, object featurePayload)
    {
      if (featurePayload is not ColorBlindnessPushConstants pushConstants) 
        return;
      
      RenderTexture source = Source?.Invoke(ctx.SubmissionFrameIndex);
      RenderTexture output = OutputSource?.Invoke(ctx.SubmissionFrameIndex);
      
      if (source == null || output == null)
        return;
      
      SetInput(0, source.Color, _sampler);
      SetPushConstants(pushConstants);
      
      Output = output;
      
      base.Record(in ctx, featurePayload);
    }
    
    public override void OnRenderTargetRebuilt(IRenderTarget target)
    {
      Invalidate();
    }
  }
}