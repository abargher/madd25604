using Godot;
using System;
using System.Collections.Generic;

public partial class Trunk : Node2D
{
	
	[Export]
	public int maxLayers = 4;


	// append a new list of branches on each layer
	private List<List<Node2D>> branchLayers = new List<List<Node2D>>();



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void CreateLayer()
	{
		List<Node2D> newLayer = new List<Node2D>();


	}
}
