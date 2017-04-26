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
    public class VTS__Security_MainMethods
    {
        public VTS__Security_MainMethods(ControlSystem ControlSystem, Panel Panel)
        {
            this.ControlSystem = ControlSystem;
            this.Panel = Panel;
        }
        

                public void SecuritySimple_Keypad(VTSmartObject SmartObject, KeypadButton Button)
        {
            

            switch (Button)
            {
                case KeypadButton.Misc_1:
                    throw new NotImplementedException();
                    break;

                case KeypadButton.Num_0:
                    //ControlSystem._videoServer.KeypadNumber(0);
                    break;

                case KeypadButton.Num_1:
                    //ControlSystem._videoServer.KeypadNumber(1);
                    break;

                case KeypadButton.Num_2:
                    //ControlSystem._videoServer.KeypadNumber(2);
                    break;

                case KeypadButton.Num_3:
                    //ControlSystem._videoServer.KeypadNumber(3);
                    break;

                case KeypadButton.Num_4:
                    //ControlSystem._videoServer.KeypadNumber(4);
                    break;

                case KeypadButton.Num_5:
                    //ControlSystem._videoServer.KeypadNumber(5);
                    break;

                case KeypadButton.Num_6:
                    //ControlSystem._videoServer.KeypadNumber(6);
                    break;

                case KeypadButton.Num_7:
                    //ControlSystem._videoServer.KeypadNumber(7);
                    break;

                case KeypadButton.Num_8:
                    //ControlSystem._videoServer.KeypadNumber(8);
                    break;

                case KeypadButton.Num_9:
                    //ControlSystem._videoServer.KeypadNumber(9);
                    break;

                case KeypadButton.Misc_2:
                    break;

                case KeypadButton.Null:
                    break;
            }
        }
        public void Security_Button_List_Vertical(VTSmartObject SmartObject, SmartObjectEventArgs Args)
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