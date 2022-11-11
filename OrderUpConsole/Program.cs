using LineItemLibrary;
using Newtonsoft.Json;
using Spectre.Console;

Console.WriteLine("Welcome to OrderUp");
Console.WriteLine("");

var itemList = new List<LineItem>();
var userWishesToContinue = true;
var pathToJson = Directory.GetCurrentDirectory() + "\\workinglist.txt";

if (File.Exists(pathToJson))
{
    string loadingString = File.ReadAllText(pathToJson);
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
            
            string customerName = AnsiConsole.Ask<string>("Enter Customer Name: ");

            Console.Clear();

            var priority = AnsiConsole.Prompt(
                new SelectionPrompt<int>()
                    .Title("Select a Priority Factor")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
                    .AddChoices(new[] {
                        1, 2, 3,
                    }));

            itemList!.Add(LineItem.CreateLineItem(quantity, metalColorSelected, customerName, priority));
            break;

        case "View Current List":
            Console.WriteLine("Current List");
            foreach (var lineItem in itemList!)
            {
                Console.WriteLine($"{lineItem.Quantity}  {lineItem.MetalColor}  {lineItem.CustomerName}  {lineItem.PriorityFactor}  {lineItem.IsMade}");
            }
            Console.WriteLine();
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

            int indexOfLineItemToUpdate = itemList!.IndexOf(lineItemToUpdate);

         
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
                    itemList[indexOfLineItemToUpdate] = LineItem.UpdateLineItem(lineItemToUpdate, newQuantity);
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
                    itemList[indexOfLineItemToUpdate] = LineItem.UpdateLineItem(lineItemToUpdate, newMetalColor);
                    break;

                case "Customer Name":
                    Console.WriteLine("Enter New Customer Name");
                    string? newCustomerName = Console.ReadLine();
                    itemList[indexOfLineItemToUpdate] = LineItem.UpdateLineItem(lineItemToUpdate, newCustomerName, true);
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
                    itemList[indexOfLineItemToUpdate] = LineItem.UpdateLineItem(lineItemToUpdate, newPriorityFactor, true);
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
            File.WriteAllText(pathToJson, saveState);
            userWishesToContinue = false;
            break;
    }
}



static string DisplaySelector (LineItem lineitem)
{
    var result = $"{lineitem.Quantity}    {lineitem.MetalColor}    {lineitem.CustomerName}"; 

    return result;
}