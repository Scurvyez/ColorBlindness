using System;
using System.Collections.Generic;
using Brutal.VulkanApi;
using ColorBlindness.Debugging;
using Waylith;
using Waylith.Graphics;
using Waylith.Rendering;
using Waylith.Rendering.Compute;
using Waylith.Rendering.PostFx;

namespace ColorBlindness.Rendering
{
  public static class ColorBlindnessRenderer
  {
    private static bool _initialized;
    
    public static void Initialize()
    {
      if (_initialized)
        return;
      
      EngineApplication app = EngineApplication.Instance;
      WindowRenderContext renderContext = app.RenderContext;
      FrameRenderer frameRenderer = app.FrameRenderer;
      CompositePass composite = FindCompositePass(frameRenderer);
      
      Func<int, RenderTexture> originalSource = 
        composite.Source ?? throw new InvalidOperationException("CompositePass has no source.");

      VkExtent2D extent = renderContext!.GetExtent();
      
      ColorBlindnessPass pass = new ColorBlindnessPass(ComputeDispatchQueue.BilinearClampSampler)
      {
        Source = originalSource
      };
      
      ColorBlindnessRenderFeature feature = new ColorBlindnessRenderFeature(pass, app.Options.FramesInFlight, 
        app.Options.ColorFormat, app.Options.DepthFormat, extent.Width, extent.Height);
      
      frameRenderer.AddFeature(feature);
      
      composite.Source = frame => feature.IsActive 
        ? feature.Current(frame) 
        : originalSource(frame);
      
      _initialized = true;
      
      //CbLog.Message("Vulkan render feature initialized.");
      //CbLog.Message($"Features after registration: {frameRenderer.Features.Count}");
    }
    
    private static CompositePass FindCompositePass(FrameRenderer frameRenderer)
    {
      IReadOnlyList<IRenderFeature> features = frameRenderer.Features;
      
      for (int i = 0; i < features.Count; i++)
      {
        IReadOnlyList<IRenderPass> passes = features[i].Passes;
        
        for (int j = 0; j < passes.Count; j++)
        {
          if (passes[j] is CompositePass composite)
            return composite;
        }
      }
      
      throw new InvalidOperationException("Could not find the renderer's CompositePass.");
    }
  }
}