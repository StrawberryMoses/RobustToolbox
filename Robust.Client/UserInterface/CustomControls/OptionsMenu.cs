﻿using Robust.Client.Graphics;
using Robust.Client.Interfaces.Graphics;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Interfaces.Configuration;
using Robust.Shared.Maths;

namespace Robust.Client.UserInterface.CustomControls
{
    internal sealed class OptionsMenu : SS14Window
    {
        private Button ApplyButton;
        private CheckBox VSyncCheckBox;
        private CheckBox FullscreenCheckBox;
        private CheckBox HighResLightsCheckBox;
        private readonly IConfigurationManager configManager;
        private readonly IDisplayManager _displayManager;

        protected override Vector2? CustomSize => (180, 160);

        public OptionsMenu(IDisplayManager displayMan, IConfigurationManager configMan) : base(displayMan)
        {
            _displayManager = displayMan;
            configManager = configMan;

            PerformLayout();
        }

        protected override void Initialize()
        {
            base.Initialize();

            HideOnClose = true;

            Title = "Options";
        }

        private void PerformLayout()
        {
            var vBox = new VBoxContainer();
            Contents.AddChild(vBox);
            vBox.SetAnchorAndMarginPreset(LayoutPreset.Wide);

            VSyncCheckBox = new CheckBox {Text = "VSync"};
            vBox.AddChild(VSyncCheckBox);
            VSyncCheckBox.OnToggled += OnCheckBoxToggled;

            FullscreenCheckBox = new CheckBox {Text = "Fullscreen"};
            vBox.AddChild(FullscreenCheckBox);
            FullscreenCheckBox.OnToggled += OnCheckBoxToggled;

            HighResLightsCheckBox = new CheckBox {Text = "High-Res Lights"};
            vBox.AddChild(HighResLightsCheckBox);
            HighResLightsCheckBox.OnToggled += OnCheckBoxToggled;

            ApplyButton = new Button
            {
                Text = "Apply", TextAlign = Button.AlignMode.Center,
                SizeFlagsVertical = SizeFlags.ShrinkCenter
            };
            vBox.AddChild(ApplyButton);
            ApplyButton.OnPressed += OnApplyButtonPressed;

            VSyncCheckBox.Pressed = configManager.GetCVar<bool>("display.vsync");
            HighResLightsCheckBox.Pressed = configManager.GetCVar<bool>("display.highreslights");
            FullscreenCheckBox.Pressed = ConfigIsFullscreen;
        }

        private void OnApplyButtonPressed(BaseButton.ButtonEventArgs args)
        {
            configManager.SetCVar("display.vsync", VSyncCheckBox.Pressed);
            configManager.SetCVar("display.highreslights", HighResLightsCheckBox.Pressed);
            configManager.SetCVar("display.windowmode",
                (int) (FullscreenCheckBox.Pressed ? WindowMode.Fullscreen : WindowMode.Windowed));
            configManager.SaveToFile();
            UpdateApplyButton();
        }

        private void OnCheckBoxToggled(BaseButton.ButtonToggledEventArgs args)
        {
            UpdateApplyButton();
        }

        private void UpdateApplyButton()
        {
            var isVSyncSame = VSyncCheckBox.Pressed == configManager.GetCVar<bool>("display.vsync");
            var isHighResLightsSame = HighResLightsCheckBox.Pressed == configManager.GetCVar<bool>("display.highreslights");
            var isFullscreenSame = FullscreenCheckBox.Pressed == ConfigIsFullscreen;
            ApplyButton.Disabled = isVSyncSame && isHighResLightsSame && isFullscreenSame;
        }

        private bool ConfigIsFullscreen =>
            configManager.GetCVar<int>("display.windowmode") == (int) WindowMode.Fullscreen;
    }
}