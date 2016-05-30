using Charts.Chart.CacheFolder;
using Charts.Chart.CacheFolder.CacheInterfaces;
using Charts.Chart.ConnectorFolder;
using Charts.Chart.Identifier;
using Charts.Factories;
using System.Drawing;
using System.Windows.Forms;

namespace Charts.Chart.Wrapper
{
    /// <summary>
    /// selected zone action will be shown on right click of panel
    /// </summary>
    public class SelectedOptionPanelWrapper : IIdentifier, ICacheAble
    {
        private string identifier;
        private bool isAlreadyCreated;
        private PopupForm popupForm;
        private ChartPanel chartPanel;
        private SelectedOptionPanel selectedOptionPanel;

        public SelectedOptionPanelWrapper()
        {
        }

        public void init()
        {
            chartPanel = getConnector().with(this).getByType(typeof(ChartPanelBar)) as ChartPanelBar;
            selectedOptionPanel = new SelectedOptionPanel();
            selectedOptionPanel = Inst.getBuilder().getInitializedSelectedOptionPanel(selectedOptionPanel, chartPanel);
            initPopupform();
        }

        public void initPopupform()
        {
            popupForm = new PopupForm();
            selectedOptionPanel.Hide();
            selectedOptionPanel.Visible = false;
            popupForm.Controls.Add(selectedOptionPanel);
            //popupForm.SuspendLayout();
            //popupForm.Opacity = 0.0f;
            popupForm.Visible = true;
            popupForm.Left = chartPanel.Location.X + 500;
            popupForm.Visible = false;
            //popupForm.ResumeLayout();
            //popupForm.StartPosition = FormStartPosition.CenterParent;
        }

        public void show()
        {
            //popupForm.Show
            //popupForm.Opacity = 1.0f;
            popupForm.Visible = true;
            popupForm.TopMost = true;
            //selectedOptionPanel.Show();
            selectedOptionPanel.Visible = true;

            chartPanel.State.State = EnumChartPanelState.selectedOption.ToString();
        }

        public void hide()
        {
            popupForm.TopMost = false;
            //popupForm.Hide();
            popupForm.Visible = false;
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