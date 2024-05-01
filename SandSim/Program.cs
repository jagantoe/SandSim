using System.Numerics;
using Raylib_cs;

var width = 1200;
var height = 800;
Raylib.InitWindow(width, height, "Hello World");

HashSet<Vector2> fallingSand = new();
Dictionary<Vector2, bool> sand = Enumerable.Range(-1, width + 2).ToDictionary(x => new Vector2(x, height), x => true);
while (!Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.White);

    if (Raylib.IsMouseButtonDown(MouseButton.Left))
    {
        var x = Raylib.GetMousePosition();
        if (x.X > 0 && x.X <= width)
        {
            fallingSand.Add(x);
        }
    }

    DrawSand();
    if (fallingSand.Count > 0)
    {
        HashSet<Vector2> newFallingSand = new();
        foreach (var f in fallingSand)
        {
            var target = f + Vector2.UnitY;
            if (sand.ContainsKey(target))
            {
                if (!sand.ContainsKey(f))
                    sand.Add(f, true);
            }
            else
            {
                Raylib.DrawPixel((int)target.X, (int)target.Y, Color.Red);
                newFallingSand.Add(target);
            }
        }
        fallingSand = newFallingSand;
    }

    Raylib.DrawFPS(0, 0);
    Raylib.EndDrawing();
}

Raylib.CloseWindow();

void DrawSand()
{
    foreach (var f in sand)
    {
        Raylib.DrawPixel((int)f.Key.X, (int)f.Key.Y, Color.Red);
    }
}
