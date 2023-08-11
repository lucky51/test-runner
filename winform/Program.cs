namespace TestRunner
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            string fullPath = string.Empty;
            if (args.Length >0)
            {
                string relativePath = args[0];
                 fullPath = System.IO.Path.Combine(Environment.CurrentDirectory, relativePath);
            }
            Application.Run(new Form1(Path.GetFullPath(fullPath)));
        }
    }
}