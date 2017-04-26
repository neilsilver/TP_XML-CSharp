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

using myNamespace.Methods;
using myNamespace.Drivers;
using myNamespace.SmartObjects;

using Crestron.ThirdPartyCommon.Interfaces;

using myNamespace.Constants;

namespace myNamespace
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

			
        __Startup_Buttons = new __StartupVTButtons(this);
        VTS__Resi_Main_Categorie__List_Methods = new VTS__Resi_Main_Categorie__ListMethods(ControlSystem, this);
        __Resi_Main_Control_Bar_Buttons = new __Resi_Main_Control_BarVTButtons(this);
        __Resi_Main_Power_Options_Buttons = new __Resi_Main_Power_OptionsVTButtons(this);
        __Resi_Main_Volume_Display_Buttons = new __Resi_Main_Volume_DisplayVTButtons(this);
        __Resi_Main_Volume_Display_Sliders = new __Resi_Main_Volume_DisplayVTSliders(this);
        VTS__Resi_Main_Media_List_AV_Sources_Methods = new VTS__Resi_Main_Media_List_AV_SourcesMethods(ControlSystem, this);
        VTS__Resi_Main_Media_Room_Select_Methods = new VTS__Resi_Main_Media_Room_SelectMethods(ControlSystem, this);
        __Resi_Main_Media_Selection_Top_Bar_Buttons = new __Resi_Main_Media_Selection_Top_BarVTButtons(this);
        __iPod_Main_Buttons = new __iPod_MainVTButtons(this);
        VTS__iPod_Main_Methods = new VTS__iPod_MainMethods(ControlSystem, this);
        __Pandora_Main_Buttons = new __Pandora_MainVTButtons(this);
        VTS__Pandora_Main_Methods = new VTS__Pandora_MainMethods(ControlSystem, this);
        __Sirius_Main_Buttons = new __Sirius_MainVTButtons(this);
        VTS__Sirius_Main_Methods = new VTS__Sirius_MainMethods(ControlSystem, this);
        __AM_FM_Main_Buttons = new __AM_FM_MainVTButtons(this);
        __AM_FM_Main_Sliders = new __AM_FM_MainVTSliders(this);
        VTS__AM_FM_Main_Methods = new VTS__AM_FM_MainMethods(ControlSystem, this);
        __DVR_Main_Buttons = new __DVR_MainVTButtons(this);
        VTS__DVR_Main_Methods = new VTS__DVR_MainMethods(ControlSystem, this);
        __DVR_Presets_Buttons = new __DVR_PresetsVTButtons(this);
        __ADMS_Main_Buttons = new __ADMS_MainVTButtons(this);
        VTS__ADMS_Main_Methods = new VTS__ADMS_MainMethods(ControlSystem, this);
        VTS__Apple_TV_Main_Methods = new VTS__Apple_TV_MainMethods(ControlSystem, this);
        __Lights_Main_Buttons = new __Lights_MainVTButtons(this);
        __Lights_Main_TextEntryBoxes = new __Lights_MainVTTextEntryBoxes(this);
        VTS__Lights_Main_Methods = new VTS__Lights_MainMethods(ControlSystem, this);
        __Climate_Main_Buttons = new __Climate_MainVTButtons(this);
        __Climate_Main_TextEntryBoxes = new __Climate_MainVTTextEntryBoxes(this);
        VTS__Climate_Main_Methods = new VTS__Climate_MainMethods(ControlSystem, this);
        __Climate_Schedule_Buttons = new __Climate_ScheduleVTButtons(this);
        __Climate_Schedule_TextEntryBoxes = new __Climate_ScheduleVTTextEntryBoxes(this);
        __Shades_Main_Buttons = new __Shades_MainVTButtons(this);
        __Shades_Main_Sliders = new __Shades_MainVTSliders(this);
        __Shades_Main_TextEntryBoxes = new __Shades_MainVTTextEntryBoxes(this);
        VTS__Shades_Main_Methods = new VTS__Shades_MainMethods(ControlSystem, this);
        __Security_Camera_Main_Buttons = new __Security_Camera_MainVTButtons(this);
        __Security_Camera_Main_TextEntryBoxes = new __Security_Camera_MainVTTextEntryBoxes(this);
        __Security_Main_Buttons = new __Security_MainVTButtons(this);
        VTS__Security_Main_Methods = new VTS__Security_MainMethods(ControlSystem, this);

            
            AddObjects();

            //SetDefaultStates();
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
            
Pages.AddElement(100,"00_Startup");
                Pages.AddElement(101,"01_Resi_Main");
                Pages.AddElement(102,"02_Video_Fullscreen");
                
        }

        private void AddSubpages()
        {
            Subpages.AddElement(32,"__Startup_Subpage_Reference");
                Subpages.AddElement(47,"__Resi_Main_Subpage_Reference_1");
                Subpages.AddElement(213,"__Resi_Main_Subpage_Reference_10");
                Subpages.AddElement(204,"__Resi_Main_Subpage_Reference_11");
                Subpages.AddElement(71,"__Resi_Main_Subpage_Reference_12");
                Subpages.AddElement(61,"__Resi_Main_Subpage_Reference_13");
                Subpages.AddElement(207,"__Resi_Main_Subpage_Reference_14");
                Subpages.AddElement(210,"__Resi_Main_Subpage_Reference_15");
                Subpages.AddElement(216,"__Resi_Main_Subpage_Reference_16");
                Subpages.AddElement(219,"__Resi_Main_Subpage_Reference_17");
                Subpages.AddElement(76,"__Resi_Main_Subpage_Reference_18");
                Subpages.AddElement(404,"__Resi_Main_Subpage_Reference_19");
                Subpages.AddElement(539,"__Resi_Main_Subpage_Reference_2");
                Subpages.AddElement(411,"__Resi_Main_Subpage_Reference_20");
                Subpages.AddElement(424,"__Resi_Main_Subpage_Reference_21");
                Subpages.AddElement(431,"__Resi_Main_Subpage_Reference_22");
                Subpages.AddElement(214,"__Resi_Main_Subpage_Reference_24");
                Subpages.AddElement(222,"__Resi_Main_Subpage_Reference_28");
                Subpages.AddElement(441,"__Resi_Main_Subpage_Reference_29");
                Subpages.AddElement(540,"__Resi_Main_Subpage_Reference_3");
                Subpages.AddElement(432,"__Resi_Main_Subpage_Reference_32");
                Subpages.AddElement(65,"__Resi_Main_Subpage_Reference_4");
                Subpages.AddElement(50,"__Resi_Main_Subpage_Reference_5");
                Subpages.AddElement(72,"__Resi_Main_Subpage_Reference_6");
                Subpages.AddElement(550,"__Resi_Main_Subpage_Reference_7");
                Subpages.AddElement(560,"__Resi_Main_Subpage_Reference_8");
                
        }

        private void AddTextEntry()
        {
           #region __Lights_Main
                #endregion 
                #region __Climate_Main
                #endregion 
                #region __Climate_Schedule
                #endregion 
                #region __Shades_Main
                #endregion 
                #region __Security_Camera_Main
                #endregion 
                
        }

        private void AddFormattedText()
        {
            #region __Lights_Main
                FormattedTextCollection.AddElement( new VTFormattedText(this, new Dictionary<VTBJoin, uint>  { { VTBJoin.IndirectText,364} }, "Formatted_Text_10"));
                #endregion 
                #region __Climate_Main
                FormattedTextCollection.AddElement( new VTFormattedText(this, new Dictionary<VTBJoin, uint>  { { VTBJoin.IndirectText,364} }, "Formatted_Text_3"));
                #endregion 
                #region __Climate_Schedule
                FormattedTextCollection.AddElement( new VTFormattedText(this, new Dictionary<VTBJoin, uint>  { { VTBJoin.IndirectText,364} }, "Formatted_Text"));
                #endregion 
                #region __Shades_Main
                FormattedTextCollection.AddElement( new VTFormattedText(this, new Dictionary<VTBJoin, uint>  { { VTBJoin.IndirectText,364} }, "Formatted_Text_7"));
                #endregion 
                #region __Security_Camera_Main
                FormattedTextCollection.AddElement( new VTFormattedText(this, new Dictionary<VTBJoin, uint>  { { VTBJoin.IndirectText,341} }, "Formatted_Text"));
                #endregion 
                
        }

        private void AddSliders()
        {
           #region __Resi_Main_Volume_Display
                SliderCollection.AddElement(new VTSlider(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Analog,1} }, "LiquidGaugeVertical", __Resi_Main_Volume_Display_Sliders.LiquidGaugeVertical));
                #endregion 
                #region __AM_FM_Main
                SliderCollection.AddElement(new VTSlider(this, new Dictionary<VTBJoin, uint> {  }, "Signal_Level_Gauge", __AM_FM_Main_Sliders.Signal_Level_Gauge));
                #endregion 
                #region __Shades_Main
                SliderCollection.AddElement(new VTSlider(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Analog,303} }, "LiquidGaugeVertical", __Shades_Main_Sliders.LiquidGaugeVertical));
                SliderCollection.AddElement(new VTSlider(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Analog,301} }, "LiquidGaugeVertical_1", __Shades_Main_Sliders.LiquidGaugeVertical_1));
                SliderCollection.AddElement(new VTSlider(this, new Dictionary<VTBJoin, uint> { { VTBJoin.Analog,302} }, "LiquidGaugeVertical_2", __Shades_Main_Sliders.LiquidGaugeVertical_2));
                #endregion 
                
        }

        private void AddButtons()
        {
			// Two constructors exsist for VTButton
			// The creator creates the non interlock version for all buttons 
			// If you want some buttons to be interlocked the change the button constructor and create an Interlock List for your group.	
			// 
			//   VTButton(this, Dictionary<VTBJoin, uint> ,InterlockList, name , Callback));
		    //	 List<VTButton> InterlockList = new List<VTButton>();
			
        
           #region __Startup
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,521},{ VTBJoin.Visbility,31}}, "Button", __Startup_Buttons.Button));
                #endregion 
                #region __Resi_Main_Control_Bar
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,52}}, "Button", __Resi_Main_Control_Bar_Buttons.Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,46}}, "Button_1", __Resi_Main_Control_Bar_Buttons.Button_1));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,51}}, "Button_2", __Resi_Main_Control_Bar_Buttons.Button_2));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,55}}, "Button_3", __Resi_Main_Control_Bar_Buttons.Button_3));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,75}}, "Button_4", __Resi_Main_Control_Bar_Buttons.Button_4));
                #endregion 
                #region __Resi_Main_Power_Options
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,37}}, "Button", __Resi_Main_Power_Options_Buttons.Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,41}}, "Button_1", __Resi_Main_Power_Options_Buttons.Button_1));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,42}}, "Button_2", __Resi_Main_Power_Options_Buttons.Button_2));
                #endregion 
                #region __Resi_Main_Volume_Display
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,55}}, "Button", __Resi_Main_Volume_Display_Buttons.Button));
                #endregion 
                #region __Resi_Main_Media_Selection_Top_Bar
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,60}}, "Button", __Resi_Main_Media_Selection_Top_Bar_Buttons.Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,70}}, "Button_2", __Resi_Main_Media_Selection_Top_Bar_Buttons.Button_2));
                #endregion 
                #region __iPod_Main
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,794}}, "Multi_Mode_Button", __iPod_Main_Buttons.Multi_Mode_Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,795}}, "Multi_Mode_Button_10", __iPod_Main_Buttons.Multi_Mode_Button_10));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,796}}, "Multi_Mode_Button_11", __iPod_Main_Buttons.Multi_Mode_Button_11));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,797}}, "Multi_Mode_Button_12", __iPod_Main_Buttons.Multi_Mode_Button_12));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,798}}, "Multi_Mode_Button_13", __iPod_Main_Buttons.Multi_Mode_Button_13));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,702},{ VTBJoin.Visbility,701}}, "Multi_Mode_Button_14", __iPod_Main_Buttons.Multi_Mode_Button_14));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,701},{ VTBJoin.Visbility,702}}, "Multi_Mode_Button_15", __iPod_Main_Buttons.Multi_Mode_Button_15));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,706}}, "Multi_Mode_Button_16", __iPod_Main_Buttons.Multi_Mode_Button_16));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,707}}, "Multi_Mode_Button_17", __iPod_Main_Buttons.Multi_Mode_Button_17));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,793}}, "Multi_Mode_Button_18", __iPod_Main_Buttons.Multi_Mode_Button_18));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,810}}, "Multi_Mode_Button_3", __iPod_Main_Buttons.Multi_Mode_Button_3));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,815}}, "Multi_Mode_Button_4", __iPod_Main_Buttons.Multi_Mode_Button_4));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,740}}, "Multi_Mode_Button_5", __iPod_Main_Buttons.Multi_Mode_Button_5));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,820}}, "Multi_Mode_Button_6", __iPod_Main_Buttons.Multi_Mode_Button_6));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,791}}, "Multi_Mode_Button_7", __iPod_Main_Buttons.Multi_Mode_Button_7));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,792}}, "Multi_Mode_Button_8", __iPod_Main_Buttons.Multi_Mode_Button_8));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,737}}, "Multi_Mode_Button_9", __iPod_Main_Buttons.Multi_Mode_Button_9));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,705}}, "SKIP_BACK", __iPod_Main_Buttons.SKIP_BACK));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,704}}, "SKIP_FORWARD", __iPod_Main_Buttons.SKIP_FORWARD));
                #endregion 
                #region __Pandora_Main
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,794}}, "Multi_Mode_Button", __Pandora_Main_Buttons.Multi_Mode_Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,704}}, "Multi_Mode_Button_1", __Pandora_Main_Buttons.Multi_Mode_Button_1));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,798}}, "Multi_Mode_Button_10", __Pandora_Main_Buttons.Multi_Mode_Button_10));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,702},{ VTBJoin.Visbility,701}}, "Multi_Mode_Button_11", __Pandora_Main_Buttons.Multi_Mode_Button_11));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,701},{ VTBJoin.Visbility,702}}, "Multi_Mode_Button_12", __Pandora_Main_Buttons.Multi_Mode_Button_12));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,706}}, "Multi_Mode_Button_13", __Pandora_Main_Buttons.Multi_Mode_Button_13));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,707}}, "Multi_Mode_Button_14", __Pandora_Main_Buttons.Multi_Mode_Button_14));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,793}}, "Multi_Mode_Button_15", __Pandora_Main_Buttons.Multi_Mode_Button_15));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,740}}, "Multi_Mode_Button_2", __Pandora_Main_Buttons.Multi_Mode_Button_2));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,820}}, "Multi_Mode_Button_3", __Pandora_Main_Buttons.Multi_Mode_Button_3));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,791}}, "Multi_Mode_Button_4", __Pandora_Main_Buttons.Multi_Mode_Button_4));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,792}}, "Multi_Mode_Button_5", __Pandora_Main_Buttons.Multi_Mode_Button_5));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,737}}, "Multi_Mode_Button_6", __Pandora_Main_Buttons.Multi_Mode_Button_6));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,795}}, "Multi_Mode_Button_7", __Pandora_Main_Buttons.Multi_Mode_Button_7));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,796}}, "Multi_Mode_Button_8", __Pandora_Main_Buttons.Multi_Mode_Button_8));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,797}}, "Multi_Mode_Button_9", __Pandora_Main_Buttons.Multi_Mode_Button_9));
                #endregion 
                #region __Sirius_Main
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,794}}, "Multi_Mode_Button", __Sirius_Main_Buttons.Multi_Mode_Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,791}}, "Multi_Mode_Button_1", __Sirius_Main_Buttons.Multi_Mode_Button_1));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,792}}, "Multi_Mode_Button_2", __Sirius_Main_Buttons.Multi_Mode_Button_2));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,737}}, "Multi_Mode_Button_3", __Sirius_Main_Buttons.Multi_Mode_Button_3));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,795}}, "Multi_Mode_Button_4", __Sirius_Main_Buttons.Multi_Mode_Button_4));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,796}}, "Multi_Mode_Button_5", __Sirius_Main_Buttons.Multi_Mode_Button_5));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,797}}, "Multi_Mode_Button_6", __Sirius_Main_Buttons.Multi_Mode_Button_6));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,798}}, "Multi_Mode_Button_7", __Sirius_Main_Buttons.Multi_Mode_Button_7));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,793}}, "Multi_Mode_Button_8", __Sirius_Main_Buttons.Multi_Mode_Button_8));
                #endregion 
                #region __AM_FM_Main
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,732}}, "Button", __AM_FM_Main_Buttons.Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,731}}, "Button_1", __AM_FM_Main_Buttons.Button_1));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,739}}, "Multi_Mode_Button", __AM_FM_Main_Buttons.Multi_Mode_Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,739}}, "Multi_Mode_Button_3", __AM_FM_Main_Buttons.Multi_Mode_Button_3));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,740}}, "Multi_Mode_Button_4", __AM_FM_Main_Buttons.Multi_Mode_Button_4));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,735}}, "Multi_Mode_Button_5", __AM_FM_Main_Buttons.Multi_Mode_Button_5));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,739}}, "Multi_Mode_Button_6", __AM_FM_Main_Buttons.Multi_Mode_Button_6));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,740}}, "Multi_Mode_Button_7", __AM_FM_Main_Buttons.Multi_Mode_Button_7));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,740}}, "Multi_Mode_Button_8", __AM_FM_Main_Buttons.Multi_Mode_Button_8));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,816}}, "Simple_Button", __AM_FM_Main_Buttons.Simple_Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,743}}, "Simple_Button_1", __AM_FM_Main_Buttons.Simple_Button_1));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,806}}, "Simple_Button_10", __AM_FM_Main_Buttons.Simple_Button_10));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,811}}, "Simple_Button_11", __AM_FM_Main_Buttons.Simple_Button_11));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,812}}, "Simple_Button_12", __AM_FM_Main_Buttons.Simple_Button_12));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,813}}, "Simple_Button_13", __AM_FM_Main_Buttons.Simple_Button_13));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,814}}, "Simple_Button_14", __AM_FM_Main_Buttons.Simple_Button_14));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,815}}, "Simple_Button_15", __AM_FM_Main_Buttons.Simple_Button_15));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,742}}, "Simple_Button_2", __AM_FM_Main_Buttons.Simple_Button_2));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,744}}, "Simple_Button_3", __AM_FM_Main_Buttons.Simple_Button_3));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,741}}, "Simple_Button_4", __AM_FM_Main_Buttons.Simple_Button_4));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,801}}, "Simple_Button_5", __AM_FM_Main_Buttons.Simple_Button_5));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,802}}, "Simple_Button_6", __AM_FM_Main_Buttons.Simple_Button_6));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,803}}, "Simple_Button_7", __AM_FM_Main_Buttons.Simple_Button_7));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,804}}, "Simple_Button_8", __AM_FM_Main_Buttons.Simple_Button_8));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,805}}, "Simple_Button_9", __AM_FM_Main_Buttons.Simple_Button_9));
                #endregion 
                #region __DVR_Main
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,264}}, "Button_2", __DVR_Main_Buttons.Button_2));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,4}}, "YELLOW", __DVR_Main_Buttons.YELLOW));
                #endregion 
                #region __DVR_Presets
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,263}}, "Button_1", __DVR_Presets_Buttons.Button_1));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,264}}, "Button_2", __DVR_Presets_Buttons.Button_2));
                #endregion 
                #region __ADMS_Main
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,794}}, "Multi_Mode_Button", __ADMS_Main_Buttons.Multi_Mode_Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,704}}, "Multi_Mode_Button_1", __ADMS_Main_Buttons.Multi_Mode_Button_1));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,795}}, "Multi_Mode_Button_10", __ADMS_Main_Buttons.Multi_Mode_Button_10));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,796}}, "Multi_Mode_Button_11", __ADMS_Main_Buttons.Multi_Mode_Button_11));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,797}}, "Multi_Mode_Button_12", __ADMS_Main_Buttons.Multi_Mode_Button_12));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,798}}, "Multi_Mode_Button_13", __ADMS_Main_Buttons.Multi_Mode_Button_13));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,702},{ VTBJoin.Visbility,701}}, "Multi_Mode_Button_14", __ADMS_Main_Buttons.Multi_Mode_Button_14));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,701},{ VTBJoin.Visbility,702}}, "Multi_Mode_Button_15", __ADMS_Main_Buttons.Multi_Mode_Button_15));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,706}}, "Multi_Mode_Button_16", __ADMS_Main_Buttons.Multi_Mode_Button_16));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,707}}, "Multi_Mode_Button_17", __ADMS_Main_Buttons.Multi_Mode_Button_17));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,793}}, "Multi_Mode_Button_18", __ADMS_Main_Buttons.Multi_Mode_Button_18));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,705}}, "Multi_Mode_Button_2", __ADMS_Main_Buttons.Multi_Mode_Button_2));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,810}}, "Multi_Mode_Button_3", __ADMS_Main_Buttons.Multi_Mode_Button_3));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,815}}, "Multi_Mode_Button_4", __ADMS_Main_Buttons.Multi_Mode_Button_4));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,740}}, "Multi_Mode_Button_5", __ADMS_Main_Buttons.Multi_Mode_Button_5));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,820}}, "Multi_Mode_Button_6", __ADMS_Main_Buttons.Multi_Mode_Button_6));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,791}}, "Multi_Mode_Button_7", __ADMS_Main_Buttons.Multi_Mode_Button_7));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Visbility,792}}, "Multi_Mode_Button_8", __ADMS_Main_Buttons.Multi_Mode_Button_8));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,737}}, "Multi_Mode_Button_9", __ADMS_Main_Buttons.Multi_Mode_Button_9));
                #endregion 
                #region __Lights_Main
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1005}}, "Simple_Button", __Lights_Main_Buttons.Simple_Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1001}}, "Simple_Button_1", __Lights_Main_Buttons.Simple_Button_1));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1002}}, "Simple_Button_2", __Lights_Main_Buttons.Simple_Button_2));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1003}}, "Simple_Button_3", __Lights_Main_Buttons.Simple_Button_3));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1004}}, "Simple_Button_4", __Lights_Main_Buttons.Simple_Button_4));
                #endregion 
                #region __Climate_Main
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1002}}, "Multi_Mode_Button", __Climate_Main_Buttons.Multi_Mode_Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1001}}, "Multi_Mode_Button_7", __Climate_Main_Buttons.Multi_Mode_Button_7));
                #endregion 
                #region __Climate_Schedule
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,461}}, "Button_1", __Climate_Schedule_Buttons.Button_1));
                #endregion 
                #region __Shades_Main
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1068}}, "Button", __Shades_Main_Buttons.Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1069}}, "Button_1", __Shades_Main_Buttons.Button_1));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1063}}, "Button_1_1", __Shades_Main_Buttons.Button_1_1));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1066}}, "Button_1_2", __Shades_Main_Buttons.Button_1_2));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1061}}, "Button_2", __Shades_Main_Buttons.Button_2));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1062}}, "Button_3", __Shades_Main_Buttons.Button_3));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1064}}, "Button_4", __Shades_Main_Buttons.Button_4));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1065}}, "Button_5", __Shades_Main_Buttons.Button_5));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1067}}, "Button_6", __Shades_Main_Buttons.Button_6));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1005}}, "Simple_Button", __Shades_Main_Buttons.Simple_Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1001}}, "Simple_Button_1", __Shades_Main_Buttons.Simple_Button_1));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1002}}, "Simple_Button_2", __Shades_Main_Buttons.Simple_Button_2));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1003}}, "Simple_Button_3", __Shades_Main_Buttons.Simple_Button_3));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1004}}, "Simple_Button_4", __Shades_Main_Buttons.Simple_Button_4));
                #endregion 
                #region __Security_Camera_Main
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,481}}, "Button", __Security_Camera_Main_Buttons.Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1042}}, "Simple_Button", __Security_Camera_Main_Buttons.Simple_Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1041}}, "Simple_Button_1", __Security_Camera_Main_Buttons.Simple_Button_1));
                #endregion 
                #region __Security_Main
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,482}}, "Button", __Security_Main_Buttons.Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1022}}, "Simple_Button", __Security_Main_Buttons.Simple_Button));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1020}}, "Simple_Button_1", __Security_Main_Buttons.Simple_Button_1));
                ButtonCollection.AddElement(new VTButton(this, new Dictionary<VTBJoin, uint> {{ VTBJoin.Press,1021}}, "Simple_Button_2", __Security_Main_Buttons.Simple_Button_2));
                #endregion 
                
        }   

        public override void InitializeSmartObjects()
        {
			
		
			 List<string> TempList = new List<string>();
           
                // Set Number of Items in ButtonList

                SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.__Resi_Main_Media_List_AV_Sources_Button_List_Vertical],"Button_List_Vertical", 10, VTS__Resi_Main_Media_List_AV_Sources_Methods.Button_List_Vertical, true));

                // Set Number of Items in ButtonList

                SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.__Resi_Main_Media_Room_Select_Button_List_Vertical],"Button_List_Vertical", 10, VTS__Resi_Main_Media_Room_Select_Methods.Button_List_Vertical, true));

                SmartObjectCollection.AddElement(new VTDynamicButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.__iPod_Main_Dynamic_Button_List_Vertical_SONGLIST],"Dynamic_Button_List_Vertical_SONGLIST", TempList, VTS__iPod_Main_Methods.Dynamic_Button_List_Vertical_SONGLIST, true));

                // Set Number of Items in ButtonList

                SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.__Pandora_Main_Button_List_Vertical],"Button_List_Vertical", 10, VTS__Pandora_Main_Methods.Button_List_Vertical, true));

                // Set Number of Items in ButtonList

                SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.__Sirius_Main_Button_List_Vertical],"Button_List_Vertical", 10, VTS__Sirius_Main_Methods.Button_List_Vertical, true));

                SmartObjectCollection.AddElement(new VTKeypad(this, null, ExtendedInterface.SmartObjects[VTSObjects.__AM_FM_Main_Simple_Keypad],"Simple_Keypad", VTS__AM_FM_Main_Methods.Simple_Keypad));

                SmartObjectCollection.AddElement(new VTKeypad(this, null, ExtendedInterface.SmartObjects[VTSObjects.__DVR_Main_Simple_Keypad_1],"Simple_Keypad_1", VTS__DVR_Main_Methods.Simple_Keypad_1));

                // Set Number of Items in ButtonList

                SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.__ADMS_Main_Button_List_Vertical],"Button_List_Vertical", 10, VTS__ADMS_Main_Methods.Button_List_Vertical, true));

                // Set Number of Items in ButtonList

                SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.__Lights_Main_Button_List_Vertical],"Button_List_Vertical", 10, VTS__Lights_Main_Methods.Button_List_Vertical, true));

                // Set Number of Items in ButtonList

                SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.__Climate_Main_Button_List_Vertical],"Button_List_Vertical", 10, VTS__Climate_Main_Methods.Button_List_Vertical, true));

                // Set Number of Items in ButtonList

                SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.__Shades_Main_Button_List_Vertical],"Button_List_Vertical", 10, VTS__Shades_Main_Methods.Button_List_Vertical, true));

                SmartObjectCollection.AddElement(new VTKeypad(this, null, ExtendedInterface.SmartObjects[VTSObjects.__Security_Main_SecuritySimple_Keypad],"SecuritySimple_Keypad", VTS__Security_Main_Methods.SecuritySimple_Keypad));

                // Set Number of Items in ButtonList

                SmartObjectCollection.AddElement(new VTButtonList(this, null, ExtendedInterface.SmartObjects[VTSObjects.__Security_Main_Security_Button_List_Vertical],"Security_Button_List_Vertical", 10, VTS__Security_Main_Methods.Security_Button_List_Vertical, true));

        }

        private bool CheckSmartObject(uint ID)
        {
            return ExtendedInterface.SmartObjects.Contains(ID);
        }
/*
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
*/
        public ControlSystem ControlSystem { get; private set; }

                //Element callbacks
        public __StartupVTButtons __Startup_Buttons { get; private set; }

        //Smart Object
        public VTS__Resi_Main_Categorie__ListMethods VTS__Resi_Main_Categorie__List_Methods { get; private set; }
        //Element callbacks
        public __Resi_Main_Control_BarVTButtons __Resi_Main_Control_Bar_Buttons { get; private set; }
        //Element callbacks
        public __Resi_Main_Power_OptionsVTButtons __Resi_Main_Power_Options_Buttons { get; private set; }
        //Element callbacks
        public __Resi_Main_Volume_DisplayVTButtons __Resi_Main_Volume_Display_Buttons { get; private set; }
        //Element callbacks
        public __Resi_Main_Volume_DisplayVTSliders __Resi_Main_Volume_Display_Sliders { get; private set; }

        //Smart Object
        public VTS__Resi_Main_Media_List_AV_SourcesMethods VTS__Resi_Main_Media_List_AV_Sources_Methods { get; private set; }

        //Smart Object
        public VTS__Resi_Main_Media_Room_SelectMethods VTS__Resi_Main_Media_Room_Select_Methods { get; private set; }
        //Element callbacks
        public __Resi_Main_Media_Selection_Top_BarVTButtons __Resi_Main_Media_Selection_Top_Bar_Buttons { get; private set; }
        //Element callbacks
        public __iPod_MainVTButtons __iPod_Main_Buttons { get; private set; }

        //Smart Object
        public VTS__iPod_MainMethods VTS__iPod_Main_Methods { get; private set; }
        //Element callbacks
        public __Pandora_MainVTButtons __Pandora_Main_Buttons { get; private set; }

        //Smart Object
        public VTS__Pandora_MainMethods VTS__Pandora_Main_Methods { get; private set; }
        //Element callbacks
        public __Sirius_MainVTButtons __Sirius_Main_Buttons { get; private set; }

        //Smart Object
        public VTS__Sirius_MainMethods VTS__Sirius_Main_Methods { get; private set; }
        //Element callbacks
        public __AM_FM_MainVTButtons __AM_FM_Main_Buttons { get; private set; }
        //Element callbacks
        public __AM_FM_MainVTSliders __AM_FM_Main_Sliders { get; private set; }

        //Smart Object
        public VTS__AM_FM_MainMethods VTS__AM_FM_Main_Methods { get; private set; }
        //Element callbacks
        public __DVR_MainVTButtons __DVR_Main_Buttons { get; private set; }

        //Smart Object
        public VTS__DVR_MainMethods VTS__DVR_Main_Methods { get; private set; }
        //Element callbacks
        public __DVR_PresetsVTButtons __DVR_Presets_Buttons { get; private set; }
        //Element callbacks
        public __ADMS_MainVTButtons __ADMS_Main_Buttons { get; private set; }

        //Smart Object
        public VTS__ADMS_MainMethods VTS__ADMS_Main_Methods { get; private set; }

        //Smart Object
        public VTS__Apple_TV_MainMethods VTS__Apple_TV_Main_Methods { get; private set; }
        //Element callbacks
        public __Lights_MainVTButtons __Lights_Main_Buttons { get; private set; }
        //Element callbacks
        public __Lights_MainVTTextEntryBoxes __Lights_Main_TextEntryBoxes { get; private set; }

        //Smart Object
        public VTS__Lights_MainMethods VTS__Lights_Main_Methods { get; private set; }
        //Element callbacks
        public __Climate_MainVTButtons __Climate_Main_Buttons { get; private set; }
        //Element callbacks
        public __Climate_MainVTTextEntryBoxes __Climate_Main_TextEntryBoxes { get; private set; }

        //Smart Object
        public VTS__Climate_MainMethods VTS__Climate_Main_Methods { get; private set; }
        //Element callbacks
        public __Climate_ScheduleVTButtons __Climate_Schedule_Buttons { get; private set; }
        //Element callbacks
        public __Climate_ScheduleVTTextEntryBoxes __Climate_Schedule_TextEntryBoxes { get; private set; }
        //Element callbacks
        public __Shades_MainVTButtons __Shades_Main_Buttons { get; private set; }
        //Element callbacks
        public __Shades_MainVTSliders __Shades_Main_Sliders { get; private set; }
        //Element callbacks
        public __Shades_MainVTTextEntryBoxes __Shades_Main_TextEntryBoxes { get; private set; }

        //Smart Object
        public VTS__Shades_MainMethods VTS__Shades_Main_Methods { get; private set; }
        //Element callbacks
        public __Security_Camera_MainVTButtons __Security_Camera_Main_Buttons { get; private set; }
        //Element callbacks
        public __Security_Camera_MainVTTextEntryBoxes __Security_Camera_Main_TextEntryBoxes { get; private set; }
        //Element callbacks
        public __Security_MainVTButtons __Security_Main_Buttons { get; private set; }

        //Smart Object
        public VTS__Security_MainMethods VTS__Security_Main_Methods { get; private set; }

    }
}