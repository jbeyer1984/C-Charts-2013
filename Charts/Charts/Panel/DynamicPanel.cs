using Charts.Chart.CacheFolder.CacheInterfaces;
using Charts.Chart.Identifier;
using System;
using System.Windows.Forms;

namespace Charts
{
    public enum DynamicPanelState
    {
        isDrawn,
        invalidate,
        isSelected,
        notInitiialized,
        isInitialized,
        reInitialize
    }

    public class DynamicPanel : Panel, ICacheAble, IIdentifier
    {
        private DynamicPanelState state;

        private String identifier = "";
        private Boolean isAlreadyCreated = false; // @todo change condition in positive

        public DynamicPanel()
        {
        }

        public String Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        public DynamicPanelState State
        {
            get { return state; }
            set { state = value; }
        }

        public Boolean IsAlreadyCreated
        {
            get { return isAlreadyCreated = false; }
            set { isAlreadyCreated = value; }
        }
    }
}