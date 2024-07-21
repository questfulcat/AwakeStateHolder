using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;


namespace AwakeStateHolder
{
    static class Program
    {
		public enum EXECUTION_STATE : uint
		{
			ES_AWAYMODE_REQUIRED = 0x00000040,
			ES_CONTINUOUS = 0x80000000,
			ES_DISPLAY_REQUIRED = 0x00000002,
			ES_SYSTEM_REQUIRED = 0x00000001
		}
		
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
		
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			
			Form f = new Form() { ClientSize = new Size(320, 220), Text = "None", FormBorderStyle = FormBorderStyle.FixedSingle, MaximizeBox = false, StartPosition = FormStartPosition.CenterScreen };
			//f.Icon = Icon.ExtractAssociatedIcon();
			Button bDisplay = new Button() { Left = 20, Top = 20, Width = 280, Height = 40, Text = "Hold Display Enabled" };
			Button bSystem = new Button() { Left = 20, Top = 80, Width = 280, Height = 40, Text = "Hold System Awake" };
			Button bBoth = new Button() { Left = 20, Top = 140, Width = 280, Height = 40, Text = "Hold System and Display" };
			
			bDisplay.Click += (sender, e) => { SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_DISPLAY_REQUIRED); f.Text = "Hold display"; };
			bSystem.Click += (sender, e) => { SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED); f.Text = "Hold system"; };
			bBoth.Click += (sender, e) => { SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_DISPLAY_REQUIRED); f.Text = "Hold system & display"; };
			
			f.Controls.Add(bDisplay);
			f.Controls.Add(bSystem);
			f.Controls.Add(bBoth);
			
            Application.Run(f);
        }
    }
}