using CefSharp;
using CefSharp.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSFrame.Browser
{
    /**
     * 获取可拖拽区域
     */
    public class DragDropHandler : IDragHandler
    {
        public Region draggableRegion = null;
        public event Action<Region> RegionsChanged;
        public bool Enable { get; set; } = true;

        public bool OnDragEnter(IWebBrowser browserControl, IBrowser browser, IDragData dragData, DragOperationsMask mask)
        {
            return false;
        }

        public void OnDraggableRegionsChanged(IWebBrowser browserControl, IBrowser browser, IList<DraggableRegion> regions)
        {
            if (browser.IsPopup == false)
            {
                draggableRegion = null;
                if (regions != null && regions.Count > 0)
                {
                    foreach (var region in regions)
                    {
                        var rect = new Rectangle(region.X, region.Y, region.Width, region.Height);

                        if (draggableRegion == null)
                        {
                            draggableRegion = new Region(rect);
                        }
                        else
                        {
                            if (region.Draggable)
                            {
                                draggableRegion.Union(rect);
                            }
                            else
                            {
                                draggableRegion.Exclude(rect);
                            }
                        }
                    }
                }

                RegionsChanged?.Invoke(draggableRegion);
            }
        }

        public void Dispose()
        {
            RegionsChanged = null;
        }

        public void OnDraggableRegionsChanged(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IList<DraggableRegion> regions)
        {
            throw new NotImplementedException();
        }
    }
}
