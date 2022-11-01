using System.Runtime.InteropServices;

namespace LineItemLibrary;

public class LineItem
{
    public int quantity { get; set; }
    public string? metalColor { get; set; }
    public string? customerName { get; set; }
    public int priorityFactor { get; set; }
    public bool isMade { get; set; }

    public LineItem(int quantity, string? metalColor, string? customerName, int priorityFactor, bool isMade = false)
    {
        this.quantity = quantity;
        this.metalColor = metalColor;
        this.customerName = customerName;
        this.priorityFactor = priorityFactor;
        this.isMade = isMade;
    }

    public static LineItem createLineItem(int quantity, string? metalColor, string? customerName, int priorityFactor)
    {
        return new LineItem(quantity, metalColor, customerName, priorityFactor);
    }

    public LineItem updateLineItem(LineItem itemToUpdate, int newQuantity)
    { 
       itemToUpdate.quantity = newQuantity;
        return itemToUpdate;
    }

    public LineItem updateLineItem(LineItem itemToUpdate, string? newMetalColor)
    {
        itemToUpdate.metalColor = newMetalColor;
        return itemToUpdate;
    }

    public LineItem updateLineItem(LineItem itemToUpdate, string? newCustomerName, bool overLoad)
    {
        itemToUpdate.customerName = newCustomerName;
        return itemToUpdate;
    }

    public LineItem updateLineItem(LineItem itemToUpdate, int newPriorityFactor, bool overLoad)
    {
        itemToUpdate.priorityFactor = newPriorityFactor;
        return itemToUpdate;
    }

    public LineItem updateLineItem(LineItem itemToUpdate, bool newIsMade)
    {
        itemToUpdate.isMade = newIsMade;
        return itemToUpdate;
    }
}