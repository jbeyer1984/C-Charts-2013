using System.Windows.Forms;

namespace Charts.Chart.Zone.ZoneInterfaces
{
    public interface IExecutorByClick : IExecutor
    {
        void executeClick(object sender, MouseEventArgs e);
    }
}