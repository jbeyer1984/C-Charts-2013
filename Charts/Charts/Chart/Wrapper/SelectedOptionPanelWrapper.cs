using Charts.Chart.CacheFolder;
using Charts.Chart.CacheFolder.CacheInterfaces;
using Charts.Chart.ConnectorFolder;
using Charts.Chart.Identifier;
using Charts.Factories;
using System.Windows.Forms;

namespace Charts.Chart.Wrapper
{
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
            popupForm.Controls.Add(selectedOptionPanel);
            popupForm.StartPosition = FormStartPosition.CenterParent;
        }

        public void show()
        {
            popupForm.Show();
            popupForm.TopMost = true;
            selectedOptionPanel.Show();

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