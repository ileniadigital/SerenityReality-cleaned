using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Water_Volume : ScriptableRendererFeature
{
    class CustomRenderPass : ScriptableRenderPass
    {
        private Material _material;
        private RTHandle tempRenderTarget;

        public CustomRenderPass(Material mat)
        {
            _material = mat;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            // Ensure tempRenderTarget is allocated
            if (tempRenderTarget == null)
            {
                tempRenderTarget = RTHandles.Alloc(
                    cameraTextureDescriptor,
                    name: "_TemporaryColourTexture"
                );
            }

            // Configure the target and clear flag
            ConfigureTarget(tempRenderTarget);
            ConfigureClear(ClearFlag.All, Color.clear);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            // Check if the camera type is Reflection (skip if true)
            if (renderingData.cameraData.cameraType == CameraType.Reflection)
                return;

            // Create command buffer
            CommandBuffer cmd = CommandBufferPool.Get();
            using (new ProfilingScope(cmd, new ProfilingSampler("WaterVolumePass")))
            {
                // Get the camera's color target
                RTHandle source = renderingData.cameraData.renderer.cameraColorTargetHandle;

                // Check if any resource is null and log appropriate error
                if (source == null)
                {
                    Debug.LogError("Camera color target is null!");
                    return;
                }

                if (tempRenderTarget == null)
                {
                    Debug.LogError("Temporary render target is null!");
                    return;
                }

                if (_material == null)
                {
                    Debug.LogError("Material is null!");
                    return;
                }

                // Perform the blit operation
                Blitter.BlitCameraTexture(cmd, source, tempRenderTarget, _material, 0);
                Blitter.BlitCameraTexture(cmd, tempRenderTarget, source);
            }

            // Execute the command buffer
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            // Release the temporary render target
            if (tempRenderTarget != null)
            {
                tempRenderTarget.Release();
                tempRenderTarget = null;
            }
        }
    }

    [System.Serializable]
    public class Settings
    {
        public Material material;
        public RenderPassEvent renderPass = RenderPassEvent.AfterRenderingSkybox;
    }

    public Settings settings = new Settings();
    private CustomRenderPass m_ScriptablePass;

    public override void Create()
    {
        if (settings.material == null)
        {
            // Load the material from Resources folder
            settings.material = Resources.Load<Material>("Water_Volume");

            if (settings.material == null)
            {
                Debug.LogError("Water_Volume material not found in Resources folder!");
                return;
            }
        }

        // Initialize the custom render pass
        m_ScriptablePass = new CustomRenderPass(settings.material)
        {
            renderPassEvent = settings.renderPass
        };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        // Only enqueue the pass if the material is not null
        if (settings.material != null)
            renderer.EnqueuePass(m_ScriptablePass);
    }
}
