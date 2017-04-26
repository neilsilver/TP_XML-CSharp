using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

using Crestron.SimplSharp;

using Crestron.SimplSharpPro;

using VTPro;
using VTPro.Objects;
using VTPro.SmartObjects;
using VTPro.Panels;

using le.Methods;
using $$addnamespace$$.Drivers;
using $$addnamespace$$.SmartObjects;

using Crestron.ThirdPartyCommon.Interfaces;

using $$addnamespace$$.Constants;

namespace $$addnamespace$$
{
    public class Panel : BasePanel
    {
        public Panel(ControlSystem ControlSystem, uint ID, PanelType Type)
            : base(ControlSystem, ID, Type)
        {
            this.ControlSystem = ControlSystem;

            ButtonCollection = new VTBCollection();
            FormattedTextCollection = new VTBCollection();
            SliderCollection = new VTBCollection();
            TextEntryCollection = new VTBCollection();
            SmartObjectCollection = new VTSCollection();
        }

        public override void Initialize()
        {
            base.Initialize();

            DisplayButtons = new DisplayVTButtons(this);
            DisplaySliders = new DisplayVTSliders(this);
            DisplayTextEntryBoxes = new DisplayVTTextEntryBoxes(this);

            CableBoxButtons = new CableBoxVTButtons(this);
            CableBoxSliders = new CableBoxVTSliders(this);
            CableBoxTextEntryBoxes = new CableBoxVTTextEntryBoxes(this);

            VideoServerButtons = new VideoServerVTButtons(this);
            VideoServerTextEntryBoxes = new VideoServerVTTextEntryBoxes(this);

            BlurayPlayerButtons = new BlurayPlayerVTButtons(this);
            BlurayPlayerTextBoxes = new BlurayPlayerVTTextEntryBoxes(this);

            VTSDisplayMethods = new VTSDisplayMethods(ControlSystem, this);
            VTSCableBoxMethods = new VTSCableBoxMethods(ControlSystem, this);
            VTSVideoServerMethods = new VTSVideoServerMethods(ControlSystem, this);
            VTSBlurayPlayerMethods = new VTSBlurayPlayerMethods(ControlSystem, this); 

            AddObjects();

            SetDefaultStates();
        }

        public override void HandleEvent(SigEventArgs Args)
        {
            switch (Args.Sig.Type)
            {
                case eSigType.Bool:
                    if (Args.Sig.BoolValue)
                    {
                        if (Pages.GetElement(Args.Sig.Number) != null)
                        {
                            Pages.FlipTo(Args.Sig.Number);
                        }

                        if (Subpages.GetElement(Args.Sig.Number) != null)
                        {
                            Subpages.FlipTo(Args.Sig.Number);
                        }
                    }
                    if (ButtonCollection.GetElement(VTBJoin.Digital, Args.Sig.Number) != null)
                    {
                        ButtonCollection.GetElement(VTBJoin.Digital, Args.Sig.Number).TriggerAction(VTBAction.PressButton, Args.Sig.BoolValue);
                    }

                    break;
                case eSigType.UShort:
                    if (SliderCollection.GetElement(VTBJoin.Analog, Args.Sig.Number) != null)
                    {
                        SliderCollection.GetElement(VTBJoin.Analog, Args.Sig.Number).TriggerAction(VTBAction.SetValue, Args.Sig.UShortValue);
                    }
                    break;
                case eSigType.String:
                    if (TextEntryCollection.GetElement(VTBJoin.Serial, Args.Sig.Number) != null)
                    {
                        TextEntryCollection.GetElement(VTBJoin.Serial, Args.Sig.Number).TriggerAction(VTBAction.GatherTextInput, Args.Sig.StringValue);
                    }
                    break;
            }
        }

        private void AddObjects()
        {
            //Page/SubPages
            AddPages();
            AddSubpages();

            //Objects
            AddFormattedText();
            AddSliders();
            AddButtons();
            AddTextEntry();
        }

        private void AddPages()
        {
            //addpagesPages.AddElement(3,"Device Control"
Pages.AddElement(0,"DeviceControl_CABLE"
Pages.AddElement(0,"DeviceControl_CODEC"
Pages.AddElement(0,"event_parameters"
Pages.AddElement(200,"Event-ADD"
Pages.AddElement(0,"Event-EDIT"
Pages.AddElement(0,"Home"
Pages.AddElement(0,"Lighting Loads"
Pages.AddElement(0,"Lighting Preset Name_SAVE"
Pages.AddElement(0,"Lighting_Load_Object"
Pages.AddElement(0,"Lighting-Loads"
Pages.AddElement(0,"Lights"
Pages.AddElement(100,"Preset Add"
Pages.AddElement(0,"Preset Edit"
Pages.AddElement(4,"Scheduling"
Pages.AddElement(0,"VC_Call_Incoming"
Pages.AddElement(2,"Video Switching"
//end page
        }

        private void AddSubpages()
        {
            #region Device Control
#endregion 
#region DeviceControl_CABLE
#endregion 
#region DeviceControl_CODEC
#endregion 
#region event_parameters
#endregion 
#region Event-ADD
#endregion 
#region Event-EDIT
#endregion 
#region Home
#endregion 
#region Lighting Loads
#endregion 
#region Lighting Preset Name_SAVE
#endregion 
#region Lighting_Load_Object
#endregion 
#region Lighting-Loads
#endregion 
#region Lights
#endregion 
#region Preset Add
#endregion 
#region Preset Edit
#endregion 
#region Scheduling
#endregion 
#region VC_Call_Incoming
#endregion 
#region Video Switching
#endregion 

        }

        private void AddTextEntry()
        {
           
        }

        private void AddFormattedText()
        {
            
        }

        private void AddSliders()
        {
           
        }

        private void AddButtons()
        {
           #region Device Control
#endregion 
#region DeviceControl_CABLE
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 302}, { VTBJoin.Enable, 302 } },InterlockList, "CableBox - FScan", DeviceControl_CABLE_Buttons.CableBox_-_FScan));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 305}, { VTBJoin.Enable, 305 } },InterlockList, "CableBox - FSkip", DeviceControl_CABLE_Buttons.CableBox_-_FSkip));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 300}, { VTBJoin.Enable, 300 } },InterlockList, "CableBox - Pause", DeviceControl_CABLE_Buttons.CableBox_-_Pause));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 303}, { VTBJoin.Enable, 303 } },InterlockList, "CableBox - Play", DeviceControl_CABLE_Buttons.CableBox_-_Play));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 301}, { VTBJoin.Enable, 301 } },InterlockList, "CableBox - RScan", DeviceControl_CABLE_Buttons.CableBox_-_RScan));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 304}, { VTBJoin.Enable, 304 } },InterlockList, "CableBox - RSkip", DeviceControl_CABLE_Buttons.CableBox_-_RSkip));
#endregion 
#region DeviceControl_CODEC
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 302}, { VTBJoin.Enable, 302 } },InterlockList, "CableBox - FScan_2", DeviceControl_CODEC_Buttons.CableBox_-_FScan_2));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 305}, { VTBJoin.Enable, 305 } },InterlockList, "CableBox - FSkip_2", DeviceControl_CODEC_Buttons.CableBox_-_FSkip_2));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 301}, { VTBJoin.Enable, 301 } },InterlockList, "CableBox - RScan_2", DeviceControl_CODEC_Buttons.CableBox_-_RScan_2));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 304}, { VTBJoin.Enable, 304 } },InterlockList, "CableBox - RSkip_2", DeviceControl_CODEC_Buttons.CableBox_-_RSkip_2));
#endregion 
#region event_parameters
#endregion 
#region Event-ADD
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 63}, { VTBJoin.Enable,  } },InterlockList, "Button_1_7", Event-ADD_Buttons.Button_1_7));
#endregion 
#region Event-EDIT
#endregion 
#region Home
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 2}, { VTBJoin.Enable,  } },InterlockList, "Button_1", Home_Buttons.Button_1));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 3}, { VTBJoin.Enable,  } },InterlockList, "Button_1_1", Home_Buttons.Button_1_1));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 4}, { VTBJoin.Enable,  } },InterlockList, "Button_1_1_1", Home_Buttons.Button_1_1_1));
#endregion 
#region Lighting Loads
#endregion 
#region Lighting Preset Name_SAVE
#endregion 
#region Lighting_Load_Object
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 1}, { VTBJoin.Enable,  } },InterlockList, "Button_1_11", Lighting_Load_Object_Buttons.Button_1_11));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 4}, { VTBJoin.Enable,  } },InterlockList, "Button_1_1_2", Lighting_Load_Object_Buttons.Button_1_1_2));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 2}, { VTBJoin.Enable,  } },InterlockList, "Button_42", Lighting_Load_Object_Buttons.Button_42));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 3}, { VTBJoin.Enable,  } },InterlockList, "Button_43", Lighting_Load_Object_Buttons.Button_43));
#endregion 
#region Lighting-Loads
#endregion 
#region Lights
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 52}, { VTBJoin.Enable,  } },InterlockList, "Button_1_17", Lights_Buttons.Button_1_17));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 51}, { VTBJoin.Enable,  } },InterlockList, "Button_1_2", Lights_Buttons.Button_1_2));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 53}, { VTBJoin.Enable,  } },InterlockList, "Button_39", Lights_Buttons.Button_39));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 50}, { VTBJoin.Enable,  } },InterlockList, "Button_40", Lights_Buttons.Button_40));
#endregion 
#region Preset Add
#endregion 
#region Preset Edit
#endregion 
#region Scheduling
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 60}, { VTBJoin.Enable,  } },InterlockList, "Button_10", Scheduling_Buttons.Button_10));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 62}, { VTBJoin.Enable,  } },InterlockList, "Button_11", Scheduling_Buttons.Button_11));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 4}, { VTBJoin.Enable,  } },InterlockList, "Button_1_1_3", Scheduling_Buttons.Button_1_1_3));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 3}, { VTBJoin.Enable,  } },InterlockList, "Button_1_3", Scheduling_Buttons.Button_1_3));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 61}, { VTBJoin.Enable,  } },InterlockList, "Button_1_6", Scheduling_Buttons.Button_1_6));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 2}, { VTBJoin.Enable,  } },InterlockList, "Button_5", Scheduling_Buttons.Button_5));
#endregion 
#region VC_Call_Incoming
#endregion 
#region Video Switching
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 6}, { VTBJoin.Enable,  } },InterlockList, "Button_45", Video Switching_Buttons.Button_45));
ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Press, 5}, { VTBJoin.Enable,  } },InterlockList, "Button_46", Video Switching_Buttons.Button_46));
#endregion 

        }   

        public override void InitializeSmartObjects()
        {
            List<string> IRPorts = new List<string>();
            List<string> COMPorts = new List<string>();

            foreach (IROutputPort port in ControlSystem.IROutputPorts)
            {
                IRPorts.Add(port.ID.ToString());
            }                       

            foreach (var port in ControlSystem.ComPorts)
            {
                COMPorts.Add(port.ID.ToString());
            }

            #region VideoServer
            SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.VideoServer_Footer_SubpageSelection],
                "VideoServer Subpage Selection", 5, VTSVideoServerMethods.SubpageSelection, true));

            SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.VideoServer_DriverSelection_DriverTypes],
                "VideoServer Driver Types", 3, VTSVideoServerMethods.DriverTypeSelection, false));

            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.VideoServer_IRDriverPorts],
                "VideoServer IR Driver Ports", IRPorts, VTSVideoServerMethods.IRDriverPorts, true));

            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.VideoServer_IRDrivers],
                "VideoServer IR Drivers", DriverHelper.IRVideoServers.Keys.ToList(), VTSVideoServerMethods.IRDrivers, true));

            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.VideoServer_SerialDriverPorts],
                "VideoServer Serial Driver Ports", COMPorts, VTSVideoServerMethods.SerialDriverPorts, true));

            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.VideoServer_SerialDrivers],
                "VideoServer Serial Drivers", DriverHelper.SerialVideoServers.Keys.ToList(), VTSVideoServerMethods.SerialDrivers, true));

            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.VideoServer_TCPIPDrivers],
                "VideoServer TCPIP Drivers", DriverHelper.TcpVideoServers.Keys.ToList(), VTSVideoServerMethods.TCPIPDrivers, true));

            SmartObjectCollection.AddElement(new VTKeypad(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Enable, 1040 } }, ExtendedInterface.SmartObjects[VTSObjects.VideoServer_Keypad],
                "VideoServer Control Keypad", VTSVideoServerMethods.TransportKeypad));

            SmartObjectCollection.AddElement(new VTDPad(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Enable, 1041 } }, ExtendedInterface.SmartObjects[VTSObjects.VideoServer_DPad],
                "VideoServer Control DPad", VTSVideoServerMethods.TransportDPad));
            #endregion

            #region CableBox
            SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.Source_Footer_SubpageSelection],
                    "CableBox Subpage Selection", 5, VTSCableBoxMethods.SubpageSelection, true));

            SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.Source_DriverSelection_DriverTypes],
                "CableBox Driver Types", 3, VTSCableBoxMethods.DriverTypeSelection, false));

            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.Source_IRDriverPorts],
                "CableBox IR Driver Ports", IRPorts, VTSCableBoxMethods.IRDriverPorts, true));

            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.Source_IRDrivers],
                "CableBox IR Drivers", DriverHelper.IRCableBoxes.Keys.ToList(), VTSCableBoxMethods.IRDrivers, true));

            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.Source_SerialDriverPorts],
                "CableBox Serial Driver Ports", COMPorts, VTSCableBoxMethods.SerialDriverPorts, true));

            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.Source_SerialDrivers],
                "CableBox Serial Drivers", DriverHelper.SerialCableBoxes.Keys.ToList(), VTSCableBoxMethods.SerialDrivers, true));

            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.Source_TCPIPDrivers],
                "CableBox TCPIP Drivers", DriverHelper.TcpCableBoxes.Keys.ToList(), VTSCableBoxMethods.TCPIPDrivers, true));

            SmartObjectCollection.AddElement(new VTKeypad(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Enable, 111 } } , ExtendedInterface.SmartObjects[VTSObjects.Source_Keypad],
                "CableBox Control Keypad", VTSCableBoxMethods.TransportKeypad));

            SmartObjectCollection.AddElement(new VTDPad(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Enable, 110 } }, ExtendedInterface.SmartObjects[VTSObjects.Source_DPad],
                "CableBox Control DPad", VTSCableBoxMethods.TransportDPad));
            #endregion

            #region Display
            SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.Display_Footer_SubpageSelection],
                "Display Subpage Selection", 5, VTSDisplayMethods.SubpageSelection, true));

            SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.Display_DriverSelection_DriverTypes],
                "Display Driver Type List", 3, VTSDisplayMethods.DriverTypes, false));

            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.Display_SerialDrivers],
                 "Display Serial Driver List", DriverHelper.SerialDisplays.Keys.ToList(), VTSDisplayMethods.SerialDrivers, true));

            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.Display_SerialDriverPorts],
                 "Display Serial Driver COM Port List", COMPorts, VTSDisplayMethods.SerialDriverCOMPorts, true));

            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.Display_TCPIPDrivers],
                "Display TCP/IP Driver List", DriverHelper.TcpDisplays.Keys.ToList(), VTSDisplayMethods.TcpIpDrivers, true));

            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.Display_IRDriverPorts],
                "Display IR Driver Port List", IRPorts, VTSDisplayMethods.IRPorts, true));

            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.Display_IRDrivers],
                "Display IR Driver List", DriverHelper.IRDisplays.Keys.ToList(), VTSDisplayMethods.IRDrivers, true));
            #endregion

            #region BlurayPlayer
            SmartObjectCollection.AddElement(new VTDPad(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Enable, 1541 } }, ExtendedInterface.SmartObjects[VTSObjects.BlurayPlayer_DPad],
                "BlurayPlayer Control DPad", VTSBlurayPlayerMethods.TransportDPad));
            SmartObjectCollection.AddElement(new VTKeypad(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Enable, 1540 } }, ExtendedInterface.SmartObjects[VTSObjects.BlurayPlayer_Keypad],
                "BlurayPlayer Keypad", VTSBlurayPlayerMethods.TransportKeypad));
            SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.BlurayPlayer_Footer_SubpageSelection],
                "BlurayPlayer Subpage Selection", 5, VTSBlurayPlayerMethods.SubpageSelection, true));
            SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.BlurayPlayer_DriverSelection_DriverTypes],
                "BlurayPlayer Driver Types", 3, VTSBlurayPlayerMethods.DriverTypeSelection, false));
            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.BlurayPlayer_IRDriverPorts],
                "BlurayPlayer IR Driver Ports", IRPorts, VTSBlurayPlayerMethods.IRDriverPorts, true));
            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.BlurayPlayer_IRDrivers],
                "BlurayPlayer IR Drivers", DriverHelper.IRBlurayPlayers.Keys.ToList(), VTSBlurayPlayerMethods.IRDrivers, true));
            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.BlurayPlayer_SerialDriverPorts],
                "BlurayPlayer Serial Driver Ports", COMPorts, VTSBlurayPlayerMethods.SerialDriverPorts, true));
            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.BlurayPlayer_SerialDrivers],
                "BlurayPlayer Serial Drivers", DriverHelper.SerialBlurayPlayers.Keys.ToList(), VTSBlurayPlayerMethods.SerialDrivers, true));
            SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.BlurayPlayer_TCPIPDrivers],
                "BlurayPlayer TCPIP Drivers", DriverHelper.TcpBlurayPlayers.Keys.ToList(), VTSBlurayPlayerMethods.TCPIPDrivers, true));
            #endregion
        }

        private bool CheckSmartObject(uint ID)
        {
            return ExtendedInterface.SmartObjects.Contains(ID);
        }

        public void SetDefaultStates()
        {
            //Display Settings
            TextEntryCollection.GetElement("TCP/IP Display IP Address").TriggerAction(VTBAction.SetIndirectText, "0.0.0.0");
            TextEntryCollection.GetElement("TCP/IP CableBox IP Address").TriggerAction(VTBAction.SetIndirectText, "0.0.0.0");
            TextEntryCollection.GetElement("TCP/IP VideoServer IP Address").TriggerAction(VTBAction.SetIndirectText, "0.0.0.0");
            TextEntryCollection.GetElement("TCP/IP BlurayPlayer IP Address").TriggerAction(VTBAction.SetIndirectText, "0.0.0.0");

            FormattedTextCollection.GetElement("DisplayHeader").TriggerAction(VTBAction.SetIndirectText, "Unknown");
            FormattedTextCollection.GetElement("CableBox ConnectionStatus").TriggerAction(VTBAction.SetIndirectText, "Unknown");
            FormattedTextCollection.GetElement("VideoServer ConnectionStatus").TriggerAction(VTBAction.SetIndirectText, "Unknown");
            FormattedTextCollection.GetElement("BlurayPlayer ConnectionStatus").TriggerAction(VTBAction.SetIndirectText, "Unknown");
        }

        public void SetVideoServerSupports()
        {
            if (ControlSystem._videoServer == null)
            {
                return;
            }
            IBasicVideoServer videoServer = ControlSystem._videoServer;

            videoServer.CustomLogger = VideoServerLogOut;
            videoServer.RxOut += VideoServerRxOut;

            #region Header
            if (videoServer.Description.Contains("Generic IR"))
            {
                FormattedTextCollection.GetElement("VideoServer ConnectionStatus").TriggerAction(VTBAction.SetIndirectText, "IR Port Ready");
            }
            else
            {
                FormattedTextCollection.GetElement("VideoServer ConnectionStatus").TriggerAction(VTBAction.SetIndirectText, videoServer.Connected ? "Connected" : "Disconnected");
            }
            #endregion

            #region Transport
            ButtonCollection.GetElement("VideoServer Pause").TriggerAction(VTBAction.SetEnableState, videoServer.SupportsPause);
            ButtonCollection.GetElement("VideoServer Rewind").TriggerAction(VTBAction.SetEnableState, videoServer.SupportsReverseScan);
            ButtonCollection.GetElement("VideoServer Forward").TriggerAction(VTBAction.SetEnableState, videoServer.SupportsForwardScan);
            ButtonCollection.GetElement("VideoServer Play").TriggerAction(VTBAction.SetEnableState, videoServer.SupportsPlay);
            ButtonCollection.GetElement("VideoServer Repeat").TriggerAction(VTBAction.SetEnableState, videoServer.SupportsRepeat);
            ButtonCollection.GetElement("VideoServer Next").TriggerAction(VTBAction.SetEnableState, videoServer.SupportsForwardSkip);
            ButtonCollection.GetElement("VideoServer Previous").TriggerAction(VTBAction.SetEnableState, videoServer.SupportsReverseSkip);
            ButtonCollection.GetElement("VideoServer Stop").TriggerAction(VTBAction.SetEnableState, videoServer.SupportsStop);

            SmartObjectCollection.GetElement("VideoServer Control DPad").TriggerAction(VTSAction.SetEnabledState, videoServer.SupportsArrowKeys);

            ButtonCollection.GetElement("VideoServer Home").TriggerAction(VTBAction.SetEnableState, videoServer.SupportsHome);
            ButtonCollection.GetElement("VideoServer Back").TriggerAction(VTBAction.SetEnableState, videoServer.SupportsBack);
            ButtonCollection.GetElement("VideoServer Clear").TriggerAction(VTBAction.SetEnableState, videoServer.SupportsClear);
            ButtonCollection.GetElement("VideoServer Return").TriggerAction(VTBAction.SetEnableState, videoServer.SupportsReturn);
            ButtonCollection.GetElement("VideoServer Menu").TriggerAction(VTBAction.SetEnableState, videoServer.SupportsMenu);
            ButtonCollection.GetElement("VideoServer Exit").TriggerAction(VTBAction.SetEnableState, videoServer.SupportsExit);
            ButtonCollection.GetElement("VideoServer Backspace").TriggerAction(VTBAction.SetEnableState, videoServer.SupportsKeypadBackSpace);

            SmartObjectCollection.GetElement("VideoServer Control Keypad").TriggerAction(VTSAction.SetEnabledState, videoServer.SupportsKeypadNumber);
            #endregion

            #region Info
            FormattedTextCollection.GetElement("VideoServer Description").TriggerAction(VTBAction.SetIndirectText, videoServer.Description);
            FormattedTextCollection.GetElement("VideoServer GUID").TriggerAction(VTBAction.SetIndirectText, videoServer.Guid.ToString());
            FormattedTextCollection.GetElement("VideoServer Manufacturer").TriggerAction(VTBAction.SetIndirectText, videoServer.Manufacturer);
            FormattedTextCollection.GetElement("VideoServer Version").TriggerAction(VTBAction.SetIndirectText, videoServer.Version);
            FormattedTextCollection.GetElement("VideoServer VersionDate").TriggerAction(VTBAction.SetIndirectText, videoServer.VersionDate.ToString());
            FormattedTextCollection.GetElement("VideoServer SupportsFeedback").TriggerAction(VTBAction.SetIndirectText, videoServer.SupportsFeedback.ToString());

            if (ControlSystem._videoServer.Description.Contains("Generic IR"))
            {
                ButtonCollection.GetElement("VideoServer EnableLogging").TriggerAction(VTBAction.SetEnableState, false);
                ButtonCollection.GetElement("VideoServer DisableLogging").TriggerAction(VTBAction.SetEnableState, false);
            }
            else
            {
                ButtonCollection.GetElement("VideoServer EnableLogging").TriggerAction(VTBAction.SetEnableState, true);
                ButtonCollection.GetElement("VideoServer DisableLogging").TriggerAction(VTBAction.SetEnableState, true);
            }

            ButtonCollection.GetElement("VideoServer DisableLogging").TriggerAction(VTBAction.PressButton, true);
            #endregion

            #region Advanced
            if (videoServer.Description.Contains("Generic IR"))
            {
                ButtonCollection.GetElement("VideoServer EnableRxOut").TriggerAction(VTBAction.SetEnableState, false);
                ButtonCollection.GetElement("VideoServer DisableRxOut").TriggerAction(VTBAction.SetEnableState, false);
            }
            else
            {
                ButtonCollection.GetElement("VideoServer EnableRxOut").TriggerAction(VTBAction.SetEnableState, true);
                ButtonCollection.GetElement("VideoServer DisableRxOut").TriggerAction(VTBAction.SetEnableState, true);
            }

            ButtonCollection.GetElement("VideoServer DisableRxOut").TriggerAction(VTBAction.PressButton, true);
            #endregion
        }

        public void SetCableBoxSupports()
        {
            if (ControlSystem._cableBox == null)
            {
                return;
            }

            IBasicCableBox cableBox = ControlSystem._cableBox;

            ControlSystem._cableBox.CustomLogger = CableBoxLogOut;
            ControlSystem._cableBox.RxOut += CableBoxRxOut;

            #region Header
            if (ControlSystem._cableBox.Description.Contains("Generic IR"))
            {
                FormattedTextCollection.GetElement("CableBox ConnectionStatus").TriggerAction(VTBAction.SetIndirectText, "IR Port Ready");
            }
            else
            {
                FormattedTextCollection.GetElement("CableBox ConnectionStatus").TriggerAction(VTBAction.SetIndirectText, cableBox.Connected ? "Connected" : "Disconnected");
            }
            #endregion

            #region Transport/left
            ButtonCollection.GetElement("CableBox Pause").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsPause);
            ButtonCollection.GetElement("CableBox Rewind").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsReverseScan);
            ButtonCollection.GetElement("CableBox Forward").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsForwardScan);
            ButtonCollection.GetElement("CableBox Play").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsPlay);
            ButtonCollection.GetElement("CableBox Repeat").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsRepeat);
            ButtonCollection.GetElement("CableBox Next").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsForwardSkip);
            ButtonCollection.GetElement("CableBox Record").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsRecord);
            ButtonCollection.GetElement("CableBox Live").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsLive);

            FormattedTextCollection.GetElement("CableBox Volume").TriggerAction(VTBAction.SetVisibilityState, cableBox.SupportsVolumePercentFeedback);
            FormattedTextCollection.GetElement("CableBox Channel").TriggerAction(VTBAction.SetVisibilityState, cableBox.SupportsChannelFeedback);

            FormattedTextCollection.GetElement("CableBox Volume").TriggerAction(VTBAction.SetIndirectText, "Unknown");
            FormattedTextCollection.GetElement("CableBox Channel").TriggerAction(VTBAction.SetIndirectText, "Unknown");

            ButtonCollection.GetElement("CableBox VolumeUp").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsChangeVolume);
            ButtonCollection.GetElement("CableBox VolumeDown").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsChangeVolume);
            ButtonCollection.GetElement("CableBox ChannelUp").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsChangeChannel);
            ButtonCollection.GetElement("CableBox ChannelDown").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsChangeChannel);

            SmartObjectCollection.GetElement("CableBox Control DPad").TriggerAction(VTSAction.SetEnabledState, cableBox.SupportsArrowKeys);
            #endregion

            #region Transport/right
            ButtonCollection.GetElement("CableBox Yellow").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsColorButtons);
            ButtonCollection.GetElement("CableBox Blue").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsColorButtons);
            ButtonCollection.GetElement("CableBox Red").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsColorButtons);
            ButtonCollection.GetElement("CableBox Green").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsColorButtons);

            ButtonCollection.GetElement("CableBox Power On").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsDiscretePower);
            ButtonCollection.GetElement("CableBox Power Off").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsDiscretePower);
            ButtonCollection.GetElement("CableBox Power Toggle").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsTogglePower);

            ButtonCollection.GetElement("CableBox Home").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsHome);
            ButtonCollection.GetElement("CableBox Back").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsBack);
            ButtonCollection.GetElement("CableBox Clear").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsClear);
            ButtonCollection.GetElement("CableBox Info").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsInfo);
            ButtonCollection.GetElement("CableBox Return").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsReturn);
            ButtonCollection.GetElement("CableBox Guide").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsGuide);
            ButtonCollection.GetElement("CableBox Menu").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsMenu);
            ButtonCollection.GetElement("CableBox Favorite").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsFavorite);
            ButtonCollection.GetElement("CableBox Exit").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsExit);

            SmartObjectCollection.GetElement("CableBox Control Keypad").TriggerAction(VTSAction.SetEnabledState, cableBox.SupportsKeypadNumber);
            #endregion

            #region Info
            FormattedTextCollection.GetElement("CableBox Description").TriggerAction(VTBAction.SetIndirectText, cableBox.Description);
            FormattedTextCollection.GetElement("CableBox GUID").TriggerAction(VTBAction.SetIndirectText, cableBox.Guid.ToString());
            FormattedTextCollection.GetElement("CableBox Manufacturer").TriggerAction(VTBAction.SetIndirectText, cableBox.Manufacturer);
            FormattedTextCollection.GetElement("CableBox Version").TriggerAction(VTBAction.SetIndirectText, cableBox.Version);
            FormattedTextCollection.GetElement("CableBox VersionDate").TriggerAction(VTBAction.SetIndirectText, cableBox.VersionDate.ToString());
            FormattedTextCollection.GetElement("CableBox SupportsFeedback").TriggerAction(VTBAction.SetIndirectText, cableBox.SupportsFeedback.ToString());

            if (ControlSystem._cableBox.Description.Contains("Generic IR"))
            {
                ButtonCollection.GetElement("CableBox EnableLogging").TriggerAction(VTBAction.SetEnableState, false);
                ButtonCollection.GetElement("CableBox DisableLogging").TriggerAction(VTBAction.SetEnableState, false);
            }
            else
            {
                ButtonCollection.GetElement("CableBox EnableLogging").TriggerAction(VTBAction.SetEnableState, true);
                ButtonCollection.GetElement("CableBox DisableLogging").TriggerAction(VTBAction.SetEnableState, true);
            }

            ButtonCollection.GetElement("CableBox DisableLogging").TriggerAction(VTBAction.PressButton, true);
            #endregion

            #region Advanced
            SliderCollection.GetElement("CableBox WarmUpTime").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsWarmUpTime);
            SliderCollection.GetElement("CableBox CoolDownTime").TriggerAction(VTBAction.SetEnableState, cableBox.SupportsCoolDownTime);

            SliderCollection.GetElement("CableBox WarmUpTime").TriggerAction(VTBAction.UpdateValue, cableBox.WarmUpTime);
            SliderCollection.GetElement("CableBox CoolDownTime").TriggerAction(VTBAction.UpdateValue, cableBox.CoolDownTime);

            if (ControlSystem._cableBox.Description.Contains("Generic IR"))
            {
                ButtonCollection.GetElement("CableBox EnableRxOut").TriggerAction(VTBAction.SetEnableState, false);
                ButtonCollection.GetElement("CableBox DisableRxOut").TriggerAction(VTBAction.SetEnableState, false);
            }
            else
            {
                ButtonCollection.GetElement("CableBox EnableRxOut").TriggerAction(VTBAction.SetEnableState, true);
                ButtonCollection.GetElement("CableBox DisableRxOut").TriggerAction(VTBAction.SetEnableState, true);
            }

            ButtonCollection.GetElement("CableBox DisableRxOut").TriggerAction(VTBAction.PressButton, true);
            #endregion

            #region Feedback
            FormattedTextCollection.GetElement("CableBox PowerFb").TriggerAction(VTBAction.SetIndirectText, "Unknown");
            FormattedTextCollection.GetElement("CableBox MuteFb").TriggerAction(VTBAction.SetIndirectText, "Unknown");
            FormattedTextCollection.GetElement("CableBox ChannelFb").TriggerAction(VTBAction.SetIndirectText, "Unknown");
            #endregion
        }

        public void SetDisplaySupports()
        {
            if (ControlSystem._display == null)
            {
                return;
            }

            IBasicVideoDisplay display = ControlSystem._display;

            display.CustomLogger = DisplayLogOut;
            display.RxOut += DisplayRxOut;

            #region Header
            if (ControlSystem._display.Description.Contains("Generic IR"))
            {
                FormattedTextCollection.GetElement("DisplayHeader").TriggerAction(VTBAction.SetIndirectText, "IR Port Ready");
            }
            else
            {
                FormattedTextCollection.GetElement("DisplayHeader").TriggerAction(VTBAction.SetIndirectText, display.Connected ? "Connected" : "Disconnected");
            }
            #endregion

            #region Info
            FormattedTextCollection.GetElement("DisplayDescription").TriggerAction(VTBAction.SetIndirectText, display.Description);
            FormattedTextCollection.GetElement("DisplayGUID").TriggerAction(VTBAction.SetIndirectText, display.Guid.ToString());
            FormattedTextCollection.GetElement("DisplayManufacturer").TriggerAction(VTBAction.SetIndirectText, display.Manufacturer);
            FormattedTextCollection.GetElement("DisplayVersion").TriggerAction(VTBAction.SetIndirectText, display.Version);
            FormattedTextCollection.GetElement("DisplayVersionDate").TriggerAction(VTBAction.SetIndirectText, display.VersionDate.ToString(CultureInfo.InvariantCulture));
            FormattedTextCollection.GetElement("DisplaySupportsFeedback").TriggerAction(VTBAction.SetIndirectText, display.SupportsFeedback.ToString());
            #endregion

            #region Control
            //Valid input sources
            for (uint i = 0; i < display.GetUsableInputs().Length; i++)
            {
                VTButton button = ButtonCollection.GetElement("DisplaySourceSelect" + i) as VTButton;
                if (button != null)
                {
                    button.TriggerAction(VTBAction.SetFeedback, false);
                    button.TriggerAction(VTBAction.SetEnableState, true);
                    button.TriggerAction(VTBAction.SetIndirectText, display.GetUsableInputs()[i].InputType.ToString());
                }
            }

            //Non-valid input sources
            for (var i = (uint)display.GetUsableInputs().Length; i < 16; i++)
            {
                VTButton button = ButtonCollection.GetElement("DisplaySourceSelect" + i) as VTButton;
                if (button != null)
                {
                    button.TriggerAction(VTBAction.SetEnableState, false);
                    button.TriggerAction(VTBAction.SetIndirectText, String.Empty);
                }
            }

            ButtonCollection.GetElement("DisplayPowerOff").TriggerAction(VTBAction.SetEnableState, display.SupportsDiscretePower);
            ButtonCollection.GetElement("DisplayPowerOn").TriggerAction(VTBAction.SetEnableState, display.SupportsDiscretePower);
            ButtonCollection.GetElement("DisplayPowerToggle").TriggerAction(VTBAction.SetEnableState, display.SupportsTogglePower);

            ButtonCollection.GetElement("DisplayPowerOff").TriggerAction(VTBAction.SetFeedback, false);
            ButtonCollection.GetElement("DisplayPowerOn").TriggerAction(VTBAction.SetFeedback, false);
            ButtonCollection.GetElement("DisplayPowerToggle").TriggerAction(VTBAction.SetFeedback, false);

            ButtonCollection.GetElement("DisplayMuteOff").TriggerAction(VTBAction.SetEnableState, display.SupportsDiscreteMute);
            ButtonCollection.GetElement("DisplayMuteOn").TriggerAction(VTBAction.SetEnableState, display.SupportsDiscreteMute);
            ButtonCollection.GetElement("DisplayMuteToggle").TriggerAction(VTBAction.SetEnableState, display.SupportsMute);

            ButtonCollection.GetElement("DisplayMuteOff").TriggerAction(VTBAction.SetFeedback, false);
            ButtonCollection.GetElement("DisplayMuteOn").TriggerAction(VTBAction.SetFeedback, false);
            ButtonCollection.GetElement("DisplayMuteToggle").TriggerAction(VTBAction.SetFeedback, false);

            SliderCollection.GetElement("DisplayVolume").TriggerAction(VTBAction.SetVisibilityState, display.SupportsVolumePercentFeedback);
            SliderCollection.GetElement("DisplayVolume").TriggerAction(VTBAction.UpdateValue, (ushort)0);

            ButtonCollection.GetElement("DisplayVolumeUp").TriggerAction(VTBAction.SetVisibilityState, display.SupportsChangeVolume);
            ButtonCollection.GetElement("DisplayVolumeDown").TriggerAction(VTBAction.SetVisibilityState, display.SupportsChangeVolume);
            #endregion

            #region Settings
            ButtonCollection.GetElement("DisplayDisconnect").TriggerAction(VTBAction.SetEnableState, display.SupportsDisconnect);
            ButtonCollection.GetElement("DisplayReconnect").TriggerAction(VTBAction.SetEnableState, display.SupportsReconnect);
            #endregion

            #region Feedback
            if (display.SupportsFeedback)
            {//Feedback is supported
                FormattedTextCollection.GetElement("DisplayPowerFeedback").TriggerAction(VTBAction.SetIndirectText, "Unknown");
                FormattedTextCollection.GetElement("DisplayMuteFeedback").TriggerAction(VTBAction.SetIndirectText, "Unknown");
                FormattedTextCollection.GetElement("DisplayInputFeedback").TriggerAction(VTBAction.SetIndirectText, "Unknown");
            }
            else
            {//Feedback is not supported
                FormattedTextCollection.GetElement("DisplayPowerFeedback").TriggerAction(VTBAction.SetIndirectText, "Not supported");
                FormattedTextCollection.GetElement("DisplayMuteFeedback").TriggerAction(VTBAction.SetIndirectText, "Not supported");
                FormattedTextCollection.GetElement("DisplayInputFeedback").TriggerAction(VTBAction.SetIndirectText, "Not supported");
            }
            #endregion

            #region Advanced
            FormattedTextCollection.GetElement("DisplayCoolDownTime").TriggerAction(VTBAction.SetIndirectText, display.CoolDownTime.ToString());
            FormattedTextCollection.GetElement("DisplayWarmUpTime").TriggerAction(VTBAction.SetIndirectText, display.CoolDownTime.ToString());

            SliderCollection.GetElement("DisplaySetWarmUpTime").TriggerAction(VTBAction.SetEnableState, display.SupportsWarmUpTime);
            SliderCollection.GetElement("DisplaySetCoolDownTime").TriggerAction(VTBAction.SetEnableState, display.SupportsCoolDownTime);

            SliderCollection.GetElement("DisplaySetWarmUpTime").TriggerAction(VTBAction.UpdateValue, display.WarmUpTime);
            SliderCollection.GetElement("DisplaySetCoolDownTime").TriggerAction(VTBAction.UpdateValue, display.CoolDownTime);

            if (!ControlSystem._display.Description.Contains("Crestron Generic IR Display Driver"))
            {//RX Out and logging enabled since this is not an IR driver
                ButtonCollection.GetElement("DisplayDisableRxOut").TriggerAction(VTBAction.SetEnableState, true);
                ButtonCollection.GetElement("DisplayDisableLogging").TriggerAction(VTBAction.SetEnableState, true);
                ButtonCollection.GetElement("DisplayEnableRxOut").TriggerAction(VTBAction.SetEnableState, true);
                ButtonCollection.GetElement("DisplayEnableLogging").TriggerAction(VTBAction.SetEnableState, true);


                ButtonCollection.GetElement("DisplayDisableRxOut").TriggerAction(VTBAction.PressButton, true);
                ButtonCollection.GetElement("DisplayDisableLogging").TriggerAction(VTBAction.PressButton, true);
            }
            else
            {//IR drivers do not support RX Out or Logging
                ButtonCollection.GetElement("DisplayDisableRxOut").TriggerAction(VTBAction.SetEnableState, false);
                ButtonCollection.GetElement("DisplayDisableLogging").TriggerAction(VTBAction.SetEnableState, false);
                ButtonCollection.GetElement("DisplayEnableRxOut").TriggerAction(VTBAction.SetEnableState, false);
                ButtonCollection.GetElement("DisplayEnableLogging").TriggerAction(VTBAction.SetEnableState, false);

                FormattedTextCollection.GetElement("DisplayLog").TriggerAction(VTBAction.SetIndirectText, "Not supported");
                FormattedTextCollection.GetElement("DisplayRxOut").TriggerAction(VTBAction.SetIndirectText, "Not supported");
            }

            #endregion
        }

        public void SetBlurayPlayerSupports()
        {
            if (ControlSystem._blurayPlayer == null)
            {
                return;
            }

            IBasicBlurayPlayer blurayPlayer = ControlSystem._blurayPlayer;

            ControlSystem._blurayPlayer.CustomLogger = BlurayPlayerLogOut;
            ControlSystem._blurayPlayer.RxOut += BlurayPlayerRxOut;

            #region Header
            if (ControlSystem._blurayPlayer.Description.Contains("Generic IR"))
            {
                FormattedTextCollection.GetElement("BlurayPlayer ConnectionStatus").TriggerAction(VTBAction.SetIndirectText, "IR Port Ready");
            }
            else
            {
                FormattedTextCollection.GetElement("BlurayPlayer ConnectionStatus").TriggerAction(VTBAction.SetIndirectText, blurayPlayer.Connected ? "Connected" : "Disconnected");
            }
            #endregion

            #region Info
            FormattedTextCollection.GetElement("BlurayPlayer Description").TriggerAction(VTBAction.SetIndirectText, blurayPlayer.Description);
            FormattedTextCollection.GetElement("BlurayPlayer GUID").TriggerAction(VTBAction.SetIndirectText, blurayPlayer.Guid.ToString());
            FormattedTextCollection.GetElement("BlurayPlayer Manufacturer").TriggerAction(VTBAction.SetIndirectText, blurayPlayer.Manufacturer);
            FormattedTextCollection.GetElement("BlurayPlayer Version").TriggerAction(VTBAction.SetIndirectText, blurayPlayer.Version);
            FormattedTextCollection.GetElement("BlurayPlayer VersionDate").TriggerAction(VTBAction.SetIndirectText, blurayPlayer.VersionDate.ToString());
            FormattedTextCollection.GetElement("BlurayPlayer SupportsFeedback").TriggerAction(VTBAction.SetIndirectText, blurayPlayer.SupportsFeedback.ToString());

            if (ControlSystem._blurayPlayer.Description.Contains("Generic IR"))
            {
                ButtonCollection.GetElement("BlurayPlayer EnableLogging").TriggerAction(VTBAction.SetEnableState, false);
                ButtonCollection.GetElement("BlurayPlayer DisableLogging").TriggerAction(VTBAction.SetEnableState, false);
            }
            else
            {
                ButtonCollection.GetElement("BlurayPlayer EnableLogging").TriggerAction(VTBAction.SetEnableState, true);
                ButtonCollection.GetElement("BlurayPlayer DisableLogging").TriggerAction(VTBAction.SetEnableState, true);
            }

            #endregion

            #region Advanced

            if (ControlSystem._blurayPlayer.Description.Contains("Generic IR"))
            {
                ButtonCollection.GetElement("BlurayPlayer EnableRxOut").TriggerAction(VTBAction.SetEnableState, false);
                ButtonCollection.GetElement("BlurayPlayer DisableRxOut").TriggerAction(VTBAction.SetEnableState, false);
            }
            else
            {
                ButtonCollection.GetElement("BlurayPlayer EnableRxOut").TriggerAction(VTBAction.SetEnableState, true);
                ButtonCollection.GetElement("BlurayPlayer DisableRxOut").TriggerAction(VTBAction.SetEnableState, true);
            }

            ButtonCollection.GetElement("BlurayPlayer DisableRxOut").TriggerAction(VTBAction.PressButton, true);

            FormattedTextCollection.GetElement("BlurayPlayer PlaybackStatus").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsPlayBackStatusFeedback);
            FormattedTextCollection.GetElement("BlurayPlayer TrackFeedback").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsTrackFeedback);
            FormattedTextCollection.GetElement("BlurayPlayer ChapterFeedback").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsChapterFeedback);
            FormattedTextCollection.GetElement("BlurayPlayer TrackElapsedTime").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsTrackElapsedTimeFeedback);
            FormattedTextCollection.GetElement("BlurayPlayer TrackRemainingTime").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsTotalRemainingTimeFeedback);
            FormattedTextCollection.GetElement("BlurayPlayer ChapterElapsedTime").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsChapterElapsedTimeFeedback);
            FormattedTextCollection.GetElement("BlurayPlayer ChapterRemainingTime").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsChapterRemainingTimeFeedback);
            FormattedTextCollection.GetElement("BlurayPlayer TotalElapsedTime").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsTotalElapsedTimeFeedback);
            FormattedTextCollection.GetElement("BlurayPlayer TotalRemainingTime").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsTotalRemainingTimeFeedback);

            #endregion

            #region Transport Controls

            ButtonCollection.GetElement("BlurayPlayer ReverseScan").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsReverseScan);
            ButtonCollection.GetElement("BlurayPlayer ReverseSkip").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsReverseSkip);
            ButtonCollection.GetElement("BlurayPlayer ForwardSkip").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsForwardScan);
            ButtonCollection.GetElement("BlurayPlayer ForwardScan").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsForwardSkip);
            ButtonCollection.GetElement("BlurayPlayer Play").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsPlay);
            ButtonCollection.GetElement("BlurayPlayer Pause").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsPause);
            ButtonCollection.GetElement("BlurayPlayer Stop").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsStop);
            ButtonCollection.GetElement("BlurayPlayer Audio").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsAudio);
            ButtonCollection.GetElement("BlurayPlayer Display").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsDisplay);
            ButtonCollection.GetElement("BlurayPlayer Menu").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsMenu);
            ButtonCollection.GetElement("BlurayPlayer Repeat").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsRepeat);
            ButtonCollection.GetElement("BlurayPlayer Return").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsReturn);
            ButtonCollection.GetElement("BlurayPlayer Exit").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsExit);
            ButtonCollection.GetElement("BlurayPlayer Back").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsBack);
            ButtonCollection.GetElement("BlurayPlayer Eject").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsEject);
            ButtonCollection.GetElement("BlurayPlayer Subtitle").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsSubtitle);
            ButtonCollection.GetElement("BlurayPlayer Options").TriggerAction(VTBAction.SetEnableState, blurayPlayer.SupportsOptions);

            SmartObjectCollection.GetElement("BlurayPlayer Control DPad").TriggerAction(VTSAction.SetEnabledState, blurayPlayer.SupportsArrowKeys);
            SmartObjectCollection.GetElement("BlurayPlayer Keypad").TriggerAction(VTSAction.SetEnabledState, blurayPlayer.SupportsKeypadNumber);

            #endregion
        }

        private void DisplayLogOut(string message)
        {
            FormattedTextCollection.GetElement("DisplayLog").TriggerAction(VTBAction.SetIndirectText, message);
            CrestronConsole.PrintLine("Message={0}", message);
        }

        private void DisplayRxOut(string message)
        {
            FormattedTextCollection.GetElement("DisplayRxOut").TriggerAction(VTBAction.SetIndirectText, message);
            CrestronConsole.PrintLine("RxOutMessage={0}", message);
        }

        private void CableBoxLogOut(string message)
        {
            FormattedTextCollection.GetElement("CableBox Log").TriggerAction(VTBAction.SetIndirectText, message);
            CrestronConsole.PrintLine("Message={0}", message);
        }

        private void CableBoxRxOut(string message)
        {
            FormattedTextCollection.GetElement("CableBox RxOut").TriggerAction(VTBAction.SetIndirectText, message);
            CrestronConsole.PrintLine("RxOutMessage={0}", message);
        }

        private void VideoServerLogOut(string message)
        {
            FormattedTextCollection.GetElement("VideoServer Log").TriggerAction(VTBAction.SetIndirectText, message);
            CrestronConsole.PrintLine("Message={0}", message);
        }

        private void VideoServerRxOut(string message)
        {
            FormattedTextCollection.GetElement("VideoServer RxOut").TriggerAction(VTBAction.SetIndirectText, message);
            CrestronConsole.PrintLine("RxOutMessage={0}", message);
        }

        private void BlurayPlayerLogOut(string message)
        {
            FormattedTextCollection.GetElement("BlurayPlayer Log").TriggerAction(VTBAction.SetIndirectText, message);
            CrestronConsole.PrintLine("Message={0}", message);
        }

        private void BlurayPlayerRxOut(string message)
        {
            FormattedTextCollection.GetElement("BlurayPlayer RxOut").TriggerAction(VTBAction.SetIndirectText, message);
            CrestronConsole.PrintLine("RxOutMessage={0}", message);
        }

        public ControlSystem ControlSystem { get; private set; }

        //Smart Object callbacks
        public VTSDisplayMethods VTSDisplayMethods { get; private set; }
        public VTSCableBoxMethods VTSCableBoxMethods { get; private set; }
        public VTSVideoServerMethods VTSVideoServerMethods { get; private set; }
        public VTSBlurayPlayerMethods VTSBlurayPlayerMethods { get; private set; }

        //Element callbacks
        public DisplayVTButtons DisplayButtons { get; private set; }
        public DisplayVTSliders DisplaySliders { get; private set; }
        public DisplayVTTextEntryBoxes DisplayTextEntryBoxes { get; private set; }

        public CableBoxVTButtons CableBoxButtons { get; private set; }
        public CableBoxVTSliders CableBoxSliders { get; private set; }
        public CableBoxVTTextEntryBoxes CableBoxTextEntryBoxes { get; private set; }

        public VideoServerVTButtons VideoServerButtons { get; private set; }
        public VideoServerVTTextEntryBoxes VideoServerTextEntryBoxes { get; private set; }

        public BlurayPlayerVTButtons BlurayPlayerButtons { get; private set; }
        public BlurayPlayerVTTextEntryBoxes BlurayPlayerTextBoxes { get; private set; }
    }
}