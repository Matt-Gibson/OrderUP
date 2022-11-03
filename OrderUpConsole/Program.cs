using LineItemLibrary;
using Newtonsoft.Json;
using Spectre.Console;

Console.WriteLine("Welcome to OrderUp");
Console.WriteLine("");

var itemList = new List<LineItem>();
var userWishesToContinue = true;

if (File.Exists(@"D:\\WorkingList.txt"))
{
    string loadingString = File.ReadAllText(@"D:\\WorkingList.txt");
    itemList = JsonConvert.DeserializeObject<List<LineItem>>(loadingString);
}
while (userWishesToContinue)
{
    var optionSelected = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("What would you like to do?")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
            .AddChoices(new[] {
                "Create New Item", "View Current List", "Update an Item",
                "Delete an Item", "Save and Exit",
            }));
    
    switch (optionSelected)
    {
        case "Create New Item":
            Console.WriteLine("Enter a Quantity");
            int quantity = int.Parse(Console.ReadLine()!);
            
            var metalColorSelected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a Color")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more colors)[/]")
                    .AddChoices(new[] {
                        "#2 White", "Barn Red", "Black", "Bright Red", "Brown", "Buckskin Tan", "Burnished Slate",
                        "Charcoal", "Clay", "Copper Penny", "Dark Red", "Gallery Blue", "Galvalume", "Gray",
                        "Green", "Hawaiian Blue", "Light Stone", "Sapphire Blue", "Tan", "Plum", "White",

                    }));
            
            Console.WriteLine("Enter a Customer Name");
            string? customerName = Console.ReadLine();
            Console.WriteLine("Enter a Priority");
            int priority = int.Parse(Console.ReadLine()!);
            itemList!.Add(LineItem.createLineItem(quantity, metalColorSelected, customerName, priority));
            break;
        case "View Current List":
            Console.WriteLine("Current List");
            foreach (var lineItem in itemList!)
            {
                Console.WriteLine(lineItem.quantity);
                Console.WriteLine(lineItem.metalColor);
                Console.WriteLine(lineItem.customerName);
                Console.WriteLine(lineItem.priorityFactor);
                Console.WriteLine(lineItem.isMade);
            }
            break;
        case "Update an Item":
            Console.WriteLine("Which Item Shall Be Updated?");
            int lineNumber = int.Parse(Console.ReadLine()!);

            Console.WriteLine("What do you wish to update?");
            Console.WriteLine("[1] Quantity");
            Console.WriteLine("[2] Metal Color");
            Console.WriteLine("[3] Customer Name");
            Console.WriteLine("[4] Priority");
            int selection = int.Parse(Console.ReadLine()!);

            switch (selection)
            {
                case 1:
                    Console.WriteLine("Enter New Quantity");
                    int newQuantity = int.Parse(Console.ReadLine()!);
                    itemList![lineNumber] = LineItem.updateLineItem(itemList[lineNumber], newQuantity);
                    break;
                case 2:
                    Console.WriteLine("Enter New Metal Color");
                    string? newMetalColor = Console.ReadLine();
                    itemList![lineNumber] = LineItem.updateLineItem(itemList[lineNumber], newMetalColor);
                    break;
                case 3:
                    Console.WriteLine("Enter New Customer Name");
                    string? newCustomerName = Console.ReadLine();
                    itemList![lineNumber] = LineItem.updateLineItem(itemList[lineNumber], newCustomerName, true);
                    break;
                case 4:
                    Console.WriteLine("Enter New Priority Factor");
                    int newPriorityFactor = int.Parse(Console.ReadLine()!);
                    itemList![lineNumber] = LineItem.updateLineItem(itemList[lineNumber], newPriorityFactor, true);
                    break;
                default:
                    break;
            }
            break;
        case "Delete an Item":
            Console.WriteLine("Which Item Shall Be Deleted?");
            itemList!.RemoveAt(int.Parse(Console.ReadLine()!));
            break;
        case "Save and Exit":
            Console.WriteLine("Goodbye!");
            string saveState = JsonConvert.SerializeObject(itemList, Formatting.Indented);
            File.WriteAllText(@"D:\\WorkingList.txt", saveState);
            userWishesToContinue = false;
            break;
        default:
            break;
    }
}