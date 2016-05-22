﻿using Charts.Chart.CacheFolder;
using Charts.Chart.CacheFolder.CacheInterfaces;
using Charts.Chart.ConnectorFolder;
using Charts.Chart.Identifier;
using Charts.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Charts.Chart.Wrapper
{
    public class DataTablePanelWrapper : IIdentifier, ICacheAble
    {
        private string identifier;
        private bool isAlreadyCreated;
        private PopupForm popupForm;
        private ChartPanel chartPanel;
        private DataTablePanel dataTablePanel;


        public DataTablePanelWrapper()
        {
        }

        public void connectChartPanelType(Type type)
        {
            chartPanel = getConnector().with(this).getByType(type) as ChartPanel;
        }

        public void init()
        {
            dataTablePanel = new DataTablePanel();
            dataTablePanel = Inst.getBuilder().getInitializedDataTablePanel(dataTablePanel, chartPanel);
            initPopupform();
        }

        public void initPopupform()
        {
            popupForm = new PopupForm();
            dataTablePanel.Hide();
            popupForm.Controls.Add(dataTablePanel);
            popupForm.StartPosition = FormStartPosition.CenterParent;
        }

        public void show()
        {
            popupForm.Show();
            popupForm.TopMost = true;
            dataTablePanel.Show();

            chartPanel.State.State = EnumChartPanelState.selectedOption.ToString();
        }

        public void hide()
        {
            popupForm.TopMost = false;
            popupForm.Hide();
        }

        public Cache getCache()
        {
            return Inst.getInstance().getCache();
        }

        public Connector getConnector()
        {
            return Inst.getInstance().getConnector();
        }
        
        public string Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        public bool IsAlreadyCreated
        {
            get { return isAlreadyCreated; }
            set { isAlreadyCreated = value; }
        }
    }
}