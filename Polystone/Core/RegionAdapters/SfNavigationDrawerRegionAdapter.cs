using Prism.Regions;
using Syncfusion.UI.Xaml.NavigationDrawer;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Polystone.Core.RegionAdapters
{
    public class SfNavigationDrawerRegionAdapter : RegionAdapterBase<SfNavigationDrawer>
    {
        public SfNavigationDrawerRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, SfNavigationDrawer regionTarget)
        {
            if (region == null)
            {
                throw new ArgumentNullException(nameof(region));
            }

            if (regionTarget == null)
            {
                throw new ArgumentNullException(nameof(regionTarget));
            }

            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (FrameworkElement view in e.NewItems) { 
                        regionTarget.ContentView = view;
                    }
                } 
            };
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}
