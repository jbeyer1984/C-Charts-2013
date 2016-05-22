using Charts.Chart.CacheFolder.CacheInterfaces;
using Charts.Chart.Debug;
using Charts.Chart.Identifier;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Charts.Chart.StateFolder
{
    public abstract class StateAbstract : INotifyPropertyChanged, ICacheAble, IIdentifier
    {
        private String state;
        private String selectMode;
        private String globalMode;
        private string identifier;
        private bool isAlreadyCreated;
        private List<string> stateList = new List<string>(5);

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, e);
            }
        }

        protected void SetPropertyField<T>(string propertyName, ref T field, T newValue)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue)) {
                field = newValue;
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        public virtual void log(Object sender, PropertyChangedEventArgs e)
        {
            //DebugSettings.log(string.Format("count of stateLIst: {0}", stateList.Count));
            //DebugSettings.log(string.Format("capacity of stateLIst: {0}", stateList.Capacity));

            if (0 < stateList.Count) {
                string stateToCompare = stateList[stateList.Count - 1];
                if (stateToCompare.Equals(EnumChartPanelState.isMarkedSelected.ToString()) // check transitions fo state
                    && state.Equals(EnumChartPanelState.isDrawn.ToString())) {
                    DebugSettings.log(string.Format("!!transition from {0} to {1} should not exist", stateToCompare, state));
                    throw new Exception("State transition should not exist");
                }
            }

            if (stateList.Count == stateList.Capacity) {
                stateList.RemoveAt(0);
            }
            stateList.Add(state);

            DebugSettings.log(string.Format("{0} # {1}", identifier, state));
            //Console.WriteLine(state);
        }

        public String State
        {
            get { return state; }
            set { SetPropertyField<String>("state", ref state, value); }
        }

        public String SelectMode
        {
            get { return selectMode; }
            set { selectMode = value; }
        }

        public string GlobalMode
        {
            get { return globalMode; }
            set { globalMode = value; }
        }

        public String Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        public List<string> StateList
        {
            get { return stateList; }

            set { stateList = value; }
        }

        public bool IsAlreadyCreated
        {
            get { return isAlreadyCreated; }
            set { isAlreadyCreated = value; }
        }
    }
}