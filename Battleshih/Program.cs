using System.ComponentModel;
using System.Numerics;
using Battleshih;
using Raylib_cs;

Raylib.InitWindow(800, 800, "Shooty shooty bang bang"); //Skapa window
Raylib.SetTargetFPS(60);
Raylib.SetExitKey(KeyboardKey.Escape); //Stäng spelet
float speed = 4; //Movement speed

Texture2D Ship = Raylib.LoadTexture(@"Ship.png"); //Skapa skepp
Rectangle SHitBox = new(400, 700, Ship.Dimensions);

Texture2D BulletTexture = Raylib.LoadTexture(@"Bullet.png");
List<Bullet> Bullets = [];
List<Bullet> toRemove = [];


while (!Raylib.WindowShouldClose())
{
    if (Raylib.IsKeyDown(KeyboardKey.Right)) SHitBox.X += speed;  //Keyboard controls höger
    if (Raylib.IsKeyDown(KeyboardKey.Left)) SHitBox.X -= speed;  //Keyboard controls vänster


    if (SHitBox.X > 750) //Out of bounds stopper höger
    {
        SHitBox.X -= speed;
    }
    if (SHitBox.X < 0) //Out of bounds stopper vänster
    {
        SHitBox.X += speed;
    }


    if (Raylib.IsKeyPressed(KeyboardKey.Space))
    {
        Bullet b = new()
        {
            Velocity = new Vector2(0, -10),
            Position = new Vector2(SHitBox.X, SHitBox.Y),
        };
        Bullets.Add(b);
    }

    foreach (Bullet bullet in Bullets)
    {
        bullet.Position += bullet.Velocity;    
    }

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);
    Raylib.DrawTexture(Ship, (int)SHitBox.X, (int)SHitBox.Y, Color.White);
    Raylib.EndDrawing();


    foreach (Bullet bullet in Bullets)
    {
        if (bullet.Position.Y < 0)
        {
            toRemove.Add(bullet);
        }
    }

    foreach (Bullet bullet in toRemove)
    {
        Bullets.Remove(bullet);
    }
}



class Bullet
{
public Vector2 Velocity;
public Vector2 Position;
}