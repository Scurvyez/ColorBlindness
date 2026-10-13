using System;
using System.Collections.Generic;
using Brutal.VulkanApi;
using ColorBlindness.Settings;
using Waylith.Graphics;
using Waylith.Rendering;

namespace ColorBlindness.Rendering
{
  public sealed class ColorBlindnessRenderFeature : IRenderFeature, IDisposable
  {
    private readonly ColorBlindnessPass _pass;
    private readonly IRenderPass[] _passes;
    
    private RenderTexture[] _targets = [];
    
    private int _width;
    private int _height;
    
    public bool IsActive => CbModSettings.EnableColorBlindModes;
    public IReadOnlyList<IRenderPass> Passes => _passes;

    private VkFormat ColorFormat { get; }
    private VkFormat DepthFormat { get; }
    
    public ColorBlindnessRenderFeature(ColorBlindnessPass pass, int framesInFlight, VkFormat colorFormat,
      VkFormat depthFormat, int width, int height)
    {
      _pass = pass;
      _passes = [pass];
      ColorFormat = colorFormat;
      DepthFormat = depthFormat;
      _pass.OutputSource = Current;

      EnsureSized(framesInFlight, width, height);
    }
    
    public RenderTexture Current(int frame)
    {
      return (uint)frame >= (uint)_targets.Length 
        ? null 
        : _targets[frame];
    }
    
    public object CaptureFrame(in FrameTiming timing)
    {
      if (!IsActive)
        return null;
      
      ColorBlindnessUtility.ColorBlindMode mode = ColorBlindnessUtility.GetActiveColorBlindMode();
      
      return ColorBlindnessUtility.GetPushConstants(mode);
    }
    
    public void OnRenderTargetRebuilt(IRenderTarget renderTarget)
    {
      if (renderTarget is IRenderContext renderContext)
      {
        VkExtent2D extent = renderContext.GetExtent();
        EnsureSized(renderContext.FramesInFlight, extent.Width, extent.Height);
      }
      
      _pass.OnRenderTargetRebuilt(renderTarget);
    }
    
    private void EnsureSized(int framesInFlight, int width, int height)
    {
      width = Math.Max(1, width);
      height = Math.Max(1, height);
      
      if (_targets.Length == framesInFlight && _width == width && _height == height)
        return;
      
      DisposeTargets();
      
      _width = width;
      _height = height;
      _targets = new RenderTexture[framesInFlight];
      
      for (int i = 0; i < framesInFlight; i++)
      {
        _targets[i] =
          new RenderTexture(width, height, ColorFormat, DepthFormat,
            RenderTextureFilter.Bilinear, RenderTextureWrap.Clamp,
            randomWrite: false, $"ColorBlindness{i}");
      }
    }
    
    private void DisposeTargets()
    {
      foreach (var t in _targets)
        t.Dispose();
      
      _targets = [];
    }
    
    public void Dispose()
    {
      _pass.Dispose();
      DisposeTargets();
    }
  }
}