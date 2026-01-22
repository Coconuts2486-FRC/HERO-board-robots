using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;
using Microsoft.SPOT;
using System;
using System.Text;
using System.Threading;

namespace beepbeep
{
    public class Program
    {
        /* create a talon */
        static TalonSRX rightSlave = new TalonSRX(4);
        static TalonSRX right = new TalonSRX(3);
        static TalonSRX leftSlave = new TalonSRX(1);
        static TalonSRX left = new TalonSRX(2);

        static StringBuilder stringBuilder = new StringBuilder();

        static CTRE.Phoenix.Controller.GameController _gamepad = null;

        public static void Main()
        {
            /* loop forever */
            while (true)
            {
                /* drive robot using gamepad */
                Drive();
                /* print whatever is in our string builder */
                Debug.Print(stringBuilder.ToString());
                stringBuilder.Clear();
                /* feed watchdog to keep Talon's enabled */
                CTRE.Phoenix.Watchdog.Feed();
                /* run this task every 20ms */
                Thread.Sleep(20);
            }
        }
        /**
         * If value is within 10% of center, clear it.
         * @param value [out] floating point value to deadband.
         */

        static void Deadband(ref float value)
        {
            if (value < -0.10)
            {
                /* outside of deadband */
            }
            else if (value > +0.10)
            {
                /* outside of deadband */
            }
            else
            {
                /* within 10% so zero it */
                value = 0;
            }
        }
        static void Drive()
        {
            if (null == _gamepad)
                _gamepad = new GameController(UsbHostDevice.GetInstance());

            float x = _gamepad.GetAxis(0);
            float y = -1 * _gamepad.GetAxis(1);
            float twist = _gamepad.GetAxis(2);

            bool spinButton1 = _gamepad.GetButton(1);
            bool spinButton2 = _gamepad.GetButton(2);
            bool spinButton3 = _gamepad.GetButton(3);
            bool spinButton4 = _gamepad.GetButton(4);
            bool spinButton5 = _gamepad.GetButton(13
                );
            Deadband(ref x);
            Deadband(ref y);
            Deadband(ref twist);

            float leftThrot = y + twist;
            float rightThrot = y - twist;

            if (spinButton1 && spinButton2 && spinButton3 && spinButton4 && spinButton5) 
            {
                // BUTTON MODE
                left.Set(ControlMode.PercentOutput, .3);
                leftSlave.Set(ControlMode.PercentOutput, .3);
                right.Set(ControlMode.PercentOutput, .3);
                rightSlave.Set(ControlMode.PercentOutput, .3);

                
            }
            else
            {
                // JOYSTICK DRIVE MODE
                left.Set(ControlMode.PercentOutput, leftThrot * 0.5);
                leftSlave.Set(ControlMode.PercentOutput, leftThrot * 0.5);
                right.Set(ControlMode.PercentOutput, -rightThrot * 0.5);
                rightSlave.Set(ControlMode.PercentOutput, -rightThrot * 0.5);
            }

            stringBuilder.Append("\t");
            stringBuilder.Append(x);
            stringBuilder.Append("\t");
            stringBuilder.Append(y);
            stringBuilder.Append("\t");
            stringBuilder.Append(twist);
        }
    }
}