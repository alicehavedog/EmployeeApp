namespace EmployeeApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //command to get addemployee page
            Routing.RegisterRoute(nameof(AddEmployee), typeof(AddEmployee));
        }
    }
}
