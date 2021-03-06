﻿using Charts.Chart.Wrapper;
using System;
using System.Data;

namespace Charts.Factories
{
    /// <summary>
    /// offers panel and state build function
    /// </summary>
    public class BuilderInstance
    {
        public virtual ChartPanel getBuiltChartPanelByData(ChartPanel chartPanel, DataTable data, String identifierName = null)
        {
            Inst.getStaticCall().initIdentifierOneTime(chartPanel);
            ChartPanel chartPanelNew = getInitializedChartPanel(chartPanel, data);
            if (null != identifierName) {
                chartPanelNew.Name = identifierName;
            }
            return chartPanelNew;
        }

        protected virtual ChartPanel getInitializedChartPanel(ChartPanel chartPanel, DataTable data)
        {
            Inst.getStaticCall().initIdentifierOneTime(chartPanel);
            chartPanel.Data = data;
            chartPanel.OverwriteDataComponents.CollectionDrawerOverwrite = new CollectionDrawerOverwrite(chartPanel); //@todo depency
            chartPanel.initDataToPlot();
            chartPanel.initDataGrid();
            chartPanel.initPanel();

            return chartPanel;
        }

        public virtual SelectedOptionPanel getInitializedSelectedOptionPanel(SelectedOptionPanel selectedOptionPanel, ChartPanel chartPanel)
        {
            Inst.getStaticCall().initIdentifierOneTime(selectedOptionPanel);
            Inst.getInstance().getConnector().connect(selectedOptionPanel).by(chartPanel);
            selectedOptionPanel.init();
            return selectedOptionPanel;
        }

        public virtual DataTablePanel getInitializedDataTablePanel(DataTablePanel dataTablePanel, ChartPanel chartPanel)
        {
            Inst.getStaticCall().initIdentifierOneTime(dataTablePanel);
            Inst.getInstance().getConnector().connect(dataTablePanel).by(chartPanel);
            dataTablePanel.connectChartPanelType(chartPanel.GetType());
            dataTablePanel.init();
            return dataTablePanel;
        }

        public virtual SelectedOptionPanelWrapper getBuiltSelectedOptionPanelWrapperOneTime(SelectedOptionPanelWrapper selectedOptionWrapper, ChartPanel chartPanel)
        {
            selectedOptionWrapper = Inst.getInstance().getCache().with(chartPanel).canBeNew().getByType(typeof(SelectedOptionPanelWrapper)) as SelectedOptionPanelWrapper;
            if (selectedOptionWrapper.IsAlreadyCreated) {
                return selectedOptionWrapper;
            }

            selectedOptionWrapper.IsAlreadyCreated = true;
            Inst.getStaticCall().initIdentifierOneTime(selectedOptionWrapper);
            Inst.getInstance().getConnector().connect(selectedOptionWrapper).by(chartPanel);
            selectedOptionWrapper.init();
            return selectedOptionWrapper;
        }

        public virtual DataTablePanelWrapper getBuiltDataTablePanelWrapperOneTime(DataTablePanelWrapper dataTablePanelWrapper, ChartPanel chartPanel)
        {
            dataTablePanelWrapper = Inst.getInstance().getCache().with(chartPanel).canBeNew().getByType(typeof(DataTablePanelWrapper)) as DataTablePanelWrapper;
            if (dataTablePanelWrapper.IsAlreadyCreated) {
                return dataTablePanelWrapper;
            }

            dataTablePanelWrapper.IsAlreadyCreated = true;
            Inst.getStaticCall().initIdentifierOneTime(dataTablePanelWrapper);
            Inst.getInstance().getConnector().connect(dataTablePanelWrapper).by(chartPanel);
            dataTablePanelWrapper.connectChartPanelType(chartPanel.GetType());
            dataTablePanelWrapper.init();
            return dataTablePanelWrapper;
        }

        public virtual CollectionDrawer getBuiltCollectionDrawerBarOneTime(CollectionDrawer collectionDrawer, ChartPanel chartPanel)
        //public virtual CollectionDrawer getBuiltCollectionDrawer(CollectionDrawer collectionDrawer)
        {
            collectionDrawer = Inst.getInstance().getCache().with(chartPanel).canBeNew().getByType(typeof(CollectionDrawerBar)) as CollectionDrawerBar;
            if (collectionDrawer.IsAlreadyCreated) {
                return collectionDrawer;
            }

            collectionDrawer.IsAlreadyCreated = true;
            Inst.getStaticCall().initIdentifierOneTime(collectionDrawer);
            Inst.getInstance().getConnector().connect(collectionDrawer).by(chartPanel);
            collectionDrawer.init();
            
            return collectionDrawer;
        }
    }
}