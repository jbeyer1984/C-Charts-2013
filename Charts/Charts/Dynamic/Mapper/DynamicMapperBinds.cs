﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Reflection;

namespace Charts
{
    class DynamicMapperBinds : DynamicMapper
    {
        //private Panel panelToBind;

        private int labelHeight;

        public DynamicMapperBinds(Panel panelToUpdate, DynamicData dynamicData, MapperView view)
        {
            PanelToUpdate = panelToUpdate;
            DynamicData = dynamicData;
            View = view;
        }

        public void bind(Panel panelToBind)
        {
            View.RootPanel = panelToBind;
            View.build();

            PropertyInfo[] propertyInfos;
            propertyInfos = DynamicData.GetType().GetProperties();
            
            foreach (PropertyInfo property in propertyInfos) {
                //Console.WriteLine("property is {0}", property.Name);
                if (property.PropertyType == typeof(LineStyle))
                {
                    this.bindLineStyle(property);

                    //property.SetValue(dynamicData, Convert.ChangeType(lineStyle, property.PropertyType), null);
                    //property.SetValue(dynamicData, lineStyle, null);
                    //lineStyle.Pattern = DashStyle.Solid;
                } else if (property.PropertyType == typeof(Color)) {
                    this.bindColor(property);
                } else if (property.PropertyType == typeof(float)) {
                    this.bindDecimal<float>(property);
                }
                //Console.WriteLine("property is {0}", property.PropertyType);
            }
        }

        public int LabelHeight
        {
            get { return labelHeight; }
            set { labelHeight = value; }
        }

        //public Panel PanelToBind
        //{
        //    get { return panelToBind; }
        //    set { panelToBind = value; }
        //}
    }
}