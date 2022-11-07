using LineItemLibrary;
using Newtonsoft.Json;
using Spectre.Console;

Console.WriteLine("Welcome to OrderUp");
Console.WriteLine("");

var itemList = new List<LineItem>();
var userWishesToContinue = true;
const string PathToJson = @"D:\\WorkingList.txt";

if (File.Exists(PathToJson))
{
    string loadingString = File.ReadAllText(PathToJson);
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

            Console.Clear();
            int quantity = AnsiConsole.Ask<int>("Enter Quantity: ");
            Console.Clear();

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
            
            string? customerName = AnsiConsole.Ask<string>("Enter Customer Name: ");

            Console.Clear();

            var priority = AnsiConsole.Prompt(
                new SelectionPrompt<int>()
                    .Title("Select a Priority Factor")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                    .AddChoices(new[] {
                        1, 2, 3,
                    }));

            itemList!.Add(LineItem.createLineItem(quantity, metalColorSelected, customerName, priority));
            break;

        case "View Current List":
            Console.WriteLine("Current List");
            foreach (var lineItem in itemList!)
            {
                Console.WriteLine($"{lineItem.quantity}  {lineItem.metalColor}  {lineItem.customerName}  {lineItem.priorityFactor}  {lineItem.isMade}");
            }
            break;

        case "Update an Item":
            Console.Clear();
            var lineItemToUpdate = AnsiConsole.Prompt(
                        new SelectionPrompt<LineItem>()
                            .Title("Select an Item to Update")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more items)[/]")
                            .AddChoices<LineItem>(itemList!)
                            .UseConverter<LineItem>(DisplaySelector));

            int lineNumber = itemList!.IndexOf(lineItemToUpdate);

         
            var selection = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Update:")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more)[/]")
                            .AddChoices(new[] {
                                "Quantity", "Metal Color", "Customer Name",
                                "Priority", }));

                switch (selection)
            {
                case "Quantity":
                    Console.WriteLine("Enter New Quantity");
                    int newQuantity = int.Parse(Console.ReadLine()!);
                    itemList![lineNumber] = LineItem.updateLineItem(lineItemToUpdate, newQuantity);
                    break;
                    
                case "Metal Color":
                    var newMetalColor = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select a Color")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more colors)[/]")
                            .AddChoices(new[] {
                             "#2 White", "Barn Red", "Black", "Bright Red", "Brown", "Buckskin Tan", "Burnished Slate",
                             "Charcoal", "Clay", "Copper Penny", "Dark Red", "Gallery Blue", "Galvalume", "Gray",
                             "Green", "Hawaiian Blue", "Light Stone", "Sapphire Blue", "Tan", "Plum", "White",

                            }));
                    itemList![lineNumber] = LineItem.updateLineItem(lineItemToUpdate, newMetalColor);
                    break;

                case "Customer Name":
                    Console.WriteLine("Enter New Customer Name");
                    string? newCustomerName = Console.ReadLine();
                    itemList![lineNumber] = LineItem.updateLineItem(lineItemToUpdate, newCustomerName, true);
                    break;

                case "Priority":
                    var newPriorityFactor = AnsiConsole.Prompt(
                        new SelectionPrompt<int>()
                            .Title("Select a Priority Factor")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                            .AddChoices(new[] {
                             1, 2, 3,
                            }));
                    itemList![lineNumber] = LineItem.updateLineItem(lineItemToUpdate, newPriorityFactor, true);
                    break;

                default:
                    break;
            }
            break;

        case "Delete an Item":
           
            Console.Clear();
            var selectedLineItem = AnsiConsole.Prompt(
                        new SelectionPrompt<LineItem>()
                            .Title("Select an Item to Delete")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Move up and down to reveal more items)[/]")
                            .AddChoices<LineItem>(itemList!)
                            .UseConverter<LineItem>(DisplaySelector));
            
            itemList!.Remove(selectedLineItem);
            break;

        case "Save and Exit":
            Console.WriteLine("Goodbye!");
            string saveState = JsonConvert.SerializeObject(itemList, Formatting.Indented);
            File.WriteAllText(PathToJson, saveState);
            userWishesToContinue = false;
            break;

        default:
            break;
    }
}



string DisplaySelector (LineItem lineitem)
{
    string result;

    result = $"{lineitem.quantity}    {lineitem.metalColor}    {lineitem.customerName}"; 

    return result;
}