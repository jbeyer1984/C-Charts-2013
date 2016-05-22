using System;
using System.Collections;
using System.Drawing;

namespace Charts
{
    public class DataCollection
    {
        private ArrayList dataSeriesList;
        private int dataSeriesIndex = 0;

        public static int seriesCounter = 0;

        public DataCollection()
        {
            dataSeriesList = new ArrayList();
        }

        public ArrayList DataSeriesList
        {
            get { return dataSeriesList; }
            set { dataSeriesList = value; }
        }

        public int DataSeriesIndex
        {
            get { return dataSeriesIndex; }
            set { dataSeriesIndex = value; }
        }

        public void add(DataSeries ds)
        {
            dataSeriesList.Add(ds);
            if (ds.SeriesName == "Default Name") {
                ds.SeriesName = "DataSeries" + DataCollection.seriesCounter;
                DataCollection.seriesCounter++;
            }
        }

        public void insert(int dataSeriesIndex, DataSeries ds)
        {
            dataSeriesList.Insert(dataSeriesIndex, ds);
            if (ds.SeriesName == "Default Name") {
                dataSeriesIndex = dataSeriesIndex + 1;
                ds.SeriesName = "DataSeries" +
                dataSeriesIndex.ToString();
            }
        }

        public void Remove(string dataSeriesName)
        {
            if (dataSeriesList != null) {
                for (int i = 0; i < dataSeriesList.Count; i++) {
                    DataSeries ds = (DataSeries)dataSeriesList[i];
                    if (ds.SeriesName == dataSeriesName) {
                        dataSeriesList.RemoveAt(i);
                    }
                }
            }
        }

        public void RemoveAll()
        {
            dataSeriesList.Clear();
        }
    }
}