namespace Presentation.ConsoleApp.Services;

public class MenuService(ManageProductsService manageProductsService)
{ 
    private readonly ManageProductsService _manageProductsService = manageProductsService;

    public void Show_MainMenu()
    {
        while (true)
        {
            DisplayMenuTitle("Main Menu");
            List<string> optionsList = [
                "1. Manage Products",
                "2. Manage Customers",
                "0. Exit Application"
            ];
            foreach (var item in optionsList)
            {
                Console.WriteLine(item);
            }
            Console.Write("\nSelect an option:");
            var option = Console.ReadKey(true).KeyChar.ToString();
            switch (option)
            {
                case "0":
                    Show_ExitApplicationOption();
                    break;
                case "1":
                    Show_ManageProductsOption();
                    break;
                case "2":
                    Show_ManageCustomersOption();
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("\nInvalid selection, please select one of the options above. Press any key to continue.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private void Show_ManageProductsOption()
    {
        DisplayMenuTitle("Manage Products");
        List<string> optionsList = [
            "1. Create Product",
            "2. Update Product",
            "3. Find Product",
            "4. Manage Stores and Inventories",
            "5. Return to Main Menu",
            "0. Exit Application"
        ];
        foreach (var item in optionsList)
        {
            Console.WriteLine(item);
        }
        Console.Write("\nSelect an option:");
        var option = Console.ReadKey(true).KeyChar.ToString();
        switch (option)
        {
            case "0":
                Show_ExitApplicationOption();
                break;
            case "1":
                _manageProductsService.CreateProductOption();
                break;
            case "2":
                _manageProductsService.UpdateProductOption();
                break;
            case "3":
                //Show_ManageCustomersOption();
                break;
            case "4":
                //Show_ManageCustomersOption();
                break;
            case "5":
                Show_MainMenu();
                break;
            default:
                Console.WriteLine();
                Console.WriteLine("\nInvalid selection, please select one of the options above. Press any key to continue.");
                Console.ReadKey();
                break;
        }
    }

    private void Show_ManageCustomersOption()
    {
        throw new NotImplementedException();
    }

    private void Show_ExitApplicationOption()
    {
        Console.Clear();
        Console.WriteLine("Are you sure you wish to exit? (y/n): ");
        var option = Console.ReadKey().KeyChar.ToString();

        if (option == "y")
        {
            Environment.Exit(0);
        }
    }

    private void DisplayMenuTitle(string title)
    {
        Console.Clear();
        Console.WriteLine($"### {title} ###");
        Console.WriteLine();
    }
}