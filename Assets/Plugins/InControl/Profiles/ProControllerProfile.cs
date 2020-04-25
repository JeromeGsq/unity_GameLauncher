using InControl;

namespace HammerDongers
{
    [AutoDiscover]
    public class ProControllerProfile : NativeInputDeviceProfile
    {
        public ProControllerProfile()
        {
            Name = "Pro Controller";
            Meta = "Pro Controller";

            DeviceClass = InputDeviceClass.Controller;
            DeviceStyle = InputDeviceStyle.NintendoSwitch;

            UpperDeadZone = 0.7f;

            IncludePlatforms = new[] {
                "Windows",
                "Android"
            };

            Matchers = new[] {
                new NativeInputDeviceMatcher {
                    VendorID = 4660,
                    ProductID = 48813,
                    VersionNumber = 48813,
                },
                new NativeInputDeviceMatcher {
                    VendorID = 1406,
                    ProductID = 8198,
                    VersionNumber = 0,
                },
                new NativeInputDeviceMatcher {
                    VendorID = 1406,
                    ProductID = 8199,
                    VersionNumber = 0,
                },
            };

            ButtonMappings = new[] {
                new InputControlMapping {
                    Handle = "A",
                    Target = InputControlType.Action1,
                    Source = Button( 0 ),
                },
                new InputControlMapping {
                    Handle = "B",
                    Target = InputControlType.Action2,
                    Source = Button( 1 ),
                },
                new InputControlMapping {
                    Handle = "X",
                    Target = InputControlType.Action4,
                    Source = Button( 2 ),
                },
                new InputControlMapping {
                    Handle = "Y",
                    Target = InputControlType.Action3,
                    Source = Button( 3 ),
                },
                new InputControlMapping {
                    Handle = "L",
                    Target = InputControlType.LeftTrigger,
                    Source = Button( 4 ),
                },
                new InputControlMapping {
                    Handle = "R",
                    Target = InputControlType.RightBumper,
                    Source = Button( 5 ),
                },
                new InputControlMapping {
                    Handle = "Minus",
                    Target = InputControlType.Start,
                    Source = Button( 8 ),
                },
                new InputControlMapping {
                    Handle = "Plus",
                    Target = InputControlType.Start,
                    Source = Button( 9 ),
                },
                new InputControlMapping {
                    Handle = "Home",
                    Target = InputControlType.Start,
                    Source = Button( 12 ),
                },
                new InputControlMapping {
                    Handle = "Capture",
                    Target = InputControlType.Start,
                    Source = Button( 13 ),
                },
            };

            AnalogMappings = new[] {
                new InputControlMapping {
                    Handle = "Right Stick Up",
                    Target = InputControlType.RightStickUp,
                    Source = Analog( 0 ),
                    SourceRange = InputRange.ZeroToMinusOne,
                    TargetRange = InputRange.ZeroToOne,
                },
                new InputControlMapping {
                    Handle = "Right Stick Down",
                    Target = InputControlType.RightStickDown,
                    Source = Analog( 0 ),
                    SourceRange = InputRange.ZeroToOne,
                    TargetRange = InputRange.ZeroToOne,
                },
                new InputControlMapping {
                    Handle = "Right Stick Left",
                    Target = InputControlType.RightStickLeft,
                    Source = Analog( 1 ),
                    SourceRange = InputRange.ZeroToMinusOne,
                    TargetRange = InputRange.ZeroToOne,
                },
                new InputControlMapping {
                    Handle = "Right Stick Right",
                    Target = InputControlType.RightStickRight,
                    Source = Analog( 1 ),
                    SourceRange = InputRange.ZeroToOne,
                    TargetRange = InputRange.ZeroToOne,
                },
                new InputControlMapping {
                    Handle = "Left Stick Up",
                    Target = InputControlType.LeftStickUp,
                    Source = Analog( 2 ),
                    SourceRange = InputRange.ZeroToMinusOne,
                    TargetRange = InputRange.ZeroToOne,
                },
                new InputControlMapping {
                    Handle = "Left Stick Down",
                    Target = InputControlType.LeftStickDown,
                    Source = Analog( 2 ),
                    SourceRange = InputRange.ZeroToOne,
                    TargetRange = InputRange.ZeroToOne,
                },
                new InputControlMapping {
                    Handle = "Left Stick Left",
                    Target = InputControlType.LeftStickLeft,
                    Source = Analog( 3 ),
                    SourceRange = InputRange.ZeroToMinusOne,
                    TargetRange = InputRange.ZeroToOne,
                },
                new InputControlMapping {
                    Handle = "Left Stick Right",
                    Target = InputControlType.LeftStickRight,
                    Source = Analog( 3 ),
                    SourceRange = InputRange.ZeroToOne,
                    TargetRange = InputRange.ZeroToOne,
                },
                new InputControlMapping {
                    Handle = "DPad Left",
                    Target = InputControlType.DPadLeft,
                    Source = Analog( 4 ),
                    SourceRange = InputRange.ZeroToMinusOne,
                    TargetRange = InputRange.ZeroToOne,
                },
                new InputControlMapping {
                    Handle = "DPad Right",
                    Target = InputControlType.DPadRight,
                    Source = Analog( 4 ),
                    SourceRange = InputRange.ZeroToOne,
                    TargetRange = InputRange.ZeroToOne,
                },
                new InputControlMapping {
                    Handle = "DPad Up",
                    Target = InputControlType.DPadUp,
                    Source = Analog( 5 ),
                    SourceRange = InputRange.ZeroToOne,
                    TargetRange = InputRange.ZeroToOne,
                },
                new InputControlMapping {
                    Handle = "DPad Down",
                    Target = InputControlType.DPadDown,
                    Source = Analog( 5 ),
                    SourceRange = InputRange.ZeroToMinusOne,
                    TargetRange = InputRange.ZeroToOne,
                },
            };
        }
    }
}

