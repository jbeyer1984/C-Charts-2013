using Charts.Chart.CacheFolder;
using Charts.Chart.CacheFolder.CacheInterfaces;
using Charts.Factories;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Charts
{
    public class PanelMatrix
    {
        private ChartForm chartForm;
        private List<ChartPanel> matrix;

        public PanelMatrix(ChartForm chartForm)
        {
            this.chartForm = chartForm; //@todo only need in one method
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
            int count = 1;
            foreach (ChartPanel chartPanel in this.matrix) {
                chartPanel.initChartArea();

                int chartFormWidth = (chartForm.Size.Width - 2 * chartForm.OffsetPanelX);
                int chartFormHeight = (chartForm.Size.Height - (2 * chartForm.OffsetPanelY) - 20);
                if (size > 1) {
                    chartPanel.Width = chartFormWidth / 2;
                    chartPanel.Height = chartFormHeight / 2;
                    if (count % 2 == 0) {
                        chartPanel.Left = chartPanel.Width + chartForm.OffsetPanelX;
                    } else if (count % 3 == 0) {
                        chartPanel.Top = chartPanel.Height + chartForm.OffsetPanelY;
                    }
                } else {
                    chartPanel.Width = chartFormWidth;
                    chartPanel.Height = chartFormHeight;
                }
                count++;
            }
        }

        public void addPanelsToForm(Form form)
        {
            int size = this.matrix.Count;
            for (int i = 0; i < size; i++) {
                form.Controls.Add(this.matrix[i]);
            }
        }

        public InstanceInstance getInstance()
        {
            return Inst.getInstance();
        }

        public List<ChartPanel> Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }
    }
}