using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Crestron.SimplSharp;
using Crestron.SimplSharpPro;

using Crestron.ThirdPartyCommon.Interfaces;
using Crestron.ThirdPartyCommon.ComponentInterfaces;

using VTPro.Objects;
using VTPro.SmartObjects;

using myNamespace.Drivers;

using myNamespace.Constants;

namespace myNamespace.SmartObjects
{
    public class VTS__Apple_TV_MainMethods
    {
        public VTS__Apple_TV_MainMethods(ControlSystem ControlSystem, Panel Panel)
        {
            this.ControlSystem = ControlSystem;
            this.Panel = Panel;
        }
        


     
        public Panel Panel;
        public ControlSystem ControlSystem;
    }
}