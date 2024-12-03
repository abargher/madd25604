using Godot;

public partial class Main : Node
{
	[Export]
	public PackedScene TreeScene { get; set; }

	public RandomNumberGenerator gen = new RandomNumberGenerator();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("new_tree")) {
			// Create a new tree in the main scene.
			PlantTree tree = TreeScene.Instantiate<PlantTree>();

			tree.Position = new Vector2(gen.RandiRange(10, 200), 1020);
			
			AddChild(tree);

		}
	}

	private void OnTreeTimerTimeout()
	{
		var allTrees = GetTree().GetNodesInGroup("trees");
		for (int i = 0; i < allTrees.Count; i++) {
			PlantTree tree = (PlantTree)allTrees[i];
			tree.decay = true;
			if (tree.RemainingBranches == PlantTree.maxDepth) {
				GetNode<Timer>("TreeRegrowTimer").Start();
			}
		}
	}

	private void GrowNewTrees()
	{
		GetNode<Timer>("TreeRegrowTimer").Stop();
		for (int i = 0; i < 3; i++) {
			PlantTree tree = TreeScene.Instantiate<PlantTree>();
			tree.Position = new Vector2(gen.RandiRange(300, 1620), 1020);
			AddChild(tree);
		}
	}

}
