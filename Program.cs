using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;
using Microsoft.SPOT;
using System;
using System.Text;
using System.Threading;

namespace walle
{
    public class Program
    {

        static TalonSRX left = new TalonSRX(1);
        static TalonSRX right = new TalonSRX(2);
        static TalonSRX hello = new TalonSRX(3);
        static TalonSRX yes = new TalonSRX(4);
        static CTRE.Phoenix.Controller.GameController _gamepad = null;
        public static void Main()
        {
            /* simple counter to print and watch using the debugger */
            int counter = 0;
            /* loop forever */
            while (true)
            {

                Drive();
                /* print the three analog inputs as three columns */
                Debug.Print("Counter Value: " + counter);

                /* increment counter */
                ++counter; /* try to land a breakpoint here and hover over 'counter' to see it's current value.  Or add it to the Watch Tab */

                /* wait a bit */
                System.Threading.Thread.Sleep(100);
            }




        }



        static void Drive()
        {

            if (null == _gamepad)

                _gamepad = new GameController(UsbHostDevice.GetInstance());

            float axis1 = _gamepad.GetAxis(0);
            float axis2 = _gamepad.GetAxis(1);

            left.Set(ControlMode.PercentOutput, (axis1*0.8));
            right.Set(ControlMode.PercentOutput, (-axis2*0.8));

        }

    }
}
