using Godot;
using System;

public partial class TheStation : TileMapLayer
{
	public const int TILE_SIZE = 16;
	public const int HALF_TILE_SIZE = 8;

	public static Vector2 GetClosestGlobalTilePosition(Vector2 currentPosition){
		return new Vector2(GetTileTruncatedPositionValue(currentPosition.X), GetTileTruncatedPositionValue(currentPosition.Y));
	}
	private static float GetTileTruncatedPositionValue(float preciseValue)
	{
		// Divide our location by the size of each tile
		// If we're somewhere in the 5th tile we might get something like 5.678
		float nearestTileIndex = preciseValue / TILE_SIZE;
		// Round to the nearest "nth" tile. 0 = first tile, 1 = second tile, 2 = third tile, and so on
		// 5.678 would round to an even 6
		nearestTileIndex = Mathf.RoundToInt(nearestTileIndex);
		// Now we multiply "n" by the tile size to get the actual position value and return the value.
		// 6 * 16 would get us 96
		return nearestTileIndex * TILE_SIZE;
	}
}
