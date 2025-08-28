namespace Constants {
    // A static class is one that can't be instantiated, meaning it's more of a collection of methods and properties that we can use for utility purposes.
    // In this case we want to have a nice spot where we can get at a certain string value that is always the same.
    // Why is that helpful? Why not just type the string every time if it never changes?
    // Well... maybe someday we DO want to change it. Maybe "Up" should really be "Above". who knows.
    // If that's ever the case it is MUCH easier to change the value of this one constant variable than finding all the locations that have "Up" and replacing them all.
    // In addition this allows us to organize the possible inputs in our game in one place.
    // Anyway. Static classes. ... Well first regular classes.
    // To instantiate an object from a class means to take the class type and call the constructor method. 
    // Taking the class, which is essenetially a blueprint, and creating a new instance of that class (an object).
    // If we had...
    // public class Blueprint
    // And that Blueprint class had stuff in it like... address, material, number of windows. Any properties relevant to what the thing can be. In this case an actual building.
    // To instantiate the Blueprint class into a building object we might do something like...
    // Blueprint myBuilding = new Blueprint();
    // myBuilding would be an instance of the Blueprint class. It would -own- its own address, material, number of windows.
    // There can be other buildings that might have their own, different, values for address, material, number of windows.
    // So then finally coming back to static classes. The thing we have here.
    // Static classes exist ONLY as a class. They cannot be instantiated into an object.
    // So that makes them ideal for holding values that never change. We can access constant values from a static class because they're always the same.
    // We would do so like...
    // string iWantTheValueForUp = Inputs.UP;
    // iWantTheValueForUp would now contain "Up" ... or "Above" if we happened to eventually change it.
    public static class InputValue {
        public const string UP = "Up";
        public const string DOWN = "Down";
        public const string LEFT = "Left";
        public const string RIGHT = "Right";
        public const string INTERACT = "Interact";
    }
}