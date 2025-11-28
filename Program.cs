using StudentAssistent.Business_Logic_Layer;

namespace StudentAssistent
{
    internal static class Program
    {
      
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // create service
            StudentService service = new StudentService();

            // open login window
            LogIn loginForm = new LogIn(service);

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // open main screen if login success
                Application.Run(new MainForm(service));
            }
        }
    }
}