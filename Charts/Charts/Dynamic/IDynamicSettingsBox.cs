﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charts.Dynamic
{
    interface IDynamicSettingsBox
    {
        String getDynamicSettingsJSONString();
        void updateChangedDynamicData(object sender, EventArgs e);
    }
}
