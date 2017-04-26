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
    public class VTS__ADMS_MainMethods
    {
        public VTS__ADMS_MainMethods(ControlSystem ControlSystem, Panel Panel)
        {
            this.ControlSystem = ControlSystem;
            this.Panel = Panel;
        }
        

        public void Button_List_Vertical(VTSmartObject SmartObject, SmartObjectEventArgs Args)
        {
            throw new NotImplementedException();

            //if (Args.Sig.Name.Contains("Pressed"))
            //{
            //    int joinValue = 1;
            //
            // joinValue = Int32.Parse(Regex.Match(Args.Sig.Name, @"\d+").Value);
            //}

        }


     
        public Panel Panel;
        public ControlSystem ControlSystem;
    }
}