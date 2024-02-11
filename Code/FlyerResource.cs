using Godot;
using Godot.Collections;

public partial class FlyerResource : Resource {

    [Export]
    public string Name { get; set; }
    [Export]
    public Array<Texture2D> Pages { get; set; }

    // Make sure you provide a parameterless constructor.
    // In C#, a parameterless constructor is different from a
    // constructor with all default values.
    // Without a parameterless constructor, Godot will have problems
    // creating and editing your resource via the inspector.
    public FlyerResource() : this(null, null) {}

    public FlyerResource(string name, Array<Texture2D> pages)
    {
        Name = name;
        Pages = pages;
    }
}