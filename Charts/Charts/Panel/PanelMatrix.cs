using System.Collections.Generic;

namespace Charts
{
    public class PanelMatrix
    {
        private ChartForm chartForm;
        private List<ChartPanel> matrix;

        public PanelMatrix(ChartForm chartForm)
        {
            this.chartForm = chartForm;
            this.init();
        }

        public void init()
        {
            matrix = new List<ChartPanel>();
        }

        public void add(ChartPanel chartPanel)
        {
            matrix.Add(chartPanel);
        }

        public void initSizeOfPanels()
        {
            int size = this.matrix.Count;
            //this.Width = chartParent.Size.Width - offsetPanelX;
            //this.Height = chartParent.Size.Height - 2 * offsetPanelY;
            int count = 1;
            foreach (ChartPanel chartPanel in this.matrix) {
                int chartFormWidth = (chartForm.Size.Width - 2 * chartForm.OffsetPanelX);
                int chartFormHeight = (chartForm.Size.Height - (2 * chartForm.OffsetPanelY) - 20);
                if (size > 1) {
                    chartPanel.Width = chartFormWidth / 2;
                    chartPanel.Height = chartFormHeight / 2;
                    if (count % 2 == 0) {
                        //chartPanel.Top = chartPanel.Height + chartForm.OffsetPanelY;
                        chartPanel.Left = chartPanel.Width + chartForm.OffsetPanelX;
                    } else if (count % 3 == 0) {
                        chartPanel.Top = chartPanel.Height + chartForm.OffsetPanelY;
                    }
                } else {
                    chartPanel.Width = chartFormWidth;
                    chartPanel.Height = chartFormHeight;
                }
                chartPanel.initChartArea();
                count++;
            }
        }

        public List<ChartPanel> Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }
    }
}