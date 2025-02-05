﻿using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Composition;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.DirectX;
using Windows.UI;
using Windows.UI.Composition;

namespace BandCentral.Uwp.UI
{
    public delegate CompositionDrawingSurface LoadTimeEffectHandler(CanvasBitmap bitmap, CompositionGraphicsDevice device, Size sizeTarget);

    public class SurfaceLoader
    {
        private static bool _initialized;
        private static Compositor _compositor;
        private static CanvasDevice _canvasDevice;
        private static CompositionGraphicsDevice _compositionDevice;

        public static void Initialize(Compositor compositor)
        {
            Debug.Assert(!_initialized || compositor == _compositor);

            if (!_initialized)
            {
                _compositor = compositor;
                _canvasDevice = new CanvasDevice();
                _compositionDevice = CanvasComposition.CreateCompositionGraphicsDevice(_compositor, _canvasDevice);

                _initialized = true;
            }
        }

        public static void Uninitialize()
        {
            _compositor = null;

            if (_compositionDevice != null)
            {
                _compositionDevice.Dispose();
                _compositionDevice = null;
            }

            if (_canvasDevice != null)
            {
                _canvasDevice.Dispose();
                _canvasDevice = null;
            }

            _initialized = false;
        }

        public static bool IsInitialized => _initialized;

        public static async Task<CompositionDrawingSurface> LoadFromUri(Uri uri) => await LoadFromUri(uri, Size.Empty);

        public static async Task<CompositionDrawingSurface> LoadFromUri(Uri uri, Size sizeTarget)
        {
            Debug.Assert(_initialized);

            var bitmap = await CanvasBitmap.LoadAsync(_canvasDevice, uri);
            var sizeSource = bitmap.Size;

            if (sizeTarget.IsEmpty)
            {
                sizeTarget = sizeSource;
            }

            var surface = _compositionDevice.CreateDrawingSurface(
                sizeTarget,
                DirectXPixelFormat.B8G8R8A8UIntNormalized, 
                DirectXAlphaMode.Premultiplied);

            using (var ds = CanvasComposition.CreateDrawingSession(surface))
            {
                ds.Clear(Color.FromArgb(0, 0, 0, 0));
                ds.DrawImage(bitmap, new Rect(0, 0, sizeTarget.Width, sizeTarget.Height), new Rect(0, 0, sizeSource.Width, sizeSource.Height));
            }

            return surface;
        }

        public static CompositionDrawingSurface LoadText(string text, Size sizeTarget, CanvasTextFormat textFormat, Color textColor, Color bgColor)
        {
            Debug.Assert(_initialized);

            var surface = _compositionDevice.CreateDrawingSurface(
                sizeTarget,
                DirectXPixelFormat.B8G8R8A8UIntNormalized,
                DirectXAlphaMode.Premultiplied);

            using (var ds = CanvasComposition.CreateDrawingSession(surface))
            {
                ds.Clear(bgColor);
                ds.DrawText(text, new Rect(0, 0, sizeTarget.Width, sizeTarget.Height), textColor, textFormat);
            }

            return surface;
        }

        public static async Task<CompositionDrawingSurface> LoadFromUri(Uri uri, Size sizeTarget, LoadTimeEffectHandler loadEffectHandler)
        {
            Debug.Assert(_initialized);

            if (loadEffectHandler != null)
            {
                var bitmap = await CanvasBitmap.LoadAsync(_canvasDevice, uri);
                return loadEffectHandler(bitmap, _compositionDevice, sizeTarget);
            }
            else
            {
                return await LoadFromUri(uri, sizeTarget);
            }
        }
    }
}
